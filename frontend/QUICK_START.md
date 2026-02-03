# 绯悦前端快速启动指南

## 前置条件

确保已安装：
- Node.js 18+
- npm 或 pnpm

## 安装依赖

```bash
cd feiyue-mono/frontend
npm install
```

## 启动开发服务器

### 方式 1: 默认配置（推荐）
```bash
npm run dev
```
- 前端：http://localhost:3000
- 后端 API：通过 vite proxy 转发到 http://localhost:8000

### 方式 2: 指定 API 地址
```bash
# 本地后端
npm run local

# 测试环境
npm run staging

# 生产环境（仅用于测试）
npm run prod
```

## 目录结构

```
frontend/
├── src/
│   ├── api/                    # API 层
│   │   ├── config.ts          # API 配置
│   │   ├── http.ts            # HTTP 客户端
│   │   ├── real.ts            # 真实 API 实现
│   │   └── index.ts           # API 入口
│   ├── app/                    # 应用组件
│   │   ├── App.tsx            # 主应用（状态机）
│   │   └── components/        # 页面组件
│   │       ├── HomePage.tsx
│   │       ├── UserProfile.tsx
│   │       ├── WaitingRoom.tsx
│   │       ├── ChatRoom.tsx
│   │       ├── ui/            # UI 组件库
│   │       └── figma/         # Figma 组件
│   ├── styles/                # 样式文件
│   │   └── index.css
│   └── main.tsx               # 入口文件
├── .env.development           # 开发环境配置
├── .env.production            # 生产环境配置
├── .env.staging               # 测试环境配置
├── package.json
└── vite.config.ts
```

## 环境变量

### .env.development (默认)
```env
VITE_API_BASE_URL=http://localhost:8000
VITE_WS_URL=ws://localhost:8000
```

### .env.production
```env
VITE_API_BASE_URL=https://api.fei-yue.net
VITE_WS_URL=wss://api.fei-yue.net
```

### .env.staging
```env
VITE_API_BASE_URL=https://api-dev.fei-yue.net
```

## 构建部署

### 构建生产版本
```bash
npm run build:prod
```

### 构建测试版本
```bash
npm run build:staging
```

### 预览构建结果
```bash
npm run preview
```

## 开发工作流

1. **启动后端服务**（另一个终端）
   ```bash
   cd feiyue-mono/backend-csharp
   # 启动后端服务
   ```

2. **启动前端开发服务器**
   ```bash
   cd feiyue-mono/frontend
   npm run dev
   ```

3. **访问应用**
   - 打开浏览器：http://localhost:3000
   - 测试完整用户流程

## 主要功能页面

### 1. 首页 (HomePage)
- 应用介绍
- "开始旅程" 按钮

### 2. 用户资料 (UserProfile)
- 性别选择（必填）
- 年龄段选择（必填）
- 身高、体重
- 偏好标签（最多 8 个）
- 个人描述（最多 200 字）

### 3. 等待匹配 (WaitingRoom)
- 显示已等待时间
- 显示角色信息
- 可取消匹配
- 轮询匹配状态（每 2 秒）

### 4. 聊天室 (ChatRoom)
- 显示故事背景
- 实时消息
- AI 故事线索
- 查看双方资料
- 离开房间

## API 端点

所有请求都通过 `/api` 前缀：

- `POST /api/match/request` - 发起匹配
- `GET /api/match/status/:matchId` - 查询匹配状态
- `POST /api/match/cancel` - 取消匹配
- `POST /api/chat/send` - 发送消息
- `GET /api/chat/messages/:roomId` - 获取聊天历史
- `POST /api/room/leave` - 离开房间

## 调试技巧

### 1. 查看 API 日志
在浏览器控制台可以看到所有 API 请求日志：
```
[API] 发送匹配请求: {...}
[匹配进行中] 已等待 10 秒...
```

### 2. 访问 API 实例
在开发环境下，可以在控制台访问：
```javascript
window.__api__
```

### 3. 查看网络请求
- 打开浏览器开发者工具
- Network 标签
- 过滤 `/api` 请求

## 常见问题

### Q: 页面空白或报错？
A: 检查：
1. 后端服务是否正常运行
2. API 地址配置是否正确
3. 浏览器控制台错误信息

### Q: 匹配一直等待？
A: 正常现象，需要另一个用户同时在等待。在开发环境可以：
1. 打开两个浏览器窗口
2. 分别以男生和女生身份发起匹配

### Q: 如何切换 API 环境？
A: 使用不同的启动命令：
```bash
npm run local    # 本地
npm run staging  # 测试
npm run prod     # 生产
```

## 技术栈

- **React 18** - UI 框架
- **TypeScript** - 类型安全
- **Vite 6** - 构建工具
- **Tailwind CSS v4** - 样式
- **Radix UI** - 无障碍组件
- **Material-UI** - 图标和组件
- **Lucide React** - 图标库

## 下一步

查看完整文档：
- [API 文档](../docs/frontend/API-DOCUMENTATION.md)
- [匹配流程](../docs/frontend/MATCHING_FLOW.md)
- [前端实现](../docs/frontend/FRONTEND-IMPLEMENTATION.md)
