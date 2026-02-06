# Feiyue Backend Migration Design

## 目标

将 Feiyue Python 后端的完整功能迁移到 Monorepo 的 C# 后端，并采用 Picasso 的架构模式和最佳实践。

## 设计原则

基于 Picasso 的工程指南，我们遵循以下核心原则：

1. **微服务分层架构**：API → Business Logic → Storage
2. **Internal/External Contracts 解耦**：API 层使用 External Contracts，内部使用 Internal Contracts
3. **不可变类型**：所有数据模型使用 `sealed record`
4. **依赖注入**：使用 `IServiceCollection` 扩展方法模式
5. **AppStartup 模式**：每个项目有独立的 `AppStartup.cs`
6. **强类型日志**：使用 `LoggerMessage` 源生成器
7. **异步优先**：所有 I/O 操作使用 `async/await`

---

## 当前 Python 后端功能分析

### 核心功能模块

#### 1. **匹配系统 (Match System)**
**文件**：`app/routes/match.py`, `app/services/match_service.py`, `app/services/queue_service.py`

**功能**：
- ✅ 用户创建（匿名ID生成）
- ✅ 匹配请求创建
- ✅ 队列管理（男女分队）
- ✅ 匹配算法（基于标签、年龄、Dom/Sub、故事等）
- ✅ 匹配超时处理
- ✅ 匹配取消
- ✅ 队列状态查询

**API 端点**：
```
POST /api/match/request
GET /api/match/status/:matchId
POST /api/match/cancel?match_id=xxx
GET /api/match/queue/status
```

**核心算法**：
```python
def calculate_match_score(user1_data, user2_data):
    score = 0
    # Same story: +100
    # Role complement: +50
    # Common tags: +10 per tag
    # Dom-Sub compatibility: +30
    # Age group match: +20
    return score

# 分阶段匹配策略
- 0-30s: 严格匹配（score >= 100）
- 30-60s: 放松匹配（score >= 50）
- 60s+: 接受任何角色互补的匹配
```

#### 2. **聊天系统 (Chat System)**
**文件**：`app/routes/chat.py`, `app/services/chat_service.py`

**功能**：
- ✅ 消息发送
- ✅ 消息历史查询
- ✅ 房间状态更新（消息计数、对话轮数）
- ✅ AI 故事线索触发
  - 轮数触发（5轮 → 10轮 → 20轮 → 40轮，指数增长）
  - 沉默触发（120秒沉默，最多3次）
- ✅ 确定性故事生成（基于 room_id 的 MD5）

**API 端点**：
```
POST /api/chat/send?room_id=xxx&user_id=xxx
GET /api/chat/messages/:roomId
```

**剧情触发机制**：
```python
# 轮数触发
def check_rounds_trigger(room):
    rounds_since_last = room.conversation_rounds - room.last_clue_rounds
    return rounds_since_last >= room.next_clue_interval

# 沉默触发
def check_silence_trigger(room):
    if room.silence_trigger_count >= SILENCE_TRIGGER_MAX:
        return False
    silence_duration = (now - room.last_message_time).total_seconds()
    return silence_duration >= SILENCE_TIMEOUT
```

#### 3. **房间管理 (Room Management)**
**文件**：`app/routes/room.py`

**功能**：
- ✅ 离开房间
- ✅ 房间状态查询

**API 端点**：
```
POST /api/room/leave?room_id=xxx&user_id=xxx
GET /api/room/:roomId/stats
```

#### 4. **AI 服务集成 (AI Service)**
**文件**：`app/services/grok_service.py`, `app/services/story_service.py`

**功能**：
- ✅ 故事生成（基于用户标签和描述）
- ✅ 故事种子生成（开场白）
- ✅ 剧情线索生成（AI驱动或模板fallback）

**AI 提示词示例**：
```python
# 故事生成
prompt = f"""
你是一个专业的角色扮演故事创作者...
基于以下信息生成一个故事：
- 用户标签: {tags}
- 用户描述: {description}
返回JSON格式...
"""

# 剧情线索生成
prompt = f"""
基于当前故事背景和对话历史，生成一个推动剧情发展的线索...
"""
```

#### 5. **后台任务 (Background Tasks)**
**文件**：`app/main.py`

**功能**：
- ✅ 沉默检查定时任务（每30秒）
- ✅ 异步 AI 生成

---

## C# 后端架构设计

### 1. 项目结构（遵循 Picasso 模式）

