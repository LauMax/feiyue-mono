# 绯悦 - 前后端接口文档

## 项目概述

**绯悦**是一个匿名角色扮演聊天应用，核心功能是通过AI生成故事种子和角色分工，让两个陌生用户进行匿名角色扮演聊天。

### 核心特性
- 完全匿名聊天
- 基于用户偏好的智能匹配
- AI生成的故事场景
- 实时双人聊天
- 移动端优化

---

## 1. 用户流程

```
首页介绍 → 
用户资料设置（性别、年龄段、偏好标签、个人描述、身高、体重） → 
AI故事生成（根据用户偏好） → 
等待匹配（系统自动根据性别分配角色） → 
实时聊天室（角色扮演）
```

**注意：**
- ❌ 已取消角色选择界面
- ✅ 系统自动分配：男生=maleRole，女生=femaleRole
- ✅ 故事种子在匹配成功后由后端生成并返回

---

## 数据结构定义

### 1. 用户资料 (UserProfile)

```typescript
interface UserProfile {
  gender: "male" | "female" | "other";
  ageGroup: "<18" | "18-23" | "23+";
  height: string;              // 身高(cm)，如 "170"
  weight: string;              // 体重(kg)，如 "60"
  tags: string[];              // 最多8个标签
  description: string;          // 最长200字
}
```

**示例数据：**
```json
{
  "gender": "female",
  "ageGroup": "18-23",
  "height": "165",
  "weight": "52",
  "tags": ["Sub", "温柔", "小清新", "浪漫", "慢热"],
  "description": "喜欢文艺浪漫的剧情，希望遇到温柔的故事伙伴"
}
```

### 2. 故事结构 (Story)

```typescript
interface Story {
  title: string;
  background: string;
  maleRole: Role;      // 男生角色
  femaleRole: Role;    // 女生角色
}

interface Role {
  name: string;
  description: string;
  personality: string;
}
```

**示例数据：**
```json
{
  "title": "午夜咖啡馆的邂逅",
  "background": "在一个下着雨的深夜，一家24小时营业的咖啡馆成为了两个陌生人的避风港。窗外霓虹灯闪烁，店内只有柔和的爵士乐在流淌。",
  "maleRole": {
    "name": "夜行者",
    "description": "一位神秘的旅行作家",
    "personality": "善于倾听，充满好奇心，总是在寻找下一个故事"
  },
  "femaleRole": {
    "name": "失眠者",
    "description": "一位无法入睡的画家",
    "personality": "敏感细腻，用艺术表达内心，渴望被理解"
  }
}
```

**角色分配规则：**
- 男生用户 → 自动分配 maleRole（角色A）
- 女生用户 → 自动分配 femaleRole（角色B）
- 其他性别 → 随机分配

### 3. 匹配请求 (MatchRequest)

```typescript
interface MatchRequest {
  userId: string;              // 用户唯一标识（匿名ID）
  profile: UserProfile;        // 用户资料
  story: Story;                // 用户选择的故事
  selectedRole: "A" | "B";     // 用户选择的角色
  timestamp: number;           // 请求时间戳
}
```

### 4. 聊天消息 (ChatMessage)

```typescript
interface ChatMessage {
  id: string;                  // 消息ID
  roomId: string;              // 聊天室ID
  role: "A" | "B" | "system";  // 发送者角色，system为AI故事线索
  message: string;             // 消息内容
  timestamp: number;           // 发送时间
  isStoryClue?: boolean;       // 是否为故事线索
}
```

**示例数据：**
```json
{
  "id": "msg_001",
  "roomId": "room_123456",
  "role": "A",
  "message": "你好，初次见面",
  "timestamp": 1737043200000
}
```

**AI故事线索消息示例：**
```json
{
  "id": "clue_001",
  "roomId": "room_123456",
  "role": "system",
  "message": "突然，咖啡馆的灯光闪烁了一下，播放的音乐突然变成了一首陌生的歌曲...",
  "timestamp": 1737043250000,
  "isStoryClue": true
}
```

### 5. AI故事线索生成规则

**触发条件：**
1. **故事种子（开场）**：聊天室创建时自动生成
2. **对话轮数触发**：首次5轮对话后触发，之后每次间隔翻倍（10轮→20轮→40轮...）
3. **沉默触发**：双方20秒无消息交互（最多触发3次）

**故事种子：**
- 在聊天室创建时立即生成
- 基于故事背景（story.background）
- 作为第一条系统消息显示
- 为角色扮演提供开场引导
- 在聊天室侧边栏/抽屉中可随时查看

**对话轮数触发规则：**
- 第1次：5轮对话后触发（累计5轮）
- 第2次：再过10轮触发（累计15轮）
- 第3次：再过20轮触发（累计35轮）
- 第4次：再过40轮触发（累计75轮）
- 以此类推，间隔呈倍数递增

