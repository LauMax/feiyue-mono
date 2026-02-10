using Service.InternalContracts;

namespace Service.ChatStorage;

/// <summary>Storage layer for chat data.</summary>
public interface IChatStorage
{
    Task<ChatRoom> CreateRoomAsync(string user1Id, string user2Id, Story? story = null, bool isVirtual = false, CancellationToken cancellationToken = default);
    Task<ChatRoom?> GetRoomAsync(string roomId, CancellationToken cancellationToken = default);
    Task<ChatRoom?> GetActiveRoomForUserAsync(string userId, CancellationToken cancellationToken = default);
    Task CloseRoomAsync(string roomId, CancellationToken cancellationToken = default);

    Task<ChatMessage> SaveMessageAsync(ChatMessage message, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ChatMessage>> GetMessagesAsync(string roomId, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>原子更新房间进程（轮次、消息数、活跃时间）。返回更新后的房间。</summary>
    Task<ChatRoom?> UpdateRoomProgressAsync(string roomId, string senderId, CancellationToken cancellationToken = default);

    /// <summary>更新线索触发状态</summary>
    Task UpdateClueStateAsync(string roomId, int clueAtRound, int nextInterval, int clueCount, CancellationToken cancellationToken = default);
}