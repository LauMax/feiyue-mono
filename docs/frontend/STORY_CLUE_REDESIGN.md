# 剧情提醒系统重构设计文档

## 📋 文档信息
- **创建日期**: 2026年1月28日
- **目标版本**: v2.0
- **优先级**: 高
- **涉及模块**: 聊天系统、剧情管理

## 🔍 问题分析

### 当前问题
1. **同步问题**: 两个用户客户端独立生成剧情线索，内容和时机不一致
2. **随机性问题**: 相同触发条件下生成不同内容，影响用户体验和沉浸感
3. **重复触发**: 两边可能同时触发，导致剧情线索过多
4. **状态不同步**: 对话轮数统计存在网络延迟导致的误差

### 影响范围
- 用户体验: 剧情线索不一致影响沉浸感
- 系统稳定性: 前端逻辑复杂，容易出现状态错乱
- 可维护性: 剧情逻辑分散在多个客户端

## 🎯 解决方案

### 核心思路
**将剧情触发和内容生成统一移至后端管理，确保两个用户体验完全一致**

### 设计原则
1. **一致性**: 同一房间内用户收到相同的剧情线索
2. **实时性**: 剧情线索及时推送给双方用户
3. **可控性**: 后端可以灵活调整触发规则和内容
4. **扩展性**: 支持后续增加更多剧情元素

## 🏗 技术架构

### 整体流程
```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   用户A发送消息   │───▶│   后端接收处理    │───▶│   推送给用户A&B   │
└─────────────────┘    │                │    └─────────────────┘
                       │  • 更新对话状态  │
┌─────────────────┐    │  • 检查触发条件  │    ┌─────────────────┐
│   用户B发送消息   │───▶│  • 生成剧情线索  │───▶│   存储到数据库    │
└─────────────────┘    │  • 广播给双方    │    └─────────────────┘
                       └─────────────────┘
```

## 📡 API接口设计

### 1. 消息发送接口 (现有接口增强)

**POST /api/chat/send**

**请求参数** (保持不变)
```typescript
{
  room_id: string,
  user_id: string,
  message: string
}
```

**响应格式** (新增字段)
```typescript
{
  success: boolean,
  data: {
    message: ChatMessage,           // 用户消息
    storyClue?: StoryClueMessage,   // 可能触发的剧情线索
    roomStats?: {                   // 房间统计信息
      totalMessages: number,
      conversationRounds: number,
      lastActivityTime: number
    }
  },
  error?: any
}
```

### 2. 剧情线索消息格式

**StoryClueMessage 类型定义**
```typescript
interface StoryClueMessage {
  id: string,                    // 唯一ID
  roomId: string,               // 房间ID
  role: 'system',               // 固定为system
  message: string,              // 剧情内容
  timestamp: number,            // 时间戳
  isStoryClue: true,           // 标识为剧情线索
  triggerType: 'rounds' | 'silence' | 'manual',  // 触发类型
  triggerData?: {              // 触发相关数据
    roundCount?: number,       // 触发时的轮数
    silenceSeconds?: number    // 沉默秒数
  }
}
```

### 3. 获取聊天历史接口 (现有接口保持不变)

**GET /api/chat/messages/:roomId**

响应中的系统消息将包含 `isStoryClue: true` 标识

## 🔧 后端实现要求

### 1. 数据存储

#### 房间状态表 (room_states)
```sql
CREATE TABLE room_states (
  room_id VARCHAR(50) PRIMARY KEY,
  total_messages INT DEFAULT 0,
  conversation_rounds DECIMAL(3,1) DEFAULT 0,
  last_activity_time BIGINT,
  last_clue_rounds DECIMAL(3,1) DEFAULT 0,
  next_clue_interval INT DEFAULT 5,
  silence_clue_count INT DEFAULT 0,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```

#### 剧情线索表 (story_clues)
```sql
CREATE TABLE story_clues (
  id VARCHAR(50) PRIMARY KEY,
  room_id VARCHAR(50) NOT NULL,
  message TEXT NOT NULL,
  trigger_type ENUM('rounds', 'silence', 'manual') NOT NULL,
  trigger_data JSON,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  INDEX idx_room_id (room_id),
  FOREIGN KEY (room_id) REFERENCES rooms(id)
);
```

### 2. 触发规则实现

