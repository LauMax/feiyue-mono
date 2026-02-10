# 计划：Grok AI 集成 + 聊天进程追踪

## 目标

在 C# 后端实现完整的 AI 故事系统，包括：

1. Grok API 调用基础设施（参考 Picasso 的 HttpClient + Liquid 模板模式）
2. 故事生成服务（从 Python 后端移植 prompt 和逻辑）
3. ChatRoom 进程追踪（精确轮次计数 + 剧情阶段）
4. 匹配时自动生成故事 + 开场叙述
5. 聊天时自动触发剧情线索

---

## Step 1：ChatRoom 模型扩展 + InternalContracts 新增模型

> 目标：扩展 ChatRoom 支持进程追踪，新增 AI 上下文模型

### 1a. 扩展 ChatRoom（ChatModels.cs）

```csharp
public sealed record ChatRoom(
    // --- 已有字段 ---
    string Id,
    string User1Id,
    string User2Id,
    Story? Story,
    string Status,               // "active" | "closed"
    DateTimeOffset CreatedAt,
    DateTimeOffset? ClosedAt,
    bool IsVirtual = false,
    // --- 新增：进程追踪 ---
    int TotalMessages = 0,       // 总消息数
    int ConversationRounds = 0,  // 真实轮次（双方交替才+1）
    string? LastSenderId = null, // 上一条消息的发送者（用于判断轮次）
    DateTimeOffset? LastActivityAt = null, // 最后活跃时间
    // --- 新增：线索触发 ---
    int LastClueAtRound = 0,     // 上次触发线索时的轮次
    int NextClueInterval = 5,    // 下次触发间隔（5→10→20→40）
    int ClueCount = 0,           // 已触发线索总数
    int SilenceTriggerCount = 0, // 沉默触发次数（max 3）
    // --- 新增：剧情阶段（为未来铺路）---
    string StoryPhase = "opening"); // "opening" | "rising" | "climax" | "resolution"
```

### 1b. 扩展 ChatMessage（ChatModels.cs）

```csharp
public sealed record ChatMessage(
    string Id,
    string RoomId,
    string SenderId,
    string Content,
    string MessageType,          // "text" | "system"
    DateTimeOffset SentAt,
    // --- 新增 ---
    string? TriggerType = null,  // "seed" | "rounds" | "silence" | null
    int SequenceNumber = 0);     // 在该房间内的消息序号
```

### 1c. 新增 AI 上下文模型（StoryModels.cs）

```csharp
// 故事生成上下文
public sealed record StoryGenerationContext(
    string? AgeGroup,
    string? GenderPreference,
    IReadOnlyList<string> Tags,
    string? Description,
    Role? MaleRole,
    Role? FemaleRole);

// 开场叙述上下文
public sealed record StorySeedContext(
    string Background,
    IReadOnlyList<string> Tags,
    Role? MaleRole,
    Role? FemaleRole);

// 剧情线索上下文
public sealed record StoryClueContext(
    string Background,
    IReadOnlyList<string> Tags,
    IReadOnlyList<ConversationMessage> RecentMessages,
    string TriggerType);  // "conversation" | "silence"

// 对话建议上下文
public sealed record DialogueContext(
    string CharacterRole,
    string StoryBackground,
    IReadOnlyList<string> Tags,
    IReadOnlyList<ConversationMessage> RecentMessages);

// 对话消息（用于构建 prompt 上下文）
public sealed record ConversationMessage(string SenderRole, string Content);
```

### 1d. 修改文件清单

| 文件 | 操作 |
| ---- | ---- |
| `Service.InternalContracts/ChatModels.cs` | 修改 ChatRoom + ChatMessage |
| `Service.InternalContracts/StoryModels.cs` | 新增上下文模型 |
| `Service.ChatStorage/ChatStorage.cs` | 适配新字段的 BsonClassMap |
| `Service.ChatStorage/IChatStorage.cs` | 新增 `UpdateRoomProgressAsync` 方法 |

---

## Step 2：新建 Service.Story 项目 + Grok 基础设施

> 目标：建立 Grok API 调用能力

### 2a. 项目结构

