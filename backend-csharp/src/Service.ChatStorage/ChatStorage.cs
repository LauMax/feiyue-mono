using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Service.InternalContracts;

namespace Service.ChatStorage;

public sealed class ChatStorage : IChatStorage
{
    private readonly IMongoCollection<ChatRoom> _chatRooms;
    private readonly IMongoCollection<ChatMessage> _chatMessages;

    public ChatStorage(IOptions<ChatStorageOptions> options)
    {
        var mongoUrl = new MongoUrl(options.Value.ConnectionString);
        var client = new MongoClient(mongoUrl);
        var db = client.GetDatabase(mongoUrl.DatabaseName ?? "feiyue");
        _chatRooms = db.GetCollection<ChatRoom>("chat_rooms");
        _chatMessages = db.GetCollection<ChatMessage>("chat_messages");
    }

    public async Task<ChatRoom> CreateRoomAsync(string user1Id, string user2Id, CancellationToken cancellationToken = default)
    {
        var room = new ChatRoom(Id: Guid.NewGuid().ToString(), User1Id: user1Id, User2Id: user2Id, CreatedAt: DateTime.UtcNow, ClosedAt: null, Status: "active");
        await _chatRooms.InsertOneAsync(room, null, cancellationToken);
        return room;
    }

    public async Task<ChatRoom?> GetRoomAsync(string roomId, CancellationToken cancellationToken = default)
    {
        var room = await _chatRooms.Find(x => x.Id == roomId).FirstOrDefaultAsync(cancellationToken);
        return room;
    }

    public async Task<ChatRoom?> GetActiveRoomForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var room = await _chatRooms
            .Find(x => (x.User1Id == userId || x.User2Id == userId) && x.Status == "active")
            .SortByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
        return room;
    }

    public async Task CloseRoomAsync(string roomId, CancellationToken cancellationToken = default)
    {
        var update = Builders<ChatRoom>.Update.Set(x => x.Status, "closed").Set(x => x.ClosedAt, DateTime.UtcNow);
        await _chatRooms.UpdateOneAsync(x => x.Id == roomId, update, null, cancellationToken);
    }

    public async Task<ChatMessage> SaveMessageAsync(ChatMessage message, CancellationToken cancellationToken = default)
    {
        await _chatMessages.InsertOneAsync(message, null, cancellationToken);
        return message;
    }

    public async Task<IReadOnlyList<ChatMessage>> GetMessagesAsync(string roomId, int limit = 50, CancellationToken cancellationToken = default)
    {
        var messages = await _chatMessages.Find(x => x.RoomId == roomId).SortByDescending(x => x.SentAt).Limit(limit).ToListAsync(cancellationToken);
        messages.Reverse(); // 返回时间正序
        return messages;
    }
}

/// <summary>Chat storage options</summary>
public sealed class ChatStorageOptions
{
    public required string ConnectionString { get; set; }
}