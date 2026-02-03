# 绯悦 - 前端实现文档

## 技术栈

- **框架：** React 18 + TypeScript
- **样式：** Tailwind CSS v4
- **构建工具：** Vite
- **UI组件：** 自定义组件库
- **图标：** Lucide React
- **动画：** CSS Transitions + Tailwind

---

## 项目结构

```
src/
├── app/
│   ├── App.tsx                 # 主应用组件，状态管理
│   └── components/
│       ├── HomePage.tsx        # 首页
│       ├── UserProfile.tsx     # 用户资料设置页面（新）
│       ├── StoryGenerator.tsx  # AI故事生成器
│       ├── RoleSelection.tsx   # 角色选择
│       ├── WaitingRoom.tsx     # 匹配等待室
│       ├── ChatRoom.tsx        # 聊天室
│       └── ui/                 # 基础UI组件
│           ├── button.tsx
│           ├── card.tsx
│           ├── input.tsx
│           ├── textarea.tsx
│           ├── label.tsx
│           ├── badge.tsx
│           └── drawer.tsx
├── styles/
│   ├── theme.css              # 主题样式
│   └── fonts.css              # 字体导入
└── imports/                   # 资源文件
```

---

## 核心组件说明

### 1. App.tsx - 主应用

**功能：** 管理全局状态和页面流转

**状态定义：**
```typescript
type AppState = "home" | "profile" | "generate" | "select-role" | "waiting" | "chatting";

// 全局状态
const [state, setState] = useState<AppState>("home");
const [userProfile, setUserProfile] = useState<UserProfileData | null>(null);
const [story, setStory] = useState<Story | null>(null);
const [selectedRole, setSelectedRole] = useState<"A" | "B" | null>(null);
```

**页面流转：**
```
home → profile → generate → select-role → waiting → chatting
```

---

### 2. UserProfile.tsx - 用户资料设置

**功能：** 收集用户偏好信息

**数据结构：**
```typescript
export type UserProfileData = {
  gender: "male" | "female" | "other" | null;
  ageGroup: "<18" | "18-23" | "23+" | null;
  height: string;              // 身高(cm)
  weight: string;              // 体重(kg)
  tags: string[];
  description: string;
};
```

**预设标签：**
```javascript
const PRESET_TAGS = [
  "Dom", "Sub", "Switch",
  "温柔", "霸道", "傲娇",
  "小清新", "重口", "剧情向",
  "浪漫", "刺激", "慢热",
  "文艺", "野性", "神秘"
];
```

**验证规则：**
- 性别和年龄段为必填项
- 身高和体重为可选项
- 标签最多8个
- 描述最长200字
- 自定义标签最长10字

---

### 3. StoryGenerator.tsx - 故事生成器

**功能：** 根据用户偏好生成个性化故事

**故事模板：** 共8个预设故事
- 前5个：温柔浪漫类（小清新）
- 后3个：刺激重口类

**智能匹配逻辑：**
```javascript
function generateStoryBasedOnProfile(profile: UserProfileData): Story {
  const hasIntenseTag = profile.tags.some(tag => 
    tag === "重口" || tag === "刺激" || tag === "野性"
  );
  const hasSoftTag = profile.tags.some(tag => 
    tag === "小清新" || tag === "温柔" || tag === "浪漫"
  );
  
  if (hasIntenseTag) {
    // 推荐故事 6-8
  } else if (hasSoftTag) {
    // 推荐故事 1-5
  }
}
```

**动画效果：**
- 粒子动画（30个星光粒子）
- 渐进式文字显示
- 3秒生成动画

---

### 4. RoleSelection.tsx - 角色选择

**功能：** 让用户选择扮演角色A或角色B

**交互：**
- 卡片悬停效果
- 选中后高亮显示
- 角色信息展示（名字、描述、性格）

---

### 5. WaitingRoom.tsx - 等待室

**功能：** 匹配等待中的视觉反馈

**动画效果：**
- 脉冲圆圈动画
- 加载点动画（...）
- 显示当前选择的角色信息

