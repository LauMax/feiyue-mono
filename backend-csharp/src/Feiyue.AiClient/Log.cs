using Microsoft.Extensions.Logging;

namespace Feiyue.AiClient;

internal static partial class Log
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Generating story for users with genders {UserAGender} and {UserBGender}.")]
    internal static partial void GeneratingStory(
        this ILogger<AiServiceClient> logger,
        string userAGender,
        string userBGender);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Generating character message for {CharacterName}.")]
    internal static partial void GeneratingCharacterMessage(
        this ILogger<AiServiceClient> logger,
        string characterName);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Calculating match score.")]
    internal static partial void CalculatingMatchScore(
        this ILogger<AiServiceClient> logger);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Failed to generate story.")]
    internal static partial void FailedToGenerateStory(
        this ILogger<AiServiceClient> logger,
        Exception exception);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Failed to generate character message.")]
    internal static partial void FailedToGenerateCharacterMessage(
        this ILogger<AiServiceClient> logger,
        Exception exception);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Failed to calculate match score.")]
    internal static partial void FailedToCalculateMatchScore(
        this ILogger<AiServiceClient> logger,
        Exception exception);
}
