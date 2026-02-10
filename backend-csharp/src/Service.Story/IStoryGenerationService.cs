using Service.InternalContracts;

namespace Service.StoryGeneration;

/// <summary>
/// 故事生成服务接口 — 4 种 AI 方法 + 降级
/// </summary>
public interface IStoryGenerationService
{
    /// <summary>
    /// AI 生成完整故事 → JSON → Story 对象。失败时使用 fallback 模板。
    /// </summary>
    Task<Story> GenerateCompleteStoryAsync(StoryGenerationContext context, CancellationToken cancellationToken = default);

    /// <summary>
    /// AI 生成开场叙述（2-3句话）。失败时使用 fallback 模板。
    /// </summary>
    Task<string> GenerateStorySeedAsync(StorySeedContext context, CancellationToken cancellationToken = default);

    /// <summary>
    /// AI 生成剧情线索（≤50字，第三人称叙述）。失败时使用 fallback 模板。
    /// </summary>
    Task<string> GenerateStoryClueAsync(StoryClueContext context, CancellationToken cancellationToken = default);

    /// <summary>
    /// AI 对话建议（1句话）。失败时使用 fallback 模板。
    /// </summary>
    Task<string> SuggestDialogueAsync(DialogueContext context, CancellationToken cancellationToken = default);
}
