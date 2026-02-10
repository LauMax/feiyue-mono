namespace Service.StoryGeneration;

/// <summary>
/// Grok API 客户端接口 — OpenAI 兼容格式
/// </summary>
public interface IGrokClient
{
    /// <summary>
    /// 调用 Grok Chat Completions API
    /// </summary>
    Task<string> ChatCompletionAsync(
        string systemPrompt,
        string userPrompt,
        double? temperature = null,
        int? maxTokens = null,
        CancellationToken cancellationToken = default);
}
