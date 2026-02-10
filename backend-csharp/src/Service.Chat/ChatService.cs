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

    public async Task<ChatRoom> CreateRoomAsync(string user1Id, string user2Id, Story? story = null, bool isVirtual = false, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating chat room for users {User1} and {User2} (virtual: {IsVirtual})", user1Id, user2Id, isVirtual);
        return await _chatStorage.CreateRoomAsync(user1Id, user2Id, story, isVirtual, cancellationToken);
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

    public async Task<bool> IsUserInRoomAsync(string roomId, string userId, CancellationToken cancellationToken = default)
    {
        var room = await _chatStorage.GetRoomAsync(roomId, cancellationToken);
        if (room is null)
            return false;

        return room.User1Id == userId || room.User2Id == userId;
    }

    public async Task<bool> EndRoomAsync(string roomId, CancellationToken cancellationToken = default)
    {
        var room = await _chatStorage.GetRoomAsync(roomId, cancellationToken);
        if (room is null)
            return false;

        _logger.LogInformation("Ending chat room {RoomId}", roomId);
        await _chatStorage.CloseRoomAsync(roomId, cancellationToken);
        return true;
    }

    public async Task<ChatMessage> SendMessageAsync(string roomId, string senderId, string content, CancellationToken cancellationToken = default)
    {
        var message = new ChatMessage(Id: Guid.NewGuid().ToString(), RoomId: roomId, SenderId: senderId, Content: content, MessageType: "text", SentAt: DateTimeOffset.UtcNow);

        _logger.LogInformation("Saving message in room {RoomId} from user {SenderId}", roomId, senderId);
        return await _chatStorage.SaveMessageAsync(message, cancellationToken);
    }

    public async Task<ChatMessage> SendSystemMessageAsync(string roomId, string content, string? triggerType = null, CancellationToken cancellationToken = default)
    {
        var message = new ChatMessage(
            Id: Guid.NewGuid().ToString(),
            RoomId: roomId,
            SenderId: "system",
            Content: content,
            MessageType: "system",
            SentAt: DateTimeOffset.UtcNow,
            TriggerType: triggerType);

        _logger.LogInformation("Saving system message in room {RoomId} (trigger: {TriggerType})", roomId, triggerType ?? "none");
        return await _chatStorage.SaveMessageAsync(message, cancellationToken);
    }

    public async Task<IReadOnlyList<ChatMessage>> GetMessagesAsync(string roomId, int limit = 50, CancellationToken cancellationToken = default)
    {
        return await _chatStorage.GetMessagesAsync(roomId, limit, cancellationToken);
    }

    public async Task<ChatRoom?> UpdateRoomProgressAsync(string roomId, string senderId, CancellationToken cancellationToken = default)
    {
        return await _chatStorage.UpdateRoomProgressAsync(roomId, senderId, cancellationToken);
    }

    public async Task UpdateClueStateAsync(string roomId, int clueAtRound, int nextInterval, int clueCount, CancellationToken cancellationToken = default)
    {
        await _chatStorage.UpdateClueStateAsync(roomId, clueAtRound, nextInterval, clueCount, cancellationToken);
    }
}