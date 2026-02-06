namespace Service.InternalContracts;

/// <summary>聊天消息</summary>
public sealed record ChatMessage(
    string Id,
    string RoomId,
    string SenderId,
    string Content,
    string MessageType, // "text" | "system"
    DateTimeOffset SentAt);

/// <summary>聊天室</summary>
public sealed record ChatRoom(
    string Id,
    string User1Id,
    string User2Id,
    string Status, // "active" | "closed"
    DateTimeOffset CreatedAt,
    DateTimeOffset? ClosedAt);

/// <summary>聊天消息发送结果</summary>
public sealed record ChatMessageResult(ChatMessage Message, ChatMessage? StoryClue, RoomStats RoomStats);

/// <summary>房间统计信息</summary>
public sealed record RoomStats(int TotalMessages, int ConversationRounds, DateTimeOffset LastActivityTime);