namespace Service.InternalContracts;

/// <summary>匹配请求的用户资料</summary>
public sealed record MatchUserProfile(
    string Gender, // "male" | "female" | "other"
    string AgeGroup, // "<18" | "18-23" | "23+"
    string? Height,
    string? Weight,
    IReadOnlyList<string> Tags,
    string? Description);

/// <summary>匹配请求</summary>
public sealed record MatchRequest(
    string Id,
    string UserId,
    string AnonymousId,
    string Gender, // 用户实际性别: "male" | "female"
    string GenderPreference, // 想找的性别: "male" | "female" | "any"
    MatchUserProfile? Profile,
    Story? Story,
    string SelectedRole, // "A" | "B"
    int Priority, // 0 = standard, 1 = VIP
    DateTimeOffset RequestedAt,
    string Status, // "waiting" | "matched" | "cancelled"
    string? RoomId,
    DateTimeOffset? MatchedAt);

/// <summary>队列优先级</summary>
public enum QueuePriority
{
    Standard = 0,
    Vip = 1,
    Super = 2
}

/// <summary>匹配队列条目</summary>
public sealed record MatchQueueEntry(
    string UserId,
    string AnonymousId,
    string Gender,
    string GenderPreference,
    MatchUserProfile? Profile,
    Story? Story,
    string SelectedRole,
    DateTimeOffset EnqueuedAt,
    int Priority);

/// <summary>队列统计信息</summary>
public sealed record QueueStats(int VipCount, int StandardCount, int TotalCount, int MaleWaiting, int FemaleWaiting);

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
    Role? YourRoleInfo,
    string? PartnerRole,
    MatchUserProfile? PartnerProfile,
    string Message,
    QueuePosition? QueuePosition);

/// <summary>队列位置信息</summary>
public sealed record QueuePosition(string Gender, int WaitingCount);

/// <summary>匹配状态响应</summary>
public sealed record MatchStatusResult(bool Success, MatchStatusData? Data, MatchError? Error);

/// <summary>匹配状态数据</summary>
public sealed record MatchStatusData(
    string UserId,
    string? AnonymousId,
    string MatchId,
    string Status,
    string? RoomId,
    Story? Story,
    string? YourRole,
    Role? YourRoleInfo,
    string? PartnerRole,
    MatchUserProfile? PartnerProfile,
    int? WaitTime,
    string Message);

/// <summary>匹配错误</summary>
public sealed record MatchError(string Code, string Message);