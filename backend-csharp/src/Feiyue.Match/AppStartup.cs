using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Feiyue.Match;

/// <summary>
/// AppStartup 模式 - 参考 Picasso
/// </summary>
public static class AppStartup
{
    private sealed class Once;

    public static IServiceCollection AddMatchServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 防止重复添加 - 参考 Picasso 模式
        if (services.IsAlreadyAdded<Once>())
        {
            return services;
        }

        services.AddSingleton<IMatchService, MatchService>();
        services.AddSingleton<IQueueService, QueueService>();

        return services;
    }
}
