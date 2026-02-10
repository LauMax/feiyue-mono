using Service.Api.WebSockets;
using Service.Chat;
using Service.ChatStorage;
using Service.Match;
using Service.MatchStorage;
using Service.StoryGeneration;
using Service.UserStorage;

var builder = WebApplication.CreateBuilder(args);

// ====== Aspire 服务默认配置 ======
builder.AddServiceDefaults();

// 添加服务
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 添加 WebSocket 支持 - Channel 驱动架构
builder.Services.AddSingleton<RoomConnectionManager>();
builder.Services.AddSingleton<IChatSessionFactory, ChatSessionFactory>();
builder.Services.AddSingleton<ChatWebSocketHandler>();

// 添加 CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

// ====== Aspire 管理的依赖 ======
// 通过Aspire自动注入Redis和MongoDB连接
builder.AddRedisClient("redis");
builder.AddMongoDBClient("feiyue");

// 注册Storage层 - 使用Aspire注入的连接
builder.Services.AddSingleton<IUserStorage, UserStorage>();
builder.Services.AddSingleton<IMatchStorage, MatchStorage>();
builder.Services.AddSingleton<IChatStorage, ChatStorage>();

// 配置Options - 从Aspire注入的连接字符串获取
builder.Services.Configure<DatabaseOptions>(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("feiyue") ?? throw new InvalidOperationException("Database connection not configured by Aspire");
});

builder.Services.Configure<MatchStorageOptions>(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("feiyue") ?? throw new InvalidOperationException("Database connection not configured by Aspire");
});

builder.Services.Configure<ChatStorageOptions>(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("feiyue") ?? throw new InvalidOperationException("Database connection not configured by Aspire");
});

// 注册业务逻辑层
builder.Services.AddStoryServices(options =>
    builder.Configuration.GetSection("Grok").Bind(options));
builder.Services.AddMatchService();
builder.Services.AddChatService();

WebApplication app = builder.Build();

// ====== Aspire 服务默认配置 ======
app.MapDefaultEndpoints();

// 中间件顺序很重要：CORS → WebSocket → Auth → Controllers
app.UseCors();
app.UseWebSockets();
app.UseAuthorization();
app.MapControllers();

// WebSocket 端点
app.Map(
    "/ws/chat",
    async context =>
    {
        if (!context.WebSockets.IsWebSocketRequest)
        {
            context.Response.StatusCode = 400;
            return;
        }

        var roomId = context.Request.Query["roomId"].ToString();
        var userId = context.Request.Query["userId"].ToString();

        if (string.IsNullOrEmpty(roomId) || string.IsNullOrEmpty(userId))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Missing roomId or userId");
            return;
        }

        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var handler = context.RequestServices.GetRequiredService<ChatWebSocketHandler>();
        await handler.HandleWebSocketAsync(webSocket, roomId, userId, context.RequestAborted);
    });

app.Run();