```
backend-csharp/src/
├── Feiyue.Api/                          # API 层
│   ├── Controllers/
│   │   ├── MatchController.cs
│   │   ├── ChatController.cs
│   │   └── RoomController.cs
│   ├── External/                        # External Contracts
│   │   ├── MatchModels.cs
│   │   ├── ChatModels.cs
│   │   └── ModelExtensions.cs
│   ├── Program.cs
│   └── appsettings.json
│
├── Feiyue.Match/                        # 匹配业务逻辑
│   ├── MatchService.cs                  # 核心匹配逻辑
│   ├── QueueService.cs                  # 队列管理
│   ├── MatchScoreCalculator.cs          # 匹配评分
│   ├── IMatchService.cs
│   ├── AppStartup.cs
│   └── Log.cs
│
├── Feiyue.Chat/                         # 聊天业务逻辑
│   ├── ChatService.cs                   # 消息处理
│   ├── StoryClueService.cs              # 剧情线索
│   ├── RoomService.cs                   # 房间管理
│   ├── BackgroundServices/
│   │   └── SilenceCheckHostedService.cs # 后台沉默检查
│   ├── IChatService.cs
│   ├── AppStartup.cs
│   └── Log.cs
│
├── Feiyue.Storage/                      # 数据访问层
│   ├── Repositories/
│   │   ├── UserRepository.cs
│   │   ├── MatchQueueRepository.cs
│   │   ├── ChatRoomRepository.cs
│   │   └── ChatMessageRepository.cs
│   ├── CosmosDb/                        # Cosmos DB (或 PostgreSQL)
│   │   └── CosmosDbContext.cs
│   ├── Redis/                           # 队列和缓存
│   │   └── RedisQueueManager.cs
│   ├── AppStartup.cs
│   └── Log.cs
│
├── Feiyue.AiClient/                     # AI 服务客户端
│   ├── AiServiceClient.cs               # Grok/OpenAI 客户端
│   ├── StoryGenerator.cs                # 故事生成
│   ├── Models.cs
│   ├── IAiServiceClient.cs
│   ├── AppStartup.cs
│   └── Log.cs
│
├── Feiyue.InternalContracts/            # 内部数据契约
│   ├── UserModels.cs
│   ├── MatchModels.cs
│   ├── ChatModels.cs
│   ├── StoryModels.cs
│   └── QueueModels.cs
│
└── Feiyue.Shared/                       # 共享库
    ├── Extensions/
    │   └── ServiceCollectionExtensions.cs
    ├── Utilities/
    │   ├── IdGenerator.cs
    │   └── DeterministicHasher.cs
    └── Constants/
        └── FeiyueConstants.cs
```

### 2. 数据模型设计

#### Internal Contracts（不可变数据模型）

```csharp
// Feiyue.InternalContracts/UserModels.cs
namespace Feiyue.InternalContracts;

public sealed record UserProfile(
    string Gender,              // "male" | "female" | "other"
    string AgeGroup,            // "<18" | "18-23" | "23+"
    string Height,
    string Weight,
    IReadOnlyList<string> Tags,
    string Description
);

public sealed record User(
    string Id,
    string AnonymousId,
    DateTimeOffset CreatedAt
);

// Feiyue.InternalContracts/MatchModels.cs
public sealed record MatchRequest(
    string Id,
    string UserId,
    UserProfile Profile,
    Story Story,
    string SelectedRole,        // "A" | "B"
    string Status,              // "waiting" | "matched" | "cancelled"
    DateTimeOffset CreatedAt
);

public sealed record MatchQueueEntry(
    string MatchId,
    string Gender,
    int WaitTimeSeconds,
    int MatchScore
);

// Feiyue.InternalContracts/StoryModels.cs
public sealed record Role(
    string Name,
    string Description,
    string Personality
);

public sealed record Story(
    string Title,
    string Background,
    Role MaleRole,
    Role FemaleRole
);

// Feiyue.InternalContracts/ChatModels.cs
public sealed record ChatMessage(
    string Id,
    string RoomId,
    string Role,                // "A" | "B" | "system"
    string Message,
    DateTimeOffset Timestamp,
    bool IsStoryClue,
    string? TriggerType         // "rounds" | "silence" | "manual" | "seed"
);

public sealed record ChatRoom(
    string Id,
    string MatchId1,
    string MatchId2,
    Story Story,
    string Status,              // "active" | "closed"
    int MessageCount,
    int ConversationRounds,     // x2 to avoid float (5 rounds = 10)
    DateTimeOffset LastMessageTime,
    int LastClueRounds,
    int NextClueInterval,
    int SilenceTriggerCount,
    DateTimeOffset CreatedAt
);
```

#### External Contracts（API 层）

```csharp
// Feiyue.Api/External/MatchModels.cs
namespace Feiyue.Api.External;

[JsonSerializable(typeof(MatchRequestData))]
[JsonSerializable(typeof(MatchResponse))]
internal sealed partial class MatchJsonSerializerContext : JsonSerializerContext { }

public sealed class MatchRequestData
{
    public required UserProfileRequest Profile { get; init; }
    public StoryData? Story { get; init; }
    public required string SelectedRole { get; init; }  // "A" | "B"
}

public sealed class MatchResponse
{
    public required bool Success { get; init; }
    public MatchResponseData? Data { get; init; }
    public ErrorData? Error { get; init; }
}

public sealed class MatchResponseData
{
    public required string UserId { get; init; }
    public required string AnonymousId { get; init; }
    public required string MatchId { get; init; }
    public required string Status { get; init; }
    public string? RoomId { get; init; }
    public StoryData? Story { get; init; }
    public string? YourRole { get; init; }
    public UserProfileRequest? PartnerProfile { get; init; }
    public required string Message { get; init; }
}
```

