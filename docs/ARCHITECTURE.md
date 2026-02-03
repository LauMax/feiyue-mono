# 绯悦 Monorepo 架构文档

## 🎯 架构对比

```
Microsoft 架构                     绯悦架构
─────────────────────────         ─────────────────────────
Picasso (C#)  ────────────────►  Feiyue.API (C#)
   ├─ Service.Match                  ├─ Feiyue.Match
   ├─ Service.Chat                   ├─ Feiyue.Chat  
   ├─ Service.Storage                ├─ Feiyue.Storage
   └─ Shared/*                       └─ Feiyue.Shared

Studio (React) ────────────────►  Feiyue.Web (React)
   ├─ Tanstack Router                ├─ Tanstack Router
   ├─ React Query                    ├─ React Query
   └─ Jotai                          └─ Jotai

Harmony API ────────────────────►  Feiyue.AI (Python)
   └─ Azure OpenAI                   └─ Grok API
```

## 📂 项目结构

### C# 后端 (backend-csharp/)

采用 **Picasso 的完整模式**：

```
backend-csharp/
├── Directory.Build.props          # ✅ 全局配置 (参考 Picasso)
├── src/
│   ├── Feiyue.Api/               # ✅ API 层 (Controllers + Program.cs)
│   │   ├── Controllers/
│   │   ├── Program.cs            # ✅ Minimal API 启动
│   │   └── appsettings.json
│   │
│   ├── Feiyue.Match/             # ✅ 匹配业务逻辑
│   │   ├── AppStartup.cs         # ✅ Once 模式
│   │   ├── IMatchService.cs
│   │   ├── MatchService.cs
│   │   ├── QueueService.cs
│   │   └── Log.cs                # ✅ LoggerMessage 源生成器
│   │
│   ├── Feiyue.Chat/              # ✅ 聊天业务逻辑
│   │   ├── AppStartup.cs
│   │   └── ...
│   │
│   ├── Feiyue.Storage/           # ✅ 数据访问层 (EF Core)
│   │   ├── AppStartup.cs
│   │   ├── FeiyueDbContext.cs
│   │   └── Repositories/
│   │
│   ├── Feiyue.AiClient/          # ✅ AI 服务 HTTP 客户端
│   │   ├── AppStartup.cs         # ✅ IHttpClientFactory + Polly
│   │   ├── IAiServiceClient.cs
│   │   ├── AiServiceClient.cs    # ✅ 参考 HarmonyInference.cs
│   │   ├── Models.cs
│   │   └── Log.cs
│   │
│   ├── Feiyue.Shared/            # ✅ 共享工具库
│   │   └── Extensions/
│   │
│   └── Feiyue.InternalContracts/ # ✅ 内部数据模型 (无 JSON 属性)
│       └── MatchModels.cs
```

**关键 Picasso 模式**：
- ✅ `AppStartup.cs` - Once 模式防止重复注册
- ✅ `Log.cs` - LoggerMessage 源生成器（零分配日志）
- ✅ `InternalContracts` - 无序列化属性的内部模型
- ✅ `IHttpClientFactory` - 带 Polly 重试和熔断
- ✅ `Directory.Build.props` - 全局 using 和配置

### 前端 (frontend/)

采用 **Studio 的完整模式**：

```
frontend/
├── src/
│   ├── main.tsx                  # ✅ React 入口 (参考 Studio)
│   ├── routeTree.gen.ts          # ✅ Tanstack Router 路由树
│   ├── index.css                 # ✅ Tailwind CSS
│   │
│   ├── services/                 # ✅ API 客户端 (参考 Studio services/)
│   │   ├── config.ts
│   │   ├── matchApi.ts
│   │   └── chatClient.ts         # ✅ WebSocket (参考 chat-client-v2.ts)
│   │
│   ├── pages/                    # ✅ 页面组件
│   │   ├── match/
│   │   ├── chat/
│   │   └── profile/
│   │
│   ├── components/               # ✅ UI 组件
│   │   └── ui/                   # Radix UI 组件
│   │
│   ├── atoms/                    # ✅ Jotai 状态管理
│   │
│   └── lib/
│       └── utils.ts              # ✅ cn() 工具函数
```

