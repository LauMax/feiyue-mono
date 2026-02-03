using Microsoft.AspNetCore.Mvc;

namespace Feiyue.Api.Controllers;

/// <summary>
/// 匹配系统控制器 - 参考 Picasso Controller 模式
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class MatchController : ControllerBase
{
    private readonly ILogger<MatchController> _logger;
    private readonly Match.IMatchService _matchService;

    public MatchController(
        ILogger<MatchController> logger,
        Match.IMatchService matchService)
    {
        _logger = logger;
        _matchService = matchService;
    }

    /// <summary>
    /// 请求匹配
    /// </summary>
    [HttpPost("request")]
    [ProducesResponseType(typeof(External.MatchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestMatch(
        [FromBody] External.MatchRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Match request received for user {UserId}", request.UserId);

        try
        {
            // ToInternal 转换 - 参考 Picasso 模式
            Match.MatchRequest internalRequest = request.ToInternal();

            // 调用业务逻辑
            Match.MatchResult result = await _matchService.RequestMatchAsync(
                internalRequest, 
                cancellationToken);

            // ToExternal 转换
            return Ok(result.ToExternal());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process match request");
            return StatusCode(500, new External.MatchResponse
            {
                Success = false,
                ErrorMessage = "Internal server error"
            });
        }
    }

    /// <summary>
    /// 查询匹配状态
    /// </summary>
    [HttpGet("status/{userId}")]
    [ProducesResponseType(typeof(External.MatchStatusResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMatchStatus(
        string userId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Checking match status for user {UserId}", userId);

        try
        {
            Match.MatchStatus status = await _matchService.GetMatchStatusAsync(
                userId, 
                cancellationToken);

            return Ok(status.ToExternal());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get match status");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// 取消匹配
    /// </summary>
    [HttpDelete("cancel/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CancelMatch(
        string userId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cancelling match for user {UserId}", userId);

        try
        {
            await _matchService.CancelMatchAsync(userId, cancellationToken);
            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to cancel match");
            return StatusCode(500);
        }
    }
}