### 3. 核心服务实现

#### 匹配服务（Match Service）

```csharp
// Feiyue.Match/MatchService.cs
internal sealed class MatchService : IMatchService
{
    private readonly ILogger<MatchService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IMatchQueueRepository _queueRepository;
    private readonly IQueueService _queueService;
    private readonly IStoryGenerator _storyGenerator;

    public async Task<MatchResult> RequestMatchAsync(
        MatchRequest request,
        CancellationToken cancellationToken)
    {
        // 1. 创建用户
        User user = await _userRepository.CreateUserAsync(cancellationToken);

        // 2. 创建匹配请求
        MatchRequest matchRequest = await _queueRepository.CreateMatchRequestAsync(
            user.Id,
            request.Profile,
            request.Story,
            request.SelectedRole,
            cancellationToken
        );

        // 3. 尝试匹配（从相反性别队列查找）
        MatchQueueEntry? partner = await _queueService.TryFindMatchAsync(
            matchRequest,
            cancellationToken
        );

        if (partner is not null)
        {
            // 匹配成功 - 创建聊天室
            return await CreateMatchedRoomAsync(matchRequest, partner, cancellationToken);
        }

        // 等待匹配
        return new MatchResult
        {
            Success = true,
            Status = "waiting",
            UserId = user.Id,
            AnonymousId = user.AnonymousId,
            MatchId = matchRequest.Id
        };
    }

    private async Task<MatchResult> CreateMatchedRoomAsync(
        MatchRequest request1,
        MatchQueueEntry partner,
        CancellationToken cancellationToken)
    {
        // 生成或选择故事
        Story story = await ResolveStoryAsync(request1, partner, cancellationToken);

        // 创建聊天室
        ChatRoom room = await _roomRepository.CreateRoomAsync(
            request1.Id,
            partner.MatchId,
            story,
            cancellationToken
        );

        // 生成故事种子
        string storySeed = await _storyGenerator.GenerateStorySeedAsync(
            story,
            request1.Profile,
            cancellationToken
        );

        // 发送系统消息
        await _chatService.CreateSystemMessageAsync(
            room.Id,
            storySeed,
            isStoryClue: true,
            triggerType: "seed",
            cancellationToken
        );

        return new MatchResult
        {
            Success = true,
            Status = "matched",
            RoomId = room.Id,
            Story = story,
            YourRole = request1.SelectedRole
        };
    }
}
```

#### 队列服务（Queue Service）

```csharp
// Feiyue.Match/QueueService.cs
internal sealed class QueueService : IQueueService
{
    private readonly IRedisQueueManager _redis;
    private readonly IMatchScoreCalculator _scoreCalculator;
    private readonly ILogger<QueueService> _logger;

    public async Task<MatchQueueEntry?> TryFindMatchAsync(
        MatchRequest newMatch,
        CancellationToken cancellationToken)
    {
        // 从相反性别队列获取等待用户
        string oppositeGender = newMatch.Profile.Gender == "male" ? "female" : "male";
        IReadOnlyList<MatchQueueEntry> waitingUsers = 
            await _redis.GetQueueEntriesAsync(oppositeGender, cancellationToken);

        MatchQueueEntry? bestMatch = null;
        int bestScore = -1;

        foreach (MatchQueueEntry candidate in waitingUsers)
        {
            // 检查基本兼容性
            if (!CanMatch(newMatch, candidate))
                continue;

            // 计算匹配分数
            int score = _scoreCalculator.Calculate(newMatch, candidate);

            // 应用分阶段匹配策略
            int waitTime = candidate.WaitTimeSeconds;
            if (waitTime < 30 && score < 100)
                continue;
            if (waitTime < 60 && score < 50)
                continue;

            if (score > bestScore)
            {
                bestScore = score;
                bestMatch = candidate;
            }
        }

        if (bestMatch is not null)
        {
            // 从队列移除
            await _redis.RemoveFromQueueAsync(bestMatch.MatchId, cancellationToken);
        }

        return bestMatch;
    }
}
```

#### 聊天服务（Chat Service）