#### A. 对话轮数触发
```python
def check_rounds_trigger(room_state):
    """检查是否达到轮数触发条件"""
    rounds_since_last = room_state.conversation_rounds - room_state.last_clue_rounds
    
    if rounds_since_last >= room_state.next_clue_interval:
        # 生成剧情线索
        clue = generate_story_clue(room_state.room_id, 'rounds')
        
        # 更新状态
        room_state.last_clue_rounds = room_state.conversation_rounds
        room_state.next_clue_interval *= 2  # 间隔翻倍
        
        return clue
    return None
```

#### B. 沉默时间触发
```python
def check_silence_trigger(room_state):
    """检查沉默时间触发 (后台定时任务)"""
    current_time = time.time() * 1000
    silence_duration = current_time - room_state.last_activity_time
    
    if (silence_duration >= 20000 and 
        room_state.silence_clue_count < 3 and
        room_state.total_messages > 1):
        
        # 生成沉默类型线索
        clue = generate_silence_clue(room_state.room_id)
        room_state.silence_clue_count += 1
        
        return clue
    return None
```

### 3. 剧情内容生成

#### 确定性生成算法
```python
def generate_story_clue(room_id: str, trigger_type: str) -> str:
    """基于房间ID和触发类型生成确定性内容"""
    
    clue_templates = [
        "突然，咖啡馆的灯光闪烁了一下，播放的音乐突然变成了一首陌生的歌曲...",
        "窗外的雨声渐渐停了，一道彩虹若隐若现在城市的天际线上...",
        "服务员端来了两杯你们没有点过的饮品，说是'店长特别招待'...",
        "你注意到对方手上戴着一个特别的饰品，似乎和故事有某种联系...",
        "远处传来了微弱的音乐声，似乎在召唤着你们走向某个方向...",
        "空气中弥漫着一股淡淡的香味，勾起了某段遗忘的记忆...",
        "时间仿佛静止了，周围的一切都变得模糊，只有彼此的存在变得清晰...",
        "一本神秘的笔记本出现在了桌上，翻开第一页写着你们的名字..."
    ]
    
    # 使用房间ID生成确定性索引，确保同一房间同一轮次生成相同内容
    room_hash = hashlib.md5(room_id.encode()).hexdigest()
    clue_count = get_room_clue_count(room_id)  # 获取当前房间已生成的线索数量
    
    seed = int(room_hash[:8], 16) + clue_count
    clue_index = seed % len(clue_templates)
    
    return clue_templates[clue_index]
```

### 4. 消息广播机制

```python
async def broadcast_message_and_clue(room_id: str, user_message: ChatMessage, story_clue: Optional[StoryClueMessage]):
    """向房间内所有用户广播消息和可能的剧情线索"""
    
    room_users = await get_room_users(room_id)
    
    for user_id in room_users:
        # 发送用户消息
        await send_to_user(user_id, {
            'type': 'new_message',
            'data': user_message
        })
        
        # 发送剧情线索 (如果有)
        if story_clue:
            await send_to_user(user_id, {
                'type': 'story_clue',
                'data': story_clue
            })
```

## 💻 前端改动要求

### 1. 移除现有剧情逻辑

#### 需要删除的代码 (ChatRoom.tsx)
- `generateStoryClue()` 函数
- `conversationRounds` 状态管理
- `lastMessageTime` 和沉默检测逻辑
- `useEffect` 中的轮数和沉默触发逻辑
- `generateStorySeed()` 中的个性化逻辑 (保留基础故事种子)

### 2. 简化消息处理

#### 修改后的消息发送逻辑
```typescript
const handleSend = async () => {
  if (!inputValue.trim() || isSending) return;

  setIsSending(true);
  const messageText = inputValue;
  
  // 立即添加到本地 (乐观更新)
  const newMessage: Message = {
    id: Date.now().toString(),
    sender: role,
    content: messageText,
    timestamp: new Date()
  };
  setMessages(prev => [...prev, newMessage]);
  setInputValue("");

  try {
    const result = await api.chatSend({
      roomId,
      userId,
      message: messageText,
    });

    if (result.success) {
      // 如果后端返回了剧情线索，添加到消息列表
      if (result.data?.storyClue) {
        const clueMessage: Message = {
          id: result.data.storyClue.id,
          sender: "system",
          content: result.data.storyClue.message,
          timestamp: new Date(result.data.storyClue.timestamp),
          isStoryClue: true
        };
        setMessages(prev => [...prev, clueMessage]);
      }
    }
  } catch (error) {
    console.error("发送消息出错:", error);
    // 移除失败的乐观更新
    setMessages(prev => prev.filter(m => m.id !== newMessage.id));
  } finally {
    setIsSending(false);
  }
};
```