**限制规则：**
- 沉默触发的线索最多生成3次，避免过度干扰
- 对话轮数触发的线索无次数限制
- 用户每次发送消息会重置沉默计时器

**实现方式：**
- 故事种子：后端在匹配成功创建聊天室时生成
- 对话轮数和沉默触发：前端跟踪，后端生成线索
- 所有线索以系统消息形式发送到聊天室

**建议后端实现：**
```javascript
// 创建聊天室时生成故事种子
function createChatRoom(userA, userB, story) {
  const room = {
    id: generateRoomId(),
    story: story,
    userA: userA,
    userB: userB,
    created_at: Date.now()
  };
  
  // 生成故事种子
  const storySeed = generateStorySeed(story);
  const seedMessage = {
    id: `seed_${room.id}`,
    roomId: room.id,
    role: 'system',
    message: storySeed,
    timestamp: Date.now(),
    isStoryClue: true
  };
  
  // 保存聊天室和故事种子
  saveRoom(room);
  saveMessage(seedMessage);
  
  return room;
}

function generateStorySeed(story) {
  const templates = [
    `${story.background}\n\n你们的目光在此刻交汇，空气中弥漫着一种微妙的气氛...`,
    `${story.background}\n\n命运的齿轮在此刻开始转动，一段全新的故事即将展开...`,
    `${story.background}\n\n这一刻，时间仿佛静止了，只有你们两人的存在变得如此真实...`
  ];
  
  return templates[Math.floor(Math.random() * templates.length)];
}

// 监控聊天室活跃度
function monitorChatActivity(roomId) {
  let messageCount = 0;
  let lastMessageTime = Date.now();
  let silenceTriggerCount = 0;
  
  // 监听新消息
  onMessage(roomId, (message) => {
    messageCount++;
    lastMessageTime = Date.now();
    
    // 对话轮数触发线索
    if (messageCount === 5) {
      generateAndSendStoryClue(roomId, 'conversation');
    } else if (messageCount === 15) {
      generateAndSendStoryClue(roomId, 'conversation');
    } else if (messageCount === 35) {
      generateAndSendStoryClue(roomId, 'conversation');
    } else if (messageCount === 75) {
      generateAndSendStoryClue(roomId, 'conversation');
    }
  });
  
  // 检查沉默时间
  setInterval(() => {
    const silenceDuration = Date.now() - lastMessageTime;
    if (silenceDuration >= 20000 && messageCount > 0 && silenceTriggerCount < 3) {
      generateAndSendStoryClue(roomId, 'silence');
      lastMessageTime = Date.now(); // 重置，避免重复触发
      silenceTriggerCount++;
    }
  }, 5000); // 每5秒检查一次
}

function generateAndSendStoryClue(roomId, trigger) {
  const clue = {
    id: `clue_${Date.now()}`,
    roomId: roomId,
    role: 'system',
    message: generateClueBasedOnStory(roomId, trigger),
    timestamp: Date.now(),
    isStoryClue: true
  };
  
  broadcastToRoom(roomId, clue);
}
```

---

## 需要的后端接口

### 1. 匹配系统

#### POST `/api/match/request`
**功能：** 用户请求匹配

**请求体：**
```json
{
  "profile": {
    "gender": "female",
    "ageGroup": "18-23",
    "height": "165",
    "weight": "52",
    "tags": ["Sub", "温柔", "小清新"],
    "description": "喜欢文艺浪漫的剧情"
  },
  "story": {
    "title": "午夜咖啡馆的邂逅",
    "background": "...",
    "maleRole": {...},
    "femaleRole": {...}
  },
  "selectedRole": "B"
}
```

**响应：**
```json
{
  "success": true,
  "data": {
    "userId": "anonymous_abc123",
    "matchId": "match_xyz789",
    "status": "waiting"
  }
}
```

#### GET `/api/match/status/:matchId`
**功能：** 查询匹配状态（轮询或长轮询）

**响应（等待中）：**
```json
{
  "success": true,
  "data": {
    "status": "waiting",
    "waitTime": 30
  }
}
```

**响应（匹配成功）：**
```json
{
  "success": true,
  "data": {
    "status": "matched",
    "roomId": "room_123456",
    "story": {...},
    "yourRole": "B",
    "partnerRole": "A"
  }
}
```

#### POST `/api/match/cancel`
**功能：** 消匹配

**请求体：**
```json
{
  "matchId": "match_xyz789"
}
```

---

### 2. 实时聊天系统

**推荐使用 WebSocket 或 Supabase Realtime**

#### WebSocket 连接
```
ws://your-domain/chat/:roomId
```

