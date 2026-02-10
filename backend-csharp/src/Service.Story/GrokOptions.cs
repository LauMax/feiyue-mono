namespace Service.StoryGeneration;

public sealed class GrokOptions
{
    public string BaseUrl { get; set; } = "https://api.x.ai/v1";
    public string ApiKey { get; set; } = "";
    public string ModelName { get; set; } = "grok-3";
    public double Temperature { get; set; } = 0.7;
    public int MaxTokens { get; set; } = 2000;
    public int TimeoutSeconds { get; set; } = 60;
}
