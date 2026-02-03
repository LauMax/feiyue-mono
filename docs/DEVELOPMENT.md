# 开发指南

## 🎯 开发原则

本项目严格遵循 **Picasso + Studio** 的代码风格和架构模式。

## 📋 开发环境要求

### 必需工具
- Node.js 20+
- .NET SDK 8.0+
- Python 3.11+
- Docker + Docker Compose
- Git

### 推荐工具
- Visual Studio Code
- Visual Studio 2022 / Rider (C# 开发)
- PyCharm / VS Code (Python 开发)

## 🏗️ 项目结构

```
feiyue-mono/
├── frontend/                      # React 前端
│   ├── src/
│   │   ├── main.tsx              # 入口文件
│   │   ├── routeTree.gen.ts      # Tanstack Router 路由
│   │   ├── services/             # API 客户端
│   │   ├── pages/                # 页面组件
│   │   ├── components/           # UI 组件
│   │   └── atoms/                # Jotai 状态
│   └── package.json
│
├── backend-csharp/               # C# 后端
│   ├── Directory.Build.props     # 全局配置
│   └── src/
│       ├── Feiyue.Api/           # API 层
│       ├── Feiyue.Match/         # 匹配服务
│       ├── Feiyue.Chat/          # 聊天服务
│       ├── Feiyue.Storage/       # 数据访问
│       ├── Feiyue.AiClient/      # AI 客户端
│       ├── Feiyue.Shared/        # 共享库
│       └── Feiyue.InternalContracts/
│
├── ai-service/                   # Python AI
│   └── src/
│       ├── main.py               # FastAPI 入口
│       ├── api/                  # API 路由
│       ├── services/             # 业务逻辑
│       └── ml/                   # ML 模型
│
└── docs/                         # 文档
```

## 🎨 代码风格指南

### C# 代码风格（参考 Picasso）

#### 1. 项目结构模式

每个服务模块必须包含：
```csharp
// AppStartup.cs - 使用 Once 模式
public static class AppStartup
{
    private sealed class Once;
    
    public static IServiceCollection AddXxxServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services.IsAlreadyAdded<Once>())
        {
            return services;
        }
        
        // 注册服务
        services.AddSingleton<IXxxService, XxxService>();
        
        return services;
    }
}
```

#### 2. 日志使用 LoggerMessage 源生成器

```csharp
// Log.cs
internal static partial class Log
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Processing request for user {UserId}.")]
    internal static partial void ProcessingRequest(
        this ILogger<MyService> logger,
        string userId);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Failed to process request.")]
    internal static partial void FailedToProcessRequest(
        this ILogger<MyService> logger,
        Exception exception);
}

// 使用
_logger.ProcessingRequest(userId);
_logger.FailedToProcessRequest(ex);
```

#### 3. 数据模型

**Internal Contracts (无序列化属性)：**
```csharp
// Feiyue.InternalContracts/
public sealed record MatchRequest(
    string UserId,
    string Gender,
    IReadOnlyList<string> Tags);
```

**External Contracts (用于 HTTP/JSON)：**
```csharp
// 在 Controller 所在项目的 .External 命名空间
namespace Feiyue.Api.External;

public sealed class MatchRequestDto
{
    [JsonPropertyName("userId")]
    public required string UserId { get; init; }
    
    [JsonPropertyName("gender")]
    public required string Gender { get; init; }
}
```

#### 4. IHttpClientFactory 模式

```csharp
// AppStartup.cs
services.AddHttpClient<IAiServiceClient, AiServiceClient>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
    client.Timeout = TimeSpan.FromSeconds(60);
})
.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
{
    PooledConnectionLifetime = TimeSpan.FromMinutes(5),
    MaxConnectionsPerServer = 50,
})
.AddPolicyHandler(GetRetryPolicy())
.AddPolicyHandler(GetCircuitBreakerPolicy());
```

#### 5. 构造函数参数顺序

```csharp
public MyService(
    ILogger<MyService> logger,           // 1. Logger
    IOptions<MyOptions> options,         // 2. Options
    IFeatures features,                  // 3. Features (如果有)
    // Metrics (如果有)
    IOtherService otherService)          // 4. 其他依赖
{
}
```

### 前端代码风格（参考 Studio）

#### 1. Tanstack Router 路由

```typescript
// routeTree.gen.ts
import { createRootRoute, createRoute, Outlet } from '@tanstack/react-router'

const rootRoute = createRootRoute({
  component: () => <Outlet />,
})

const indexRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/',
  component: HomePage,
})

export const routeTree = rootRoute.addChildren([indexRoute])
```

#### 2. API 客户端

```typescript
// services/xxxApi.ts
import { fetchApi } from './config'

export interface XxxRequest {
  userId: string
  // ...
}

export interface XxxResponse {
  success: boolean
  data?: any
}

export const xxxApi = {
  async doSomething(request: XxxRequest): Promise<XxxResponse> {
    return fetchApi('/api/xxx/something', {
      method: 'POST',
      body: JSON.stringify(request),
    })
  },
}
```

#### 3. WebSocket 客户端

```typescript
// services/chatClient.ts
export class ChatClient {
  private ws: WebSocket | null = null
  private reconnectAttempts = 0
  private maxReconnectAttempts = 5
  
  constructor(private roomId: string) {}
  
  connect(): void {
    const wsUrl = `${config.wsBaseUrl}/ws/chat/${this.roomId}`
    this.ws = new WebSocket(wsUrl)
    
    this.ws.onopen = () => {
      this.reconnectAttempts = 0
    }
    
    this.ws.onclose = () => {
      this.attemptReconnect()
    }
  }
  
  private attemptReconnect(): void {
    if (this.reconnectAttempts < this.maxReconnectAttempts) {
      this.reconnectAttempts++
      setTimeout(() => this.connect(), 1000 * this.reconnectAttempts)
    }
  }
}
```

#### 4. React Query 使用

```typescript
import { useQuery, useMutation } from '@tanstack/react-query'
import { matchApi } from '@/services/matchApi'

export function MatchPage() {
  const { data, isLoading } = useQuery({
    queryKey: ['matchStatus', userId],
    queryFn: () => matchApi.getMatchStatus(userId),
    refetchInterval: 2000, // 轮询
  })
  
  const mutation = useMutation({
    mutationFn: matchApi.requestMatch,
    onSuccess: () => {
      // 刷新数据
    },
  })
}
```

### Python 代码风格

#### 1. FastAPI 路由

```python
from fastapi import APIRouter, HTTPException
from pydantic import BaseModel

router = APIRouter(prefix="/api/story", tags=["story"])

class StoryRequest(BaseModel):
    user_a_gender: str
    user_b_gender: str

class StoryResponse(BaseModel):
    title: str
    background: str

@router.post("/generate", response_model=StoryResponse)
async def generate_story(request: StoryRequest):
    try:
        # 业务逻辑
        return StoryResponse(title="...", background="...")
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))
```

## 🔧 开发工作流

### 1. 创建新功能

```bash
# 1. 创建功能分支
git checkout -b feature/xxx

# 2. C# 后端开发
cd backend-csharp
# 创建 Service 类、Interface、Log.cs
# 在 AppStartup.cs 注册服务
dotnet build

# 3. 前端开发
cd ../frontend
# 创建 API 客户端、页面组件
npm run dev

# 4. Python AI 开发
cd ../ai-service
# 创建 API 路由、业务逻辑
python -m uvicorn src.main:app --reload

# 5. 测试完整流程
docker-compose up
```

### 2. 代码提交规范

```bash
# Commit 格式
git commit -m "feat(match): add ML-based matching score"
git commit -m "fix(chat): resolve WebSocket reconnection issue"
git commit -m "docs: update API documentation"

# 类型
# feat: 新功能
# fix: 修复
# docs: 文档
# style: 格式
# refactor: 重构
# test: 测试
# chore: 构建/工具
```

### 3. 本地测试

```bash
# C# 单元测试
cd backend-csharp
dotnet test

# Python 单元测试
cd ai-service
pytest

# 前端测试
cd frontend
npm test

# E2E 测试
npm run test:e2e
```

## 📦 依赖管理

### C# 依赖

```bash
# 添加 NuGet 包
cd backend-csharp/src/Feiyue.Api
dotnet add package PackageName

# 更新依赖
dotnet restore
```

### 前端依赖

```bash
cd frontend
# 添加依赖
npm install package-name

# 开发依赖
npm install -D package-name

# 更新依赖
npm update
```

### Python 依赖

```bash
cd ai-service
# 添加到 requirements.txt
echo "package-name==1.0.0" >> requirements.txt
pip install -r requirements.txt
```

## 🐛 调试

### C# 调试

**VS Code:**
```json
// .vscode/launch.json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Core Launch (web)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/backend-csharp/src/Feiyue.Api/bin/Debug/net8.0/Feiyue.Api.dll",
      "cwd": "${workspaceFolder}/backend-csharp/src/Feiyue.Api",
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  ]
}
```

### 前端调试

**Chrome DevTools + VS Code:**
- 使用 React DevTools
- 使用 Tanstack Router DevTools
- 使用 React Query DevTools

### Python 调试

**VS Code:**
```json
{
  "name": "Python: FastAPI",
  "type": "python",
  "request": "launch",
  "module": "uvicorn",
  "args": ["src.main:app", "--reload"],
  "cwd": "${workspaceFolder}/ai-service"
}
```

## 📝 文档维护

当添加新功能时，请更新：
1. API 文档 (`docs/API.md`)
2. 架构文档 (`docs/ARCHITECTURE.md`)
3. 本文档 (如果涉及新的开发模式)

## 🚀 性能优化

### C# 优化
- 使用 `ValueStopwatch` 而非 `Stopwatch`
- 使用异步方法 (`async/await`)
- 避免不必要的分配
- 使用对象池

### 前端优化
- 使用 React.memo 避免重复渲染
- 使用 useMemo/useCallback
- 代码分割 (Lazy loading)
- 图片优化

### Python 优化
- 使用异步 I/O
- 缓存频繁请求
- 批量处理

## ⚠️ 常见问题

### C# 构建失败
```bash
# 清理并重新构建
dotnet clean
dotnet restore
dotnet build
```

### 前端依赖冲突
```bash
# 删除 node_modules 重新安装
rm -rf node_modules package-lock.json
npm install
```

### Docker 问题
```bash
# 清理所有容器和镜像
docker-compose down -v
docker system prune -a
docker-compose up --build
```
