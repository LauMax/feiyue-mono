using Microsoft.Extensions.Logging;

namespace Feiyue.Match;

/// <summary>
/// 日志定义 - 参考 Picasso 的 LoggerMessage 模式
/// </summary>
internal static partial class Log
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Requesting match for user {UserId} with gender {Gender}.")]
    internal static partial void RequestingMatch(
        this ILogger<MatchService> logger,
        string userId,
        string gender);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Checking match status for user {UserId}.")]
    internal static partial void CheckingMatchStatus(
        this ILogger<MatchService> logger,
        string userId);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Cancelling match for user {UserId}.")]
    internal static partial void CancellingMatch(
        this ILogger<MatchService> logger,
        string userId);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Failed to process match request.")]
    internal static partial void FailedToProcessMatchRequest(
        this ILogger<MatchService> logger,
        Exception exception);
}