```csharp
// Feiyue.Chat/ChatService.cs
internal sealed class ChatService : IChatService
{
    private readonly ILogger<ChatService> _logger;
    private readonly IChatMessageRepository _messageRepository;
    private readonly IChatRoomRepository _roomRepository;
    private readonly IStoryClueService _storyClueService;

    public async Task<ChatMessageResult> SendMessageAsync(
        string roomId,
        string userId,
        string message,
        CancellationToken cancellationToken)
    {
        // 1. 获取房间和发送者角色
        ChatRoom room = await _roomRepository.GetRoomAsync(roomId, cancellationToken);
        string senderRole = await GetUserRoleInRoomAsync(room, userId, cancellationToken);

        // 2. 创建消息
        ChatMessage chatMessage = await _messageRepository.CreateMessageAsync(
            roomId,
            senderRole,
            message,
            cancellationToken
        );

        // 3. 更新房间统计
        await _roomRepository.IncrementStatsAsync(
            roomId,
            messageCount: 1,
            conversationRounds: 1,  // 每条消息 +1 (实际 0.5 轮)
            cancellationToken
        );

        // 4. 检查剧情触发
        StoryClueMessage? storyClue = await _storyClueService.CheckAndTriggerAsync(
            room,
            cancellationToken
        );

        return new ChatMessageResult
        {
            Message = chatMessage,
            StoryClue = storyClue,
            RoomStats = await _roomRepository.GetStatsAsync(roomId, cancellationToken)
        };
    }
}
```

#### 剧情线索服务（Story Clue Service）

```csharp
// Feiyue.Chat/StoryClueService.cs
internal sealed class StoryClueService : IStoryClueService
{
    private readonly IAiServiceClient _aiClient;
    private readonly IChatMessageRepository _messageRepository;
    private readonly ILogger<StoryClueService> _logger;

    public async Task<StoryClueMessage?> CheckAndTriggerAsync(
        ChatRoom room,
        CancellationToken cancellationToken)
    {
        // 检查轮数触发
        if (ShouldTriggerByRounds(room))
        {
            return await GenerateAndSaveClueAsync(
                room,
                triggerType: "rounds",
                cancellationToken
            );
        }

        // 沉默触发由后台服务处理
        return null;
    }

    private bool ShouldTriggerByRounds(ChatRoom room)
    {
        int roundsSinceLastClue = room.ConversationRounds - room.LastClueRounds;
        return roundsSinceLastClue >= room.NextClueInterval;
    }

    private async Task<StoryClueMessage> GenerateAndSaveClueAsync(
        ChatRoom room,
        string triggerType,
        CancellationToken cancellationToken)
    {
        string clueContent;

        try
        {
            // 尝试 AI 生成
            clueContent = await _aiClient.GenerateStoryClueAsync(
                room.Story,
                recentMessages: await GetRecentMessagesAsync(room.Id, cancellationToken),
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            _logger.FailedToGenerateStoryClue(room.Id, ex);
            
            // Fallback: 确定性模板生成
            clueContent = GenerateDeterministicClue(room.Id, room.MessageCount);
        }

        // 保存剧情线索消息
        ChatMessage clueMessage = await _messageRepository.CreateMessageAsync(
            room.Id,
            role: "system",
            clueContent,
            isStoryClue: true,
            triggerType: triggerType,
            cancellationToken
        );

        // 更新房间的下次触发条件
        await UpdateNextClueIntervalAsync(room, cancellationToken);

        return new StoryClueMessage
        {
            Id = clueMessage.Id,
            RoomId = room.Id,
            Message = clueContent,
            TriggerType = triggerType,
            Timestamp = clueMessage.Timestamp
        };
    }

    private string GenerateDeterministicClue(string roomId, int clueCount)
    {
        // 使用 MD5(roomId + clueCount) 生成确定性索引
        string seed = $"{roomId}_{clueCount}";
        byte[] hash = MD5.HashData(Encoding.UTF8.GetBytes(seed));
        int index = BitConverter.ToInt32(hash, 0) % StoryClueTemplates.Count;
        return StoryClueTemplates[index];
    }
}
```

#### 后台沉默检查服务

```csharp
// Feiyue.Chat/BackgroundServices/SilenceCheckHostedService.cs
internal sealed class SilenceCheckHostedService : BackgroundService
{
    private readonly ILogger<SilenceCheckHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.SilenceCheckServiceStarted();

        using PeriodicTimer timer = new(TimeSpan.FromSeconds(30));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await CheckSilenceTriggersAsync(stoppingToken);
            }
            catch (Exception ex) when (ex.IsNotCancelled())
            {
                _logger.SilenceCheckFailed(ex);
            }
        }
    }

    private async Task CheckSilenceTriggersAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        
        IChatRoomRepository roomRepository = 
            scope.ServiceProvider.GetRequiredService<IChatRoomRepository>();
        IStoryClueService storyClueService = 
            scope.ServiceProvider.GetRequiredService<IStoryClueService>();

        IReadOnlyList<ChatRoom> activeRooms = 
            await roomRepository.GetActiveRoomsAsync(cancellationToken);

        foreach (ChatRoom room in activeRooms)
        {
            if (ShouldTriggerSilenceClue(room))
            {
                await storyClueService.TriggerSilenceClueAsync(room, cancellationToken);
                _logger.SilenceClueTriggered(room.Id);
            }
        }
    }

    private bool ShouldTriggerSilenceClue(ChatRoom room)
    {
        if (room.SilenceTriggerCount >= 3) // MAX_SILENCE_TRIGGERS
            return false;

        if (room.MessageCount < 1)
            return false;

        TimeSpan silenceDuration = DateTimeOffset.UtcNow - room.LastMessageTime;
        return silenceDuration.TotalSeconds >= 120; // SILENCE_TIMEOUT
    }
}
```