```
src/Service.Story/
├── Service.Story.csproj          ← 引用 Fluid.Core, Service.InternalContracts
├── AppStartup.cs                 ← DI 注册
├── GrokOptions.cs                ← 配置类
├── IGrokClient.cs                ← 接口
├── GrokClient.cs                 ← IHttpClientFactory 封装
├── ILiquidTemplateService.cs     ← 模板渲染接口
├── LiquidTemplateService.cs      ← Fluid 渲染实现
└── LiquidTemplates/story/
    ├── generate_complete.liquid
    ├── seed.liquid
    ├── clue.liquid
    └── dialogue.liquid
```

### 2b. GrokOptions

```csharp
public sealed class GrokOptions
{
    public string BaseUrl { get; set; } = "https://api.x.ai/v1";
    public string ApiKey { get; set; } = "";
    public string ModelName { get; set; } = "grok-3";
    public double Temperature { get; set; } = 0.7;
    public int MaxTokens { get; set; } = 2000;
    public int TimeoutSeconds { get; set; } = 60;
}
```

### 2c. IGrokClient → GrokClient

参考 Picasso `HarmonyInference` 模式：

- `IHttpClientFactory.CreateClient("GrokClient")` — 命名客户端
- `POST {BaseUrl}/chat/completions` — OpenAI 兼容格式
- 请求：`{ model, messages: [{role,content}], temperature, max_tokens }`
- 响应：提取 `choices[0].message.content`
- 错误分类：超时 / 429 限流 / 401 认证 / 5xx

```csharp
public interface IGrokClient
{
    Task<string> ChatCompletionAsync(
        string systemPrompt,
        string userPrompt,
        double? temperature,
        int? maxTokens,
        CancellationToken cancellationToken);
}
```

### 2d. Liquid 模板服务

参考 Picasso `ILiquidFormatter` 模式：

- 从文件系统加载 `.liquid` 文件
- Fluid 引擎解析 + 渲染
- 缓存已解析的模板

```csharp
public interface ILiquidTemplateService
{
    Task<string> RenderAsync(string templateName, object context, CancellationToken cancellationToken);
}
```

### 2e. 4 个 Liquid 模板

从 Python `grok_service.py` 移植，核心 prompt：

| 模板文件 | 对应 Python 常量 | System Prompt | Max Tokens |
| -------- | ---------------- | ------------- | ---------- |
| `generate_complete.liquid` | `STORY_GENERATION_TEMPLATE` | 生成 JSON 故事 | 800 |
| `seed.liquid` | `STORY_SEED_TEMPLATE` | 开场叙述 | 500 |
| `clue.liquid` | `STORY_CLUE_TEMPLATE` | 剧情线索 ≤50 字 | 300 |
| `dialogue.liquid` | `DIALOGUE_SUGGESTION_TEMPLATE` | 对话建议 | 200 |

### 2f. 修改文件清单

| 文件 | 操作 |
| ---- | ---- |
| `Service.Story/Service.Story.csproj` | 新建 |
| `Service.Story/GrokOptions.cs` | 新建 |
| `Service.Story/IGrokClient.cs` | 新建 |
| `Service.Story/GrokClient.cs` | 新建 |
| `Service.Story/ILiquidTemplateService.cs` | 新建 |
| `Service.Story/LiquidTemplateService.cs` | 新建 |
| `Service.Story/AppStartup.cs` | 新建 |
| 4 个 `.liquid` 文件 | 新建 |
| `Service.Api/Service.Api.csproj` | 添加 Service.Story 引用 |
| `Service.Api/appsettings.json` | 添加 Grok 配置节 |

---

## Step 3：故事生成服务 + Fallback

> 目标：IStoryGenerationService — 4 种 AI 方法 + 降级模板

### 3a. IStoryGenerationService

