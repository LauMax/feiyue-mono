using System.Text.Json.Serialization;

namespace Feiyue.Api.External;

/// <summary>
/// External 模型 - 用于 HTTP JSON 序列化
/// 参考 Picasso 的 External 命名空间模式
/// </summary>

public sealed class MatchRequest
{
    [JsonPropertyName("userId")]
    public required string UserId { get; init; }

    [JsonPropertyName("gender")]
    public required string Gender { get; init; }

    [JsonPropertyName("ageGroup")]
    public required string AgeGroup { get; init; }

    [JsonPropertyName("tags")]
    public required string[] Tags { get; init; }
}

public sealed class MatchResponse
{
    [JsonPropertyName("success")]
    public required bool Success { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("roomId")]
    public string? RoomId { get; init; }

    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; init; }

    [JsonPropertyName("story")]
    public StoryData? Story { get; init; }
}

public sealed class StoryData
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("background")]
    public required string Background { get; init; }

    [JsonPropertyName("maleRole")]
    public required RoleData MaleRole { get; init; }

    [JsonPropertyName("femaleRole")]
    public required RoleData FemaleRole { get; init; }
}

public sealed class RoleData
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("personality")]
    public required string Personality { get; init; }
}

public sealed class MatchStatusResponse
{
    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("position")]
    public required int Position { get; init; }

    [JsonPropertyName("roomId")]
    public string? RoomId { get; init; }
}
