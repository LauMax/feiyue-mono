using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Service.StoryGeneration;

/// <summary>
/// Grok API 客户端 — 通过 IHttpClientFactory 命名客户端调用 xAI OpenAI 兼容 API
/// </summary>
internal sealed class GrokClient : IGrokClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly GrokOptions _options;
    private readonly ILogger<GrokClient> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public GrokClient(
        ILogger<GrokClient> logger,
        IOptions<GrokOptions> options,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _options = options.Value;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> ChatCompletionAsync(
        string systemPrompt,
        string userPrompt,
        double? temperature = null,
        int? maxTokens = null,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("GrokClient");

        var request = new ChatCompletionRequest
        {
            Model = _options.ModelName,
            Temperature = temperature ?? _options.Temperature,
            MaxTokens = maxTokens ?? _options.MaxTokens,
            Messages =
            [
                new ChatMessage { Role = "system", Content = systemPrompt },
                new ChatMessage { Role = "user", Content = userPrompt },
            ],
        };

        _logger.LogInformation("Calling Grok API model={Model}, temp={Temperature}, maxTokens={MaxTokens}.",
            request.Model, request.Temperature, request.MaxTokens);

        using var response = await client.PostAsJsonAsync(
            "chat/completions", request, JsonOptions, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Grok API error {StatusCode}: {Body}.",
                (int)response.StatusCode, errorBody[..Math.Min(errorBody.Length, 500)]);
            response.EnsureSuccessStatusCode();
        }

        var result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>(
            JsonOptions, cancellationToken);

        if (result?.Choices is not { Count: > 0 })
        {
            _logger.LogWarning("Grok API returned empty choices.");
            return string.Empty;
        }

        var content = result.Choices[0].Message?.Content ?? string.Empty;
        _logger.LogInformation("Grok API responded, length={Length}.", content.Length);
        return content;
    }

    // ---- OpenAI-compatible request/response DTOs ----

    private sealed class ChatCompletionRequest
    {
        public string Model { get; init; } = "";
        public double Temperature { get; init; }
        public int MaxTokens { get; init; }
        public List<ChatMessage> Messages { get; init; } = [];
    }

    private sealed class ChatMessage
    {
        public string Role { get; init; } = "";
        public string Content { get; init; } = "";
    }

    private sealed class ChatCompletionResponse
    {
        public List<Choice>? Choices { get; init; }
    }

    private sealed class Choice
    {
        public ResponseMessage? Message { get; init; }
    }

    private sealed class ResponseMessage
    {
        public string? Content { get; init; }
    }
}
