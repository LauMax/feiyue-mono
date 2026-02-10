# WebSocket 聊天架构文档

## 概述

飞跃匿名聊天系统的后端采用 **Channel 驱动的 WebSocket 架构**，借鉴了微软 Picasso（Copilot 后端）的核心设计模式。

核心思想：**把 WebSocket 的读和写彻底分离**，用 `System.Threading.Channels.Channel<T>` 作为中间缓冲，避免并发写入问题。

---

## 架构图

### 整体数据流

```
┌──────────────┐          WebSocket           ┌──────────────────────────────────┐
│   用户A浏览器  │ ◄──────────────────────────► │          服务器                   │
│              │                              │                                  │
│  ┌─────────┐ │   {"type":"message",         │  ┌─────────┐    ┌─────────────┐  │
│  │ 乐观更新 │ │    "content":"你好"}          │  │ReadLoop │───►│ ChatSession  │  │
│  │ 立即显示 │ │ ────────────────────────►    │  │ (解析)   │    │ (业务逻辑)   │  │
│  └─────────┘ │                              │  └─────────┘    └──────┬──────┘  │
│              │   {"type":"message",         │                        │         │
│  ┌─────────┐ │    "data":{...}}             │                        ▼         │
│  │ 收到对方 │ │ ◄────────────────────────    │  ┌─────────┐    ┌───────────┐   │
│  │  消息   │ │                              │  │WriteLoop│◄───│ Channel   │   │
│  └─────────┘ │                              │  │ (序列化) │    │ (缓冲队列) │   │
└──────────────┘                              │  └─────────┘    └───────────┘   │
                                              └──────────────────────────────────┘
```

### 房间广播流程（A 发消息给 B）

```
用户A浏览器                       服务器                              用户B浏览器
    │                              │                                    │
    │  {"type":"message",          │                                    │
    │   "content":"你好"}          │                                    │
    │ ─────WebSocket─────►   ReadLoop(A)                                │
    │                        JSON反序列化(camelCase)                     │
    │                              │                                    │
    │                        HumanChatSession                           │
    │                        .HandleMessageAsync()                      │
    │                              │                                    │
    │                        ┌─────┴──────┐                             │
    │                        │            │                             │
    │                  广播(排除A)   异步写MongoDB                       │
    │                        │      (fire-and-forget)                   │
    │                        ▼                                          │
    │                 RoomConnectionManager                              │
    │                 .BroadcastToRoomAsync()                            │
    │                 excludeUserId = A                                  │
    │                        │                                          │
    │                        │  B的Channel.TryWrite(event)              │
    │                        ▼                                          │
    │                  B的Channel ──► B的WriteLoop                      │
    │                                 JSON序列化(camelCase)              │
    │                                 WebSocket.SendAsync()             │
    │                                   ──────WebSocket─────►           │
    │                                                           显示消息 │
    │                                                                   │
  前端乐观显示                                                           │
  (自己发的立刻显示)                                                      │
```

---

## 为什么需要 Channel？

### 问题：WebSocket 不能并发写入

WebSocket 有一个关键限制：**不支持多线程同时 `SendAsync()`**。如果两个线程同时写入同一个 WebSocket，会导致数据错乱或异常。

在聊天场景下，多个来源都需要写入用户的 WebSocket：

- 对方发了消息 → 需要广播给你
- 系统通知（用户加入/离开） → 需要发给你
- 虚拟人回复 → 需要发给你

### 解决方案：多生产者单消费者（MPSC）

```
                    ┌─────────────────┐
 对方发消息 ────►   │                 │
                    │   Channel<T>    │ ────►  WriteLoop ────► WebSocket ────► 用户
 系统通知 ─────►   │   (线程安全队列)  │        (唯一写入者)
                    │                 │
 虚拟人回复 ───►   └─────────────────┘
```

- **多个生产者**：可以并发调用 `Channel.Writer.TryWrite()`，完全线程安全
- **唯一消费者**：WriteLoop 从 Channel 读取事件，依次序列化并写入 WebSocket

这就是经典的 **MPSC（Multi-Producer Single-Consumer）** 模式。

---

## 核心组件