**后端集成点：**
```typescript
// 需要轮询匹配状态
useEffect(() => {
  const checkMatchStatus = async () => {
    const response = await fetch(`/api/match/status/${matchId}`);
    const data = await response.json();
    
    if (data.status === 'matched') {
      // 跳转到聊天室
      onMatchSuccess(data.roomId);
    }
  };
  
  const interval = setInterval(checkMatchStatus, 2000);
  return () => clearInterval(interval);
}, [matchId]);
```

---

### 6. ChatRoom.tsx - 聊天室

**功能：** 实时双人角色扮演聊天

**核心特性：**
- 实时消息推送
- 角色颜色区分（A=蓝色，B=粉色）
- 自动滚动到最新消息
- 移动端抽屉式菜单
- **故事种子**：聊天开始时显示基于故事背景的引导
- **AI故事线索**：每5轮对话或20秒沉默自动生成
- **对方信息展示**：查看匹配对象的个人资料

**数据结构：**
```typescript
interface Message {
  id: string;
  sender: "A" | "B" | "system";  // system为故事线索
  content: string;
  timestamp: Date;
  isStoryClue?: boolean;
}
```

**故事种子生成：**
```typescript
const generateStorySeed = (story: Story) => {
  const seeds = [
    `${story.background}\n\n你们的目光在此刻交汇，空气中弥漫着一种微妙的气氛...`,
    `${story.background}\n\n命运的齿轮在此刻开始转动，一段全新的故事即将展开...`,
    // ... 更多模板
  ];
  return seeds[Math.floor(Math.random() * seeds.length)];
};
```

**AI线索触发逻辑：**
```typescript
// 对话轮数触发（倍数递增间隔）
const [nextClueInterval, setNextClueInterval] = useState(5); // 初始间隔5轮
const [lastClueRounds, setLastClueRounds] = useState(0); // 上次触发时的轮数

useEffect(() => {
  const roundsSinceLastClue = conversationRounds - lastClueRounds;
  
  // 检查是否达到触发条件
  if (conversationRounds > 0 && roundsSinceLastClue >= nextClueInterval) {
    const clueMessage = {
      id: `clue-${Date.now()}`,
      sender: "system",
      content: generateStoryClue(),
      timestamp: new Date(),
      isStoryClue: true
    };
    setMessages(prev => [...prev, clueMessage]);
    setLastClueRounds(conversationRounds); // 记录触发时的轮数
    setNextClueInterval(prevInterval => prevInterval * 2); // 间隔翻倍：5 -> 10 -> 20 -> 40
  }
}, [conversationRounds, lastClueRounds, nextClueInterval]);

// 沉默触发（20秒，最多3次）
const [silenceClueCount, setSilenceClueCount] = useState(0);

useEffect(() => {
  if (silenceClueCount >= 3) return; // 达到3次后停止
  
  const silenceTimer = setTimeout(() => {
    const timeSinceLastMessage = Date.now() - lastMessageTime;
    if (timeSinceLastMessage >= 20000 && messages.length > 1 && silenceClueCount < 3) {
      // 生成沉默触发的故事线索
      setSilenceClueCount(prev => prev + 1);
    }
  }, 20000);
  
  return () => clearTimeout(silenceTimer);
}, [lastMessageTime, messages.length, silenceClueCount]);
```

**触发时机示例：**
- 5轮对话 → 第1次触发
- 15轮对话 → 第2次触发（+10轮）
- 35轮对话 → 第3次触发（+20轮）
- 75轮对话 → 第4次触发（+40轮）
- 以此类推

**对方信息展示：**
- 桌面端：左侧边栏显示对方性别、年龄、身高、体重、标签、描述
- 移动端：抽屉菜单中显示
- 实时展示，无需额外请求

