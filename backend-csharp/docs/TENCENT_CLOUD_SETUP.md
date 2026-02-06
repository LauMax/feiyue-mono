# 快速配置腾讯云数据库

## 1. 准备腾讯云连接信息

### MongoDB 连接信息

登录腾讯云控制台 → MongoDB → 实例详情，获取：
- 内网地址：例如 `10.x.x.x:27017`
- 用户名：通常是 `mongouser`
- 密码：创建实例时设置的密码
- 数据库：`admin`（authSource）

### Redis 连接信息

登录腾讯云控制台 → Redis → 实例详情，获取：
- 内网地址：例如 `10.x.x.x:6379`
- 密码：实例密码

## 2. 创建生产配置文件

```bash
cd backend-csharp/AppHost
cp appsettings.Production.json.template appsettings.Production.json
```

编辑 `appsettings.Production.json`：

```json
{
  "ConnectionStrings": {
    "feiyue": "mongodb://mongouser:你的MongoDB密码@10.x.x.x:27017/feiyue?authSource=admin&ssl=true",
    "redis": "10.x.x.x:6379,password=你的Redis密码,ssl=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

⚠️ **注意：** 
- 使用**内网地址**，不是外网地址
- MongoDB 连接字符串中包含 `?authSource=admin&ssl=true`
- Redis 连接字符串中包含 `ssl=True`

## 3. 初始化 MongoDB 数据库

```bash
cd backend-csharp

# 使用你的连接信息替换下面的占位符
mongosh "mongodb://mongouser:你的密码@10.x.x.x:27017/feiyue?authSource=admin&ssl=true" scripts/init-db.js
```

成功会看到：
```
✅ MongoDB initialized for database: feiyue
```

## 4. 测试连接

### 方式1：通过 Aspire（推荐）

```bash
cd backend-csharp

# 生产模式启动
ASPNETCORE_ENVIRONMENT=Production USE_REMOTE_DB=true dotnet run --project AppHost/Feiyue.AppHost.csproj
```

启动后应该看到：
```
🌐 使用远程数据库模式
   MongoDB: mongodb://mongouser:***@10.x.x.x:27017...
   Redis: 10.x.x.x:6379,password=***...
```

访问 Aspire Dashboard: https://localhost:17250

### 方式2：直接运行 API

```bash
cd backend-csharp/src/Service.Api

# 设置环境和连接字符串
export ASPNETCORE_ENVIRONMENT=Production
export ConnectionStrings__feiyue="mongodb://mongouser:密码@10.x.x.x:27017/feiyue?authSource=admin&ssl=true"
export ConnectionStrings__redis="10.x.x.x:6379,password=密码,ssl=True"

dotnet run
```

## 5. 验证功能

API 启动后测试：

```bash
# 健康检查
curl http://localhost:5000/health

# 创建用户
curl -X POST http://localhost:5000/api/user \
  -H "Content-Type: application/json" \
  -d '{"anonymousId": "test-user-001"}'

# 获取用户
curl http://localhost:5000/api/user/test-user-001
```

## 6. 安全检查清单

- [ ] `appsettings.Production.json` 已加入 `.gitignore`
- [ ] 使用内网地址（不是公网地址）
- [ ] 腾讯云安全组已配置 IP 白名单
- [ ] MongoDB 已开启 SSL
- [ ] Redis 已开启 SSL
- [ ] 数据库用户权限最小化（不使用 root）

## 7. 常见错误

### MongoDB 连接错误

```
MongoAuthenticationException: Unable to authenticate
```

**解决：**
1. 确认用户名密码正确
2. 检查 `authSource=admin`
3. 确认安全组开放 27017 端口

### Redis 连接超时

```
RedisConnectionException: Connection timeout
```

**解决：**
1. 确认使用内网地址
2. 检查 `ssl=True` 参数
3. 确认安全组开放 6379 端口
4. 验证密码正确

### 权限被拒绝

```
MongoCommandException: not authorized
```

**解决：**
1. 数据库用户需要有 `readWrite` 权限
2. 在腾讯云控制台检查用户权限设置

## 8. 生产部署

配置完成后，可以部署到 Kubernetes：

```bash
# 更新 Kubernetes Secret
kubectl create secret generic feiyue-secrets \
  --from-literal=mongodb-connection="mongodb://..." \
  --from-literal=redis-connection="..." \
  --dry-run=client -o yaml | kubectl apply -f -

# 部署应用
kubectl apply -f k8s/
```

详见 [k8s/README.md](../k8s/README.md)
