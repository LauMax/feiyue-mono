namespace Service.InternalContracts;

/// <summary>匹配请求</summary>
public sealed record MatchRequest(
    string Id,
    string UserId,
    string Gender, // 用户实际性别: "male" | "female"
    string GenderPreference, // 想找的性别: "male" | "female" | "any"
    int Priority, // 0 = standard, 1 = VIP
    DateTimeOffset RequestedAt,
    string Status // "waiting" | "matched" | "cancelled"
);

/// <summary>队列优先级</summary>
public enum QueuePriority
{
    Standard = 0,
    Vip = 1,
    Super = 2
}

/// <summary>匹配队列条目</summary>
public sealed record MatchQueueEntry(string UserId, string Gender, string GenderPreference, DateTimeOffset EnqueuedAt, int Priority);

/// <summary>队列统计信息</summary>
public sealed record QueueStats(int VipCount, int StandardCount, int TotalCount);

/// <summary>匹配结果</summary>
public sealed record MatchResult(
    bool Success,
    string Status,
    string UserId,
    string AnonymousId,
    string MatchId,
    string? RoomId,
    Story? Story,
    string? YourRole,
    UserProfile? PartnerProfile,
    string Message);