**后端集成点（WebSocket）：**
```typescript
useEffect(() => {
  const ws = new WebSocket(`ws://your-domain/chat/${roomId}`);
  
  ws.onopen = () => {
    console.log('Connected to chat room');
  };
  
  ws.onmessage = (event) => {
    const data = JSON.parse(event.data);
    
    if (data.type === 'message') {
      setMessages(prev => [...prev, data.data]);
    } else if (data.type === 'story_clue') {
      // 接收后端生成的故事线索
      setMessages(prev => [...prev, data.data]);
    } else if (data.type === 'user_left') {
      // 显示对方离开通知
    }
  };
  
  return () => ws.close();
}, [roomId]);

const handleSendMessage = (message: string) => {
  ws.send(JSON.stringify({
    type: 'message',
    role: role,
    message: message
  }));
  
  // 更新对话轮数和时间
  setConversationRounds(prev => prev + 0.5);
  setLastMessageTime(Date.now());
};
```

---

## 响应式设计

### 断点策略
- **移动端：** < 640px
- **平板：** 640px - 1024px
- **桌面：** > 1024px

### 移动端优化
```css
/* 主要使用 Tailwind 的响应式前缀 */
p-4 sm:p-6        /* 移动端padding 1rem，桌面端 1.5rem */
text-2xl sm:text-3xl  /* 响应式字体大小 */
grid-cols-1 sm:grid-cols-2  /* 响应式网格 */
```

### 触摸优化
- 按钮最小高度：48px (12 Tailwind units)
- 输入框高度：48px
- 点击区域扩大

---

## 样式系统

### 主题配色
```css
/* src/styles/theme.css */
:root {
  /* 渐变背景 */
  background: linear-gradient(135deg, 
    #1e1b4b 0%,      /* slate-900 */
    #581c87 50%,     /* purple-900 */
    #1e1b4b 100%
  );
  
  /* 主色调 */
  --primary-pink: #ec4899;    /* pink-500 */
  --primary-purple: #9333ea;  /* purple-600 */
  
  /* 玻璃态效果 */
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  border: 1px solid rgba(255, 255, 255, 0.2);
}
```

### 常用组合类
```css
/* 渐变按钮 */
.btn-gradient {
  @apply bg-gradient-to-r from-pink-500 to-purple-600 
         hover:from-pink-600 hover:to-purple-700 
         text-white;
}

/* 玻璃态卡片 */
.glass-card {
  @apply bg-white/10 backdrop-blur-lg 
         border border-white/20 rounded-2xl;
}
```

---

## 状态管理方案

### 当前实现（useState）
```typescript
// App.tsx
const [state, setState] = useState<AppState>("home");
const [userProfile, setUserProfile] = useState<UserProfileData | null>(null);
const [story, setStory] = useState<Story | null>(null);
const [selectedRole, setSelectedRole] = useState<"A" | "B" | null>(null);
```

### 未来扩展（建议使用 Context）
```typescript
// contexts/AppContext.tsx
interface AppContextType {
  userProfile: UserProfileData | null;
  story: Story | null;
  selectedRole: "A" | "B" | null;
  roomId: string | null;
  updateProfile: (profile: UserProfileData) => void;
  // ...
}

