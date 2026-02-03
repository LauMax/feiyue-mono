# 绯悦 Monorepo - 快速开始指南

## ✨ 项目已创建！

基于 **Picasso + Studio** 架构的完整 Monorepo 已经搭建完成。

## 📊 当前状态

✅ **已完成**：
- Monorepo 基础结构
- C# 后端项目（Picasso 风格）
  - `Feiyue.Api` - API 层
  - `Feiyue.Match` - 匹配服务（AppStartup + LoggerMessage）
  - `Feiyue.AiClient` - AI 服务客户端（IHttpClientFactory + Polly）
  - `Feiyue.Storage` - 数据访问层
  - `Feiyue.Chat` - 聊天服务
  - `Feiyue.Shared` - 共享库
  - `Feiyue.InternalContracts` - 内部模型
- 前端项目（Studio 风格）
  - Tanstack Router 路由
  - React Query 数据获取
  - services/ API 客户端
  - WebSocket 聊天客户端
- Python AI 服务基础
- Docker Compose 配置
- 完整文档

🔄 **待实现**：
- C# Controllers（MatchController、ChatController）
- 前端页面组件（从现有 Feiyue_silver_frontend_figma 迁移）
- Python AI 功能（从现有 Feiyue_silver_backend_python 迁移）
- PostgreSQL 数据模型
- Redis 集成

## 🚀 立即开始

### 1. 运行自动化设置

```bash
cd /Users/liuxiaosheng/Desktop/Repos/feiyue-mono
./setup.sh
```

这会自动：
- 检查所有依赖（Node.js, .NET, Python, Docker）
- 安装前端依赖（npm install）
- 恢复 C# 依赖（dotnet restore）
- 安装 Python 依赖（pip install）
- 启动 PostgreSQL 和 Redis

### 2. 手动启动（如果需要）

**启动数据库：**
```bash
docker-compose up -d postgres redis
```

**启动 C# 后端：**
```bash
cd backend-csharp
dotnet run --project src/Feiyue.Api/Feiyue.Api.csproj
```
访问: http://localhost:5000/swagger

**启动 Python AI：**
```bash
cd ai-service
python3 -m uvicorn src.main:app --reload --port 8000
```
访问: http://localhost:8000/docs

**启动前端：**
```bash
cd frontend
npm install  # 首次需要
npm run dev
```
访问: http://localhost:3000

### 3. 使用 Docker（推荐）

```bash
docker-compose up --build
```

一次启动所有服务：
- 前端: http://localhost:80
- C# API: http://localhost:5000
- Python AI: http://localhost:8000
- PostgreSQL: localhost:5432
- Redis: localhost:6379

## 📂 关键文件位置

### C# 后端

**入口：** `backend-csharp/src/Feiyue.Api/Program.cs`
- 参考 Picasso 的 Minimal API 风格
- 使用 `builder.Services.AddXxxServices()` 添加模块

**示例 Controller：**
```csharp
// backend-csharp/src/Feiyue.Api/Controllers/MatchController.cs
[ApiController]
[Route("api/[controller]")]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;
    
    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }
    
    [HttpPost("request")]
    public async Task<IActionResult> RequestMatch(
        [FromBody] MatchRequest request,
        CancellationToken cancellationToken)
    {
        // TODO: 实现
    }
}
```

**AI 客户端已实现：**
`backend-csharp/src/Feiyue.AiClient/AiServiceClient.cs`
- 完整的 IHttpClientFactory 配置
- Polly 重试和熔断策略
- LoggerMessage 日志
- 参考了 Picasso 的 `HarmonyInference.cs`

### 前端

**入口：** `frontend/src/main.tsx`
**路由：** `frontend/src/routeTree.gen.ts`
**API 客户端：** `frontend/src/services/`
- `matchApi.ts` - 匹配 API
- `chatClient.ts` - WebSocket 聊天（参考 Studio 的 chat-client-v2.ts）

