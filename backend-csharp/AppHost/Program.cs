using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// ====== 配置模式：本地容器 vs 远程数据库 ======
// 设置环境变量 USE_REMOTE_DB=true 可切换到远程数据库
var useRemoteDatabase = builder.Configuration.GetValue<bool>("USE_REMOTE_DB");

IResourceBuilder<IResourceWithConnectionString> feiyueDb;
IResourceBuilder<IResourceWithConnectionString> redis;

if (useRemoteDatabase)
{
    // 远程模式：连接线上数据库（需要在 appsettings.json 中配置连接字符串）
    // 使用方式: USE_REMOTE_DB=true aspire run --project AppHost/Feiyue.AppHost.csproj --no-build
    var mongoConnectionString = builder.Configuration.GetConnectionString("feiyue") 
        ?? throw new InvalidOperationException("远程模式需要配置 ConnectionStrings:feiyue");
    var redisConnectionString = builder.Configuration.GetConnectionString("redis")
        ?? throw new InvalidOperationException("远程模式需要配置 ConnectionStrings:redis");
    feiyueDb = builder.AddConnectionString("feiyue");
    redis = builder.AddConnectionString("redis");
    Console.WriteLine("🌐 使用远程数据库模式");
    Console.WriteLine($"   MongoDB: {mongoConnectionString.Substring(0, Math.Min(50, mongoConnectionString.Length))}...");
    Console.WriteLine($"   Redis: {redisConnectionString.Substring(0, Math.Min(50, redisConnectionString.Length))}...");
}
else
{
    // 本地模式：使用 Docker 容器（默认，开发推荐）
    // 开发环境不持久化数据，每次重启都是干净环境
    var mongo = builder.AddMongoDB("mongodb");
    feiyueDb = mongo.AddDatabase("feiyue");
    redis = builder.AddRedis("redis");
    Console.WriteLine("🐳 使用本地容器模式（默认）");
    Console.WriteLine("   MongoDB: 本地容器（不持久化）");
    Console.WriteLine("   Redis: 本地容器（不持久化）");
}

// ====== 应用服务 ======

// API 服务 - 当前是单体，后续可以拆分为微服务
var apiService = builder.AddProject("api", @"../src/Service.Api/Service.Api.csproj")
    .WithReference(feiyueDb)
    .WithReference(redis);

builder.Build().Run();
