# Feiyue Backend - Quick Start

## ✅ 已完成功能

### 1. Internal Contracts（内部数据模型）
- **User Models**: `User`, `UserProfile` - 用户基本信息和资料
- **Match Models**: `MatchRequest`, `MatchQueueEntry`, `QueuePriority`, `QueueStats` - 匹配队列
- **Chat Models**: `ChatRoom`, `ChatMessage`, `RoomStats` - 聊天室和消息
- **Story Models**: `Story`, `Role` - 用户故事

### 2. Storage Layer（数据访问层）
- **User.Storage**: Supabase PostgreSQL 用户数据存储
- **Match.Storage**: Redis + PostgreSQL 匹配队列管理
- **Chat.Storage**: PostgreSQL 聊天消息存储

### 3. API Layer（API 接口层）
- **UserController**: 
  - `POST /api/user/create` - 创建用户
  - `GET /api/user/{userId}` - 获取用户
  - `GET /api/user/{userId}/profile` - 获取用户资料
  - `POST /api/user/{userId}/profile` - 更新用户资料
  
- **MatchController**:
  - `POST /api/match/enqueue` - 加入匹配队列
  - `GET /api/match/stats` - 获取队列统计
  - `DELETE /api/match/{userId}/leave` - 离开队列

### 4. 数据库 Schema
- 完整的 PostgreSQL 表结构（`database/schema.sql`）
- 包含测试数据和验证查询

### 5. CI/CD
- GitHub Actions workflows
- 自动化测试和部署

## 🚀 本地开发步骤

### 1. 配置 Supabase 数据库

```bash
# 1. 访问 https://supabase.com 创建项目

# 2. 在 SQL Editor 中运行 database/schema.sql

# 3. 获取连接字符串（Settings → Database → Connection String）
# postgresql://postgres:[YOUR-PASSWORD]@db.[YOUR-PROJECT].supabase.co:5432/postgres
```

### 2. 启动 Redis

```bash
# Docker 方式（推荐）
docker run -d --name redis -p 6379:6379 redis:7-alpine

# 验证
docker ps | grep redis
redis-cli ping  # 应返回 PONG
```

### 3. 配置连接字符串

编辑 `src/Feiyue.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Supabase": "postgresql://postgres:[YOUR-PASSWORD]@db.[YOUR-PROJECT].supabase.co:5432/postgres",
    "Redis": "localhost:6379"
  }
}
```

### 4. 构建和运行

```bash
# 构建所有项目
chmod +x build.sh
./build.sh

# 运行 API
cd src/Feiyue.Api
dotnet run

# API 将在 http://localhost:5000 启动
# Swagger UI: http://localhost:5000/swagger
```

## 📝 测试 API

### Health Check
```bash
curl http://localhost:5000/health
```

### 创建用户
```bash
curl -X POST http://localhost:5000/api/user/create \
  -H "Content-Type: application/json" \
  -d '{}'
```

### 更新用户资料
```bash
curl -X POST http://localhost:5000/api/user/{userId}/profile \
  -H "Content-Type: application/json" \
  -d '{
    "gender": "male",
    "age": 25,
    "interests": ["coding", "reading"],
    "stories": [],
    "isVip": false
  }'
```

### 加入匹配队列
```bash
curl -X POST http://localhost:5000/api/match/enqueue \
  -H "Content-Type: application/json" \
  -d '{
    "userId": "{userId}",
    "genderPreference": "female",
    "isVip": false
  }'
```

### 获取队列统计
```bash
curl http://localhost:5000/api/match/stats
```

## 📂 项目结构

```
backend-csharp/
├── src/
│   ├── Feiyue.InternalContracts/    # 内部数据模型
│   ├── Feiyue.User.Storage/         # 用户数据存储
│   ├── Feiyue.Match.Storage/        # 匹配队列存储
│   ├── Feiyue.Chat.Storage/         # 聊天消息存储
│   ├── Feiyue.User/                 # 用户业务逻辑
│   ├── Feiyue.Match/                # 匹配业务逻辑
│   ├── Feiyue.Chat/                 # 聊天业务逻辑
│   └── Feiyue.Api/                  # API 接口层
├── database/
│   ├── schema.sql                   # 数据库 Schema
│   └── README.md                    # 数据库文档
└── build.sh                         # 构建脚本
```

## 🔧 故障排查

### 编译错误
```bash
# 清理并重新构建
dotnet clean
dotnet restore
./build.sh
```

### Supabase 连接失败
```bash
# 测试连接
psql "postgresql://postgres:PASSWORD@db.PROJECT.supabase.co:5432/postgres" -c "\dt"
```

### Redis 连接失败
```bash
# 检查 Redis 是否运行
redis-cli ping

# 重启 Redis
docker restart redis
```

## 📚 后续开发任务

1. ✅ Internal Contracts - 完成
2. ✅ Storage 层 - 完成
3. ✅ API Controllers - 基础完成
4. ⏳ Business Logic 层 - 待实现匹配算法
5. ⏳ WebSocket 支持 - 待实现实时聊天
6. ⏳ 单元测试 - 待添加
7. ⏳ 集成测试 - 待添加

## 🎯 现在可以做什么

**立即可测试**：
- 创建用户
- 更新用户资料
- 加入匹配队列
- 查看队列统计

**需要实现**：
- 自动匹配算法
- WebSocket 实时聊天
- AI 故事验证
- VIP 功能

需要帮助继续实现哪个功能？