```csharp
public interface IStoryGenerationService
{
    // AI 生成完整故事 → JSON → Story 对象（失败时 fallback）
    Task<Story> GenerateCompleteStoryAsync(StoryGenerationContext context, CancellationToken ct);

    // AI 生成开场叙述（2-3句话，失败时 fallback）
    Task<string> GenerateStorySeedAsync(StorySeedContext context, CancellationToken ct);

    // AI 生成剧情线索（≤50字，第三人称，失败时 fallback）
    Task<string> GenerateStoryClueAsync(StoryClueContext context, CancellationToken ct);

    // AI 对话建议（1句话，失败时 fallback）
    Task<string> SuggestDialogueAsync(DialogueContext context, CancellationToken ct);
}
```

### 3b. StoryGenerationService

核心逻辑：

1. 用 `ILiquidTemplateService` 渲染 prompt
2. 调用 `IGrokClient.ChatCompletionAsync()`
3. JSON 解析（处理 markdown code block 包裹）
4. 失败时走 fallback

### 3c. StoryFallbackService

移植 Python 的降级模板：

- 6 类故事模板（dominant / romantic / mysterious / playful / gentle / default）
- 12 条对话线索 + 5 条沉默线索
- 按标签匹配 + MD5 确定性选择

### 3d. 修改文件清单

| 文件 | 操作 |
| ---- | ---- |
| `Service.Story/IStoryGenerationService.cs` | 新建 |
| `Service.Story/StoryGenerationService.cs` | 新建 |
| `Service.Story/StoryFallbackService.cs` | 新建 |

---

## Step 4：ChatStorage 扩展 + 进程追踪

> 目标：MongoDB 层支持进程更新

### 4a. IChatStorage 新增方法

```csharp
// 原子更新房间进程（单次 MongoDB FindOneAndUpdate）
Task<ChatRoom?> UpdateRoomProgressAsync(
    string roomId,
    string senderId,
    CancellationToken cancellationToken);

// 更新线索触发状态
Task UpdateClueStateAsync(
    string roomId,
    int clueAtRound,
    int nextInterval,
    int clueCount,
    CancellationToken cancellationToken);

// 创建房间时带 Story（修改已有方法签名）
Task<ChatRoom> CreateRoomAsync(
    string user1Id,
    string user2Id,
    Story? story,           // ← 新增参数
    bool isVirtual = false,
    CancellationToken cancellationToken = default);

// 发送消息时带额外字段
Task<ChatMessage> SendMessageAsync(
    string roomId,
    string senderId,
    string content,
    string messageType = "text",
    string? triggerType = null,
    CancellationToken cancellationToken = default);
```

### 4b. UpdateRoomProgressAsync 实现

关键：**精确轮次计数** — 用 MongoDB FindOneAndUpdate 原子操作

```csharp
async Task<ChatRoom?> UpdateRoomProgressAsync(string roomId, string senderId, CancellationToken ct)
{
    var room = await GetRoomAsync(roomId, ct);
    if (room is null) return null;

    // 判断是否构成新轮次：发送者与上一条不同 = 交替发言
    bool isNewRound = room.LastSenderId is not null && room.LastSenderId != senderId;

    var update = Builders<ChatRoom>.Update
        .Inc(r => r.TotalMessages, 1)
        .Set(r => r.LastSenderId, senderId)
        .Set(r => r.LastActivityAt, DateTimeOffset.UtcNow);

    if (isNewRound)
        update = update.Inc(r => r.ConversationRounds, 1);

    var options = new FindOneAndUpdateOptions<ChatRoom> { ReturnDocument = ReturnDocument.After };
    return await _chatRooms.FindOneAndUpdateAsync(r => r.Id == roomId, update, options, ct);
}
```

### 4c. 修改文件清单

| 文件 | 操作 |
| ---- | ---- |
| `Service.ChatStorage/IChatStorage.cs` | 新增方法签名 |
| `Service.ChatStorage/ChatStorage.cs` | 实现 + 更新 BsonClassMap |

---

## Step 5：聊天进程引擎 — 消息后自动触发线索

> 目标：HumanChatSession 发消息后检查是否该触发剧情线索

### 5a. IChatProgressService（新接口）

```csharp
public interface IChatProgressService
{
    /// <summary>
    /// 处理消息发送后的进程更新 + 线索触发检查。
    /// 返回生成的线索消息（如果有），调用方负责广播。
    /// </summary>
    Task<ChatServerEvent?> ProcessMessageAsync(
        string roomId,
        string senderId,
        CancellationToken cancellationToken);
}
```

