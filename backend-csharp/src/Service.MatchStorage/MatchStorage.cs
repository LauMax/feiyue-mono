using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Service.InternalContracts;
using StackExchange.Redis;

namespace Service.MatchStorage;

public sealed class MatchStorage : IMatchStorage
{
    private readonly ILogger<MatchStorage> _logger;
    private readonly IConnectionMultiplexer _redis;
    private readonly IMongoCollection<MatchRequest> _matchRequests;

    private const string VipQueueKey = "match:queue:vip";
    private const string StandardQueueKey = "match:queue:standard";
    private const string UserQueueKey = "match:user:{0}";

    public MatchStorage(ILogger<MatchStorage> logger, IConnectionMultiplexer redis, IOptions<MatchStorageOptions> options)
    {
        _logger = logger;
        _redis = redis;
        var mongoUrl = new MongoUrl(options.Value.ConnectionString);
        var client = new MongoClient(mongoUrl);
        var db = client.GetDatabase(mongoUrl.DatabaseName ?? "feiyue");
        _matchRequests = db.GetCollection<MatchRequest>("match_requests");
    }

    public async Task EnqueueAsync(MatchQueueEntry entry, CancellationToken cancellationToken = default)
    {
        var db = _redis.GetDatabase();
        var json = JsonSerializer.Serialize(entry);

        if (entry.Priority == (int)QueuePriority.Vip)
        {
            // VIP queue: Sorted Set with score = timestamp
            var score = entry.EnqueuedAt.ToUnixTimeMilliseconds();
            await db.SortedSetAddAsync(VipQueueKey, json, score);
        }
        else
        {
            // Standard queue: List (FIFO)
            await db.ListRightPushAsync(StandardQueueKey, json);
        }

        // Track user in queue
        var userKey = string.Format(UserQueueKey, entry.UserId);
        await db.StringSetAsync(userKey, entry.Priority.ToString(), TimeSpan.FromHours(24));

        _logger.LogInformation("User {UserId} enqueued with priority {Priority}.", entry.UserId, entry.Priority);
    }

    public async Task<MatchQueueEntry?> DequeueAsync(QueuePriority priority, CancellationToken cancellationToken = default)
    {
        var db = _redis.GetDatabase();

        if (priority == QueuePriority.Vip)
        {
            // Get oldest VIP user (lowest score)
            var values = await db.SortedSetRangeByScoreAsync(VipQueueKey, take: 1);
            if (values.Length == 0)
                return null;

            var json = values[0].ToString();
            var entry = JsonSerializer.Deserialize<MatchQueueEntry>(json);
            if (entry is null)
                return null;

            // Remove from queue
            await db.SortedSetRemoveAsync(VipQueueKey, json);

            // Remove user tracking
            var userKey = string.Format(UserQueueKey, entry.UserId);
            await db.KeyDeleteAsync(userKey);

            return entry;
        }
        else
        {
            // Get oldest standard user (FIFO)
            var json = await db.ListLeftPopAsync(StandardQueueKey);
            if (json.IsNullOrEmpty)
                return null;

            var entry = JsonSerializer.Deserialize<MatchQueueEntry>(json.ToString());
            if (entry is null)
                return null;

            // Remove user tracking
            var userKey = string.Format(UserQueueKey, entry.UserId);
            await db.KeyDeleteAsync(userKey);

            return entry;
        }
    }

    public async Task<QueueStats> GetQueueStatsAsync(CancellationToken cancellationToken = default)
    {
        var db = _redis.GetDatabase();

        var vipCount = await db.SortedSetLengthAsync(VipQueueKey);
        var standardCount = await db.ListLengthAsync(StandardQueueKey);

        return new QueueStats(VipCount: (int)vipCount, StandardCount: (int)standardCount, TotalCount: (int)(vipCount + standardCount));
    }