export const AppContext = createContext<AppContextType>(null!);
```

---

## 后端集成指南

### 1. 匹配流程集成

**UserProfile.tsx → 提交资料**
```typescript
const handleProfileComplete = async (profile: UserProfileData) => {
  setUserProfile(profile);
  setState("generate");
};
```

**RoleSelection.tsx → 开始匹配**
```typescript
const handleRoleSelected = async (role: "A" | "B") => {
  setSelectedRole(role);
  
  // 调用后端匹配API
  const response = await fetch('/api/match/request', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      profile: userProfile,
      story: story,
      selectedRole: role
    })
  });
  
  const data = await response.json();
  setMatchId(data.matchId);
  setState("waiting");
};
```

**WaitingRoom.tsx → 轮询匹配状态**
```typescript
useEffect(() => {
  const checkMatch = async () => {
    const response = await fetch(`/api/match/status/${matchId}`);
    const data = await response.json();
    
    if (data.status === 'matched') {
      setRoomId(data.roomId);
      setState("chatting");
    }
  };
  
  const interval = setInterval(checkMatch, 2000);
  return () => clearInterval(interval);
}, [matchId]);
```

### 2. 聊天系统集成

**ChatRoom.tsx → WebSocket连接**
```typescript
useEffect(() => {
  // 建立WebSocket连接
  const ws = new WebSocket(`${WS_URL}/chat/${roomId}`);
  
  ws.onmessage = (event) => {
    const message = JSON.parse(event.data);
    if (message.type === 'message') {
      setMessages(prev => [...prev, message.data]);
      scrollToBottom();
    }
  };
  
  setWebSocket(ws);
  
  return () => {
    ws.close();
  };
}, [roomId]);
```

**发送消息**
```typescript
const handleSend = () => {
  if (!newMessage.trim() || !websocket) return;
  
  const message = {
    type: 'message',
    role: role,
    message: newMessage.trim()
  };
  
  websocket.send(JSON.stringify(message));
  setNewMessage("");
};
```

---

## 需要的环境变量

创建 `.env` 文件：

```env
# API 端点
VITE_API_URL=http://localhost:3000
VITE_WS_URL=ws://localhost:3001

# Supabase (如果使用)
VITE_SUPABASE_URL=https://your-project.supabase.co
VITE_SUPABASE_ANON_KEY=your-anon-key
```

使用方式：
```typescript
const API_URL = import.meta.env.VITE_API_URL;
const WS_URL = import.meta.env.VITE_WS_URL;
```

---

## 本地开发

### 安装依赖
```bash
npm install
```

### 启动开发服务器
```bash
npm run dev
```

### 构建生产版本
```bash
npm run build
```

### 预览生产版本
```bash
npm run preview
```

---

## 测试建议

### 1. 用户流程测试
- [ ] 从首页到聊天室的完整流程
- [ ] 资料表单验证
- [ ] 角色选择确认
- [ ] 等待匹配超时处理

### 2. 聊天功能测试
- [ ] 消息发送和接收
- [ ] 实时性验证
- [ ] 断线重连
- [ ] 对方离开通知

### 3. 响应式测试
- [ ] iPhone SE (375px)
- [ ] iPhone 12/13 (390px)
- [ ] iPad (768px)
- [ ] Desktop (1920px)

### 4. 边界情况
- [ ] 网络中断
- [ ] 长消息处理
- [ ] 快速连续发送
- [ ] 刷新页面

---

## 性能优化

### 1. 代码分割
```typescript
// 使用 React.lazy 按需加载
const ChatRoom = React.lazy(() => import('./components/ChatRoom'));
```

### 2. 图片优化
- 使用 WebP 格式
- 懒加载图片
- 响应式图片

### 3. 减少重渲染
```typescript
// 使用 React.memo
export const ChatMessage = React.memo(({ message }: Props) => {
  // ...
});

// 使用 useCallback
const handleSend = useCallback(() => {
  // ...
}, [dependencies]);
```

---

## 已知问题和待办事项

### 待实现
- [ ] WebSocket 实时通信
- [ ] 匹配算法集成
- [ ] 用户举报功能
- [ ] 消息历史加载
- [ ] 离线消息处理

### 优化计划
- [ ] 添加加载骨架屏
- [ ] 优化动画性能
- [ ] 添加错误边界
- [ ] 实现状态持久化

---

## 协作流程

### Git 工作流
```bash
# 创建功能分支
git checkout -b feature/your-feature-name

# 提交代码
git add .
git commit -m "feat: 添加用户资料设置页面"

# 推送到远程
git push origin feature/your-feature-name
```

### Commit 规范
- `feat:` 新功能
- `fix:` 修复bug
- `style:` 样式调整
- `refactor:` 重构
- `docs:` 文档更新

---

## 联系方式

**前端团队：**
- 📧 Email: frontend@example.com
- 💬 Slack: #feiyue-frontend
- 📖 文档: [Notion链接]

**版本：** v1.0  
**最后更新：** 2026-01-16