### 3. 优化轮询逻辑

```typescript
useEffect(() => {
  if (!roomId) return;

  const pollMessages = async () => {
    try {
      const result = await api.chatMessages(roomId);
      if (result.success && result.data) {
        const serverMessages = Array.isArray(result.data) ? result.data : result.data.messages;
        
        // 筛选新消息 (包括系统剧情线索)
        const existingIds = new Set(messages.map(m => m.id));
        const newMessages = serverMessages.filter(msg => !existingIds.has(msg.id));
        
        if (newMessages.length > 0) {
          const formattedMessages = newMessages.map(msg => ({
            id: msg.id,
            sender: msg.role,
            content: msg.message,
            timestamp: new Date(msg.timestamp),
            isStoryClue: msg.isStoryClue || false
          }));
          
          setMessages(prev => [...prev, ...formattedMessages]);
        }
      }
    } catch (error) {
      console.error("轮询消息失败:", error);
    }
  };

  const pollInterval = setInterval(pollMessages, 2000);
  return () => clearInterval(pollInterval);
}, [roomId, messages]);
```

## ⚙️ 沉默检测改进

### 后台定时任务 (推荐方案)

```python
# 定时任务，每30秒检查一次所有活跃房间
async def check_silence_triggers():
    """检查所有房间的沉默触发条件"""
    active_rooms = await get_active_rooms()
    
    for room in active_rooms:
        clue = check_silence_trigger(room.state)
        if clue:
            await broadcast_story_clue(room.id, clue)
            await save_story_clue(clue)  # 持久化
```

### WebSocket 实时推送 (可选增强)

```python
# 如果使用WebSocket，可以实时推送剧情线索
async def on_story_clue_triggered(room_id: str, story_clue: StoryClueMessage):
    """剧情线索触发时的WebSocket推送"""
    await websocket_manager.broadcast_to_room(room_id, {
        'type': 'story_clue',
        'data': story_clue
    })
```

## 📅 实施计划

### Phase 1: 后端开发 (预计3天)
- [ ] 创建数据库表结构
- [ ] 实现房间状态管理
- [ ] 实现触发规则检查
- [ ] 实现确定性内容生成
- [ ] 修改消息发送API
- [ ] 添加后台定时任务

### Phase 2: 前端重构 (预计2天)
- [ ] 移除现有剧情生成逻辑
- [ ] 简化消息发送处理
- [ ] 优化消息轮询逻辑
- [ ] 更新类型定义

### Phase 3: 测试验证 (预计2天)
- [ ] 单元测试
- [ ] 集成测试
- [ ] 用户体验测试
- [ ] 性能测试

### Phase 4: 部署上线 (预计1天)
- [ ] 数据库迁移
- [ ] 灰度发布
- [ ] 监控验证

## 🔍 验收标准

### 功能验收
- [ ] 两个用户在同一房间内收到完全相同的剧情线索
- [ ] 剧情线索触发时机准确 (5轮→10轮→20轮→40轮)
- [ ] 沉默20秒触发生效，最多3次
- [ ] 聊天历史包含所有剧情线索
- [ ] 性能无明显影响

### 技术验收
- [ ] 代码通过CodeReview
- [ ] 单元测试覆盖率 > 80%
- [ ] API响应时间 < 500ms
- [ ] 数据库查询优化
- [ ] 错误处理完善

## 📈 监控指标

### 关键指标
- 剧情线索触发成功率
- 消息发送延迟
- 房间状态更新频率
- 用户满意度反馈

### 告警设置
- API错误率 > 1%
- 响应时间 > 1s
- 数据库连接异常
- 定时任务执行失败

## 🚀 后续优化

### 可能的增强功能
1. **智能剧情生成**: 基于对话内容生成更贴合的剧情线索
2. **个性化触发**: 根据用户喜好调整触发频率
3. **多样化内容**: 增加更多剧情线索模板
4. **实时推送**: 考虑使用WebSocket替代轮询
5. **剧情分支**: 支持用户选择影响后续剧情发展

---

**文档版本**: v1.0  
**最后更新**: 2026年1月28日  
**负责人**: 开发团队  
**审批人**: 产品经理