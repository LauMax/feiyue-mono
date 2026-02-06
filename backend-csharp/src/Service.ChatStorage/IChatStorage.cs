using Service.InternalContracts;

namespace Service.ChatStorage;

/// <summary>Storage layer for chat data using Supabase PostgreSQL.</summary>
public interface IChatStorage
{
    Task<ChatRoom> CreateRoomAsync(string user1Id, string user2Id, CancellationToken cancellationToken = default);
    Task<ChatRoom?> GetRoomAsync(string roomId, CancellationToken cancellationToken = default);
    Task<ChatRoom?> GetActiveRoomForUserAsync(string userId, CancellationToken cancellationToken = default);
    Task CloseRoomAsync(string roomId, CancellationToken cancellationToken = default);

    Task<ChatMessage> SaveMessageAsync(ChatMessage message, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ChatMessage>> GetMessagesAsync(string roomId, int limit = 50, CancellationToken cancellationToken = default);
}