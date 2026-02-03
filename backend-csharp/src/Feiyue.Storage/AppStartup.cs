using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Feiyue.Storage;

public static class AppStartup
{
    private sealed class Once;

    public static IServiceCollection AddStorageServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services.IsAlreadyAdded<Once>())
        {
            return services;
        }

        // TODO: 添加 EF Core DbContext

        return services;
    }
}
