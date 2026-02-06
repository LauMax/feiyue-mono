using Microsoft.Extensions.Logging;
using Service.ChatStorage;
using Service.InternalContracts;

namespace Service.Chat;

/// <summary>聊天服务实现 - 参考Picasso模式</summary>
internal sealed class ChatService : IChatService
{
    private readonly ILogger<ChatService> _logger;
    private readonly IChatStorage _chatStorage;

    public ChatService(ILogger<ChatService> logger, IChatStorage chatStorage)
    {
        _logger = logger;
        _chatStorage = chatStorage;
    }

    public async Task<ChatRoom> CreateRoomAsync(string user1Id, string user2Id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating chat room for users {User1} and {User2}", user1Id, user2Id);
        return await _chatStorage.CreateRoomAsync(user1Id, user2Id, cancellationToken);
    }

    public async Task<ChatRoom?> GetRoomAsync(string roomId, CancellationToken cancellationToken = default)
    {
        return await _chatStorage.GetRoomAsync(roomId, cancellationToken);
    }

    public async Task<ChatRoom?> GetActiveRoomForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _chatStorage.GetActiveRoomForUserAsync(userId, cancellationToken);
    }

    public async Task CloseRoomAsync(string roomId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Closing chat room {RoomId}", roomId);
        await _chatStorage.CloseRoomAsync(roomId, cancellationToken);
    }

    public async Task<ChatMessage> SendMessageAsync(string roomId, string senderId, string content, CancellationToken cancellationToken = default)
    {
        var message = new ChatMessage(Id: Guid.NewGuid().ToString(), RoomId: roomId, SenderId: senderId, Content: content, MessageType: "text", SentAt: DateTimeOffset.UtcNow);

        _logger.LogInformation("Saving message in room {RoomId} from user {SenderId}", roomId, senderId);
        return await _chatStorage.SaveMessageAsync(message, cancellationToken);
    }

    public async Task<IReadOnlyList<ChatMessage>> GetMessagesAsync(string roomId, int limit = 50, CancellationToken cancellationToken = default)
    {
        return await _chatStorage.GetMessagesAsync(roomId, limit, cancellationToken);
    }
}