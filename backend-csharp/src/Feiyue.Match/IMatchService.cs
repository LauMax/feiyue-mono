namespace Feiyue.Match;

/// <summary>
/// 匹配服务接口
/// </summary>
public interface IMatchService
{
    Task<MatchResult> RequestMatchAsync(MatchRequest request, CancellationToken cancellationToken);
    Task<MatchStatus> GetMatchStatusAsync(string userId, CancellationToken cancellationToken);
    Task CancelMatchAsync(string userId, CancellationToken cancellationToken);
}