#### 消息格式

**客户端发送：**
```json
{
  "type": "message",
  "role": "B",
  "message": "你好，初次见面"
}
```

**服务端广播：**
```json
{
  "type": "message",
  "data": {
    "id": "msg_001",
    "role": "B",
    "message": "你好，初次见面",
    "timestamp": 1737043200000
  }
}
```

**连接状态：**
```json
{
  "type": "user_joined",
  "data": {
    "role": "A",
    "timestamp": 1737043200000
  }
}
```

```json
{
  "type": "user_left",
  "data": {
    "role": "A",
    "reason": "disconnected"
  }
}
```

#### REST API (备用方案)

**POST `/api/chat/send`**
```json
{
  "roomId": "room_123456",
  "role": "B",
  "message": "你好，初次见面"
}
```

**GET `/api/chat/messages/:roomId?since=timestamp`**
```json
{
  "success": true,
  "data": [
    {
      "id": "msg_001",
      "role": "A",
      "message": "你好",
      "timestamp": 1737043200000
    }
  ]
}
```

---

### 3. 聊天室管理

#### POST `/api/room/leave`
**功能：** 用户主动离开聊天室

**请求体：**
```json
{
  "roomId": "room_123456",
  "role": "B"
}
```

**响应：**
```json
{
  "success": true,
  "message": "已退出聊天室"
}
```

---

## 匹配算法建议

### 基础匹配规则

1. **相同故事优先**
   - 优先匹配选择相同故事的用户
   - 确保角色互补（一个选A，一个选B）

2. **偏好标签匹配**
   - Dom ↔ Sub（互补匹配）
   - 重口 ↔ 重口（相似匹配）
   - 小清新 ↔ 小清新（相似匹配）

3. **年龄段匹配**
   - <18 只能匹配 <18
   - 18-23 和 23+ 可以互相匹配

4. **等待时间策略**
   - 0-30秒：严格匹配（故事+标签+年龄）
   - 30-60秒：放宽标签要求
   - 60秒+：只要求相同故事和角色互补

### 匹配优先级算分示例

```javascript
function calculateMatchScore(user1, user2) {
  let score = 0;
  
  // 相同故事：+100分
  if (user1.story.title === user2.story.title) {
    score += 100;
  }
  
  // 角色互补：+50分
  if (user1.selectedRole !== user2.selectedRole) {
    score += 50;
  }
  
  // 标签匹配：每个共同标签 +10分
  const commonTags = user1.profile.tags.filter(tag => 
    user2.profile.tags.includes(tag)
  );
  score += commonTags.length * 10;
  
  // Dom-Sub互补：+30分
  const user1IsDom = user1.profile.tags.some(t => t.includes('Dom'));
  const user1IsSub = user1.profile.tags.some(t => t.includes('Sub'));
  const user2IsDom = user2.profile.tags.some(t => t.includes('Dom'));
  const user2IsSub = user2.profile.tags.some(t => t.includes('Sub'));
  
  if ((user1IsDom && user2IsSub) || (user1IsSub && user2IsDom)) {
    score += 30;
  }
  
  // 年龄段匹配：+20分
  if (user1.profile.ageGroup === user2.profile.ageGroup) {
    score += 20;
  }
  
  return score;
}
```

---

## 数据库设计建议

### 表结构（以Supabase为例）

#### 1. users 表
```sql
CREATE TABLE users (
  id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  anonymous_id TEXT UNIQUE NOT NULL,
  created_at TIMESTAMP DEFAULT NOW(),
  last_active TIMESTAMP DEFAULT NOW()
);
```

#### 2. user_profiles 表
```sql
CREATE TABLE user_profiles (
  id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  user_id UUID REFERENCES users(id) ON DELETE CASCADE,
  gender TEXT NOT NULL CHECK (gender IN ('male', 'female', 'other')),
  age_group TEXT NOT NULL CHECK (age_group IN ('<18', '18-23', '23+')),
  height TEXT,
  weight TEXT,
  tags JSONB DEFAULT '[]',
  description TEXT,
  created_at TIMESTAMP DEFAULT NOW()
);
```

#### 3. match_queue 表
```sql
CREATE TABLE match_queue (
  id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  user_id UUID REFERENCES users(id) ON DELETE CASCADE,
  profile JSONB NOT NULL,
  story JSONB NOT NULL,
  selected_role TEXT NOT NULL CHECK (selected_role IN ('A', 'B')),
  status TEXT DEFAULT 'waiting' CHECK (status IN ('waiting', 'matched', 'cancelled')),
  created_at TIMESTAMP DEFAULT NOW(),
  matched_at TIMESTAMP,
  room_id UUID
);

CREATE INDEX idx_match_queue_status ON match_queue(status);
CREATE INDEX idx_match_queue_created_at ON match_queue(created_at);
```

