-- Feiyue Database Schema for Supabase PostgreSQL
-- Created: 2026-02-05

-- Enable UUID extension
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ==================== Users Table ====================
CREATE TABLE IF NOT EXISTS users (
    id VARCHAR(255) PRIMARY KEY,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_users_created_at ON users(created_at);

-- ==================== User Profiles Table ====================
CREATE TABLE IF NOT EXISTS user_profiles (
    user_id VARCHAR(255) PRIMARY KEY REFERENCES users(id) ON DELETE CASCADE,
    gender VARCHAR(50) NOT NULL,
    age INTEGER NOT NULL CHECK (age >= 18 AND age <= 100),
    interests JSONB NOT NULL DEFAULT '[]'::jsonb,
    stories JSONB NOT NULL DEFAULT '[]'::jsonb,
    is_vip BOOLEAN NOT NULL DEFAULT FALSE,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_user_profiles_gender ON user_profiles(gender);
CREATE INDEX IF NOT EXISTS idx_user_profiles_is_vip ON user_profiles(is_vip);
CREATE INDEX IF NOT EXISTS idx_user_profiles_age ON user_profiles(age);

-- ==================== Match Requests Table ====================
CREATE TABLE IF NOT EXISTS match_requests (
    user_id VARCHAR(255) PRIMARY KEY REFERENCES users(id) ON DELETE CASCADE,
    gender_preference VARCHAR(50) NOT NULL,
    priority VARCHAR(50) NOT NULL DEFAULT 'Standard',
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_match_requests_priority ON match_requests(priority);
CREATE INDEX IF NOT EXISTS idx_match_requests_created_at ON match_requests(created_at);

-- ==================== Chat Rooms Table ====================
CREATE TABLE IF NOT EXISTS chat_rooms (
    id VARCHAR(255) PRIMARY KEY,
    user1_id VARCHAR(255) NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    user2_id VARCHAR(255) NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    closed_at TIMESTAMP WITH TIME ZONE,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT unique_user_pair UNIQUE (user1_id, user2_id)
);

CREATE INDEX IF NOT EXISTS idx_chat_rooms_user1 ON chat_rooms(user1_id);
CREATE INDEX IF NOT EXISTS idx_chat_rooms_user2 ON chat_rooms(user2_id);
CREATE INDEX IF NOT EXISTS idx_chat_rooms_is_active ON chat_rooms(is_active);
CREATE INDEX IF NOT EXISTS idx_chat_rooms_created_at ON chat_rooms(created_at);

-- ==================== Chat Messages Table ====================
CREATE TABLE IF NOT EXISTS chat_messages (
    id VARCHAR(255) PRIMARY KEY,
    room_id VARCHAR(255) NOT NULL REFERENCES chat_rooms(id) ON DELETE CASCADE,
    sender_id VARCHAR(255) NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    content TEXT NOT NULL,
    message_type VARCHAR(50) NOT NULL DEFAULT 'Text',
    sent_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_chat_messages_room_id ON chat_messages(room_id);
CREATE INDEX IF NOT EXISTS idx_chat_messages_sender_id ON chat_messages(sender_id);
CREATE INDEX IF NOT EXISTS idx_chat_messages_sent_at ON chat_messages(sent_at);

-- ==================== Functions ====================

-- Update updated_at timestamp automatically
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = NOW();
    RETURN NEW;
END;
$$ language 'plpgsql';

-- Trigger for user_profiles
DROP TRIGGER IF EXISTS update_user_profiles_updated_at ON user_profiles;
CREATE TRIGGER update_user_profiles_updated_at
    BEFORE UPDATE ON user_profiles
    FOR EACH ROW
    EXECUTE FUNCTION update_updated_at_column();

-- ==================== Sample Data (Optional) ====================

-- Insert test VIP user
INSERT INTO users (id, created_at) VALUES 
    ('test-vip-user-1', NOW())
ON CONFLICT (id) DO NOTHING;

INSERT INTO user_profiles (user_id, gender, age, interests, stories, is_vip, created_at, updated_at) VALUES 
    ('test-vip-user-1', 'male', 25, '["reading", "coding", "travel"]'::jsonb, '[]'::jsonb, TRUE, NOW(), NOW())
ON CONFLICT (user_id) DO NOTHING;

-- Insert test standard user
INSERT INTO users (id, created_at) VALUES 
    ('test-standard-user-1', NOW())
ON CONFLICT (id) DO NOTHING;

INSERT INTO user_profiles (user_id, gender, age, interests, stories, is_vip, created_at, updated_at) VALUES 
    ('test-standard-user-1', 'female', 23, '["music", "art", "cooking"]'::jsonb, '[]'::jsonb, FALSE, NOW(), NOW())
ON CONFLICT (user_id) DO NOTHING;

-- ==================== Verification ====================

-- Check table row counts
SELECT 'users' AS table_name, COUNT(*) AS row_count FROM users
UNION ALL
SELECT 'user_profiles', COUNT(*) FROM user_profiles
UNION ALL
SELECT 'match_requests', COUNT(*) FROM match_requests
UNION ALL
SELECT 'chat_rooms', COUNT(*) FROM chat_rooms
UNION ALL
SELECT 'chat_messages', COUNT(*) FROM chat_messages;
