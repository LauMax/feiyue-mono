using Microsoft.Extensions.DependencyInjection;

namespace Feiyue.Shared;

/// <summary>
/// 服务集合扩展 - 参考 Picasso
/// </summary>
public static class ServiceCollectionExtensions
{
    public static bool IsAlreadyAdded<T>(this IServiceCollection services)
    {
        return services.Any(descriptor => descriptor.ServiceType == typeof(T));
    }
}
