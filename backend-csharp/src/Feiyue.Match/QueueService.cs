namespace Feiyue.Match;

public interface IQueueService
{
    Task EnqueueAsync(string userId, string gender, CancellationToken cancellationToken);
    Task<string?> DequeueAsync(string gender, CancellationToken cancellationToken);
    Task RemoveAsync(string userId, CancellationToken cancellationToken);
}

internal sealed class QueueService : IQueueService
{
    public Task EnqueueAsync(string userId, string gender, CancellationToken cancellationToken)
    {
        // TODO: Redis 队列实现
        return Task.CompletedTask;
    }

    public Task<string?> DequeueAsync(string gender, CancellationToken cancellationToken)
    {
        // TODO: Redis 队列实现
        return Task.FromResult<string?>(null);
    }

    public Task RemoveAsync(string userId, CancellationToken cancellationToken)
    {
        // TODO: Redis 队列实现
        return Task.CompletedTask;
    }
}
