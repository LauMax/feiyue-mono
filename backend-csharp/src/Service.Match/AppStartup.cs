using Microsoft.Extensions.DependencyInjection;

namespace Service.Match;

public static class AppStartup
{
    public static IServiceCollection AddMatchService(this IServiceCollection services)
    {
        services.AddSingleton<IMatchService, MatchService>();
        return services;
    }
}