#### 4. chat_rooms 表
```sql
CREATE TABLE chat_rooms (
  id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  story JSONB NOT NULL,
  user_a_id UUID REFERENCES users(id),
  user_b_id UUID REFERENCES users(id),
  status TEXT DEFAULT 'active' CHECK (status IN ('active', 'ended')),
  created_at TIMESTAMP DEFAULT NOW(),
  ended_at TIMESTAMP
);
```

#### 5. chat_messages 表
```sql
CREATE TABLE chat_messages (
  id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  room_id UUID REFERENCES chat_rooms(id) ON DELETE CASCADE,
  role TEXT NOT NULL CHECK (role IN ('A', 'B', 'system')),
  message TEXT NOT NULL,
  created_at TIMESTAMP DEFAULT NOW()
);

CREATE INDEX idx_messages_room_id ON chat_messages(room_id);
CREATE INDEX idx_messages_created_at ON chat_messages(created_at);
```

---

## 安全与隐私

### 1. 匿名性保护
- ❌ 不存储任何真实个人信息
- ✅ 使用随机生成的匿名ID
- ✅ 聊天记录仅保留24小时
- ✅ 用户无法查看对方任何个人信息

### 2. 数据清理策略
```sql
-- 自动删除24小时前的聊天记录
DELETE FROM chat_messages 
WHERE created_at < NOW() - INTERVAL '24 hours';

-- 自动清理已结束的聊天室
DELETE FROM chat_rooms 
WHERE status = 'ended' 
AND ended_at < NOW() - INTERVAL '24 hours';

-- 清理超时的匹配请求
UPDATE match_queue 
SET status = 'cancelled' 
WHERE status = 'waiting' 
AND created_at < NOW() - INTERVAL '5 minutes';
```

### 3. 内容审核
- 建议集成敏感词过滤
- 用户举报功能
- 自动断开含违规内容的聊天

---

## 性能优化建议

### 1. 匹配系统
- 使用Redis缓存等待中的匹配请求
- 实现高效的匹配算法（如优先队列）
- 考虑分片策略处理高并发

### 2. 聊天系统
- WebSocket服务器集群
- 消息队列处理（如RabbitMQ）
- CDN加速静态资源

### 3. 数据库
- 适当的索引优化
- 读写分离
- 连接池管理

---

## 错误处理

### 错误码定义

| 错误码 | 说明 | HTTP状态码 |
|-------|------|-----------|
| `MATCH_TIMEOUT` | 匹配超时 | 408 |
| `ROOM_NOT_FOUND` | 聊天室不存在 | 404 |
| `USER_NOT_IN_ROOM` | 用户不在此聊天室 | 403 |
| `MESSAGE_TOO_LONG` | 消息过长 | 400 |
| `INVALID_PROFILE` | 用户资料无效 | 400 |
| `RATE_LIMIT` | 请求频率过高 | 429 |

### 错误响应格式
```json
{
  "success": false,
  "error": {
    "code": "MATCH_TIMEOUT",
    "message": "未能找到合适的匹配对象，请重试"
  }
}
```

---

## 测试场景

### 1. 匹配测试
- ✅ 两个用户选择相同故事和不同角色，��成功匹配
- ✅ 两个用户选择相同故事和相同角色，不应匹配
- ✅ 匹配超时应返回超时状态
- ✅ 取消匹配应正确清理队列

### 2. 聊天测试
- ✅ 消息实时推送
- ✅ 一方断线另一方收到通知
- ✅ 消息顺序正确
- ✅ 历史消息加载

### 3. 边界情况
- ❌ 用户在等待时刷新页面
- ❌ 网络不稳定导致消息丢失
- ❌ 同一用户多次发起匹配
- ❌ 聊天室已结束但用户尝试发送消息

---

## 部署建议

### 推荐技术栈
- **数据库：** Supabase (PostgreSQL + Realtime)
- **WebSocket：** Socket.io 或 Supabase Realtime
- **缓存：** Redis
- **后端框架：** Node.js + Express / Python + FastAPI
- **消息队列：** RabbitMQ (可选)

### 环境变量
```env
DATABASE_URL=postgresql://...
REDIS_URL=redis://...
SUPABASE_URL=https://...
SUPABASE_ANON_KEY=...
SUPABASE_SERVICE_KEY=...
JWT_SECRET=...
WEBSOCKET_PORT=3001
```

---

## 联系方式

如有任何问题，请联系前端团队：
- 📧 Email: frontend@example.com
- 💬 Slack: #feiyue-dev
- 📖 Wiki: [项目Wiki链接]

**文档版本：** v1.0  
**最后更新：** 2026-01-16