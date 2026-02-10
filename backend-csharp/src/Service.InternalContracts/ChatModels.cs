namespace Service.InternalContracts;

/// <summary>聊天消息</summary>
public sealed record ChatMessage(
    string Id,
    string RoomId,
    string SenderId,
    string Content,
    string MessageType, // "text" | "system"
    DateTimeOffset SentAt,
    string? TriggerType = null, // "seed" | "rounds" | "silence" | null
    int SequenceNumber = 0);

/// <summary>聊天室</summary>
public sealed record ChatRoom(
    string Id,
    string User1Id,
    string User2Id,
    Story? Story,
    string Status, // "active" | "closed"
    DateTimeOffset CreatedAt,
    DateTimeOffset? ClosedAt,
    bool IsVirtual = false,
    // 进程追踪
    int TotalMessages = 0,
    int ConversationRounds = 0, // 双方交替发言才+1
    string? LastSenderId = null,
    DateTimeOffset? LastActivityAt = null,
    // 线索触发
    int LastClueAtRound = 0,
    int NextClueInterval = 5, // 5→10→20→40
    int ClueCount = 0,
    int SilenceTriggerCount = 0, // max 3
    // 剧情阶段
    string StoryPhase = "opening"); // "opening" | "rising" | "climax" | "resolution"

/// <summary>虚拟人档案</summary>
public sealed record VirtualPersonProfile(
    string Id,
    string Name,
    string Gender, // "male" | "female"
    string AgeGroup,
    string Personality,
    string AvatarUrl);

/// <summary>WebSocket 事件信封 - 客户端发送</summary>
public sealed record ChatClientEvent(string Type, string? Content);

/// <summary>WebSocket 事件 - 服务端发送</summary>
public sealed record ChatServerEvent(string Type, object? Data);