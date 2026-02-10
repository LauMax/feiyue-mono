using System.Collections.Concurrent;
using System.Threading.Channels;
using Service.InternalContracts;

namespace Service.Api.WebSockets;

/// <summary>
/// 房间连接管理器 - 管理每个用户的 ChannelWriter 与房间的映射。
///
/// 关键设计：不直接操作 WebSocket，而是写入每个用户的 Channel，
/// 由 ChatWebSocketHandler 的写循环统一投递到 WebSocket。
/// 这样避免了多个线程并发写同一个 WebSocket 的问题。
/// </summary>
public sealed class RoomConnectionManager
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, ChannelWriter<ChatServerEvent>>> _rooms = new();

    /// <summary>注册用户的输出 Channel 到房间</summary>
    public void AddConnection(string roomId, string userId, ChannelWriter<ChatServerEvent> channelWriter)
    {
        var roomChannels = _rooms.GetOrAdd(roomId, _ => new ConcurrentDictionary<string, ChannelWriter<ChatServerEvent>>());
        roomChannels[userId] = channelWriter;
    }

    /// <summary>从房间移除用户的 Channel</summary>
    public void RemoveConnection(string roomId, string userId)
    {
        if (_rooms.TryGetValue(roomId, out var roomChannels))
        {
            roomChannels.TryRemove(userId, out _);
            if (roomChannels.IsEmpty)
                _rooms.TryRemove(roomId, out _);
        }
    }

    /// <summary>广播消息到房间内所有用户的 Channel，可排除指定用户（如发送者）</summary>
    public Task BroadcastToRoomAsync(string roomId, ChatServerEvent serverEvent, CancellationToken cancellationToken, string? excludeUserId = null)
    {
        if (!_rooms.TryGetValue(roomId, out var roomChannels))
            return Task.CompletedTask;

        foreach (var (userId, channel) in roomChannels)
        {
            if (userId == excludeUserId)
                continue;

            channel.TryWrite(serverEvent);
        }

        return Task.CompletedTask;
    }

    /// <summary>发送消息给房间内指定用户的 Channel</summary>
    public Task SendToUserAsync(string roomId, string userId, ChatServerEvent serverEvent, CancellationToken cancellationToken)
    {
        if (!_rooms.TryGetValue(roomId, out var roomChannels))
            return Task.CompletedTask;

        if (roomChannels.TryGetValue(userId, out var channel))
        {
            channel.TryWrite(serverEvent);
        }

        return Task.CompletedTask;
    }

    /// <summary>获取房间内的连接数</summary>
    public int GetConnectionCount(string roomId)
    {
        if (_rooms.TryGetValue(roomId, out var roomChannels))
            return roomChannels.Count;
        return 0;
    }
}
