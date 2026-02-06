# 数据库配置指南

## 方案对比

### 🆓 本地 Docker PostgreSQL（推荐开发环境）
- **价格**: 免费
- **速度**: 最快（本地）
- **适用**: 开发、测试
- **命令**: `./dev-start.sh`

### 💰 云数据库方案

#### 1. **阿里云 RDS PostgreSQL**（推荐生产）
- 按量付费：~0.15元/小时（1核1GB）
- 包年包月：~50元/月起
- 特点：稳定、有备份、监控完善
- 链接：https://www.aliyun.com/product/rds/postgresql

#### 2. **腾讯云 PostgreSQL**
- 按量付费：~0.12元/小时（1核1GB）
- 包年包月：~45元/月起
- 特点：与腾讯云 Redis 集成好
- 链接：https://cloud.tencent.com/product/postgres

#### 3. **华为云 GaussDB**
- 包年包月：~50元/月起
- 特点：兼容 PostgreSQL，性能好

#### 4. **本地/VPS 自建**
- 成本：VPS 约 30-50元/月
- 适合：预算有限，愿意自己维护

## 🚀 快速开始（本地开发）

```bash
# 1. 启动数据库和 Redis
cd backend-csharp
chmod +x dev-start.sh
./dev-start.sh

# 2. 数据库会自动初始化（schema.sql）

# 3. 运行 API
cd src/Feiyue.Api
dotnet run

# API: http://localhost:5000
# Swagger: http://localhost:5000/swagger
```

## 📊 数据库管理工具

- **pgAdmin**: https://www.pgadmin.org
- **DBeaver**: https://dbeaver.io
- **VS Code PostgreSQL**: 安装 `PostgreSQL` 扩展

连接信息：
```
Host: localhost
Port: 5432
Database: feiyue
Username: postgres
Password: postgres123
```

## 🔄 切换到云数据库

只需修改 `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Supabase": "Host=YOUR_HOST;Port=5432;Database=YOUR_DB;Username=YOUR_USER;Password=YOUR_PASSWORD;SSL Mode=Require",
    "Redis": "YOUR_REDIS_HOST:6379,password=YOUR_PASSWORD"
  }
}
```

## 💡 推荐配置

**开发**: 本地 Docker（免费，快速）  
**测试**: 阿里云/腾讯云按量付费（~3元/天）  
**生产**: 阿里云/腾讯云包年包月（~50元/月）