### 5b. ChatProgressService 实现

```
ProcessMessageAsync(roomId, senderId):
  1. 调用 storage.UpdateRoomProgressAsync(roomId, senderId)
  2. 获取更新后的 room（返回值包含最新轮次）
  3. 检查轮次触发条件：
     roundsSinceLastClue = room.ConversationRounds - room.LastClueAtRound
     shouldTrigger = roundsSinceLastClue >= room.NextClueInterval
  4. 如果触发：
     a. 从 storage 取最近 10 条消息，过滤 system，取最后 6 条
     b. 构建 StoryClueContext
     c. 调用 storyService.GenerateStoryClueAsync()
     d. 保存线索为 system 消息（triggerType = "rounds"）
     e. 更新 room 的线索状态（lastClueAtRound, nextInterval*2, clueCount+1）
     f. 返回 ChatServerEvent("system_clue", { content, triggerType })
  5. 不触发 → 返回 null
```

### 5c. HumanChatSession 集成

```csharp
// HandleMessageAsync 末尾增加（fire-and-forget）：
_ = CheckAndTriggerClueAsync();

async Task CheckAndTriggerClueAsync()
{
    var clueEvent = await _progressService.ProcessMessageAsync(_roomId, _userId, default);
    if (clueEvent is not null)
    {
        // 线索广播给房间所有人（包括双方）
        await _connectionManager.BroadcastToRoomAsync(_roomId, clueEvent, default);
    }
}
```

### 5d. 修改文件清单

| 文件 | 操作 |
| ---- | ---- |
| `Service.Chat/IChatProgressService.cs` | 新建 |
| `Service.Chat/ChatProgressService.cs` | 新建 |
| `Service.Chat/AppStartup.cs` | 注册新服务 |
| `Service.Api/WebSockets/HumanChatSession.cs` | 注入 + 调用 |
| `Service.Api/WebSockets/ChatSessionFactory.cs` | 传递依赖 |

---

## Step 6：匹配时自动生成故事 + 开场

> 目标：匹配成功后自动生成完整故事 + 开场叙述

### 6a. MatchService 修改

```
匹配成功流程（已有）:
  1. 从队列取出两个用户
  2. 创建聊天室

新增流程:
  3. 确定故事：
     - 用户A有Story → 用A的
     - 用户B有Story → 用B的
     - 都没有 → AI 生成（合并双方 tags）→ fallback
  4. 创建聊天室时传入 Story
  5. 生成开场叙述（seed）
  6. 保存 seed 为 system 消息（triggerType = "seed"）
```

### 6b. CreateRoomAsync 修改

当前问题：`ChatStorage.CreateRoomAsync` **忽略了 Story 参数**，始终设为 `null`。
修复：传入 story 并保存。

### 6c. 修改文件清单

| 文件 | 操作 |
| ---- | ---- |
| `Service.Match/MatchService.cs` | 注入 IStoryGenerationService，匹配成功时调用 |
| `Service.Match/Service.Match.csproj` | 添加 Service.Story 引用 |
| `Service.ChatStorage/ChatStorage.cs` | CreateRoomAsync 传入 story |
| `Service.Api/Program.cs` | 注册 Service.Story 的 DI |

---

## Step 7：appsettings 配置 + 端到端验证

> 目标：配置 Grok API 并验证完整流程

### 7a. appsettings.json

```json
{
  "Grok": {
    "BaseUrl": "https://api.x.ai/v1",
    "ApiKey": "",
    "ModelName": "grok-3",
    "Temperature": 0.7,
    "MaxTokens": 2000,
    "TimeoutSeconds": 60
  }
}
```

支持环境变量覆盖：`Grok__ApiKey=xai-xxx`

### 7b. 验证场景

