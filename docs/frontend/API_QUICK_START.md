# API 模式快速参考

## 🚀 一句话启动

### Mock 模式（本地开发 - 默认）
```bash
npm run dev
```
✅ 无需后端，立即可用  
✅ 本地模拟所有API  
✅ 速度快，适合UI测试

### Real 模式（真实API）
```bash
VITE_API_MODE=real npm run dev
```
✅ 连接真实后端  
⚠️ 需要后端运行在 `http://localhost:8000`

## 📁 关键文件

```
src/api/
├── index.ts       👈 使用这个：import api from '@/api'
├── config.ts      ⚙️ API配置
├── http.ts        🔌 HTTP客户端
├── mock.ts        🎭 Mock API
└── real.ts        🔗 真实API

.env*             🌍 环境变量
├── .env          默认（生产）
├── .env.local    本地（优先）⭐
├── .env.development  开发
└── .env.production   生产
```

## 🎯 核心 API

```javascript
// ✅ 发起匹配（3秒后返回成功）
await api.matchRequest({
  profile: { /* ... */ },
  story: { /* ... */ },
  selectedRole: 'A' | 'B'
})

// ✅ 查询匹配状态（每2秒查询一次）
await api.matchStatus(matchId)

// ✅ 发送消息
await api.chatSend({
  roomId,
  userId,
  message: '你好！'
})

// ✅ 获取消息历史
await api.chatMessages(roomId)

// ✅ 离开房间
await api.roomLeave(roomId, userId)
```

## 🔧 快速切换模式

### 方式1：修改 `.env.local`
```
VITE_API_MODE=mock    # 或 real
```
然后重启服务

### 方式2：启动时指定
```bash
VITE_API_MODE=real npm run dev
```

### 方式3：浏览器控制台（开发环境）
```javascript
__api__.switchToMock()   // 切换到Mock
__api__.switchToReal()   // 切换到真实API
```

## ✨ Mock API 特性

| 功能 | Mock | Real |
|-----|------|------|
| 匹配 | 3秒自动成功 | 等待对方 |
| 消息 | 自动回复 | 真实对方 |
| 网络 | 无需 | 需要后端 |
| 速度 | 极快 ⚡ | 实时 🔄 |
| 开发 | 100% 离线 | 需要后端 |

## 🐛 调试技巧

### 查看当前模式
```javascript
// 浏览器控制台
console.log(import.meta.env.VITE_API_MODE);  // 'mock' 或 'real'
console.log(import.meta.env.VITE_API_URL);   // API地址
```

### 监控API调用
```javascript
// 浏览器控制台看日志
// [API] Using MOCK API
// [API] Using REAL API
// [API] Base URL: ...
```

### 打开网络检查
- Mock API：Network 标签无请求 ✅
- Real API：Network 标签有 http://localhost:8000 的请求 ✅

## 📝 状态码

所有API返回统一格式：

```typescript
{
  success: true,           // 成功
  data: { /* ... */ }     // 业务数据
}

// 失败
{
  success: false,
  error: {
    code: 'TIMEOUT',
    message: '请求超时'
  }
}
```

## 🔗 后端集成

### 启动后端
```bash
cd /path/to/backend
APP_ENV=local python3 -m uvicorn app.main:app --reload
# 运行在 http://localhost:8000
```

### 切换前端
```bash
VITE_API_MODE=real npm run dev
```

### 测试流程
1. 用户资料 → 
2. 故事生成 → 
3. 匹配请求（调用后端API）→
4. 轮询状态 → 
5. 进入聊天

## ❓ 常见问题

**Q: 我想先测试UI？**  
A: 使用 Mock 模式（默认），完全不需要后端

**Q: 如何连接我的后端？**  
A: 设置 `VITE_API_MODE=real`，确保后端运行在 http://localhost:8000

**Q: 如何在开发和生产之间切换？**  
A: 
- 开发：`VITE_API_MODE=real VITE_API_URL=http://localhost:8000`
- 生产：`.env.production` 中设置生产API地址

**Q: 为什么 Mock API 立即成功？**  
A: 模拟真实场景，3秒内自动匹配。可修改 `src/api/mock.ts` 中的延迟时间

**Q: 错误如何处理？**  
```typescript
const result = await api.matchRequest(payload);
if (!result.success) {
  console.error(result.error?.message);
}
```

## 🎨 Response 格式示例

```json
{
  "success": true,
  "data": {
    "userId": "anon_abc123",
    "matchId": "match_xyz789",
    "status": "waiting"
  }
}
```

## 🚀 生产部署

```bash
# 使用生产配置构建
npm run build

# 生产环境使用 .env.production
# VITE_API_MODE=real
# VITE_API_URL=https://api.feiyue.com
```

## 📚 完整文档

详见 [API_INTEGRATION.md](./API_INTEGRATION.md)

---

**💡 推荐流程：**
1. 本地开发 → 使用 Mock API（无依赖）
2. 测试功能 → 使用 Real API（连接后端）
3. 集成测试 → Dev 环境（dev-api.xxx）
4. 生产发布 → .env.production（api.xxx）

**⏱️ 快速参考：**
```bash
# 开发
npm run dev              # Mock 模式

# 测试后端集成
VITE_API_MODE=real npm run dev

# 构建生产
npm run build
```
