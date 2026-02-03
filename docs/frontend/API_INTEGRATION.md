# 绯悦 - API 集成指南

## 快速开始

### 1. 启动模式选择

项目支持两种模式：

#### Mock 模式（本地开发）
```bash
npm run dev
# 或者显式设置
VITE_API_MODE=mock npm run dev
```

#### Real 模式（真实 API）
```bash
VITE_API_MODE=real npm run dev
```

### 2. 环境配置

项目包含三个环境配置文件：

**`.env`** - 默认配置（生产）
```
VITE_API_MODE=real
VITE_API_URL=http://localhost:8000
```

**`.env.local`** - 本地开发（优先级最高）
```
VITE_API_MODE=mock
```

**`.env.development`** - 开发环境
```
VITE_API_MODE=real
VITE_API_URL=https://dev-api.feiyue.example.com
```

**`.env.production`** - 生产环境
```
VITE_API_MODE=real
VITE_API_URL=https://api.feiyue.com
```

### 3. 切换 API 模式

#### 方法1：修改环境变量
编辑 `.env.local` 文件：
```
# 使用 Mock API（默认）
VITE_API_MODE=mock

# 或使用真实 API
VITE_API_MODE=real
VITE_API_URL=http://localhost:8000
```

#### 方法2：启动时指定
```bash
# 使用 Mock API
VITE_API_MODE=mock npm run dev

# 使用真实 API
VITE_API_MODE=real npm run dev
```

#### 方法3：浏览器控制台切换（仅开发环境）
```javascript
// 切换到 Mock API
__api__.switchToMock();

// 切换到真实 API
__api__.switchToReal();
```

## API 结构

### 文件组织

```
src/api/
├── config.ts      # API 配置
├── http.ts        # HTTP 客户端
├── mock.ts        # Mock API 实现
├── real.ts        # 真实 API 实现
└── index.ts       # API 统一接口
```

### 核心接口

```typescript
// 匹配系统
api.matchRequest(payload)           // 创建匹配请求
api.matchStatus(matchId)            // 查询匹配状态
api.matchCancel(matchId)            // 取消匹配

// 聊天系统
api.chatSend(payload)               // 发送消息
api.chatMessages(roomId)            // 获取聊天历史
api.roomLeave(roomId, userId)       // 离开房间
```

## 使用示例

### 1. 发起匹配

```typescript
import api from '@/api';

const result = await api.matchRequest({
  profile: {
    gender: 'female',
    ageGroup: '18-23',
    height: '165',
    weight: '52',
    tags: ['温柔', '浪漫'],
    description: '...'
  },
  story: {
    title: '午夜咖啡馆的邂逅',
    background: '...',
    maleRole: {...},
    femaleRole: {...}
  },
  selectedRole: 'B'
});

if (result.success) {
  const { userId, matchId } = result.data;
  console.log('匹配请求成功，用户ID:', userId);
}
```

### 2. 轮询查询匹配状态

```typescript
let pollCount = 0;
const maxPolls = 150;  // 5分钟

const pollInterval = setInterval(async () => {
  pollCount++;
  
  const result = await api.matchStatus(matchId);
  
  if (result.data.status === 'matched') {
    clearInterval(pollInterval);
    console.log('匹配成功！', result.data);
    // 进入聊天页面
  } else if (pollCount >= maxPolls) {
    clearInterval(pollInterval);
    console.log('匹配超时');
  }
}, 2000);  // 每2秒查询一次
```

### 3. 发送聊天消息

```typescript
const result = await api.chatSend({
  roomId: 'room_xxxxx',
  userId: 'anon_yyyyy',
  message: '你好，初次见面！'
});

if (result.success) {
  console.log('消息已发送');
}
```

### 4. 获取聊天历史

```typescript
const result = await api.chatMessages('room_xxxxx');

if (result.success) {
  const messages = result.data.messages;
  console.log('聊天记录:', messages);
}
```

### 5. 离开房间

```typescript
const result = await api.roomLeave('room_xxxxx', 'anon_yyyyy');

if (result.success) {
  console.log('已退出聊天室');
}
```

## Mock API 特性

Mock API 用于本地开发和测试，模拟以下行为：