### 4. 存储层设计

#### 数据库选择

**✅ 最终方案: PostgreSQL + Redis**
- **PostgreSQL**: 主数据存储（用户、匹配、房间、消息）- 持久化数据
- **Redis**: 队列管理、实时状态缓存 - 高性能操作

**为什么选择 Redis 做队列**：
1. **高性能**：内存操作，延迟 < 1ms，支持高并发
2. **原子操作**：RPUSH/LPOP/ZADD 等命令天然并发安全
3. **丰富数据结构**：支持 List（FIFO）、Sorted Set（优先级队列）
4. **扩展性强**：为 VIP 通道、优先匹配等高级功能预留架构空间

**未来扩展支持**：
- ✅ VIP 优先队列（Sorted Set with priority scores）
- ✅ 地理位置匹配（GEO commands）
- ✅ 实时在线状态（Pub/Sub）
- ✅ 匹配限流（Rate limiting）

#### Repository 实现

```csharp
// Feiyue.Storage/Repositories/MatchQueueRepository.cs
internal sealed class MatchQueueRepository : IMatchQueueRepository
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly ILogger<MatchQueueRepository> _logger;

    public async Task<MatchRequest> CreateMatchRequestAsync(
        string userId,
        UserProfile profile,
        Story story,
        string selectedRole,
        CancellationToken cancellationToken)
    {
        string id = IdGenerator.Generate();

        await using NpgsqlCommand cmd = _dataSource.CreateCommand("""
            INSERT INTO match_queue (id, user_id, profile, story, selected_role, status, created_at)
            VALUES ($1, $2, $3, $4, $5, $6, $7)
            """);

        cmd.Parameters.AddWithValue(id);
        cmd.Parameters.AddWithValue(userId);
        cmd.Parameters.AddWithValue(JsonSerializer.Serialize(profile));
        cmd.Parameters.AddWithValue(JsonSerializer.Serialize(story));
        cmd.Parameters.AddWithValue(selectedRole);
        cmd.Parameters.AddWithValue("waiting");
        cmd.Parameters.AddWithValue(DateTimeOffset.UtcNow);

        await cmd.ExecuteNonQueryAsync(cancellationToken);

        return new MatchRequest(
            id,
            userId,
            profile,
            story,
            selectedRole,
            "waiting",
            Dat（支持优先级扩展）

```csharp
// Feiyue.Storage/Redis/RedisQueueManager.cs
internal sealed class RedisQueueManager : IRedisQueueManager
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<RedisQueueManager> _logger;
    
    // 队列命名规范
    private const string StandardQueuePrefix = "queue:standard";  // 普通队列
    private const string VipQueuePrefix = "queue:vip";            // VIP 队列
    private const string PriorityQueuePrefix = "queue:priority";  // 优先级队列（Sorted Set）

    /// <summary>
    /// 添加用户到队列（支持优先级）
    /// </summary>
    public async Task AddToQueueAsync(
        string gender,
        MatchQueueEntry entry,
        QueuePriority priority = QueuePriority.Standard,
        CancellationToken cancellationToken = default)
    {
        IDatabase db = _redis.GetDatabase();
        string entryJson = JsonSerializer.Serialize(entry);

        if (priority == QueuePriority.Vip)
        {
            // VIP 用户使用 Sorted Set，按加入时间排序（但优先级高于普通用户）
            string vipKey = $"{VipQueuePrefix}:{gender}";
            double score = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            await db.SortedSetAddAsync(vipKey, entryJson, score);
            _logger.AddedToVipQueue(gender, entry.MatchId);
        }
        else
        {
            // 普通用户使用 List（FIFO）
            string queueKey = $"{StandardQueuePrefix}:{gender}";
            await db.ListRightPushAsync(queueKey, entryJson);
            _logger.AddedToQueue(gender, entry.MatchId);
        }

        // 设置 TTL（5分钟后自动清理）
        await SetQueueEntryTtlAsync(db, entry.MatchId, TimeSpan.FromMinutes(5));
    }

    /// <summary>
    /// 获取队列中的用户（VIP 优先）
    /// </summary>
    public async Task<IReadOnlyList<MatchQueueEntry>> GetQueueEntriesAsync(
        string gender,
        int maxCount = 50,
        CancellationToken cancellationToken = default)
    {
        IDatabase db = _redis.GetDatabase();
        List<MatchQueueEntry> allEntries = new();

        // 1. 先获取 VIP 队列（按优先级）
        string vipKey = $"{VipQueuePrefix}:{gender}";
        SortedSetEntry[] vipEntries = await db.SortedSetRangeByScoreWithScoresAsync(
            vipKey,
            take: maxCount
        );

        foreach (SortedSetEntry entry in vipEntries)
        {
            MatchQueueEntry? queueEntry = JsonSerializer.Deserialize<MatchQueueEntry>(entry.Element.ToString());
            if (queueEntry is not null)
            {
                allEntries.Add(queueEntry with { Priority = QueuePriority.Vip });
            }
        }

        // 2. 再获取普通队列
        if (allEntries.Count < maxCount)
        {
            string standardKey = $"{StandardQueuePrefix}:{gender}";
            int remaining = maxCount - allEntries.Count;
            RedisValue[] standardEntries = await db.ListRangeAsync(standardKey, 0, remaining - 1);

            foreach (RedisValue entry in standardEntries)
            {
                MatchQueueEntry? queueEntry = JsonSerializer.Deserialize<MatchQueueEntry>(entry.ToString()!);
                if (queueEntry is not null)
                {
                    allEntries.Add(queueEntry with { Priority = QueuePriority.Standard });
                }
            }
        }

        return allEntries.ToArray();
    }

    /// <summary>
    /// 从队列中移除用户
    /// </summary>
    public async Task RemoveFromQueueAsync(
        string matchId,
        string gender,
        QueuePriority priority,
        CancellationToken cancellationToken = default)
    {
        IDatabase db = _redis.GetDatabase();

        if (priority == QueuePriority.Vip)
        {
            string vipKey = $"{VipQueuePrefix}:{gender}";
            // Sorted Set 需要通过 value 查找删除
            await db.SortedSetRemoveAsync(vipKey, matchId);
        }
        else
        {
            string standardKey = $"{StandardQueuePrefix}:{gender}";
            await db.ListRemoveAsync(standardKey, matchId);
        }

        _logger.RemovedFromQueue(matchId, gender, priority);
    }

    /// <summary>
    /// 获取队列统计信息
    /// </summary>
    public async Task<QueueStats> GetQueueStatsAsync(CancellationToken cancellationToken = default)
    {
        IDatabase db = _redis.GetDatabase();

        long maleStandard = await db.ListLengthAsync($"{StandardQueuePrefix}:male");
        long maleVip = await db.SortedSetLengthAsync($"{VipQueuePrefix}:male");
        long femaleStandard = await db.ListLengthAsync($"{StandardQueuePrefix}:female");
        long femaleVip = await db.SortedSetLengthAsync($"{VipQueuePrefix}:female");

        return new QueueStats
        {
            MaleWaiting = (int)(maleStandard + maleVip),
            MaleVip = (int)maleVip,
            FemaleWaiting = (int)(femaleStandard + femaleVip),
            FemaleVip = (int)femaleVip,
            TotalWaiting = (int)(maleStandard + maleVip + femaleStandard + femaleVip)
        };
    }

    private async Task SetQueueEntryTtlAsync(
        IDatabase db,
        string matchId,
        TimeSpan ttl)
    {
        string ttlKey = $"queue:ttl:{matchId}";
        await db.StringSetAsync(ttlKey, "1", ttl);
    }
}

