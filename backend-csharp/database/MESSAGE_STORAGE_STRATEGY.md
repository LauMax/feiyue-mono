# 聊天消息存储方案

## 🎯 推荐架构：PostgreSQL + Redis 混合

### 方案设计

```
写入流程：
用户发消息 → Redis (缓存) → PostgreSQL (持久化)
              ↓
         实时推送到 WebSocket

读取流程：
1. 先查 Redis (最近 100 条)
2. Redis 没有 → 查 PostgreSQL
3. 加载历史消息 → 只查 PostgreSQL
```

---

## 📊 存储方案对比

### 方案 A: 纯 PostgreSQL（推荐-初期）
**优点**：
- ✅ 无额外成本（已有 Supabase）
- ✅ ACID 保证，消息不丢
- ✅ 统一技术栈
- ✅ 索引优化后性能够用

**缺点**：
- ⚠️ 百万级以上消息需要分区优化
- ⚠️ 写入性能不如 NoSQL

**适用**：日活 <10000 用户

**成本**：¥0（已有数据库）

---

### 方案 B: PostgreSQL + Redis 缓存（推荐-生产）
**架构**：
```sql
-- PostgreSQL: 持久化存储
CREATE TABLE chat_messages (
    id VARCHAR(255) PRIMARY KEY,
    room_id VARCHAR(255) NOT NULL,
    sender_id VARCHAR(255) NOT NULL,
    content TEXT NOT NULL,
    sent_at TIMESTAMP WITH TIME ZONE NOT NULL
);
CREATE INDEX idx_messages_room_time ON chat_messages(room_id, sent_at DESC);

-- Redis: 缓存最近消息
-- Key: chat:room:{roomId}:messages
-- Type: List (LPUSH + LTRIM 保留最近 100 条)
```

**优点**：
- ✅ 读取速度快（大部分查询命中 Redis）
- ✅ 写入性能好（先写 Redis 异步写 DB）
- ✅ 成本低（Redis 只缓存热数据）
- ✅ 可靠性高（PostgreSQL 兜底）

**缺点**：
- ⚠️ 代码复杂度增加
- ⚠️ 需要处理缓存一致性

**适用**：日活 10000-100000 用户

**成本**：PostgreSQL (已有) + Redis ¥28/月

---

### 方案 C: MongoDB（不推荐-初期）
**优点**：
- ✅ 天然适合文档存储
- ✅ 横向扩展容易
- ✅ 写入性能极好

**缺点**：
- ❌ 额外成本（¥30-50/月）
- ❌ 多一套技术栈
- ❌ 小规模应用优势不明显

**适用**：日活 >100000 用户

**成本**：¥30-50/月

---

### 方案 D: PostgreSQL + TimescaleDB
**说明**：PostgreSQL 的时序数据库扩展

**优点**：
- ✅ 自动时间分区
- ✅ 压缩历史数据
- ✅ 查询性能好

**缺点**：
- ⚠️ 需要自建（Supabase 不支持）
- ⚠️ 运维成本高

**适用**：自建数据库 + 超大量消息

---

## 🎯 推荐实施路线

### 阶段 1: 纯 PostgreSQL（现在）
```sql
-- 已有的 schema
CREATE TABLE chat_messages (
    id VARCHAR(255) PRIMARY KEY,
    room_id VARCHAR(255) NOT NULL,
    sender_id VARCHAR(255) NOT NULL,
    content TEXT NOT NULL,
    sent_at TIMESTAMP WITH TIME ZONE NOT NULL
);

-- 关键索引
CREATE INDEX idx_messages_room_time ON chat_messages(room_id, sent_at DESC);
CREATE INDEX idx_messages_sender ON chat_messages(sender_id);
```

**性能测试**：
- 百万级消息：查询 <100ms
- 单房间查询：<10ms
- 写入：1000+ msg/s

**够用场景**：
- 1000 个聊天室
- 每个房间 1000 条消息
- 总共 100 万条消息
- **完全够用！**

---

