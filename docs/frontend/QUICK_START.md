# 🎉 绯悦 Frontend - 快速开始指南

## ⚡ 5分钟快速开始

### 1. 环境配置

```bash
# 克隆仓库
git clone https://github.com/LauMax/Feiyue_Silver_frontend_figma.git
cd Feiyue_Silver

# 安装依赖
npm install

# 启动开发服务器
npm run dev
```

### 2. 选择 API 模式

编辑 `.env.local`：

```env
# 本地开发 - 使用 Mock API（无需后端）
VITE_API_MODE=mock

# 或者连接真实后端
VITE_API_MODE=real
VITE_API_URL=http://localhost:8000
```

**推荐用法：**
- 🧪 **本地开发**: 使用 `mock` 模式，快速迭代UI
- 🔌 **集成测试**: 切换到 `real` 模式，连接真实后端

### 3. 访问应用

在浏览器打开：
```
http://localhost:5173
```

## 🔧 详细配置

### Mock 模式（推荐用于本地开发）

```env
# .env.local
VITE_API_MODE=mock
```

**特点：**
- ✅ 无需后端服务
- ✅ 自动模拟匹配过程（3秒）
- ✅ 自动生成对方资料
- ✅ 快速测试UI流程

**测试场景：**
```typescript
// 在浏览器控制台运行
__api__.switchToMock()    // 切换到 Mock
__api__.switchToReal()    // 切换到 Real API
```

### Real 模式（生产环境）

```env
# .env.local - 本地连接后端
VITE_API_MODE=real
VITE_API_URL=http://localhost:8000

# .env.development - 开发环境
VITE_API_URL=https://dev-api.feiyue.example.com

# .env.production - 生产环境
VITE_API_URL=https://api.feiyue.com
```

**要求：**
- ✅ 后端服务运行中
- ✅ API 端点可访问
- ✅ CORS 配置正确

## 📚 项目结构

```
src/
├── api/
│   ├── config.ts          ⚙️  API 配置
│   ├── http.ts            🌐 HTTP 客户端
│   ├── mock.ts            🎭 Mock API 实现
│   ├── real.ts            🔗 真实 API 实现
│   └── index.ts           📦 统一接口
│
├── app/
│   ├── App.tsx            🏠 主应用
│   ├── components/
│   │   ├── HomePage.tsx   📖 首页
│   │   ├── UserProfile.tsx 👤 资料编辑
│   │   ├── StoryGenerator.tsx ✨ 故事生成
│   │   ├── WaitingRoom.tsx ⏳ 等待室
│   │   ├── ChatRoom.tsx    💬 聊天室
│   │   └── ui/            🎨 UI 组件库
│   └── styles/            🖌️  全局样式
│
└── main.tsx               📄 应用入口
```

## 🔄 API 切换原理

### 自动选择

```typescript
// src/api/index.ts
const api = isMockApiMode() ? mockApi : realApi;
```

### 开发调试

```typescript
// 浏览器控制台
window.__api__.switchToMock()   // 切换到 Mock
window.__api__.switchToReal()   // 切换到真实 API
```

## 🚀 核心功能

### 匹配流程

```
首页 → 填写资料 → 选择故事 → 自动分配角色 → 进入等待室 → 聊天
```

- **自动匹配**：后端自动分配男生=maleRole，女生=femaleRole
- **等待提示**：显示已等待时间和队列状态
- **可取消**：用户可随时取消匹配
- **超时保护**：10分钟未匹配则超时

### 聊天系统

- 📨 实时消息发送
- 🤖 AI 故事线索自动触发
- 📜 聊天历史加载
- 👥 对方状态显示

### 故事线索

自动触发条件：
- 📍 **对话轮数**: 5轮、15轮、35轮、75轮...
- 🤫 **沉默时间**: 20秒无消息（最多3次）

## 🐛 调试技巧

### 查看日志

```typescript
// API 模式日志
[API] Using MOCK API
[API] Using REAL API
[API] Base URL: http://localhost:8000

// 匹配日志
[匹配进行中] 已等待 30 秒，队列中有用户等待...
```

### 浏览器控制台

```javascript
// 查看当前 API 配置
window.__api__

// 手动切换模式
window.__api__.switchToMock()
window.__api__.switchToReal()

// 查看 Redux 状态（如果有 Redux DevTools）
window.__REDUX_DEVTOOLS_EXTENSION__
```

### Network 标签

- 监控所有 API 请求
- 查看请求/响应数据
- 检查 CORS 问题

## 📋 环境变量完整列表

| 变量名 | 说明 | 默认值 | 可选值 |
|--------|------|--------|--------|
| `VITE_API_MODE` | API 运行模式 | `real` | `mock`, `real` |
| `VITE_API_URL` | API 基础地址 | `http://localhost:8000` | 任何有效的 URL |

## ⚠️ 常见问题

### Q: 后端还没准备好，如何测试前端？
**A:** 使用 Mock 模式：
```env
VITE_API_MODE=mock
```

### Q: 如何在本地调试与后端的集成？
**A:** 
1. 启动后端服务
2. 配置 `.env.local`：
```env
VITE_API_MODE=real
VITE_API_URL=http://localhost:8000
```
3. 在浏览器 Network 标签监控请求

### Q: Mock 模式下的匹配流程是否和真实一样？
**A:** 完全一样！Mock API 完全模拟真实行为：
- 匹配请求立即返回 `status: "waiting"`
- 3秒后自动返回 `status: "matched"`
- 生成模拟对方资料

### Q: 如何快速在 Mock 和 Real 之间切换？
**A:** 在浏览器控制台运行：
```javascript
__api__.switchToMock()  // 切换到 Mock
__api__.switchToReal()  // 切换到 Real API
```

### Q: 生产构建时如何使用?
**A:** Vite 会自动根据 `.env.production` 配置：
```bash
npm run build  # 使用 .env.production 配置
```

## 🔌 后端集成检查清单

- [ ] 后端 `/api/match/request` 端点就绪
- [ ] 后端 `/api/match/status/:matchId` 端点就绪
- [ ] 后端 `/api/chat/send` 端点就绪
- [ ] 后端 `/api/chat/messages/:roomId` 端点就绪
- [ ] 后端 `/api/room/leave` 端点就绪
- [ ] CORS 已配置
- [ ] 可在 `http://localhost:8000/docs` 查看 API 文档

## 📞 技术支持

遇到问题？

1. 📖 查看 [MATCHING_FLOW.md](./MATCHING_FLOW.md) - 匹配流程详解
2. 📋 查看 [API-DOCUMENTATION.md](./API-DOCUMENTATION.md) - API 完整文档
3. 🐛 检查浏览器 Network 标签
4. 💻 查看浏览器控制台日志

## 🎯 下一步

- [ ] 理解 [匹配流程](./MATCHING_FLOW.md)
- [ ] 集成真实后端
- [ ] 添加 WebSocket 支持（可选）
- [ ] 配置生产环境
- [ ] 部署到云服务

---

**祝开发顺利！** 🚀

版本: 1.0  
更新: 2026-01-19
