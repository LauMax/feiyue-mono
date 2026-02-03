namespace Feiyue.Match;

/// <summary>
/// 匹配请求 - Internal Contract (无 JSON 序列化属性)
/// </summary>
public sealed record MatchRequest(
    string UserId,
    string Gender,
    string AgeGroup,
    IReadOnlyList<string> Tags);

/// <summary>
/// 匹配结果
/// </summary>
public sealed record MatchResult(
    bool Success,
    string Status,
    string? RoomId = null,
    string? ErrorMessage = null);

/// <summary>
/// 匹配状态
/// </summary>
public sealed record MatchStatus(
    string Status,
    int Position,
    string? RoomId = null);
