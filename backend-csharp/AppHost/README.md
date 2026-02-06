# Feiyue AppHost - Aspire Orchestration

## 🚀 运行模式

### 模式1：本地容器模式（默认，推荐日常开发）

**特点：**
- ✅ 快速启动，无需网络
- ✅ 数据隔离，不影响生产
- ✅ 可以随时重置数据
- ✅ 离线可用

**启动命令：**
```bash
cd /path/to/backend-csharp
dotnet run --project AppHost/Feiyue.AppHost.csproj
```

**数据位置：**
- MongoDB: Docker volume (mongo:7.0 镜像)
- Redis: Docker volume

**重置数据：**
```bash
# 停止Aspire后执行
docker volume ls | grep feiyue
docker volume rm <volume-name>
```

---

### 模式2：远程数据库模式（连接腾讯云）

**特点：**
- ✅ 连接真实生产/测试数据库
- ✅ 测试更真实，可复现生产问题
- ⚠️ 需要网络连接
- ⚠️ 注意不要污染生产数据

**配置步骤：**

1. **配置连接字符串** - 创建 `AppHost/appsettings.Production.json`:
   ```bash
   cp appsettings.Production.json.template appsettings.Production.json
   ```
   
   编辑内容：
   ```json
   {
     "ConnectionStrings": {
       "feiyue": "mongodb://<username>:<password>@<tencent-host>:27017/feiyue?authSource=admin&ssl=true",
       "redis": "<tencent-redis-host>:6379,password=<password>,ssl=True"
     }
   }
   ```

2. **启动远程模式（生产环境）**:
   ```bash
   # 使用生产配置
   ASPNETCORE_ENVIRONMENT=Production USE_REMOTE_DB=true dotnet run --project AppHost/Feiyue.AppHost.csproj
   ```

**安全提示：**
- ⚠️ **切勿提交** `appsettings.Production.json` 到Git（已在.gitignore）
- 生产环境建议使用 Kubernetes Secret 管理密钥
- 腾讯云实例配置 IP 白名单和安全组

---

## 🔄 快速切换

**开发 → 生产：**
```bash
# 1. 配置 appsettings.Production.json
# 2. 启动时指定环境
ASPNETCORE_ENVIRONMENT=Production USE_REMOTE_DB=true dotnet run --project AppHost/Feiyue.AppHost.csproj
```

**生产 → 开发：**
```bash
# 直接启动（不带环境变量）
dotnet run --project AppHost/Feiyue.AppHost.csproj
```

---

## 📊 验证连接

**检查当前模式：**
启动时会在终端输出：
- `🐳 使用本地容器模式（默认）` - 本地模式
- `🌐 使用远程数据库模式` - 远程模式（MongoDB/Redis 连接字符串）

**测试连接：**
```bash
# 等Aspire启动后，访问Dashboard
open https://localhost:17250

# 在Resources页面查看：
# - 本地模式：会看到 mongodb 和 redis 容器
# - 远程模式：只会看到 api 服务
```

---

## 🛠️ 故障排查

**问题：远程模式连接失败**
```
InvalidOperationException: 远程模式需要配置 ConnectionStrings:feiyue
```
**解决：** 检查 `appsettings.Production.json` 是否存在且格式正确

---

**问题：MongoDB 认证失败**
```
MongoAuthenticationException: Unable to authenticate
```
**解决：** 
1. 确认连接字符串中的用户名密码正确
2. 检查 authSource 参数（通常是 admin）
3. 腾讯云 MongoDB 确保 SSL 开启：`ssl=true`
4. 检查防火墙/安全组规则，开放 27017 端口

---

**问题：Redis 连接超时**
```
RedisConnectionException: Connection timeout
```
**解决：**
1. 检查腾讯云 Redis 实例是否启用
2. 确认使用内网地址（不是外网地址）
3. 连接字符串包含 `ssl=True`
4. 安全组开放 6379 端口

---

## 📚 详细文档

完整配置指南: [ENVIRONMENT_CONFIG.md](../docs/ENVIRONMENT_CONFIG.md)

参考Picasso的实现：
- 本地开发文档: `picasso/.claude/skills/local-dev/SKILL.md`
- AppHost配置: `picasso/AppHost/Program.cs`