### 匹配流程
- **立即返回** userId 和 matchId
- **3秒后** 自动更新状态为 matched
- **初始化聊天室** 并生成故事种子

### 聊天流程
- **立即返回** 用户消息
- **500-2000ms 后** 生成对方回复
- **支持聊天历史** 查询

### 特点
- ✅ 无网络依赖，完全本地运行
- ✅ 模拟真实 API 响应格式
- ✅ 适合界面测试和演示
- ✅ 可快速切换到真实 API

## 错误处理

所有 API 调用都返回统一的响应格式：

```typescript
interface ApiResponse<T> {
  success: boolean;
  data?: T;
  error?: {
    code: string;
    message: string;
  };
}
```

### 常见错误码

| 错误码 | 说明 |
|-------|------|
| `TIMEOUT` | 请求超时 |
| `NETWORK_ERROR` | 网络错误 |
| `MATCH_TIMEOUT` | 匹配超时 |
| `RATE_LIMIT_EXCEEDED` | 请求过于频繁 |
| `USER_NOT_IN_ROOM` | 用户不在房间中 |

### 错误处理示例

```typescript
const result = await api.matchRequest(payload);

if (!result.success) {
  console.error('错误码:', result.error?.code);
  console.error('错误信息:', result.error?.message);
  
  // 根据错误类型处理
  if (result.error?.code === 'RATE_LIMIT_EXCEEDED') {
    alert('请求过于频繁，请稍后重试');
  }
}
```

## 调试技巧

### 1. 查看当前 API 模式

```javascript
// 在浏览器控制台输入
console.log(import.meta.env.VITE_API_MODE);
console.log(import.meta.env.VITE_API_URL);
```

### 2. 查看 API 日志

项目会在控制台输出 API 初始化信息：
```
[API] Using MOCK API
[API] Using REAL API
[API] Base URL: http://localhost:8000
```

### 3. 在开发工具中切换 API

浏览器控制台：
```javascript
__api__.switchToMock();    // 切换到 Mock
__api__.switchToReal();    // 切换到真实 API
```

### 4. 网络监控

使用浏览器开发者工具的 Network 标签：
- Mock API：无网络请求（全本地）
- Real API：可看到 http://localhost:8000 的请求

## 集成后端

### 1. 启动后端服务

```bash
cd /path/to/backend
APP_ENV=local python3 -m uvicorn app.main:app --reload
```

后端将运行在 `http://localhost:8000`

### 2. 切换前端到真实 API

```bash
VITE_API_MODE=real npm run dev
```

### 3. 测试完整流程

1. 打开 http://localhost:3000（前端）
2. 填写用户资料
3. 生成故事
4. 发起匹配（会调用真实后端 API）
5. 等待匹配成功
6. 开始聊天

## 常见问题

### Q: 如何在本地测试真实 API？
A: 需要先启动后端服务，然后设置：
```bash
VITE_API_MODE=real VITE_API_URL=http://localhost:8000 npm run dev
```

### Q: Mock API 支持长轮询吗？
A: 不支持，Mock API 模拟即时响应。如需测试长轮询，请使用真实后端。

### Q: 如何切换不同的后端地址？
A: 修改 `.env.local` 中的 `VITE_API_URL`：
```
VITE_API_MODE=real
VITE_API_URL=https://dev-api.feiyue.example.com
```

### Q: 生产构建时用哪个 API？
A: 默认使用 `.env.production` 中的配置：
```bash
npm run build  # 使用 .env.production
```

## 下一步

1. **实现 WebSocket** - 用于实时聊天
2. **添加消息持久化** - 保存聊天记录
3. **实现故事线索生成** - 调用 AI 接口
4. **添加用户认证** - 保护隐私和安全

## 技术栈

- **HTTP 客户端**: 原生 Fetch API（无额外依赖）
- **类型系统**: TypeScript
- **状态管理**: React Hooks
- **环境配置**: Vite

## 更多资源

- [后端 API 文档](../API-DOCUMENTATION.md)
- [Vite 环境变量文档](https://vitejs.dev/guide/env-and-mode.html)
- [TypeScript 文档](https://www.typescriptlang.org/docs/)
