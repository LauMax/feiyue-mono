using System.Threading.Channels;
using Service.Chat;
using Service.InternalContracts;

namespace Service.Api.WebSockets;

/// <summary>
/// 真人聊天会话 - 用户对用户。
///
/// 借鉴 Picasso 的 "先发后存" 模式：
/// 1. 收到用户消息 → 立即广播给房间所有人（低延迟）
/// 2. 异步写 MongoDB（不阻塞用户体验）
/// 3. 即使 DB 写入失败，消息也已经送达对方
/// </summary>
public sealed class HumanChatSession : IChatSession
{
    private readonly ILogger<HumanChatSession> _logger;
    private readonly IChatService _chatService;
    private readonly IChatProgressService _progressService;
    private readonly RoomConnectionManager _connectionManager;
    private readonly string _roomId;
    private readonly string _userId;
    private readonly ChannelWriter<ChatServerEvent> _outputChannel;

    public HumanChatSession(
        ILogger<HumanChatSession> logger,
        IChatService chatService,
        IChatProgressService progressService,
        RoomConnectionManager connectionManager,
        string roomId,
        string userId,
        ChannelWriter<ChatServerEvent> outputChannel)
    {
        _logger = logger;
        _chatService = chatService;
        _progressService = progressService;
        _connectionManager = connectionManager;
        _roomId = roomId;
        _userId = userId;
        _outputChannel = outputChannel;
    }

    public async Task HandleMessageAsync(string content, CancellationToken cancellationToken)
    {
        var messageId = Guid.NewGuid().ToString();
        var sentAt = DateTimeOffset.UtcNow;

        _logger.LogInformation("Human message from {UserId} in room {RoomId}", _userId, _roomId);

        // Step 1: 立即广播给房间所有人（包括自己的 Channel 和对方的 WebSocket）
        var messageEvent = new ChatServerEvent("message", new
        {
            id = messageId,
            roomId = _roomId,
            senderId = _userId,
            content,
            messageType = "text",
            sentAt = sentAt.ToUnixTimeMilliseconds()
        });

        // 广播给房间内其他人（排除发送者，前端已乐观添加）
        await _connectionManager.BroadcastToRoomAsync(_roomId, messageEvent, cancellationToken, excludeUserId: _userId);

        // Step 2: 异步写 DB - 借鉴 Picasso 的 fire-and-forget + 容错模式
        _ = PersistMessageAsync(messageId, content, sentAt);

        // Step 3: 异步检查是否触发剧情线索（fire-and-forget）
        _ = CheckAndTriggerClueAsync();
    }

    /// <summary>
    /// 检查并触发剧情线索 — 更新进度后如果满足条件，广播线索给房间所有人。
    /// </summary>
    private async Task CheckAndTriggerClueAsync()
    {
        try
        {
            var clueEvent = await _progressService.ProcessMessageAsync(_roomId, _userId, default);
            if (clueEvent is not null)
            {
                // 线索广播给房间所有人（包括双方）
                await _connectionManager.BroadcastToRoomAsync(_roomId, clueEvent, default);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to check/trigger story clue for room {RoomId}", _roomId);
        }
    }

    /// <summary>
    /// 异步持久化消息 - 借鉴 Picasso 的 PersistStateNoCancelAsync 模式。
    /// 即使取消或失败也尝试完成写入。
    /// </summary>
    private async Task PersistMessageAsync(string messageId, string content, DateTimeOffset sentAt)
    {
        try
        {
            await _chatService.SendMessageAsync(_roomId, _userId, content, default);
            _logger.LogDebug("Message {MessageId} persisted to DB", messageId);
        }
        catch (Exception ex)
        {
            // DB 写入失败不影响聊天体验，只记录日志
            _logger.LogWarning(ex, "Failed to persist message {MessageId} to database", messageId);
        }
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
