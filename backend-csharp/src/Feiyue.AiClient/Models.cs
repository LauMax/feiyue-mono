namespace Feiyue.AiClient;

// Internal Models (不用于 HTTP 序列化)
public sealed record StoryRequest(
    string UserAGender,
    string UserBGender,
    IReadOnlyList<string> UserATags,
    IReadOnlyList<string> UserBTags);

public sealed record StoryResponse(
    string Title,
    string Background,
    string MaleRoleName,
    string MaleRoleDescription,
    string FemaleRoleName,
    string FemaleRoleDescription);

public sealed record CharacterRequest(
    string CharacterName,
    string Personality,
    string Context);

public sealed record CharacterResponse(
    string Message);

public sealed record MatchScoreRequest(
    string UserAGender,
    string UserBGender,
    IReadOnlyList<string> UserATags,
    IReadOnlyList<string> UserBTags);

public sealed record MatchScoreResponse(
    double Score,
    string Reasoning);
