using Service.InternalContracts;

namespace Service.Chat;

/// <summary>聊天服务接口</summary>
public interface IChatService
{
    /// <summary>创建聊天室</summary>
    Task<ChatRoom> CreateRoomAsync(string user1Id, string user2Id, CancellationToken cancellationToken = default);

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

    /// <summary>获取聊天室消息</summary>
    Task<IReadOnlyList<ChatMessage>> GetMessagesAsync(string roomId, int limit = 50, CancellationToken cancellationToken = default);
}