### 1. ChatWebSocketHandler — 传输层

**职责**：管理 WebSocket 连接的生命周期，建立 Channel，启动读写循环。

```
WebSocket连接建立
    │
    ├── 创建 Channel<ChatServerEvent>
    ├── 注册 ChannelWriter 到 RoomConnectionManager
    ├── 创建 ChatSession（Human 或 Virtual）
    ├── 启动 ReadLoop（读 WebSocket → 解析 → 交给 Session 处理）
    ├── 启动 WriteLoop（读 Channel → 序列化 → 写 WebSocket）
    └── 等待连接断开 → 清理资源
```

**关键设计**：
- 使用 `JsonNamingPolicy.CamelCase` 确保与前端 JavaScript 的命名一致
- ReadLoop 和 WriteLoop 独立运行，互不阻塞

### 2. RoomConnectionManager — 路由层

**职责**：维护 `房间 → 用户 → ChannelWriter` 的映射关系，负责消息路由。

```csharp
// 内部数据结构
ConcurrentDictionary<
    string,                                            // roomId
    ConcurrentDictionary<
        string,                                        // userId
        ChannelWriter<ChatServerEvent>                 // 该用户的输出 Channel
    >
>
```

**核心方法**：

| 方法 | 作用 |
|------|------|
| `AddConnection(roomId, userId, channelWriter)` | 用户加入房间 |
| `RemoveConnection(roomId, userId)` | 用户离开房间 |
| `BroadcastToRoomAsync(roomId, event, excludeUserId)` | 广播给房间所有人（可排除发送者） |
| `SendToUserAsync(roomId, userId, event)` | 发送给指定用户 |

### 3. IChatSession — 业务层接口

**两种实现**：

| 实现 | 场景 | 行为 |
|------|------|------|
| `HumanChatSession` | 真人 1v1 | 收到消息 → 广播给对方（排除自己） → 异步写 DB |
| `VirtualChatSession` | 虚拟人陪聊 | 收到消息 → 延迟一会 → 生成预设回复 → 写入自己的 Channel |

### 4. ChatSessionFactory — 决策层

根据匹配结果决定创建哪种 Session：
- 匹配到真人 → `HumanChatSession`
- 超时匹配虚拟人 → `VirtualChatSession`

---

## "先发后存" 模式

借鉴 Picasso 的核心设计理念：**可用性优先于一致性**。

### 传统做法

```
收到消息 → 写数据库 → 等待成功 → 广播给对方
          ╰── 延迟：50-200ms ──╯
```

用户感知延迟 = 数据库写入时间。

### 飞跃的做法（借鉴 Picasso）

```
收到消息 → 立即广播给对方（毫秒级） → 异步写数据库（fire-and-forget）
                                      ╰── 失败也不影响聊天体验
```

```csharp
// HumanChatSession.HandleMessageAsync()

// Step 1: 立即广播（低延迟）
await _connectionManager.BroadcastToRoomAsync(_roomId, messageEvent, ct, excludeUserId: _userId);

// Step 2: 异步写 DB（不阻塞）
_ = PersistMessageAsync(messageId, content, sentAt);
```

**设计取舍**：消息极少概率丢失（可重发），但聊天永远不会卡顿。

---

## JSON 序列化

C# 默认使用 **PascalCase**（`Type`, `Content`），JavaScript 使用 **camelCase**（`type`, `content`）。

解决方案：在 `ChatWebSocketHandler` 中配置全局 JSON 选项：

```csharp
private static readonly JsonSerializerOptions JsonOptions = new()
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,        // 序列化时输出 camelCase
    PropertyNameCaseInsensitive = true                         // 反序列化时忽略大小写
};
```

---

## 消息去重

前端发送消息时使用**乐观更新**（立即在界面显示），服务器广播时**排除发送者**：

```csharp
// 服务器不会把消息回发给发送者自己
await _connectionManager.BroadcastToRoomAsync(
    _roomId, messageEvent, ct,
    excludeUserId: _userId  // 排除发送者
);
```

这样前端只显示一次消息，不会重复。

---

## 数据库存储

### MongoDB 集合

