using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Feiyue.AiClient;

/// <summary>
/// AI 服务客户端 - 参考 Picasso 的 HarmonyInference.cs
/// </summary>
internal sealed class AiServiceClient : IAiServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AiServiceClient> _logger;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public AiServiceClient(
        HttpClient httpClient,
        ILogger<AiServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<StoryResponse> GenerateStoryAsync(
        StoryRequest request,
        CancellationToken cancellationToken)
    {
        _logger.GeneratingStory(request.UserAGender, request.UserBGender);

        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                "/api/story/generate",
                request,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            StoryResponse? result = await response.Content
                .ReadFromJsonAsync<StoryResponse>(JsonOptions, cancellationToken);

            return result ?? throw new InvalidOperationException("Empty response from AI service");
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.FailedToGenerateStory(ex);
            throw;
        }
    }

    public async Task<CharacterResponse> GenerateCharacterMessageAsync(
        CharacterRequest request,
        CancellationToken cancellationToken)
    {
        _logger.GeneratingCharacterMessage(request.CharacterName);

        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                "/api/character/generate",
                request,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            CharacterResponse? result = await response.Content
                .ReadFromJsonAsync<CharacterResponse>(JsonOptions, cancellationToken);

            return result ?? throw new InvalidOperationException("Empty response from AI service");
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.FailedToGenerateCharacterMessage(ex);
            throw;
        }
    }

    public async Task<MatchScoreResponse> GetMatchScoreAsync(
        MatchScoreRequest request,
        CancellationToken cancellationToken)
    {
        _logger.CalculatingMatchScore();

        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                "/api/match/score",
                request,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            MatchScoreResponse? result = await response.Content
                .ReadFromJsonAsync<MatchScoreResponse>(JsonOptions, cancellationToken);

            return result ?? throw new InvalidOperationException("Empty response from AI service");
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.FailedToCalculateMatchScore(ex);
            throw;
        }
    }
}
