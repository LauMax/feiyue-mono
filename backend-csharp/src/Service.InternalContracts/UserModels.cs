namespace Service.InternalContracts;

/// <summary>用户资料</summary>
public sealed record UserProfile(
    string UserId,
    string Gender,
    int Age,
    IReadOnlyList<string> Interests,
    IReadOnlyList<Story> Stories,
    bool IsVip,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);

/// <summary>用户</summary>
public sealed record User(string Id, string AnonymousId, DateTimeOffset CreatedAt);