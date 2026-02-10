namespace Service.InternalContracts;

/// <summary>角色信息</summary>
public sealed record Role(string Name, string Description, string Personality);

/// <summary>故事</summary>
public sealed record Story(string Title, string Background, Role MaleRole, Role FemaleRole);

/// <summary>故事生成上下文</summary>
public sealed record StoryGenerationContext(
    string? AgeGroup,
    string? GenderPreference,
    IReadOnlyList<string> Tags,
    string? Description,
    Role? MaleRole,
    Role? FemaleRole);

/// <summary>开场叙述上下文</summary>
public sealed record StorySeedContext(
    string Background,
    IReadOnlyList<string> Tags,
    Role? MaleRole,
    Role? FemaleRole);

/// <summary>剧情线索上下文</summary>
public sealed record StoryClueContext(
    string Background,
    IReadOnlyList<string> Tags,
    IReadOnlyList<ConversationMessage> RecentMessages,
    string TriggerType); // "conversation" | "silence"

/// <summary>对话建议上下文</summary>
public sealed record DialogueContext(
    string CharacterRole,
    string StoryBackground,
    IReadOnlyList<string> Tags,
    IReadOnlyList<ConversationMessage> RecentMessages);

/// <summary>对话消息（用于构建 prompt 上下文）</summary>
public sealed record ConversationMessage(string SenderRole, string Content);