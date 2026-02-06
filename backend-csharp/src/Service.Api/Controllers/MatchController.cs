using Microsoft.AspNetCore.Mvc;
using Service.Chat;
using Service.InternalContracts;
using Service.Match;

namespace Service.Api.Controllers;

/// <summary>匹配控制器 - 对齐 Python 后端 API</summary>
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

    /// <summary>创建匹配请求 - Frontend 主要使用的 API</summary>
    /// <remarks>POST /api/match/request</remarks>
    [HttpPost("request")]
    public async Task<IActionResult> RequestMatch([FromBody] MatchRequestBody request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Match request from user with gender {Gender}, role {Role}", request.Profile.Gender, request.SelectedRole);

        var profile = new MatchUserProfile(
            Gender: request.Profile.Gender,
            AgeGroup: request.Profile.AgeGroup,
            Height: request.Profile.Height,
            Weight: request.Profile.Weight,
            Tags: request.Profile.Tags ?? [],
            Description: request.Profile.Description);

        Story? story = null;
        if (request.Story is not null)
        {
            story = new Story(
                Title: request.Story.Title,
                Background: request.Story.Background,
                MaleRole: new Role(request.Story.MaleRole.Name, request.Story.MaleRole.Description, request.Story.MaleRole.Personality),
                FemaleRole: new Role(request.Story.FemaleRole.Name, request.Story.FemaleRole.Description, request.Story.FemaleRole.Personality));
        }

        var result = await _matchService.CreateMatchRequestAsync(profile, story, request.SelectedRole, cancellationToken);

        return Ok(new MatchResponseDto(Success: true, Data: result, Error: null));
    }

    /// <summary>获取匹配状态</summary>
    /// <remarks>GET /api/match/status/{matchId}</remarks>
    [HttpGet("status/{matchId}")]
    public async Task<IActionResult> GetMatchStatus(string matchId, CancellationToken cancellationToken)
    {
        var result = await _matchService.GetMatchStatusAsync(matchId, cancellationToken);

        if (!result.Success)
            return Ok(new { success = false, error = result.Error });

        return Ok(new { success = true, data = result.Data });
    }

    /// <summary>取消匹配</summary>
    /// <remarks>POST /api/match/cancel?match_id=xxx</remarks>
    [HttpPost("cancel")]
    public async Task<IActionResult> CancelMatch([FromQuery(Name = "match_id")] string matchId, CancellationToken cancellationToken)
    {
        var success = await _matchService.CancelMatchAsync(matchId, cancellationToken);

        if (!success)
            return NotFound(new { success = false, error = new { code = "NOT_FOUND", message = "匹配不存在" } });

        return Ok(new { success = true, data = new { message = "匹配已取消" } });
    }

    /// <summary>获取队列状态</summary>
    /// <remarks>GET /api/match/queue/status</remarks>
    [HttpGet("queue/status")]
    public async Task<IActionResult> GetQueueStatus(CancellationToken cancellationToken)
    {
        var stats = await _matchService.GetQueueStatsAsync(cancellationToken);
        return Ok(
            new
            {
                success = true,
                data = new
                {
                    queue_status = new
                    {
                        male_waiting = stats.MaleWaiting,
                        female_waiting = stats.FemaleWaiting,
                        total_waiting = stats.TotalCount
                    },
                    timestamp = DateTimeOffset.UtcNow.ToString("o")
                }
            });
    }

    // ===== 以下是简化版 API，用于测试 =====

    /// <summary>加入匹配队列（简化版）</summary>
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

// ===== Request DTOs =====

public record MatchRequestBody(UserProfileDto Profile, StoryDto? Story, string SelectedRole);

public record UserProfileDto(string Gender, string AgeGroup, string? Height, string? Weight, IReadOnlyList<string>? Tags, string? Description);

public record StoryDto(string Title, string Background, RoleDto MaleRole, RoleDto FemaleRole);

public record RoleDto(string Name, string Description, string Personality);

public record MatchResponseDto(bool Success, MatchResult? Data, MatchError? Error);

public record EnqueueRequest(string UserId, string Gender, string GenderPreference, int Priority);

public record LeaveQueueRequest(string UserId);

public record FindMatchRequest(string UserId);