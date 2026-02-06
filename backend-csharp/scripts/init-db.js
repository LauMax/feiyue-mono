// ============================================
// Feiyue MongoDB 初始化脚本
// 适用于：腾讯云 MongoDB
// 执行方式：mongosh "<connection-string>" init-db.js
// ============================================

const dbName = "feiyue";
const database = db.getSiblingDB(dbName);

// Collections
const users = database.getCollection("users");
const userProfiles = database.getCollection("user_profiles");
const matchRequests = database.getCollection("match_requests");
const chatRooms = database.getCollection("chat_rooms");
const chatMessages = database.getCollection("chat_messages");

// Indexes
users.createIndex({ created_at: 1 });

userProfiles.createIndex({ gender: 1 });
userProfiles.createIndex({ is_vip: 1 });

matchRequests.createIndex({ priority: 1 });
matchRequests.createIndex({ created_at: 1 });

chatRooms.createIndex({ user1_id: 1 });
chatRooms.createIndex({ user2_id: 1 });
chatRooms.createIndex({ is_active: 1 });
chatRooms.createIndex({ created_at: 1 });

chatMessages.createIndex({ room_id: 1 });
chatMessages.createIndex({ sent_at: 1 });
chatMessages.createIndex({ room_id: 1, sent_at: 1 });

print(`✅ MongoDB initialized for database: ${dbName}`);