**示例页面：**
```tsx
// frontend/src/pages/match/MatchPage.tsx
import { useQuery } from '@tanstack/react-query'
import { matchApi } from '@/services/matchApi'

export function MatchPage() {
  const { data } = useQuery({
    queryKey: ['matchStatus'],
    queryFn: () => matchApi.getMatchStatus('user123'),
  })
  
  return <div>匹配状态: {data?.status}</div>
}
```

### Python AI

**入口：** `ai-service/src/main.py`

**示例 API：**
```python
# ai-service/src/api/story_api.py
from fastapi import APIRouter

router = APIRouter(prefix="/api/story", tags=["story"])

@router.post("/generate")
async def generate_story(request: StoryRequest):
    # TODO: 调用 Grok API
    pass
```

## 🔄 迁移现有代码

### 从 Feiyue_silver_backend_python 迁移

**匹配逻辑：**
- `app/services/match_service.py` → `backend-csharp/src/Feiyue.Match/MatchService.cs`
- `app/services/queue_service.py` → `backend-csharp/src/Feiyue.Match/QueueService.cs`

**AI 功能：**
- `app/services/grok_service.py` → `ai-service/src/services/story/story_engine.py`
- `app/services/story_service.py` → `ai-service/src/services/story/`
- `app/services/chat_service.py` → `ai-service/src/services/character/`

**数据模型：**
- `app/schemas.py` → `backend-csharp/src/Feiyue.InternalContracts/`

### 从 Feiyue_silver_frontend_figma 迁移

**页面组件：**
- `src/app/components/` → `frontend/src/pages/`
- 保持现有的 Radix UI 组件

**API 调用：**
- `src/api/` → `frontend/src/services/`
- 已创建的 `matchApi.ts` 和 `chatClient.ts` 提供了模板

## 📚 参考文档

- **架构文档：** `shared/docs/ARCHITECTURE.md`
- **Picasso 对比：** 查看 `/Users/liuxiaosheng/Desktop/Repos/picasso`
- **Studio 对比：** 查看 `/Users/liuxiaosheng/Desktop/Repos/studio`

## 🎯 下一步建议

### Day 1 (今天)

1. **创建第一个 Controller：**
   ```bash
   # 在 backend-csharp/src/Feiyue.Api/Controllers/
   # 创建 MatchController.cs
   ```

2. **实现第一个前端页面：**
   ```bash
   # 从 Feiyue_silver_frontend_figma 复制匹配页面
   # 到 frontend/src/pages/match/
   ```

3. **创建第一个 AI 接口：**
   ```bash
   # 在 ai-service/src/api/
   # 创建 story_api.py
   ```

4. **测试端到端流程：**
   ```bash
   # 前端发起请求 → C# API → Python AI → 返回结果
   ```

### Day 2-3

- 完善 Controllers（Match、Chat、Room）
- 迁移前端所有页面
- 实现 WebSocket 聊天
- 集成 Grok API

### Week 1

- 完整的匹配系统
- 完整的聊天系统
- AI 故事生成
- PostgreSQL 数据持久化

## 💡 提示

- **C# 代码风格** 完全参考 Picasso：
  - 使用 `sealed record`
  - LoggerMessage 源生成器
  - AppStartup 模式
  - IHttpClientFactory + Polly

- **前端代码风格** 完全参考 Studio：
  - Tanstack Router
  - React Query
  - services/ 目录结构
  - WebSocket 重连机制

- **所有项目都在一个 Monorepo**：
  - 一次 commit 包含前后端所有改动
  - AI 工具能看到完整上下文
  - 类型定义可以共享

## 🆘 遇到问题？

查看已创建的文档：
- `README.md` - 项目概览
- `shared/docs/ARCHITECTURE.md` - 架构详解
- `docker-compose.yml` - 服务配置

准备好开始了吗？运行 `./setup.sh` 启动项目！