    public async Task RemoveFromQueueAsync(string userId, CancellationToken cancellationToken = default)
    {
        var db = _redis.GetDatabase();
        var userKey = string.Format(UserQueueKey, userId);

        // Check which queue the user is in
        var priorityStr = await db.StringGetAsync(userKey);
        if (priorityStr.IsNullOrEmpty)
            return;

        if (Enum.TryParse<QueuePriority>(priorityStr.ToString(), out var priority))
        {
            if (priority == QueuePriority.Vip)
            {
                // Remove from VIP sorted set (need to scan)
                var allVips = await db.SortedSetRangeByScoreAsync(VipQueueKey);
                foreach (var item in allVips)
                {
                    var entry = JsonSerializer.Deserialize<MatchQueueEntry>(item.ToString());
                    if (entry?.UserId == userId)
                    {
                        await db.SortedSetRemoveAsync(VipQueueKey, item);
                        break;
                    }
                }
            }
            else
            {
                // Remove from standard list (need to scan)
                var allStandard = await db.ListRangeAsync(StandardQueueKey);
                foreach (var item in allStandard)
                {
                    var entry = JsonSerializer.Deserialize<MatchQueueEntry>(item.ToString());
                    if (entry?.UserId == userId)
                    {
                        await db.ListRemoveAsync(StandardQueueKey, item);
                        break;
                    }
                }
            }

            await db.KeyDeleteAsync(userKey);
        }
    }

    public async Task SaveMatchRequestAsync(MatchRequest request, CancellationToken cancellationToken = default)
    {
        await _matchRequests.ReplaceOneAsync(x => x.UserId == request.UserId, request, new ReplaceOptions { IsUpsert = true }, cancellationToken);
    }

    public async Task<MatchRequest?> GetMatchRequestAsync(string userId, CancellationToken cancellationToken = default)
    {
        var request = await _matchRequests.Find(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        return request;
    }

    public async Task DeleteMatchRequestAsync(string userId, CancellationToken cancellationToken = default)
    {
        await _matchRequests.DeleteOneAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task DequeueAsync(string userId, CancellationToken cancellationToken = default)
    {
        var db = _redis.GetDatabase();

        // Check which queue the user is in
        var userKey = string.Format(UserQueueKey, userId);
        var queueType = await db.StringGetAsync(userKey);

        if (!queueType.IsNullOrEmpty)
        {
            // Priority is stored as number: "1" = VIP, "0" = Standard
            if (queueType == "1" || queueType == ((int)QueuePriority.Vip).ToString())
            {
                // Remove from VIP queue - need to find and remove the entry
                var allEntries = await db.SortedSetRangeByScoreAsync(VipQueueKey);
                foreach (var entry in allEntries)
                {
                    var queueEntry = JsonSerializer.Deserialize<MatchQueueEntry>(entry.ToString());
                    if (queueEntry?.UserId == userId)
                    {
                        await db.SortedSetRemoveAsync(VipQueueKey, entry);
                        break;
                    }
                }
            }
            else
            {
                // Remove from standard queue - need to find and remove the entry
                var allEntries = await db.ListRangeAsync(StandardQueueKey);
                foreach (var entry in allEntries)
                {
                    var queueEntry = JsonSerializer.Deserialize<MatchQueueEntry>(entry.ToString());
                    if (queueEntry?.UserId == userId)
                    {
                        await db.ListRemoveAsync(StandardQueueKey, entry);
                        break;
                    }
                }
            }

            await db.KeyDeleteAsync(userKey);
        }

        // Also remove from PostgreSQL
        await DeleteMatchRequestAsync(userId, cancellationToken);
    }

    public async Task<IReadOnlyList<MatchQueueEntry>> GetAllEntriesAsync(CancellationToken cancellationToken = default)
    {
        var db = _redis.GetDatabase();
        var entries = new List<MatchQueueEntry>();

        // Get VIP entries
        var vipEntries = await db.SortedSetRangeByScoreAsync(VipQueueKey);
        foreach (var entry in vipEntries)
        {
            var queueEntry = JsonSerializer.Deserialize<MatchQueueEntry>(entry.ToString());
            if (queueEntry is not null)
                entries.Add(queueEntry);
        }

        // Get standard entries
        var standardEntries = await db.ListRangeAsync(StandardQueueKey);
        foreach (var entry in standardEntries)
        {
            var queueEntry = JsonSerializer.Deserialize<MatchQueueEntry>(entry.ToString());
            if (queueEntry is not null)
                entries.Add(queueEntry);
        }

        return entries;
    }
}

/// <summary>Match storage options</summary>
public sealed class MatchStorageOptions
{
    public required string ConnectionString { get; set; }
}