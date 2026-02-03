# 🎯 绯悦 - API 集成已完成！

## 📋 项目现状

✅ **API 架构设计完成**  
✅ **Mock API 开发完成**  
✅ **Real API 集成完成**  
✅ **前端组件更新完成**  
✅ **环境配置已设置**  
✅ **文档齐全**  

## 🚀 立即开始

### 选项 1️⃣：使用 Mock API（推荐初始化）

完全离线开发，无需任何依赖。

```bash
npm run dev
```

✅ 启动本地开发服务器  
✅ 自动使用 Mock API（`.env.local` 配置）  
✅ 完全本地运行，零网络延迟  
✅ 3 秒自动匹配成功  
✅ 对方自动回复消息  

**测试流程**：
1. 打开 http://localhost:5173
2. 填写用户资料
3. 生成故事选择
4. 等待 3 秒自动匹配
5. 开始聊天（对方自动回复）

### 选项 2️⃣：使用 Real API（需要后端）

连接真实后端 API。

**步骤 1：启动后端**

```bash
cd /path/to/Feiyue_silver_backend_python
APP_ENV=local python3 -m uvicorn app.main:app --reload
```

后端运行在：http://localhost:8000

**步骤 2：启动前端（使用 Real API）**

```bash
VITE_API_MODE=real npm run dev
```

或者修改 `.env.local`：
```
VITE_API_MODE=real
VITE_API_URL=http://localhost:8000
```

然后：
```bash
npm run dev
```

## 📚 文档导航

按推荐阅读顺序：

### 🟢 新手入门（5 分钟）
👉 [**API_QUICK_START.md**](./API_QUICK_START.md)
- 一页纸快速参考
- 关键代码示例
- 常见问题解答

### 🟡 完整指南（30 分钟）
👉 [**API_INTEGRATION.md**](./API_INTEGRATION.md)
- 详细集成说明
- 所有 API 用法
- 错误处理方案

### 🔴 后端规范（参考）
👉 [**API-DOCUMENTATION.md**](./API-DOCUMENTATION.md)
- 后端 API 规范
- 数据结构定义
- 匹配算法说明

### 🟣 完成总结（概览）
👉 [**API_COMPLETION_SUMMARY.md**](./API_COMPLETION_SUMMARY.md)
- 完成工作清单
- 技术细节说明
- 下一步计划

## 🎭 Mock vs Real API

| 特性 | Mock | Real |
|------|------|------|
| 启动方式 | `npm run dev` | `VITE_API_MODE=real npm run dev` |
| 需要后端 | ❌ | ✅ |
| 网络延迟 | ❌ 零延迟 | ✅ 真实网络 |
| 自动匹配 | ✅ 3秒 | ❌ 等待对方 |
| 自动回复 | ✅ 是 | ❌ 否 |
| 开发效率 | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ |
| 推荐场景 | 本地开发 | 集成测试 |

## 🔄 核心 API

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
  story: { /* 故事对象 */ },
  selectedRole: 'B'
});

// 返回
{
  success: true,
  data: {
    userId: 'anon_abc123',
    matchId: 'match_xyz789',
    status: 'waiting'
  }
}
```

### 2. 查询匹配状态

```typescript
// 轮询查询（每 2 秒）
const result = await api.matchStatus(matchId);

// 返回
{
  success: true,
  data: {
    status: 'matched',  // 或 'waiting'
    roomId: 'room_123',
    story: { /* ... */ },
    yourRole: 'B',
    partnerRole: 'A'
  }
}
```

### 3. 发送消息

```typescript
const result = await api.chatSend({
  roomId: 'room_123',
  userId: 'anon_abc123',
  message: '你好，初次见面！'
});

// 返回
{
  success: true,
  data: {
    id: 'msg_001',
    roomId: 'room_123',
    role: 'B',
    message: '你好，初次见面！',
    timestamp: 1673000000000
  }
}
```

### 4. 获取聊天历史

```typescript
const result = await api.chatMessages(roomId);

// 返回
{
  success: true,
  data: {
    messages: [ /* 消息数组 */ ],
    total: 5
  }
}
```

### 5. 离开房间

```typescript
const result = await api.roomLeave(roomId, userId);

// 返回
{
  success: true,
  message: '已退出聊天室'
}
```

## 🛠️ 配置选项

### 环境变量

```bash
# 模式选择
VITE_API_MODE=mock|real

# API 地址
VITE_API_URL=http://localhost:8000
```

### 配置文件

```
.env              → 生产配置
.env.local        → 本地开发（⭐ 优先级最高）
.env.development  → 开发环境
.env.production   → 生产构建
```

## 🎯 开发流程建议

### 第一阶段：界面开发
```bash
npm run dev          # 使用 Mock API
```
- 完全离线开发
- 无需后端依赖
- 快速迭代 UI
- 即时反馈

### 第二阶段：功能测试
```bash
VITE_API_MODE=real npm run dev
```
- 启动后端服务
- 测试真实流程
- 验证 API 集成
- 找出兼容性问题

### 第三阶段：集成测试
```bash
npm run build       # 生产构建
# 使用 .env.production 配置
```
- 完整流程测试
- 性能验证
- 准备上线

## 💡 常用命令

```bash
# 本地开发（Mock API）
npm run dev

