using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Service.InternalContracts;

namespace Service.ChatStorage;

public sealed class ChatStorage : IChatStorage
{
    private readonly IMongoCollection<ChatRoom> _chatRooms;
    private readonly IMongoCollection<ChatMessage> _chatMessages;

    static ChatStorage()
    {
        // 注册全局约定：camelCase 字段名
        var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("camelCase", conventionPack, _ => true);

        // 注册 ChatMessage 的 BsonClassMap
        if (!BsonClassMap.IsClassMapRegistered(typeof(ChatMessage)))
        {
            BsonClassMap.RegisterClassMap<ChatMessage>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetSerializer(new StringSerializer(BsonType.String));
                cm.MapMember(c => c.SentAt).SetSerializer(new DateTimeOffsetSerializer(BsonType.String));
            });
        }

        // 注册 ChatRoom 的 BsonClassMap
        if (!BsonClassMap.IsClassMapRegistered(typeof(ChatRoom)))
        {
            BsonClassMap.RegisterClassMap<ChatRoom>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetSerializer(new StringSerializer(BsonType.String));
                cm.MapMember(c => c.CreatedAt).SetSerializer(new DateTimeOffsetSerializer(BsonType.String));
                cm.MapMember(c => c.ClosedAt).SetSerializer(new NullableSerializer<DateTimeOffset>(new DateTimeOffsetSerializer(BsonType.String)));
                cm.MapMember(c => c.LastActivityAt).SetSerializer(new NullableSerializer<DateTimeOffset>(new DateTimeOffsetSerializer(BsonType.String)));
                cm.SetIgnoreExtraElements(true);
            });
        }

        // 注册 Story 的 BsonClassMap
        if (!BsonClassMap.IsClassMapRegistered(typeof(Story)))
        {
            BsonClassMap.RegisterClassMap<Story>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }

        // 注册 Role 的 BsonClassMap
        if (!BsonClassMap.IsClassMapRegistered(typeof(Role)))
        {
            BsonClassMap.RegisterClassMap<Role>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }

    public ChatStorage(IOptions<ChatStorageOptions> options)
    {
        var mongoUrl = new MongoUrl(options.Value.ConnectionString);
        var client = new MongoClient(mongoUrl);
        var db = client.GetDatabase(mongoUrl.DatabaseName ?? "feiyue");
        _chatRooms = db.GetCollection<ChatRoom>("chat_rooms");
        _chatMessages = db.GetCollection<ChatMessage>("chat_messages");
    }

    public async Task<ChatRoom> CreateRoomAsync(string user1Id, string user2Id, Story? story = null, bool isVirtual = false, CancellationToken cancellationToken = default)
    {
        var room = new ChatRoom(
            Id: Guid.NewGuid().ToString(),
            User1Id: user1Id,
            User2Id: user2Id,
            Story: story,
            Status: "active",
            CreatedAt: DateTimeOffset.UtcNow,
            ClosedAt: null,
            IsVirtual: isVirtual);
        await _chatRooms.InsertOneAsync(room, cancellationToken: cancellationToken);
        return room;
    }

    public async Task<ChatRoom?> GetRoomAsync(string roomId, CancellationToken cancellationToken = default)
    {
        return await _chatRooms.Find(x => x.Id == roomId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ChatRoom?> GetActiveRoomForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _chatRooms
            .Find(x => (x.User1Id == userId || x.User2Id == userId) && x.Status == "active")
            .SortByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CloseRoomAsync(string roomId, CancellationToken cancellationToken = default)
    {
        var update = Builders<ChatRoom>.Update
            .Set(x => x.Status, "closed")
            .Set(x => x.ClosedAt, DateTimeOffset.UtcNow);
        await _chatRooms.UpdateOneAsync(x => x.Id == roomId, update, cancellationToken: cancellationToken);
    }

    public async Task<ChatMessage> SaveMessageAsync(ChatMessage message, CancellationToken cancellationToken = default)
    {
        await _chatMessages.InsertOneAsync(message, cancellationToken: cancellationToken);
        return message;
    }

    public async Task<IReadOnlyList<ChatMessage>> GetMessagesAsync(string roomId, int limit = 50, CancellationToken cancellationToken = default)
    {
        var messages = await _chatMessages
            .Find(x => x.RoomId == roomId)
            .SortBy(x => x.SentAt)
            .Limit(limit)
            .ToListAsync(cancellationToken);
        return messages;
    }

    public async Task<ChatRoom?> UpdateRoomProgressAsync(string roomId, string senderId, CancellationToken cancellationToken = default)
    {
        var room = await GetRoomAsync(roomId, cancellationToken);
        if (room is null) return null;

        // 精确轮次：发送者与上一条不同 = 交替发言 = 新轮次
        bool isNewRound = room.LastSenderId is not null && room.LastSenderId != senderId;

        var update = Builders<ChatRoom>.Update
            .Inc(r => r.TotalMessages, 1)
            .Set(r => r.LastSenderId, senderId)
            .Set(r => r.LastActivityAt, DateTimeOffset.UtcNow);

        if (isNewRound)
            update = update.Inc(r => r.ConversationRounds, 1);

        var options = new FindOneAndUpdateOptions<ChatRoom> { ReturnDocument = ReturnDocument.After };
        return await _chatRooms.FindOneAndUpdateAsync(r => r.Id == roomId, update, options, cancellationToken);
    }

    public async Task UpdateClueStateAsync(string roomId, int clueAtRound, int nextInterval, int clueCount, CancellationToken cancellationToken = default)
    {
        var update = Builders<ChatRoom>.Update
            .Set(r => r.LastClueAtRound, clueAtRound)
            .Set(r => r.NextClueInterval, nextInterval)
            .Set(r => r.ClueCount, clueCount);
        await _chatRooms.UpdateOneAsync(r => r.Id == roomId, update, cancellationToken: cancellationToken);
    }
}

/// <summary>Chat storage options</summary>
public sealed class ChatStorageOptions
{
    public required string ConnectionString { get; set; }
}