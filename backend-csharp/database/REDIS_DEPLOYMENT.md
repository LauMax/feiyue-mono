# Redis 部署方案

## 🎯 问题：多服务器共享匹配队列

**现状**: 本地 Docker Redis 只能单机访问  
**需求**: 多个 API 实例共享同一个匹配队列  
**解决**: 使用中心化的云 Redis 服务

---

## 💰 云 Redis 方案对比（国内）

### 1. **腾讯云 Redis**（推荐）
- **价格**: 
  - 标准版 256MB: ¥12/月（包年）
  - 标准版 1GB: ¥28/月
  - 按量付费: ¥0.05/小时
- **特点**: 
  - 可用区内网免费流量
  - 支持主从高可用
  - 与腾讯云 CVM 内网互通
- **地域**: 北京、上海、广州、成都等
- **链接**: https://cloud.tencent.com/product/redis

### 2. **阿里云 Redis**
- **价格**:
  - 社区版 256MB: ¥18/月
  - 社区版 1GB: ¥36/月
  - 按量付费: ¥0.06/小时
- **特点**: 
  - 性能监控完善
  - 自动备份
  - 支持多可用区
- **链接**: https://www.aliyun.com/product/kvstore

### 3. **华为云 Redis**
- **价格**:
  - 单机版 256MB: ¥15/月
  - 单机版 1GB: ¥30/月
- **特点**: 兼容 Redis 6.0，性能好

### 4. **Upstash Redis**（国际）
- **价格**: 
  - 免费层: 10,000 命令/天
  - 按请求付费: $0.2/100K 命令
- **特点**: 
  - Serverless，按用量计费
  - 全球边缘网络
  - 支持 REST API
- **链接**: https://upstash.com

### 5. **自建 Redis（VPS）**
- **价格**: VPS ¥30-50/月
- **方案**: 
  - 搬瓦工/RackNerd 等 VPS
  - Docker 部署 Redis
  - 配置 Redis 密码认证
- **适合**: 预算极低，愿意自己维护

---

## 📝 推荐配置

### 开发环境（本地测试）
```json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379"
  }
}
```

### 生产环境（云 Redis）
```json
{
  "ConnectionStrings": {
    "Redis": "your-redis-host.tencentcloudapi.com:6379,password=your_password,ssl=true"
  }
}
```

---

## 🔧 配置示例

### 腾讯云 Redis 连接字符串
```
redis-xxxxx.redis.tencentcloudapi.com:6379,password=YourPassword,ssl=true,abortConnect=false
```

### 阿里云 Redis 连接字符串
```
r-xxxxx.redis.rds.aliyuncs.com:6379,password=YourPassword,ssl=false
```

### Upstash Redis 连接字符串
```
global-xxxxx.upstash.io:6379,password=YourPassword,ssl=true
```

---

## 🚀 迁移步骤

### 1. 购买云 Redis 实例
```bash
# 选择最小配置测试（256MB 够用）
# 记录：Host、Port、Password
```

### 2. 配置白名单/安全组
```bash
# 允许你的服务器 IP 访问 Redis
# 或使用 VPC 内网访问（更安全）
```

### 3. 更新配置文件
```bash
# appsettings.Production.json
{
  "ConnectionStrings": {
    "Redis": "your-host:6379,password=xxx,ssl=true"
  }
}
```

### 4. 测试连接
```bash
# 使用 redis-cli 测试
redis-cli -h your-host -p 6379 -a your-password --tls
PING  # 应返回 PONG
```

### 5. 部署多实例
```bash
# 所有 API 实例使用相同的 Redis 配置
# 它们会自动共享匹配队列
```

---

## 💡 性能优化建议

### 1. **使用内网连接**
- 购买与服务器同区域的 Redis
- 通过 VPC 内网访问（免费流量）

### 2. **开启 SSL/TLS**（生产环境）
```
Redis连接字符串加: ssl=true
```

### 3. **连接池配置**
```csharp
var options = ConfigurationOptions.Parse(connectionString);
options.ConnectRetry = 3;
options.ConnectTimeout = 5000;
options.SyncTimeout = 5000;
options.KeepAlive = 60;
options.AbortOnConnectFail = false;
```

### 4. **监控和告警**
- 设置内存使用率告警（>80%）
- 监控慢查询
- 定期备份（RDB）

---

## 💰 成本估算（匹配队列场景）

### 小型应用（<1000 活跃用户）
- **推荐**: 腾讯云 Redis 256MB
- **成本**: ¥12/月
- **够用**: 队列数据 < 50MB

### 中型应用（1000-10000 用户）
- **推荐**: 腾讯云 Redis 1GB
- **成本**: ¥28/月
- **够用**: 队列 + 缓存

### 大型应用（>10000 用户）
- **推荐**: 腾讯云 Redis 2GB 主从版
- **成本**: ¥90/月
- **特性**: 高可用、自动故障转移

---

## 🔍 验证多实例共享

### 测试步骤
1. 启动 API 实例 1
2. 启动 API 实例 2
3. 实例 1 创建用户 A 并加入队列
4. 实例 2 创建用户 B 并加入队列
5. 调用队列统计 API，两个实例应该看到相同的队列状态

### 验证脚本
```bash
# 实例 1: localhost:5000
curl -X POST http://localhost:5000/api/match/enqueue \
  -H "Content-Type: application/json" \
  -d '{"userId":"user1","genderPreference":"female","isVip":false}'

# 实例 2: localhost:5001  
curl http://localhost:5001/api/match/stats
# 应该能看到 user1 在队列中
```

---

## ⚠️ 注意事项

1. **不要暴露 Redis 到公网**
   - 使用 VPC 内网
   - 或配置严格的安全组规则

2. **设置强密码**
   - 至少 16 位随机字符
   - 包含大小写、数字、特殊字符

3. **定期备份**
   - 云服务通常自动备份
   - 自建需手动配置

4. **监控资源**
   - 内存使用率
   - 连接数
   - QPS（每秒查询数）
