# Supabase Database Setup

## Quick Start

1. 创建 Supabase 项目
   - 访问 [supabase.com](https://supabase.com)
   - 创建新项目
   - 等待数据库初始化完成（~2分钟）

2. 获取连接信息
   - 进入项目 Settings → Database
   - 复制 Connection String
   - 格式：`postgresql://postgres:[YOUR-PASSWORD]@db.[YOUR-PROJECT-REF].supabase.co:5432/postgres`

3. 初始化数据库
   ```bash
   # 方法 1: Supabase SQL Editor
   # 在 Supabase Dashboard → SQL Editor 中粘贴 schema.sql 内容并执行

   # 方法 2: psql 命令行
   psql "postgresql://postgres:[YOUR-PASSWORD]@db.[YOUR-PROJECT-REF].supabase.co:5432/postgres" -f schema.sql
   ```

4. 配置后端
   ```bash
   # 编辑 appsettings.Development.json
   {
     "Database": {
       "ConnectionString": "postgresql://postgres:[YOUR-PASSWORD]@db.[YOUR-PROJECT-REF].supabase.co:5432/postgres"
     },
     "Redis": {
       "ConnectionString": "localhost:6379"
     }
   }
   ```

## 数据库结构

### Tables

1. **users** - 基础用户表
   - `id`: 用户唯一标识
   - `created_at`: 创建时间

2. **user_profiles** - 用户资料
   - `user_id`: 关联 users.id
   - `gender`: 性别
   - `age`: 年龄
   - `interests`: 兴趣爱好（JSONB）
   - `stories`: 用户故事（JSONB）
   - `is_vip`: 是否 VIP

3. **match_requests** - 匹配请求
   - `user_id`: 关联 users.id
   - `gender_preference`: 匹配偏好
   - `priority`: 队列优先级（Vip/Standard）

4. **chat_rooms** - 聊天室
   - `id`: 房间 ID
   - `user1_id`, `user2_id`: 匹配的两个用户
   - `is_active`: 是否活跃

5. **chat_messages** - 聊天消息
   - `id`: 消息 ID
   - `room_id`: 关联 chat_rooms.id
   - `sender_id`: 发送者
   - `content`: 消息内容
   - `message_type`: 消息类型

## Redis 配置

### 本地开发
```bash
# Docker 方式
docker run -d --name redis -p 6379:6379 redis:7-alpine

# Homebrew 方式（macOS）
brew install redis
brew services start redis
```

### 生产环境
推荐使用腾讯云 Redis 或 Upstash Redis（与 Supabase 搭配）

## 验证安装

```sql
-- 检查表是否创建
SELECT table_name 
FROM information_schema.tables 
WHERE table_schema = 'public';

-- 检查索引
SELECT indexname, tablename 
FROM pg_indexes 
WHERE schemaname = 'public';

-- 查看测试数据
SELECT * FROM users;
SELECT * FROM user_profiles;
```

## 常见问题

### Q: Supabase 连接超时
A: 检查 IP 白名单设置（Supabase Dashboard → Settings → Database → Connection Pooling）

### Q: Redis 连接失败
A: 确保 Redis 服务运行 `redis-cli ping` 应返回 `PONG`

### Q: 数据库权限问题
A: Supabase 默认 postgres 用户有完整权限，无需额外配置