// Feiyue.InternalContracts/QueueModels.cs
public enum QueuePriority
{
    Standard = 0,
    Vip = 1,
    Super = 2  // 为未来的超级 VIP 预留
}

public sealed record QueueStats
{
    public required int MaleWaiting { get; init; }
    public required int MaleVip { get; init; }
    public required int FemaleWaiting { get; init; }
    public required int FemaleVip { get; init; }
    public required int TotalWaiting { get; init;     RedisValue[] entries = await db.ListRangeAsync(queueKey);

        return entries
            .Select(e => JsonSerializer.Deserialize<MatchQueueEntry>(e.ToString()!))
            .WhereNotNull()
            .ToArray();
    }
}
```

### 5. 配置和启动

#### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Feiyue": "Debug"
    }
  },
  "ConnectionStrings": {
    "PostgreSQL": "Host=localhost;Database=feiyue;Username=postgres;Password=***",
    "Redis": "localhost:6379"
  },
  "Feiyue": {
    "Match": {
      "TimeoutSeconds": 300,
      "InitialStrictTimeSeconds": 30,
      "RelaxedTimeSeconds": 60
    },
    "Chat": {
      "SilenceTimeoutSeconds": 120,
      "MaxSilenceTriggers": 3,
      "InitialClueInterval": 10,
      "MaxClueInterval": 80
    },
    "Ai": {
      "Provider": "Grok",
      "ApiKey": "***",
      "BaseUrl": "https://api.x.ai/v1"
    }
  }
}
```

#### AppStartup Pattern

```csharp
// Feiyue.Match/AppStartup.cs
namespace Feiyue.Match;

public static class AppStartup
{
    private sealed class Once { }

    public static IServiceCollection AddMatchServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services.IsAlreadyAdded<Once>())
            return services;

        services.AddOptions<MatchOptions>(configuration, "Feiyue:Match");
        
        services.AddSingleton<IMatchService, MatchService>();
        services.AddSingleton<IQueueService, QueueService>();
        services.AddSingleton<IMatchScoreCalculator, MatchScoreCalculator>();

        return services;
    }
}
```

