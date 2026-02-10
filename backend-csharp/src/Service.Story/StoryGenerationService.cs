using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Service.InternalContracts;

namespace Service.StoryGeneration;

/// <summary>
/// 故事生成服务 — 调用 Grok AI 生成内容，失败时降级到本地模板。
/// </summary>
internal sealed partial class StoryGenerationService : IStoryGenerationService
{
    private readonly ILogger<StoryGenerationService> _logger;
    private readonly IGrokClient _grokClient;
    private readonly ILiquidTemplateService _templateService;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public StoryGenerationService(
        ILogger<StoryGenerationService> logger,
        IGrokClient grokClient,
        ILiquidTemplateService templateService)
    {
        _logger = logger;
        _grokClient = grokClient;
        _templateService = templateService;
    }

    public async Task<Story> GenerateCompleteStoryAsync(
        StoryGenerationContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var userPrompt = await _templateService.RenderAsync("generate_complete", new
            {
                age_group = context.AgeGroup ?? "18-23",
                gender_preference = context.GenderPreference ?? "romantic meeting between strangers",
                tags = context.Tags,
                description = context.Description ?? "",
                male_role = context.MaleRole,
                female_role = context.FemaleRole,
            }, cancellationToken);

            var systemPrompt =
                """
                You are a romantic story creator. Generate a JSON response with a complete story object.
                MUST return valid JSON with this exact structure:
                {
                    "title": "story title",
                    "background": "story background description",
                    "maleRole": {
                        "name": "character name",
                        "description": "character description",
                        "personality": "personality traits"
                    },
                    "femaleRole": {
                        "name": "character name",
                        "description": "character description",
                        "personality": "personality traits"
                    }
                }
                """;

            var response = await _grokClient.ChatCompletionAsync(
                systemPrompt, userPrompt, temperature: 0.7, maxTokens: 800, cancellationToken);

            var story = ParseStoryJson(response);
            if (story is not null)
            {
                _logger.LogInformation("AI generated story: {Title}.", story.Title);
                return story;
            }

            _logger.LogWarning("Failed to parse AI story response, using fallback.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Grok API error for complete story, using fallback.");
        }

        return StoryFallbackService.GetFallbackStory(context.Tags);
    }

    public async Task<string> GenerateStorySeedAsync(
        StorySeedContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var userPrompt = await _templateService.RenderAsync("seed", new
            {
                background = context.Background,
                tags = context.Tags,
                male_role = context.MaleRole,
                female_role = context.FemaleRole,
            }, cancellationToken);

            var systemPrompt = "你是一个浪漫故事叙述者，为角色扮演场景创造引人入胜的开场叙述。根据用户的标签和描述，创造符合其偏好的故事氛围。";

            var response = await _grokClient.ChatCompletionAsync(
                systemPrompt, userPrompt, temperature: 0.7, maxTokens: 500, cancellationToken);

            if (!string.IsNullOrWhiteSpace(response))
            {
                _logger.LogInformation("AI generated story seed, length={Length}.", response.Length);
                return response;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Grok API error for story seed, using fallback.");
        }

        return StoryFallbackService.GetFallbackSeed(context.Background);
    }

    public async Task<string> GenerateStoryClueAsync(
        StoryClueContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var triggerInstruction = context.TriggerType.Equals("silence", StringComparison.OrdinalIgnoreCase)
                ? "角色沉默了一会儿。创造一个微妙的叙事时刻，轻轻引导他们重新投入对话。"
                : "对话自然发展。创造一个微妙的叙事元素，丰富故事并增添情感深度。";

            var userPrompt = await _templateService.RenderAsync("clue", new
            {
                background = context.Background,
                tags = context.Tags,
                recent_messages = context.RecentMessages,
                trigger_instruction = triggerInstruction,
            }, cancellationToken);

            var systemPrompt = "You are a romantic story narrator. Create brief, engaging narrative clues that subtly advance the romantic plot without being intrusive.";

            var response = await _grokClient.ChatCompletionAsync(
                systemPrompt, userPrompt, temperature: 0.7, maxTokens: 300, cancellationToken);

            if (!string.IsNullOrWhiteSpace(response))
            {
                _logger.LogInformation("AI generated story clue ({TriggerType}), length={Length}.",
                    context.TriggerType, response.Length);
                return response;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Grok API error for story clue, using fallback.");
        }

        return StoryFallbackService.GetFallbackClue(context.TriggerType);
    }

    public async Task<string> SuggestDialogueAsync(
        DialogueContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var userPrompt = await _templateService.RenderAsync("dialogue", new
            {
                character_role = context.CharacterRole,
                story_background = context.StoryBackground,
                tags = context.Tags,
                recent_messages = context.RecentMessages,
            }, cancellationToken);

            var systemPrompt = "You are a helpful writing assistant. Suggest natural, romantic dialogue for a character in a role-play scenario. Keep suggestions brief and authentic.";

            var response = await _grokClient.ChatCompletionAsync(
                systemPrompt, userPrompt, temperature: 0.56, maxTokens: 200, cancellationToken);

            if (!string.IsNullOrWhiteSpace(response))
            {
                _logger.LogInformation("AI generated dialogue suggestion, length={Length}.", response.Length);
                return response;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Grok API error for dialogue suggestion, using fallback.");
        }

        return StoryFallbackService.GetFallbackDialogue();
    }

    // ========== JSON 解析 ==========

    /// <summary>
    /// 解析 AI 返回的故事 JSON — 处理可能的 markdown code block 包裹
    /// </summary>
    private Story? ParseStoryJson(string response)
    {
        if (string.IsNullOrWhiteSpace(response))
            return null;

        var jsonStr = ExtractJsonFromResponse(response);

        try
        {
            var obj = JsonSerializer.Deserialize<StoryJsonDto>(jsonStr, JsonOptions);
            if (obj is null || string.IsNullOrWhiteSpace(obj.Title))
                return null;

            return new Story(
                obj.Title,
                obj.Background ?? "",
                new Role(
                    obj.MaleRole?.Name ?? "他",
                    obj.MaleRole?.Description ?? "",
                    obj.MaleRole?.Personality ?? ""),
                new Role(
                    obj.FemaleRole?.Name ?? "她",
                    obj.FemaleRole?.Description ?? "",
                    obj.FemaleRole?.Personality ?? ""));
        }
        catch (JsonException ex)
        {
            _logger.LogWarning(ex, "Failed to parse story JSON: {Preview}.",
                jsonStr[..Math.Min(jsonStr.Length, 200)]);
            return null;
        }
    }

    /// <summary>
    /// 从 AI 响应中提取 JSON 字符串 — 去除 markdown code block 包裹
    /// </summary>
    private static string ExtractJsonFromResponse(string response)
    {
        // Handle ```json ... ``` or ``` ... ```
        var match = CodeBlockRegex().Match(response);
        if (match.Success)
            return match.Groups[1].Value.Trim();

        return response.Trim();
    }

    [GeneratedRegex(@"```(?:json)?\s*([\s\S]*?)```", RegexOptions.Compiled)]
    private static partial Regex CodeBlockRegex();

    // ========== JSON DTO ==========

    private sealed class StoryJsonDto
    {
        public string? Title { get; init; }
        public string? Background { get; init; }
        public RoleJsonDto? MaleRole { get; init; }
        public RoleJsonDto? FemaleRole { get; init; }
    }

    private sealed class RoleJsonDto
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Personality { get; init; }
    }
}
