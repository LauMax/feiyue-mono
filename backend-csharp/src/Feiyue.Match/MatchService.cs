using Microsoft.Extensions.Logging;

namespace Feiyue.Match;

/// <summary>
/// 匹配服务实现
/// </summary>
internal sealed class MatchService : IMatchService
{
    private readonly ILogger<MatchService> _logger;
    private readonly IQueueService _queueService;

    public MatchService(
        ILogger<MatchService> logger,
        IQueueService queueService)
    {
        _logger = logger;
        _queueService = queueService;
    }

    public async Task<MatchResult> RequestMatchAsync(
        MatchRequest request,
        CancellationToken cancellationToken)
    {
        _logger.RequestingMatch(request.UserId, request.Gender);

        // TODO: 实现匹配逻辑
        await Task.Delay(100, cancellationToken);

        return new MatchResult
        {
            Success = true,
            Status = "waiting"
        };
    }

    public async Task<MatchStatus> GetMatchStatusAsync(
        string userId,
        CancellationToken cancellationToken)
    {
        _logger.CheckingMatchStatus(userId);

        // TODO: 从 Redis 查询状态
        await Task.Delay(10, cancellationToken);

        return new MatchStatus
        {
            Status = "waiting",
            Position = 1
        };
    }

    public async Task CancelMatchAsync(
        string userId,
        CancellationToken cancellationToken)
    {
        _logger.CancellingMatch(userId);

        // TODO: 从队列移除
        await Task.Delay(10, cancellationToken);
    }
}
