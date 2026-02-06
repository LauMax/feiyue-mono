-- ============================================
-- Feiyue 数据库初始化脚本
-- 适用于：腾讯云 PostgreSQL
-- 执行方式：psql -h <host> -U <user> -d feiyue -f init.sql
-- ============================================

-- 1. 用户表
CREATE TABLE IF NOT EXISTS users (
    id          TEXT PRIMARY KEY,
    anonymous_id TEXT,
    created_at  TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_users_created_at ON users(created_at);

-- 2. 用户资料表
CREATE TABLE IF NOT EXISTS user_profiles (
    user_id     TEXT PRIMARY KEY REFERENCES users(id) ON DELETE CASCADE,
    gender      TEXT NOT NULL DEFAULT 'unknown',
    age         INTEGER NOT NULL DEFAULT 0,
    interests   JSONB NOT NULL DEFAULT '[]',
    stories     JSONB NOT NULL DEFAULT '[]',
    is_vip      BOOLEAN NOT NULL DEFAULT FALSE,
    created_at  TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at  TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_user_profiles_gender ON user_profiles(gender);
CREATE INDEX IF NOT EXISTS idx_user_profiles_is_vip ON user_profiles(is_vip);

-- 3. 匹配请求表
CREATE TABLE IF NOT EXISTS match_requests (
    user_id           TEXT PRIMARY KEY REFERENCES users(id) ON DELETE CASCADE,
    gender_preference TEXT NOT NULL DEFAULT 'any',
    priority          TEXT NOT NULL DEFAULT 'Standard',
    created_at        TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_match_requests_priority ON match_requests(priority);
CREATE INDEX IF NOT EXISTS idx_match_requests_created_at ON match_requests(created_at);

-- 4. 聊天室表
CREATE TABLE IF NOT EXISTS chat_rooms (
    id          TEXT PRIMARY KEY,
    user1_id    TEXT NOT NULL REFERENCES users(id),
    user2_id    TEXT NOT NULL REFERENCES users(id),
    is_active   BOOLEAN NOT NULL DEFAULT TRUE,
    created_at  TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    closed_at   TIMESTAMPTZ
);

CREATE INDEX IF NOT EXISTS idx_chat_rooms_user1 ON chat_rooms(user1_id);
CREATE INDEX IF NOT EXISTS idx_chat_rooms_user2 ON chat_rooms(user2_id);
CREATE INDEX IF NOT EXISTS idx_chat_rooms_active ON chat_rooms(is_active);
CREATE INDEX IF NOT EXISTS idx_chat_rooms_created_at ON chat_rooms(created_at);

-- 5. 聊天消息表
CREATE TABLE IF NOT EXISTS chat_messages (
    id           TEXT PRIMARY KEY,
    room_id      TEXT NOT NULL REFERENCES chat_rooms(id) ON DELETE CASCADE,
    sender_id    TEXT NOT NULL,
    content      TEXT NOT NULL,
    message_type TEXT NOT NULL DEFAULT 'text',
    sent_at      TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_chat_messages_room_id ON chat_messages(room_id);
CREATE INDEX IF NOT EXISTS idx_chat_messages_sent_at ON chat_messages(sent_at);
CREATE INDEX IF NOT EXISTS idx_chat_messages_room_sent ON chat_messages(room_id, sent_at);

-- ============================================
-- 验证：查看所有表
-- ============================================
SELECT table_name, 
       (SELECT count(*) FROM information_schema.columns WHERE table_schema = 'public' AND columns.table_name = tables.table_name) as column_count
FROM information_schema.tables 
WHERE table_schema = 'public' 
ORDER BY table_name;
