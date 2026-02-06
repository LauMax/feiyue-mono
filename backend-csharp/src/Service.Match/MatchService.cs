using Microsoft.Extensions.Logging;
using Service.InternalContracts;
using Service.MatchStorage;

namespace Service.Match;

/// <summary>匹配服务实现 - 参考Picasso模式</summary>
internal sealed class MatchService : IMatchService
{
    private readonly ILogger<MatchService> _logger;
    private readonly IMatchStorage _matchStorage;

    public MatchService(ILogger<MatchService> logger, IMatchStorage matchStorage)
    {
        _logger = logger;
        _matchStorage = matchStorage;
    }

    public async Task EnqueueMatchAsync(string userId, string gender, string genderPreference, int priority, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("User {UserId} (gender: {Gender}) entering match queue with preference {Preference}", userId, gender, genderPreference);

        var now = DateTimeOffset.UtcNow;
        var entry = new MatchQueueEntry(UserId: userId, Gender: gender, GenderPreference: genderPreference, EnqueuedAt: now, Priority: priority);

        // Save to Redis queue
        await _matchStorage.EnqueueAsync(entry, cancellationToken);

        // Save to MongoDB for persistence
        var request = new MatchRequest(
            Id: Guid.NewGuid().ToString(),
            UserId: userId,
            Gender: gender,
            GenderPreference: genderPreference,
            Priority: priority,
            RequestedAt: now,
            Status: "waiting"
        );
        await _matchStorage.SaveMatchRequestAsync(request, cancellationToken);
    }

    public async Task LeaveQueueAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("User {UserId} leaving match queue", userId);
        await _matchStorage.DequeueAsync(userId, cancellationToken);
    }

    public async Task<QueueStats> GetQueueStatsAsync(CancellationToken cancellationToken = default)
    {
        return await _matchStorage.GetQueueStatsAsync(cancellationToken);
    }

    public async Task<string?> TryMatchAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to match user {UserId}", userId);

        // 获取当前用户的请求
        var request = await _matchStorage.GetMatchRequestAsync(userId, cancellationToken);
        if (request is null)
        {
            _logger.LogWarning("User {UserId} not in queue", userId);
            return null;
        }

        // 简单匹配逻辑：找到第一个兼容的用户
        var allEntries = await _matchStorage.GetAllEntriesAsync(cancellationToken);
        var compatibleEntry = allEntries.FirstOrDefault(e => e.UserId != userId && IsCompatible(request.Gender, request.GenderPreference, e.Gender, e.GenderPreference));

        if (compatibleEntry is null)
        {
            _logger.LogInformation("No compatible match found for user {UserId}", userId);
            return null;
        }

        // 从队列中移除两个用户
        await _matchStorage.DequeueAsync(userId, cancellationToken);
        await _matchStorage.DequeueAsync(compatibleEntry.UserId, cancellationToken);

        _logger.LogInformation("Matched users {UserId1} and {UserId2}", userId, compatibleEntry.UserId);

        // 返回匹配的用户ID
        return compatibleEntry.UserId;
    }

    private static bool IsCompatible(string gender1, string pref1, string gender2, string pref2)
    {
        // User1 的性别偏好检查：User1 想找的性别是否匹配 User2 的实际性别
        var user1Compatible = pref1 == "any" || pref1 == gender2;
        
        // User2 的性别偏好检查：User2 想找的性别是否匹配 User1 的实际性别
        var user2Compatible = pref2 == "any" || pref2 == gender1;
        
        // 双方都满足才能匹配
        return user1Compatible && user2Compatible;
    }
}