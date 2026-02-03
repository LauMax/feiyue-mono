namespace Feiyue.AiClient;

public interface IAiServiceClient
{
    Task<StoryResponse> GenerateStoryAsync(
        StoryRequest request,
        CancellationToken cancellationToken);

    Task<CharacterResponse> GenerateCharacterMessageAsync(
        CharacterRequest request,
        CancellationToken cancellationToken);

    Task<MatchScoreResponse> GetMatchScoreAsync(
        MatchScoreRequest request,
        CancellationToken cancellationToken);
}
