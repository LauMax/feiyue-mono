using Service.InternalContracts;

namespace Service.Chat;

/// <summary>聊天服务接口</summary>
public interface IChatService
{
    /// <summary>创建聊天室</summary>
    Task<ChatRoom> CreateRoomAsync(string user1Id, string user2Id, Story? story = null, bool isVirtual = false, CancellationToken cancellationToken = default);

    /// <summary>获取聊天室</summary>
    Task<ChatRoom?> GetRoomAsync(string roomId, CancellationToken cancellationToken = default);

    /// <summary>获取用户的活跃聊天室</summary>
    Task<ChatRoom?> GetActiveRoomForUserAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>关闭聊天室</summary>
    Task CloseRoomAsync(string roomId, CancellationToken cancellationToken = default);

    /// <summary>检查用户是否在房间内</summary>
    Task<bool> IsUserInRoomAsync(string roomId, string userId, CancellationToken cancellationToken = default);

    /// <summary>结束聊天室</summary>
    Task<bool> EndRoomAsync(string roomId, CancellationToken cancellationToken = default);

    /// <summary>发送消息</summary>
    Task<ChatMessage> SendMessageAsync(string roomId, string senderId, string content, CancellationToken cancellationToken = default);

    /// <summary>发送系统消息</summary>
    Task<ChatMessage> SendSystemMessageAsync(string roomId, string content, string? triggerType = null, CancellationToken cancellationToken = default);

    /// <summary>获取聊天室消息</summary>
    Task<IReadOnlyList<ChatMessage>> GetMessagesAsync(string roomId, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>更新房间进程</summary>
    Task<ChatRoom?> UpdateRoomProgressAsync(string roomId, string senderId, CancellationToken cancellationToken = default);

    /// <summary>更新线索状态</summary>
    Task UpdateClueStateAsync(string roomId, int clueAtRound, int nextInterval, int clueCount, CancellationToken cancellationToken = default);
}