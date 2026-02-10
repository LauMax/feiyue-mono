using Microsoft.Extensions.Logging;
using Service.InternalContracts;
using Service.StoryGeneration;

namespace Service.Chat;

/// <summary>
/// 聊天进度引擎 — 每条消息后更新轮次，并检查是否该触发剧情线索。
///
/// 触发规则（从 Python 后端移植）：
/// - 首次线索在第 5 轮触发
/// - 后续间隔翻倍：5 → 10 → 20 → 40
/// - 沉默触发最多 3 次（未来实现）
/// </summary>
internal sealed class ChatProgressService : IChatProgressService
{
    private readonly ILogger<ChatProgressService> _logger;
    private readonly IChatService _chatService;
    private readonly IStoryGenerationService _storyService;

    public ChatProgressService(
        ILogger<ChatProgressService> logger,
        IChatService chatService,
        IStoryGenerationService storyService)
    {
        _logger = logger;
        _chatService = chatService;
        _storyService = storyService;
    }

    public async Task<ChatServerEvent?> ProcessMessageAsync(
        string roomId, string senderId, CancellationToken cancellationToken)
    {
        // Step 1: 原子更新房间进程（轮次、消息数、活跃时间）
        var room = await _chatService.UpdateRoomProgressAsync(roomId, senderId, cancellationToken);
        if (room is null)
        {
            _logger.LogWarning("Room {RoomId} not found for progress update.", roomId);
            return null;
        }

        // Step 2: 检查是否有故事（没有故事则不触发线索）
        if (room.Story is null)
            return null;

        // Step 3: 计算是否触发
        var roundsSinceLastClue = room.ConversationRounds - room.LastClueAtRound;
        var shouldTrigger = roundsSinceLastClue >= room.NextClueInterval;

        if (!shouldTrigger)
            return null;

        _logger.LogInformation(
            "Triggering story clue for room {RoomId}: rounds={Rounds}, lastClueAt={LastClue}, interval={Interval}.",
            roomId, room.ConversationRounds, room.LastClueAtRound, room.NextClueInterval);

        // Step 4: 构建上下文 — 取最近消息，过滤 system，取最后 6 条
        var messages = await _chatService.GetMessagesAsync(roomId, 20, cancellationToken);
        var recentMessages = messages
            .Where(m => m.MessageType != "system")
            .TakeLast(6)
            .Select(m => new ConversationMessage(
                m.SenderId == room.User1Id ? "用户A" : "用户B",
                m.Content))
            .ToArray();

        var clueContext = new StoryClueContext(
            room.Story.Background,
            Array.Empty<string>(),
            recentMessages,
            "conversation");

        // Step 5: AI 生成线索（失败会自动 fallback）
        var clueContent = await _storyService.GenerateStoryClueAsync(clueContext, cancellationToken);

        // Step 6: 保存为 system 消息
        await _chatService.SendSystemMessageAsync(roomId, clueContent, "rounds", cancellationToken);

        // Step 7: 更新线索状态（间隔翻倍）
        var newClueCount = room.ClueCount + 1;
        var newInterval = room.NextClueInterval * 2; // 5→10→20→40
        await _chatService.UpdateClueStateAsync(
            roomId, room.ConversationRounds, newInterval, newClueCount, cancellationToken);

        _logger.LogInformation(
            "Story clue #{ClueCount} sent for room {RoomId}. Next interval: {NextInterval}.",
            newClueCount, roomId, newInterval);

        // Step 8: 返回事件让调用方广播（type 必须为 "message"，前端只处理此类型）
        return new ChatServerEvent("message", new
        {
            id = Guid.NewGuid().ToString(),
            roomId,
            senderId = "system",
            content = clueContent,
            messageType = "system",
            sentAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
        });
    }
}