---

## 迁移策略

### 阶段 1: 基础设施（Week 1-2）

1. ✅ **项目结构搭建**
   - 创建所有项目和文件夹结构
   - 配置 Directory.Build.props
   - 添加 NuGet 包依赖

2. ✅ **数据模型定义**
   - Internal Contracts（不可变 record）
   - External Contracts（API 层）
   - 数据库 Schema 迁移脚本

3. ✅ **存储层实现**
   - PostgreSQL Repositories
   - Redis 队列管理
   - 单元测试

### 阶段 2: 核心业务逻辑（Week 3-4）

1. ✅ **匹配系统**
   - 用户创建
   - 队列管理
   - 匹配算法
   - 匹配超时

2. ✅ **聊天系统**
   - 消息发送/接收
   - 房间管理
   - 统计更新

3. ✅ **AI 服务集成**
   - Grok API 客户端
   - 故事生成
   - 剧情线索生成

### 阶段 3: 高级功能（Week 5-6）

1. ✅ **剧情触发系统**
   - 轮数触发逻辑
   - 沉默触发逻辑
   - 确定性 Fallback
   - 后台定时任务

2. ✅ **API Controllers**
   - MatchController
   - ChatController
   - RoomController
   - Swagger 文档

### 阶段 4: 测试和优化（Week 7-8）

1. ✅ **单元测试**
   - 匹配算法测试
   - 剧情触发测试
   - 队列管理测试

2. ✅ **集成测试**
   - 完整匹配流程
   - 端到端聊天流程

3. ✅ **性能优化**
   - Redis 连接池
   - 数据库查询优化
   - 并发处理

---

## 技术要点

### 1. Analyzer 遵循

遵循 Picasso 的所有 Analyzer 规则：

```csharp
// ✅ 使用不可变 record
public sealed record UserProfile(string Gender, string AgeGroup);

// ✅ 使用 IReadOnlyList
IReadOnlyList<string> tags = profile.Tags;

// ✅ 使用 DateTimeOffset 而非 DateTime
DateTimeOffset createdAt = DateTimeOffset.UtcNow;

// ✅ 所有异步方法接受 CancellationToken
public async Task<MatchResult> RequestMatchAsync(
    MatchRequest request,
    CancellationToken cancellationToken);

// ✅ 使用 .EmptyIfNull()
IReadOnlyList<string> tags = profile.Tags.EmptyIfNull();
✅ **Redis**：成熟、高性能、支持多种数据结构
- **为什么选择 Redis**：
  - 支持 List（FIFO 普通队列）
  - 支持 Sorted Set（优先级队列，适合 VIP）
  - 支持 Pub/Sub（实时通知）
  - 支持 GEO（地理位置匹配）
  - 原子操作，并发安全
- **架构设计**：抽象 `IQueueManager` 接口，支持未来扩展Comparer.Ordinal);

// ✅ 使用 .WhereNotNull()
var validEntries = entries.WhereNotNull();

// ✅ 使用强类型日志
_logger.MatchRequestReceived(userId, gender);
```

### 2. 日志实现
高级功能扩展（Future Roadmap）

### VIP 功能设计

基于 Redis 优先级队列，我们可以轻松实现 VIP 功能：

```csharp
// VIP 用户匹配优先级
public enum VipTier
{
    Free = 0,        // 普通用户
    Basic = 1,       // 基础 VIP - 优先匹配
    Premium = 2,     // 高级 VIP - 优先匹配 + 更多故事选择
    Elite = 3        // 尊享 VIP - 最高优先级 + 专属客服
}

// VIP 特权
public sealed record VipPrivileges
{
    public int MatchPriority { get; init; }           // 匹配优先级
    public int MaxDailyMatches { get; init; }         // 每日匹配次数
    public bool CanSkipQueue { get; init; }           // 是否跳过队列
    public bool UnlimitedStories { get; init; }       // 无限故事生成
    public bool CustomStories { get; init; }          // 自定义故事
    public bool PrioritySupport { get; init; }        // 优先客服
}
```

### 其他扩展方向

1. **地理位置匹配**
   ```csharp
   // Redis GEO 支持
   await _redis.GeoAddAsync("users:location", longitude, latitude, userId);
   var nearbyUsers = await _redis.GeoRadiusAsync("users:location", ...);
   ```

2. **兴趣标签匹配优化**
   ```csharp
   // Redis Sets 交集运算
   await _redis.SetAddAsync($"tags:{userId}", tags);
   var commonTags = await _redis.SetCombineAsync(SetOperation.Intersect, ...);
   ```

3. **实时在线状态**
   ```csharp
   // Redis Pub/Sub
   await _redis.PublishAsync("match:success", notification);
   ```