### 阶段 2: 添加 Redis 缓存（未来优化）
```csharp
// 写入消息
public async Task SaveMessageAsync(ChatMessage message)
{
    // 1. 写入 PostgreSQL
    await _postgres.SaveAsync(message);
    
    // 2. 缓存到 Redis（最近 100 条）
    var key = $"chat:room:{message.RoomId}:messages";
    await _redis.ListLeftPushAsync(key, JsonSerializer.Serialize(message));
    await _redis.ListTrimAsync(key, 0, 99); // 只保留最近 100 条
    await _redis.KeyExpireAsync(key, TimeSpan.FromHours(24));
}

// 读取消息
public async Task<List<ChatMessage>> GetMessagesAsync(string roomId, int limit = 50)
{
    var key = $"chat:room:{roomId}:messages";
    
    // 1. 先查 Redis
    var cached = await _redis.ListRangeAsync(key, 0, limit - 1);
    if (cached.Length > 0)
    {
        return cached.Select(x => JsonSerializer.Deserialize<ChatMessage>(x)).ToList();
    }
    
    // 2. Redis miss，查 PostgreSQL
    var messages = await _postgres.GetMessagesAsync(roomId, limit);
    
    // 3. 回填 Redis
    foreach (var msg in messages)
    {
        await _redis.ListRightPushAsync(key, JsonSerializer.Serialize(msg));
    }
    
    return messages;
}
```

---

### 阶段 3: 数据分区（如果真的很大）
```sql
-- 按月分区（PostgreSQL 12+）
CREATE TABLE chat_messages (
    id VARCHAR(255),
    room_id VARCHAR(255) NOT NULL,
    sender_id VARCHAR(255) NOT NULL,
    content TEXT NOT NULL,
    sent_at TIMESTAMP WITH TIME ZONE NOT NULL,
    PRIMARY KEY (id, sent_at)
) PARTITION BY RANGE (sent_at);

-- 创建月度分区
CREATE TABLE chat_messages_2026_02 PARTITION OF chat_messages
    FOR VALUES FROM ('2026-02-01') TO ('2026-03-01');
    
CREATE TABLE chat_messages_2026_03 PARTITION OF chat_messages
    FOR VALUES FROM ('2026-03-01') TO ('2026-04-01');
```

---

## 💰 成本对比

### 小型应用（<1000 日活）
- **方案**: 纯 PostgreSQL
- **成本**: ¥0（Supabase 免费层）
- **性能**: ⭐⭐⭐⭐

### 中型应用（1000-10000 日活）
- **方案**: PostgreSQL + Redis 缓存
- **成本**: PostgreSQL ¥0 + Redis ¥28/月
- **性能**: ⭐⭐⭐⭐⭐

### 大型应用（>10000 日活）
- **方案**: PostgreSQL 分区表 + Redis
- **成本**: PostgreSQL ¥100/月 + Redis ¥90/月
- **性能**: ⭐⭐⭐⭐⭐

---

## 🔧 PostgreSQL 性能优化技巧

### 1. 索引优化
```sql
-- 复合索引（覆盖最常见查询）
CREATE INDEX idx_messages_room_time ON chat_messages(room_id, sent_at DESC);

-- 部分索引（只索引最近 30 天）
CREATE INDEX idx_messages_recent ON chat_messages(room_id, sent_at DESC)
WHERE sent_at > NOW() - INTERVAL '30 days';
```

### 2. 查询优化
```sql
-- ✅ 好：限制数量 + 索引
SELECT * FROM chat_messages 
WHERE room_id = 'room123' 
ORDER BY sent_at DESC 
LIMIT 50;

-- ❌ 差：全表扫描
SELECT * FROM chat_messages 
WHERE content LIKE '%keyword%';
```

### 3. 定期清理
```sql
-- 删除 90 天前的消息
DELETE FROM chat_messages 
WHERE sent_at < NOW() - INTERVAL '90 days';

-- 或归档到冷存储
INSERT INTO chat_messages_archive 
SELECT * FROM chat_messages 
WHERE sent_at < NOW() - INTERVAL '90 days';
```

### 4. 连接池配置
```csharp
// appsettings.json
{
  "ConnectionStrings": {
    "Supabase": "Host=...;Maximum Pool Size=100;Connection Lifetime=300"
  }
}
```

---

## 📈 何时迁移到 NoSQL？

**信号**：
1. ✅ 单表消息 >1000 万条
2. ✅ 查询响应时间 >500ms
3. ✅ PostgreSQL CPU 持续 >70%
4. ✅ 需要全球多区域部署

**迁移方案**：
- MongoDB Atlas（¥50/月起）
- Cassandra（自建）
- DynamoDB（AWS，按请求计费）

**迁移成本**：
- 开发时间：2-3 周
- 数据迁移：需要停机或双写
- 学习成本：中等

---

## 🎯 结论

**当前阶段（MVP）**：
✅ **用 PostgreSQL**
- 成本：¥0
- 开发速度快
- 性能完全够用
- 技术栈统一

**未来优化（用户多了）**：
1. 添加 Redis 缓存（¥28/月）
2. PostgreSQL 分区表
3. 考虑 MongoDB（¥50/月）

**建议**：
**先用 PostgreSQL 把产品做出来，有用户了再优化！过早优化是万恶之源。**
