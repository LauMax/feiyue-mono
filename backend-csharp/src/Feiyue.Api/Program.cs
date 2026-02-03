using Feiyue.Match;
using Feiyue.Chat;
using Feiyue.Storage;
using Feiyue.AiClient;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// 添加服务 - 参考 Picasso 的 AppStartup 模式
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 添加 CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// 添加各个模块 - 参考 Picasso 的 AppStartup 模式
builder.Services.AddMatchServices(builder.Configuration);
builder.Services.AddChatServices(builder.Configuration);
builder.Services.AddStorageServices(builder.Configuration);
builder.Services.AddAiClient(builder.Configuration);

// 添加健康检查
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("PostgreSQL")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