| 集合名 | 用途 | 示例文档 |
|--------|------|---------|
| `chat_rooms` | 聊天室 | `{ _id, user1Id, user2Id, status, isVirtual, createdAt }` |
| `chat_messages` | 聊天消息 | `{ _id, roomId, senderId, content, messageType, sentAt }` |
| `match_requests` | 匹配请求 | `{ _id, userId, status, matchedUserId, roomId }` |

### 已验证的数据示例

**chat_rooms**（聊天室记录）：
```json
{
  "_id": "17926c58-88b0-48c3-a7ae-29766bfca67d",
  "user1Id": "b9938ebd-a48b-4452-b2ef-732434c4436f",
  "user2Id": "b3ecc7e0-3ebc-43ea-9ab1-0d7f629c0551",
  "status": "active",
  "isVirtual": false,
  "createdAt": "2026-02-06T17:12:19.864131+00:00"
}
```

**chat_messages**（消息记录）：
```json
[
  {
    "_id": "f46b0988-4457-47c1-96b6-ce8b0316d27c",
    "roomId": "17926c58-88b0-48c3-a7ae-29766bfca67d",
    "senderId": "b3ecc7e0-3ebc-43ea-9ab1-0d7f629c0551",
    "content": "hi",
    "sentAt": "2026-02-06T17:12:25.271952+00:00"
  },
  {
    "_id": "64c50dec-5fc7-4a8c-97a3-8a5534ba093b",
    "roomId": "17926c58-88b0-48c3-a7ae-29766bfca67d",
    "senderId": "b9938ebd-a48b-4452-b2ef-732434c4436f",
    "content": "你好",
    "sentAt": "2026-02-06T17:12:29.13176+00:00"
  }
]
```

---

## 匹配系统

### 流程

```
用户提交匹配请求
    │
    ├── 写入 MongoDB（match_requests 集合）
    ├── 加入 Redis 队列（SortedSet / List）
    │
    ├── 尝试即时匹配（TryMatchAsync）
    │   ├── 成功 → 创建 ChatRoom → 返回匹配结果
    │   └── 失败 → 等待下一个人加入
    │
    └── 超时（60秒）→ 匹配虚拟人 → 创建虚拟 ChatRoom
```

### Redis 队列设计

| 队列类型 | 数据结构 | 用途 |
|----------|---------|------|
| VIP 队列 | SortedSet | 按分数排序，优先匹配高质量用户 |
| 标准队列 | List | FIFO 先进先出 |

---

## 调试过程中解决的问题

| # | Bug | 根因 | 修复方案 |
|---|-----|------|---------|
| 1 | 端口 5000 被占 | macOS AirPlay Receiver 占用 | 改为 5050 + `isProxied: false` |
| 2 | 获取消息 404 | 前端路由与后端不匹配 | 统一为 `/api/chat/room/:roomId/messages` |
| 3 | WebSocket 无限重连 | `useCallback` 依赖变化导致重建 | 用 `useRef` 稳定回调引用 |
| 4 | 匹配后一方卡住 | `DequeueAsync` 删除了 MongoDB 记录 | 改用 `RemoveFromQueueAsync` 只删 Redis |
| 5 | 消息发不出去 | 多线程并发写 WebSocket | 改为写入 Channel，WriteLoop 统一投递 |
| 6 | 虚拟人太快匹配 | 20 秒超时太短 | 改为 60 秒 |
| 7 | JSON 大小写不匹配 | C# PascalCase vs JS camelCase | 配置 `JsonNamingPolicy.CamelCase` |
| 8 | 消息显示两次 | 乐观更新 + 服务器回发 | 广播时 `excludeUserId` 排除发送者 |

---

## 技术栈

| 层次 | 技术 |
|------|------|
| 编排 | .NET Aspire 9 |
| 后端 | .NET 10 + ASP.NET Core |
| 实时通信 | 原生 WebSocket + Channel<T> |
| 数据库 | MongoDB (聊天室、消息、匹配) |
| 缓存/队列 | Redis (匹配队列) |
| 前端 | React 18 + Vite 6 + TypeScript |
| 容器 | Docker (MongoDB/Redis 由 Aspire 管理) |
