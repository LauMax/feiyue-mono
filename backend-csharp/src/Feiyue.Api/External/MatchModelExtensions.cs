namespace Feiyue.Api.External;

/// <summary>
/// ToInternal/ToExternal 转换扩展 - 参考 Picasso 模式
/// 专门用于 External 和 Internal 模型之间的转换
/// </summary>
internal static class MatchModelExtensions
{
    // External → Internal
    internal static Match.MatchRequest ToInternal(this MatchRequest external)
    {
        return new Match.MatchRequest(
            UserId: external.UserId,
            Gender: external.Gender,
            AgeGroup: external.AgeGroup,
            Tags: external.Tags);
    }

    // Internal → External
    internal static MatchResponse ToExternal(this Match.MatchResult internal)
    {
        return new MatchResponse
        {
            Success = internal.Success,
            Status = internal.Status,
            RoomId = internal.RoomId,
            ErrorMessage = internal.ErrorMessage
        };
    }

    internal static MatchStatusResponse ToExternal(this Match.MatchStatus internal)
    {
        return new MatchStatusResponse
        {
            Status = internal.Status,
            Position = internal.Position,
            RoomId = internal.RoomId
        };
    }
}
