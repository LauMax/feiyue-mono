using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Feiyue.Chat;

public static class AppStartup
{
    private sealed class Once;

    public static IServiceCollection AddChatServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services.IsAlreadyAdded<Once>())
        {
            return services;
        }

        // TODO: 添加聊天服务

        return services;
    }
}
