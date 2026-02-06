# 环境配置指南

本项目支持多环境配置（Development、Production），通过 Aspire 的混合模式（Hybrid Mode）实现：

- **Development（开发）**: 默认使用本地 Docker 容器
- **Production（生产）**: 连接腾讯云 MongoDB 和 Redis

## 1. 开发环境（默认）

### 方式1：本地容器（推荐）

直接启动 Aspire，会自动使用本地 Docker 容器：

```bash
cd backend-csharp
dotnet run --project AppHost/Feiyue.AppHost.csproj
```

Aspire 会自动创建：
- MongoDB 容器：`localhost:27017`
- Redis 容器：`localhost:6379`

### 方式2：连接远程开发数据库

如果你有测试用的远程 MongoDB/Redis：

1. 复制模板创建配置文件：
```bash
cp AppHost/appsettings.Development.json.template AppHost/appsettings.Development.json
```

2. 编辑 `appsettings.Development.json`，填入真实连接字符串

3. 启动时设置环境变量：
```bash
USE_REMOTE_DB=true dotnet run --project AppHost/Feiyue.AppHost.csproj
```

## 2. 生产环境

### 配置腾讯云连接字符串

1. 复制模板创建生产配置：
```bash
cp AppHost/appsettings.Production.json.template AppHost/appsettings.Production.json
```

2. 编辑 `appsettings.Production.json`，填入腾讯云实例信息：

```json
{
  "ConnectionStrings": {
    "feiyue": "mongodb://<username>:<password>@<tencent-mongodb-host>:27017/feiyue?authSource=admin&ssl=true",
    "redis": "<tencent-redis-host>:6379,password=<redis-password>,ssl=True"
  }
}
```

**MongoDB 连接字符串格式：**
- 腾讯云 MongoDB 通常是：`mongodb://用户名:密码@内网IP:27017/feiyue?authSource=admin&ssl=true`
- 如果是副本集：`mongodb://用户名:密码@host1:27017,host2:27017/feiyue?replicaSet=xxx&authSource=admin&ssl=true`

**Redis 连接字符串格式：**
- 腾讯云 Redis 通常是：`内网IP:6379,password=实例密码,ssl=True`

3. 初始化 MongoDB 数据库：

```bash
# 使用腾讯云连接字符串
mongosh "mongodb://<username>:<password>@<host>:27017/feiyue?authSource=admin&ssl=true" scripts/init-db.js
```

4. 启动生产模式：

```bash
# 方式1：通过环境变量
ASPNETCORE_ENVIRONMENT=Production USE_REMOTE_DB=true dotnet run --project AppHost/Feiyue.AppHost.csproj

# 方式2：直接运行 API（跳过 Aspire）
cd src/Service.Api
ASPNETCORE_ENVIRONMENT=Production dotnet run
```

## 3. Kubernetes 部署

生产环境建议使用 Kubernetes。配置文件在 `k8s/` 目录：

1. 创建 Secret（包含连接字符串）：
```bash
kubectl apply -f k8s/secret.yaml
```

2. 编辑 `k8s/secret.yaml` 中的连接字符串（Base64 编码）：
```bash
echo -n "mongodb://..." | base64
echo -n "redis-host:6379,password=xxx,ssl=True" | base64
```

3. 部署应用：
```bash
kubectl apply -f k8s/
```

## 4. 环境变量说明

| 变量 | 说明 | 默认值 |
|------|------|--------|
| `ASPNETCORE_ENVIRONMENT` | ASP.NET 环境名（Development/Production） | Development |
| `USE_REMOTE_DB` | 是否使用远程数据库 | false |
| `ConnectionStrings__feiyue` | MongoDB 连接字符串 | - |
| `ConnectionStrings__redis` | Redis 连接字符串 | - |

## 5. 安全注意事项

⚠️ **重要：**
- `appsettings.Development.json` 和 `appsettings.Production.json` 已加入 `.gitignore`
- 不要提交包含真实密码的配置文件
- 使用 Kubernetes Secret 管理生产环境密钥
- 腾讯云实例建议配置 IP 白名单和防火墙规则

## 6. 验证配置

启动后检查连接：

```bash
# 查看 Aspire Dashboard
open https://localhost:17250

# 查看日志确认数据库连接
# 应该看到："🌐 使用远程数据库模式" 或 "🐳 使用本地容器模式"
```

## 7. 常见问题

**Q: MongoDB 连接超时？**
- 检查腾讯云安全组规则，确保开放 27017 端口
- 确认使用内网 IP（不是外网 IP）
- 验证用户名密码和 authSource

**Q: Redis 连接失败？**
- 检查端口（默认 6379）
- 确认密码正确
- 腾讯云 Redis 需要 SSL，确保连接字符串包含 `ssl=True`

**Q: 如何切换环境？**
- 开发：直接运行 `dotnet run --project AppHost/Feiyue.AppHost.csproj`（使用容器）
- 生产：`ASPNETCORE_ENVIRONMENT=Production USE_REMOTE_DB=true dotnet run --project AppHost/Feiyue.AppHost.csproj`
