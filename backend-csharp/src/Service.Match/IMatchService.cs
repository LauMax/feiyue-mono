using Service.InternalContracts;

namespace Service.Match;

/// <summary>匹配服务接口</summary>
public interface IMatchService
{
    /// <summary>创建匹配请求 - Frontend 主要使用的 API</summary>
    Task<MatchResult> CreateMatchRequestAsync(MatchUserProfile profile, Story? story, string selectedRole, CancellationToken cancellationToken = default);

    /// <summary>获取匹配状态</summary>
    Task<MatchStatusResult> GetMatchStatusAsync(string matchId, CancellationToken cancellationToken = default);

    /// <summary>取消匹配</summary>
    Task<bool> CancelMatchAsync(string matchId, CancellationToken cancellationToken = default);

    /// <summary>加入匹配队列（简化版，用于测试）</summary>
    Task EnqueueMatchAsync(string userId, string gender, string genderPreference, int priority, CancellationToken cancellationToken = default);

    /// <summary>离开匹配队列</summary>
    Task LeaveQueueAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>获取队列统计</summary>
    Task<QueueStats> GetQueueStatsAsync(CancellationToken cancellationToken = default);

    /// <summary>尝试自动匹配</summary>
    Task<string?> TryMatchAsync(string userId, CancellationToken cancellationToken = default);
}