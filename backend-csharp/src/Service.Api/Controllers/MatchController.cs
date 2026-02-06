using Microsoft.AspNetCore.Mvc;
using Service.Chat;
using Service.Match;

namespace Service.Api.Controllers;

/// <summary>匹配控制器</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class MatchController : ControllerBase
{
    private readonly ILogger<MatchController> _logger;
    private readonly IMatchService _matchService;
    private readonly IChatService _chatService;

    public MatchController(ILogger<MatchController> logger, IMatchService matchService, IChatService chatService)
    {
        _logger = logger;
        _matchService = matchService;
        _chatService = chatService;
    }

    /// <summary>加入匹配队列</summary>
    [HttpPost("enqueue")]
    public async Task<IActionResult> EnqueueMatch([FromBody] EnqueueRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {UserId} joining match queue", request.UserId);

        await _matchService.EnqueueMatchAsync(request.UserId, request.Gender, request.GenderPreference, request.Priority, cancellationToken);

        return Ok(new { message = "Entered queue successfully" });
    }

    /// <summary>离开匹配队列</summary>
    [HttpPost("leave")]
    public async Task<IActionResult> LeaveQueue([FromBody] LeaveQueueRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {UserId} leaving match queue", request.UserId);

        await _matchService.LeaveQueueAsync(request.UserId, cancellationToken);

        return Ok(new { message = "Left queue successfully" });
    }

    /// <summary>获取队列统计</summary>
    [HttpGet("stats")]
    public async Task<IActionResult> GetQueueStats(CancellationToken cancellationToken)
    {
        var stats = await _matchService.GetQueueStatsAsync(cancellationToken);
        return Ok(stats);
    }

    /// <summary>尝试匹配 - 找到匹配后自动创建聊天室</summary>
    [HttpPost("find")]
    public async Task<IActionResult> FindMatch([FromBody] FindMatchRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {UserId} attempting to find match", request.UserId);

        var matchedUserId = await _matchService.TryMatchAsync(request.UserId, cancellationToken);

        if (matchedUserId is null)
            return Ok(new { matched = false, message = "No match found yet" });

        // 创建聊天室
        var room = await _chatService.CreateRoomAsync(request.UserId, matchedUserId, cancellationToken);

        _logger.LogInformation("Match successful! Created room {RoomId}", room.Id);

        return Ok(
            new
            {
                matched = true,
                roomId = room.Id,
                matchedWith = matchedUserId
            });
    }
}

// Request DTOs
public record EnqueueRequest(string UserId, string Gender, string GenderPreference, int Priority);

public record LeaveQueueRequest(string UserId);

public record FindMatchRequest(string UserId);