# 本地开发（Real API）
VITE_API_MODE=real npm run dev

# 构建生产版本
npm run build

# 查看构建结果
npm run build
```

## 🔧 调试技巧

### 浏览器控制台

```javascript
// 查看当前 API 模式
console.log(import.meta.env.VITE_API_MODE);   // 'mock' 或 'real'
console.log(import.meta.env.VITE_API_URL);    // API 地址

// 切换 API（仅开发环境）
__api__.switchToMock();    // 切换到 Mock
__api__.switchToReal();    // 切换到 Real
```

### Network 标签

- **Mock API**：无网络请求（本地运行）
- **Real API**：有 http://localhost:8000 的请求

### 浏览器日志

```
[API] Using MOCK API
[API] Using REAL API
[API] Base URL: http://localhost:8000
```

## ⚠️ 常见问题

### Q1: 如何在 Mock 和 Real 之间快速切换？

**方法 1**：修改 `.env.local`
```
VITE_API_MODE=mock  # 或 real
```
重启开发服务器。

**方法 2**：启动时指定
```bash
VITE_API_MODE=real npm run dev
```

**方法 3**：浏览器控制台（开发环境）
```javascript
__api__.switchToMock()
__api__.switchToReal()
```

### Q2: 为什么 Mock API 立即匹配？

模拟实际场景，3 秒内自动完成匹配。如需修改，编辑 `src/api/mock.ts` 中的延迟时间。

### Q3: Real API 需要什么条件？

1. 后端运行在 `http://localhost:8000`
2. 设置 `VITE_API_MODE=real`
3. 设置正确的 `VITE_API_URL`

### Q4: 如何处理 API 错误？

```typescript
const result = await api.matchRequest(payload);

if (!result.success) {
  console.error('错误:', result.error?.message);
  // 处理错误...
}
```

### Q5: 生产环境用哪个 API？

自动使用 `.env.production` 中的配置：
```
VITE_API_MODE=real
VITE_API_URL=https://api.feiyue.com
```

## 📁 文件结构

```
src/api/
├── index.ts        ← 👈 使用这个，自动选择 mock 或 real
├── config.ts       ← 配置（环境变量）
├── http.ts         ← HTTP 客户端（Fetch API）
├── mock.ts         ← Mock API 实现（本地）
└── real.ts         ← Real API 实现（真实后端）

src/app/
├── App.tsx         ← ✅ 已更新（支持匹配 API）
├── components/
│   ├── ChatRoom.tsx ← ✅ 已更新（支持消息 API）
│   └── ...

根目录/
├── .env            ← 默认（生产）
├── .env.local      ← 👈 本地（优先，Mock）
├── .env.development ← 开发环境
├── .env.production ← 生产环境
├── API_QUICK_START.md ← 快速参考
├── API_INTEGRATION.md ← 完整指南
└── API-DOCUMENTATION.md ← 后端规范
```

## ✅ 下一步

1. **立即测试**
   ```bash
   npm run dev        # 运行 Mock API
   ```

2. **阅读快速参考**
   👉 [API_QUICK_START.md](./API_QUICK_START.md)

3. **集成真实后端**
   - 启动后端
   - 修改 `.env.local` 为 `VITE_API_MODE=real`
   - 测试完整流程

4. **部署到生产**
   ```bash
   npm run build      # 使用 .env.production
   ```

## 🎓 学习路径

### 5 分钟 - 快速了解
1. 这个文件（README）
2. [API_QUICK_START.md](./API_QUICK_START.md)
3. `npm run dev` 测试

### 30 分钟 - 深入学习
1. [API_INTEGRATION.md](./API_INTEGRATION.md)
2. 查看 `src/api/` 源代码
3. 测试 Real API

### 1 小时 - 完全掌握
1. [API-DOCUMENTATION.md](./API-DOCUMENTATION.md)
2. 启动后端服务
3. 完整流程测试

## 🚀 快速命令

```bash
# 开发（Mock）
npm run dev

# 开发（Real，需要后端）
VITE_API_MODE=real npm run dev

# 生产构建
npm run build
```

## 📞 获取帮助

1. 📖 查看文档
   - [API_QUICK_START.md](./API_QUICK_START.md) - 快速参考
   - [API_INTEGRATION.md](./API_INTEGRATION.md) - 完整指南
   - [API-DOCUMENTATION.md](./API-DOCUMENTATION.md) - 后端规范

2. 🔍 查看代码
   - `src/api/` - API 实现
   - `src/app/App.tsx` - 应用入口
   - `src/app/components/ChatRoom.tsx` - 聊天组件

3. 💬 联系支持
   - GitHub Issues
   - GitHub Discussions

## 🎉 现在就开始！

```bash
npm run dev
```

然后打开 http://localhost:5173 开始体验！

---

**🌟 项目状态：** Ready for Development  
**📅 最后更新：** 2026-01-19  
**✨ API 版本：** 1.0.0
