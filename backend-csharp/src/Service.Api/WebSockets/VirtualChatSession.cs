using System.Threading.Channels;
using Service.Chat;
using Service.InternalContracts;

namespace Service.Api.WebSockets;

/// <summary>
/// 虚拟人聊天会话 - 用户对 AI 虚拟人。
///
/// 借鉴 Picasso 的 Orchestrator.Inner 模式：
/// 1. 收到用户消息 → 立即存用户消息（Picasso step 3.1: PersistHumanMessageAsync）
/// 2. 调用 AI 后端获取回复 → 通过 Channel 流式推给客户端
/// 3. AI 回复完成后存 AI 消息（Picasso step 14.1: PersistStateNoCancelAsync）
///
/// 当前为 Mock 实现：延迟 1-2 秒后回复固定消息。
/// 后续会替换为调用 Python AI 后端。
/// </summary>
public sealed class VirtualChatSession : IChatSession
{
    private readonly ILogger<VirtualChatSession> _logger;
    private readonly IChatService _chatService;
    private readonly string _roomId;
    private readonly string _userId;
    private readonly ChannelWriter<ChatServerEvent> _outputChannel;
    private CancellationTokenSource? _pendingReplyCts;

    // 虚拟人ID前缀，便于识别
    private const string VirtualUserIdPrefix = "virtual_";

    // Mock 回复列表 - 后续替换为 LLM 调用
    private static readonly string[] MockReplies =
    [
        "你好呀～今天过得怎么样？",
        "哈哈，是吗？跟我说说看～",
        "我觉得你说得很有道理呢",
        "嗯嗯，然后呢？",
        "真的吗！好有趣哦～",
        "我也这么觉得！",
        "你好有想法啊～",
        "继续说继续说，我在认真听～"
    ];

    private int _replyIndex;

    public VirtualChatSession(
        ILogger<VirtualChatSession> logger,
        IChatService chatService,
        string roomId,
        string userId,
        ChannelWriter<ChatServerEvent> outputChannel)
    {
        _logger = logger;
        _chatService = chatService;
        _roomId = roomId;
        _userId = userId;
        _outputChannel = outputChannel;
    }

    public async Task HandleMessageAsync(string content, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Virtual session: User {UserId} sent message in room {RoomId}", _userId, _roomId);

        // 取消前一个等待中的虚拟人回复（如果用户连续发送消息）
        _pendingReplyCts?.Cancel();

        // Step 1: 立即推送用户消息回显（让前端看到自己的消息）
        var userMessageId = Guid.NewGuid().ToString();
        var sentAt = DateTimeOffset.UtcNow;
        _outputChannel.TryWrite(new ChatServerEvent("message", new
        {
            id = userMessageId,
            roomId = _roomId,
            senderId = _userId,
            content,
            messageType = "text",
            sentAt = sentAt.ToUnixTimeMilliseconds()
        }));

        // Step 2: 异步存用户消息 - 借鉴 Picasso step 3.1: PersistHumanMessageAsync
        _ = PersistMessageAsync(_userId, content);

        // Step 3: 模拟虚拟人 "正在输入..." → 延迟回复
        _pendingReplyCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _ = GenerateVirtualReplyAsync(_pendingReplyCts.Token);
    }

    /// <summary>
    /// 生成虚拟人回复 - 当前为 Mock 实现。
    ///
    /// TODO: 后续替换为调用 Python AI 后端：
    /// var response = await httpClient.PostAsJsonAsync("http://ai-service/chat", new { roomId, userId, content });
    /// 并支持流式返回（SSE/streaming），对接 Picasso 的 StreamPipeline 模式。
    /// </summary>
    private async Task GenerateVirtualReplyAsync(CancellationToken cancellationToken)
    {
        try
        {
            // 发送 "正在输入" 状态
            _outputChannel.TryWrite(new ChatServerEvent("typing", new { userId = GetVirtualUserId() }));

            // Mock: 延迟 1-2 秒模拟 AI 思考时间
            var delay = Random.Shared.Next(1000, 2000);
            await Task.Delay(delay, cancellationToken);

            // 选择回复
            var reply = MockReplies[_replyIndex % MockReplies.Length];
            _replyIndex++;

            var virtualUserId = GetVirtualUserId();
            var replyId = Guid.NewGuid().ToString();
            var repliedAt = DateTimeOffset.UtcNow;

            // 通过 Channel 推送虚拟人回复 → 写循环发给客户端
            _outputChannel.TryWrite(new ChatServerEvent("message", new
            {
                id = replyId,
                roomId = _roomId,
                senderId = virtualUserId,
                content = reply,
                messageType = "text",
                sentAt = repliedAt.ToUnixTimeMilliseconds(),
                isVirtual = true
            }));

            // Step 4: 回复完成后存 AI 消息 - 借鉴 Picasso step 14.1: PersistStateNoCancelAsync
            _ = PersistMessageAsync(virtualUserId, reply);

            _logger.LogInformation("Virtual reply sent in room {RoomId}: {Reply}", _roomId, reply);
        }
        catch (OperationCanceledException)
        {
            // 用户发了新消息，取消当前回复
            _logger.LogDebug("Virtual reply cancelled for room {RoomId}", _roomId);
        }
    }

    /// <summary>
    /// 异步持久化消息 - 借鉴 Picasso 的 PersistStateNoCancelAsync 模式。
    /// 使用独立的 CancellationToken（不跟随请求取消），确保消息尽量写入。
    /// </summary>
    private async Task PersistMessageAsync(string senderId, string content)
    {
        try
        {
            await _chatService.SendMessageAsync(_roomId, senderId, content, default);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to persist virtual chat message to database");
        }
    }

    private string GetVirtualUserId()
    {
        // 从房间中找出虚拟人的 userId（不是当前用户的那个）
        return $"{VirtualUserIdPrefix}{_roomId[..8]}";
    }

    public ValueTask DisposeAsync()
    {
        _pendingReplyCts?.Cancel();
        _pendingReplyCts?.Dispose();
        return ValueTask.CompletedTask;
    }
}
