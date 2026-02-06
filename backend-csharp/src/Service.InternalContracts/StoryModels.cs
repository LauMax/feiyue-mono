namespace Service.InternalContracts;

/// <summary>角色信息</summary>
public sealed record Role(string Name, string Description, string Personality);

/// <summary>故事</summary>
public sealed record Story(string Title, string Background, Role MaleRole, Role FemaleRole);