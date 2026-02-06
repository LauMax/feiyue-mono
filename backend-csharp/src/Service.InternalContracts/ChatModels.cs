namespace Service.InternalContracts;

/// <summary>聊天消息</summary>
public sealed record ChatMessage(
    string Id,
    string RoomId,
    string SenderId, // 发送者ID
    string Content, // 消息内容
    string MessageType, // "text" | "system"
    DateTimeOffset SentAt);

/// <summary>聊天室</summary>
public sealed record ChatRoom(
    string Id,
    string User1Id,
    string User2Id,
    Story? Story,
    string Status, // "active" | "closed"
    DateTimeOffset CreatedAt,
    DateTimeOffset? ClosedAt);

/// <summary>房间统计信息</summary>
public sealed record RoomStats(int TotalMessages, int ConversationRounds, long LastActivityTime);