4. **匹配历史和黑名单**
   ```csharp
   // Redis Sets 存储匹配历史
   await _redis.SetAddAsync($"history:{userId}", partnerId);
   var hasMatched = await _redis.SetContainsAsync($"history:{userId}", partnerId);
   ```

---

## 总结

本设计文档提供了一个完整的迁移方案，将 Feiyue Python 后端迁移到 C# Monorepo，并遵循 Picasso 的最佳实践：

✅ **微服务分层架构**（API → Business → Storage）  
✅ **不可变数据模型**（`sealed record`）  
✅ **Internal/External Contracts 解耦**  
✅ **AppStartup 模式**  
✅ **强类型日志**  
✅ **完整的错误处理**  
✅ **异步优先**  
✅ **依赖注入**  
✅ **Redis 高性能队列**（支持优先级、VIP 等扩展）  
✅ **可扩展架构**（为未来功能预留空间）ring userId,
        string gender);

    [LoggerMessage(
        EventId = 100_002,
        Level = LogLevel.Information,
        Message = "Match found. UserId={UserId}, PartnerId={PartnerId}, Score={Score}")]
    public static partial void MatchFound(
        this ILogger logger,
        string userId,
        string partnerId,
        int score);

    [LoggerMessage(
        EventId = 100_003,
        Level = LogLevel.Error,
        Message = "Failed to generate story clue. RoomId={RoomId}")]
    public static partial void FailedToGenerateStoryClue(
        this ILogger logger,
        string roomId,
        Exception exception);
}
```

### 3. 错误处理

```csharp
// 遵循 Picasso 的异常过滤模式
try
{
    await ProcessMatchAsync(request, cancellationToken);
}
catch (Exception ex) when (ex.IsNotCancelled())
{
    _logger.MatchProcessingFailed(request.UserId, ex);
    throw;
}
```

### 4. 配置模式

```csharp
// Feiyue.Match/MatchOptions.cs
internal sealed class MatchOptions
{
    public int TimeoutSeconds { get; init; } = 300;
    public int InitialStrictTimeSeconds { get; init; } = 30;
    public int RelaxedTimeSeconds { get; init; } = 60;
}

// 使用 AddOptions 扩展
services.AddOptions<MatchOptions>(configuration, "Feiyue:Match");
```

---

## 与 Python 版本的差异

| 特性 | Python 版本 | C# 版本 |
|------|------------|---------|
| **数据库** | SQLite (SQLAlchemy) | PostgreSQL (Npgsql) |
| **队列** | 内存（Python list） | Redis |
| **框架** | FastAPI | ASP.NET Core |
| **异步** | `asyncio` | `async/await` |
| **类型系统** | Pydantic | `sealed record` |
| **日志** | `print()` | `ILogger` + 源生成器 |
| **配置** | `.env` | `appsettings.json` |
| **后台任务** | `asyncio.create_task` | `BackgroundService` |
| **依赖注入** | FastAPI Depends | ASP.NET DI |

---

## 下一步行动

### 立即开始（本周）

1. **Review 本设计文档**
   - 讨论架构选择
   - 确认数据库方案
   - 调整项目结构

2. **创建基础项目**
   - 初始化所有 .csproj 文件
   - 配置 Directory.Build.props
   - 添加基本依赖

3. **定义 Internal Contracts**
   - 所有数据模型
   - 接口定义

### 后续规划

1. **实现存储层**（Week 1-2）
2. **实现匹配系统**（Week 3-4）
3. **实现聊天系统**（Week 5-6）
4. **测试和优化**（Week 7-8）

---

## 问题和讨论点

### 1. 数据库选择
- **PostgreSQL**：与 Python 版本一致，成熟稳定
- **Cosmos DB**：更符合 Picasso 风格，更好的扩展性
- **建议**：初期用 PostgreSQL，抽象存储层方便后续切换

### 2. 队列实现
- **Redis**：成熟、高性能、持久化
- **Azure Service Bus**：企业级、更多功能
- **建议**：使用 Redis，抽象 `IQueueManager` 接口

### 3. AI 服务
- **当前**：Grok API
- **扩展性**：设计 `IAiServiceClient` 接口，支持多种 AI 提供商
- **Fallback**：确定性模板生成（已有）

### 4. 测试策略
- **单元测试**：所有业务逻辑
- **集成测试**：完整流程测试
- **负载测试**：性能和并发测试
- **建议**：参考 Picasso.Tests.Unit 的测试模式

---

## 总结

本设计文档提供了一个完整的迁移方案，将 Feiyue Python 后端迁移到 C# Monorepo，并遵循 Picasso 的最佳实践：

✅ **微服务分层架构**（API → Business → Storage）  
✅ **不可变数据模型**（`sealed record`）  
✅ **Internal/External Contracts 解耦**  
✅ **AppStartup 模式**  
✅ **强类型日志**  
✅ **完整的错误处理**  
✅ **异步优先**  
✅ **依赖注入**  

下一步：一起 Review 本文档，讨论细节，然后开始实施！🚀
