using Service.InternalContracts;

namespace Service.Match;

/// <summary>匹配服务接口</summary>
public interface IMatchService
{
    /// <summary>加入匹配队列</summary>
    Task EnqueueMatchAsync(string userId, string gender, string genderPreference, int priority, CancellationToken cancellationToken = default);

    /// <summary>离开匹配队列</summary>
    Task LeaveQueueAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>获取队列统计</summary>
    Task<QueueStats> GetQueueStatsAsync(CancellationToken cancellationToken = default);

    /// <summary>尝试自动匹配</summary>
    Task<string?> TryMatchAsync(string userId, CancellationToken cancellationToken = default);
}