| # | 场景 | 验证方法 |
| --- | ---- | ------- |
| 1 | Grok API 连通性 | 配置 API Key → 调用生成故事 |
| 2 | 无 API Key 降级 | 不配 Key → 匹配后自动用 fallback 故事 |
| 3 | 匹配生成故事 | 两用户匹配 → 检查 chat_rooms 的 story 字段 |
| 4 | 开场叙述 | 匹配后 → 检查 chat_messages 第一条 system 消息 |
| 5 | 轮次追踪 | 双方交替发消息 → 检查 room.conversationRounds |
| 6 | 连发不虚增 | 同一人连发 3 条 → rounds 不变 |
| 7 | 线索触发 | 发 5 轮消息 → 检查自动生成的 system 线索消息 |
| 8 | 线索间隔递增 | 5→10→20→40 验证 |

---

## 实施顺序

```
Step 1 — 模型扩展（InternalContracts + ChatStorage）      ← 基础，无外部依赖
Step 2 — Service.Story 项目 + Grok 基础设施               ← 新项目，可独立开发
Step 3 — 故事生成服务 + Fallback                          ← 依赖 Step 2
Step 4 — ChatStorage 进程追踪方法                          ← 依赖 Step 1
Step 5 — 聊天进程引擎（自动触发线索）                       ← 依赖 Step 3 + Step 4
Step 6 — 匹配时自动生成故事                                ← 依赖 Step 3
Step 7 — 配置 + 端到端验证                                 ← 依赖全部
```

Step 1 和 Step 2 可以并行。Step 3 和 Step 4 也可以并行。

---

## 新增文件总览

| 文件 | 类型 |
| ---- | ---- |
| `Service.Story/Service.Story.csproj` | 新建 |
| `Service.Story/AppStartup.cs` | 新建 |
| `Service.Story/GrokOptions.cs` | 新建 |
| `Service.Story/IGrokClient.cs` | 新建 |
| `Service.Story/GrokClient.cs` | 新建 |
| `Service.Story/ILiquidTemplateService.cs` | 新建 |
| `Service.Story/LiquidTemplateService.cs` | 新建 |
| `Service.Story/IStoryGenerationService.cs` | 新建 |
| `Service.Story/StoryGenerationService.cs` | 新建 |
| `Service.Story/StoryFallbackService.cs` | 新建 |
| `Service.Story/LiquidTemplates/story/*.liquid` (×4) | 新建 |
| `Service.Chat/IChatProgressService.cs` | 新建 |
| `Service.Chat/ChatProgressService.cs` | 新建 |

## 修改文件总览

| 文件 | 修改内容 |
| ---- | ------- |
| `Service.InternalContracts/ChatModels.cs` | ChatRoom + ChatMessage 扩展字段 |
| `Service.InternalContracts/StoryModels.cs` | 新增 AI 上下文模型 |
| `Service.ChatStorage/IChatStorage.cs` | 新增方法签名 |
| `Service.ChatStorage/ChatStorage.cs` | 实现新方法 + BsonClassMap 更新 |
| `Service.Chat/AppStartup.cs` | 注册 ChatProgressService |
| `Service.Match/MatchService.cs` | 注入 IStoryGenerationService |
| `Service.Match/Service.Match.csproj` | 添加 Service.Story 引用 |
| `Service.Api/Service.Api.csproj` | 添加 Service.Story 引用 |
| `Service.Api/Program.cs` | 注册 Service.Story DI + Grok 配置 |
| `Service.Api/appsettings.json` | 添加 Grok 配置节 |
| `Service.Api/WebSockets/HumanChatSession.cs` | 集成进程追踪 |
| `Service.Api/WebSockets/ChatSessionFactory.cs` | 传递新依赖 |

## 轮次计数精确定义

```
真正的轮次 = 双方交替发言的完整回合

User A: "你好"          → messages=1, rounds=0 (A说了，等B)
User B: "嗨~"           → messages=2, rounds=1 ✅ (完成一轮)
User A: "今天天气不错"   → messages=3, rounds=1 (A又说了，等B)
User A: "你喜欢什么"     → messages=4, rounds=1 (A连发，不算新轮次)
User B: "我喜欢音乐"     → messages=5, rounds=2 ✅ (B回应了)

实现方式：
  room.LastSenderId 记录上一条消息发送者
  新消息 senderId != lastSenderId → rounds + 1
  连发 senderId == lastSenderId → rounds 不变
```
