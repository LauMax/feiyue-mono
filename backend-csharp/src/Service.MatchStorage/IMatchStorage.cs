using Service.InternalContracts;

namespace Service.MatchStorage;

/// <summary>Storage layer for match queue management using Redis and PostgreSQL.</summary>
public interface IMatchStorage
{
    // Queue operations
    Task EnqueueAsync(MatchQueueEntry entry, CancellationToken cancellationToken = default);
    Task<MatchQueueEntry?> DequeueAsync(QueuePriority priority, CancellationToken cancellationToken = default);
    Task DequeueAsync(string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MatchQueueEntry>> GetAllEntriesAsync(CancellationToken cancellationToken = default);
    Task<QueueStats> GetQueueStatsAsync(CancellationToken cancellationToken = default);
    Task RemoveFromQueueAsync(string userId, CancellationToken cancellationToken = default);

    // Match request persistence
    Task SaveMatchRequestAsync(MatchRequest request, CancellationToken cancellationToken = default);
    Task<MatchRequest?> GetMatchRequestAsync(string userId, CancellationToken cancellationToken = default);
    Task DeleteMatchRequestAsync(string userId, CancellationToken cancellationToken = default);
}