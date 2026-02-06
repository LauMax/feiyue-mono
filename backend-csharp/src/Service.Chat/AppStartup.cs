using Microsoft.Extensions.DependencyInjection;

namespace Service.Chat;

public static class AppStartup
{
    public static IServiceCollection AddChatService(this IServiceCollection services)
    {
        services.AddSingleton<IChatService, ChatService>();
        return services;
    }
}