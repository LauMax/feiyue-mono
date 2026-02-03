using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Feiyue.AiClient;

public static class AppStartup
{
    private sealed class Once;

    public static IServiceCollection AddAiClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services.IsAlreadyAdded<Once>())
        {
            return services;
        }

        // 配置 HTTP Client - 参考 Picasso 的 HarmonyInference 模式
        services.AddHttpClient<IAiServiceClient, AiServiceClient>(client =>
        {
            string baseUrl = configuration["AiService:BaseUrl"]
                ?? throw new InvalidOperationException("AiService:BaseUrl not configured");
            
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(
                configuration.GetValue<int>("AiService:TimeoutSeconds", 60));
        })
        .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(5),
            MaxConnectionsPerServer = 50,
        })
        .AddPolicyHandler(GetRetryPolicy(configuration))
        .AddPolicyHandler(GetCircuitBreakerPolicy());

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IConfiguration configuration)
    {
        int maxRetries = configuration.GetValue<int>("AiService:MaxRetries", 3);
        
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                maxRetries,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}
