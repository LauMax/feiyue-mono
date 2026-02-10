using Microsoft.Extensions.DependencyInjection;

namespace Service.StoryGeneration;

/// <summary>
/// Service.Story 的 DI 注册入口
/// </summary>
public static class AppStartup
{
    public static IServiceCollection AddStoryServices(
        this IServiceCollection services,
        Action<GrokOptions>? configureGrok = null)
    {
        // Grok 配置
        if (configureGrok is not null)
        {
            services.Configure(configureGrok);
        }

        // Grok HTTP 命名客户端
        services.AddHttpClient("GrokClient", (sp, client) =>
        {
            var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<GrokOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.ApiKey}");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
        });

        // 服务注册
        services.AddSingleton<IGrokClient, GrokClient>();
        services.AddSingleton<ILiquidTemplateService, LiquidTemplateService>();
        services.AddSingleton<IStoryGenerationService, StoryGenerationService>();

        return services;
    }
}
