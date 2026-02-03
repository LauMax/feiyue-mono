# 绯悦 (Feiyue) Monorepo

基于 **Picasso + Studio** 架构的完整全栈匿名角色扮演聊天应用。

## 🏗️ 架构

```
Picasso (C#) → Studio (React) → Harmony API
    ↓              ↓                  ↓
Feiyue.API   → Feiyue.Web    → Feiyue.AI
```

## 📦 项目结构

- `frontend/` - React 18 + TypeScript + Tanstack Router
- `backend-csharp/` - ASP.NET Core + EF Core + PostgreSQL
- `ai-service/` - FastAPI + Grok API + ML
- `analytics-service/` - Jupyter + Data Analysis
- `docs/` - 完整文档

## 🚀 快速开始

```bash
# 自动化设置
./setup.sh

# 或使用 Docker
docker-compose up --build
```

访问服务：
- 前端: http://localhost:3000
- C# API: http://localhost:5000/swagger
- Python AI: http://localhost:8000/docs

## 📚 文档

- [快速开始指南](docs/GETTING_STARTED.md) - 详细的安装和运行指南
- [架构文档](docs/ARCHITECTURE.md) - 完整的系统架构设计
- [开发指南](docs/DEVELOPMENT.md) - 开发规范和最佳实践
- [API 文档](docs/API.md) - API 接口说明
- [部署指南](docs/DEPLOYMENT.md) - 生产环境部署

## 🎯 核心功能

- ✅ 智能匹配系统 - Redis 队列 + ML 评分
- ✅ 实时聊天 - WebSocket + SignalR
- ✅ AI 故事生成 - Grok API
- ✅ 虚拟角色系统 - AI 角色实时参与
- ✅ 情节控制 - 多分支故事

## 🛠️ 技术栈

| 组件 | 技术 |
|------|------|
| 后端 | C# .NET 8, ASP.NET Core, EF Core |
| 前端 | React 18, TypeScript, Tanstack Router |
| AI | Python 3.11, FastAPI, Grok API |
| 数据库 | PostgreSQL, Redis |
| 部署 | Docker, Kubernetes |

## 📄 许可证

MIT License