**关键 Studio 模式**：
- ✅ Tanstack Router (类型安全路由)
- ✅ React Query (数据获取和缓存)
- ✅ Jotai (轻量级状态管理)
- ✅ WebSocket 客户端（重连机制）
- ✅ Radix UI + Tailwind CSS

### Python AI 服务 (ai-service/)

```
ai-service/
├── src/
│   ├── main.py                   # FastAPI 入口
│   │
│   ├── api/                      # API 路由
│   │   ├── story_api.py          # 故事生成
│   │   ├── character_api.py      # 虚拟角色
│   │   └── match_api.py          # ML 匹配评分
│   │
│   ├── services/                 # 业务逻辑
│   │   ├── story/
│   │   │   ├── story_engine.py   # Grok API 集成
│   │   │   └── prompt_manager.py
│   │   ├── character/
│   │   │   └── virtual_character.py
│   │   └── matching/
│   │       └── ml_matcher.py     # scikit-learn 模型
│   │
│   ├── ml/                       # ML 模型
│   └── prompts/                  # Grok Prompts
```

## 🔄 数据流

### 匹配流程
```
Frontend                  C# Backend               Python AI
  │                          │                        │
  │─── POST /api/match ────►│                        │
  │                          │                        │
  │                          │─ Redis 队列            │
  │                          │─ 寻找匹配伙伴          │
  │                          │                        │
  │                          │── POST /story ────────►│
  │                          │                        │── Grok API
  │                          │◄── Story Response ─────│
  │                          │                        │
  │◄── Match Success ────────│                        │
  │    (with Story)          │                        │
```

### 聊天流程
```
Frontend                  C# Backend               Python AI
  │                          │                        │
  │─── WebSocket Open ──────►│                        │
  │                          │                        │
  │─── Send Message ────────►│─ PostgreSQL Write     │
  │                          │─ Redis Pub/Sub        │
  │◄── Receive Message ──────│                        │
  │                          │                        │
  │                          │─ 检测沉默/轮次触发     │
  │                          │                        │
  │                          │── POST /character ────►│
  │                          │                        │── Grok API
  │                          │◄── Character Msg ──────│
  │                          │                        │
  │◄── Virtual Character ────│                        │
```

## 🛠️ 技术栈对比

| 组件 | Picasso/Studio | 绯悦 | 说明 |
|------|---------------|------|------|
| **后端语言** | C# (.NET 8) | C# (.NET 8) | ✅ 完全一致 |
| **前端框架** | React 19 | React 18 | ⚠️ 版本差异 |
| **路由** | Tanstack Router | Tanstack Router | ✅ 完全一致 |
| **状态管理** | Jotai | Jotai | ✅ 完全一致 |
| **UI 组件** | 自定义 | Radix UI | ⚠️ 不同但都是 Headless UI |
| **HTTP 客户端** | IHttpClientFactory | IHttpClientFactory | ✅ 完全一致 |
| **日志** | LoggerMessage | LoggerMessage | ✅ 完全一致 |
| **依赖注入** | MS DI | MS DI | ✅ 完全一致 |
| **AI 服务** | Harmony API | Grok API | ⚠️ 接口类似 |
| **数据库** | Cosmos DB | PostgreSQL | ⚠️ 不同但都是 NoSQL/SQL |

## 📝 代码风格对齐

### C# 后端

✅ **已对齐的 Picasso 模式**：
- `AppStartup.cs` 的 Once 模式
- `Log.cs` 的 LoggerMessage 源生成器
- InternalContracts vs External 分离
- IHttpClientFactory + Polly 策略
- sealed record 数据模型
- 构造函数参数顺序 (ILogger → IOptions → 其他)

### 前端

✅ **已对齐的 Studio 模式**：
- Tanstack Router 路由定义
- services/ 目录结构
- WebSocket 客户端重连机制
- React Query 数据获取
- Tailwind CSS + cn() 工具

## 🚀 下一步

1. **完善 C# Controllers** - 添加 External 模型和 ToInternal/ToExternal 转换
2. **实现 WebSocket Chat** - 参考 Picasso ChatController.cs
3. **完善前端页面** - 参考现有 Feiyue_silver_frontend_figma
4. **集成 Grok API** - 实现 Python AI 服务
5. **添加测试** - 参考 Picasso.Tests.Unit
