# Cloudflare Pages 部署指南

## 概述

本项目使用 Cloudflare Pages 进行静态网站托管，支持多环境部署。

---

## 域名规划

| 环境 | 前端域名 | 后端 API 域名 |
|------|----------|---------------|
| **Production** | `fei-yue.net` | `fei-yue.net/api` |
| **Staging/Dev** | `dev.fei-yue.net` | `api-dev.fei-yue.net` |
| **Local** | `localhost:5173` | `localhost:8000` |

---

## 环境变量配置

### 环境文件

| 文件 | 用途 | API 地址 |
|------|------|----------|
| `.env.development` | 本地开发 | `http://localhost:8000` |
| `.env.staging` | Dev/Staging 环境 | `https://api-dev.fei-yue.net` |
| `.env.production` | 生产环境 | `https://fei-yue.net` |

### 环境变量说明

```bash
# 必须以 VITE_ 开头才能在代码中访问
VITE_API_BASE_URL=https://fei-yue.net
VITE_API_MODE=real
```

---

## Cloudflare 控制台设置

### 1. 创建 Pages 项目

1. 登录 [Cloudflare Dashboard](https://dash.cloudflare.com)
2. 进入 `Workers & Pages`
3. 点击 `Create application`
4. 在 "Ship something new" 页面选择：
   - **Continue with GitHub**：连接 GitHub 仓库，自动部署（推荐）
   - **Upload your static files**：手动上传 dist 文件夹

5. 需要创建两个项目：
   - `fei-yue-prod` (生产环境)
   - `fei-yue-dev` (开发环境)

#### 方式 A：Continue with GitHub（推荐）

1. 点击 `Continue with GitHub`
2. 授权 Cloudflare 访问你的 GitHub
3. 选择仓库 `Feiyue_silver_frontend_figma`
4. 配置构建设置：
   - **Project name**: `fei-yue-prod`（或 `fei-yue-dev`）
   - **Production branch**: `main`（生产）或 `dev`（开发）
   - **Framework preset**: `None`
   - **Build command**: `npm run build:prod`（或 `npm run build:staging`）
   - **Build output directory**: `dist`
5. 点击 `Save and Deploy`

#### 方式 B：Upload your static files

1. 点击 `Upload your static files`
2. 输入项目名称：`fei-yue-prod`（或 `fei-yue-dev`）
3. 本地先运行打包命令：
   ```bash
   npm run build:prod   # 生产环境
   # 或
   npm run build:staging  # 开发环境
   ```
4. 拖拽 `dist` 文件夹到上传区域
5. 点击 `Deploy site`

### 2. 绑定自定义域名

| Pages 项目 | 自定义域名 |
|------------|-----------|
| `fei-yue-prod` | `fei-yue.net` |
| `fei-yue-dev` | `dev.fei-yue.net` |

设置步骤：
1. 进入 Pages 项目 → `Custom domains`
2. 点击 `Set up a custom domain`
3. 输入域名，按提示配置 DNS

### 3. 获取 API Token

1. 进入 `My Profile` → `API Tokens`
2. 点击 `Create Token`
3. 选择 `Create Custom Token`
4. 配置权限：
   - **Account** - Cloudflare Pages: Edit
   - **Account** - Account Settings: Read
5. 复制生成的 Token

### 4. 获取 Account ID

在任意域名的 Overview 页面右下角 `API` 区域可以找到 Account ID

---

## GitHub Secrets 配置

在 GitHub 仓库 `Settings` → `Secrets and variables` → `Actions` 中添加：

| Secret 名称 | 说明 |
|-------------|------|
| `CLOUDFLARE_API_TOKEN` | Cloudflare API Token |
| `CLOUDFLARE_ACCOUNT_ID` | Cloudflare Account ID |

---

## NPM 命令

### 开发命令

```bash
# 本地开发（默认配置）
npm run dev

# 本地开发 - 连接本地后端
npm run local

# 本地开发 - 连接 staging API
npm run staging

# 本地开发 - 连接生产 API
npm run prod
```

### 打包命令

```bash
# 打包生产版本
npm run build:prod

# 打包 staging 版本
npm run build:staging

# 预览打包结果
npm run preview
```

### 部署命令（需要先登录 wrangler）

```bash
# 部署到生产环境
npm run deploy:prod

# 部署到 staging 环境
npm run deploy:staging
```

---

## 部署方式

### 方式 1：自动部署（Git Push）

```bash
# 推送到 main 分支 → 自动部署到生产环境
git push origin main

# 推送到 dev 分支 → 自动部署到 staging 环境
git push origin dev
```

### 方式 2：本地手动部署

```bash
# 1. 安装 wrangler CLI（如果还没有）
npm install -g wrangler

# 2. 登录 Cloudflare
wrangler login

# 3. 部署
npm run deploy:staging  # 或 npm run deploy:prod
```

### 方式 3：GitHub Actions 手动触发

1. 进入 GitHub 仓库 → `Actions` 页面
2. 选择 `Deploy Frontend to Cloudflare Pages`
3. 点击 `Run workflow`
4. 选择环境（staging / production）
5. 点击 `Run workflow` 确认

---

## 验证部署

### 检查打包结果

```bash
# 打包 staging 版本
npm run build:staging

# 检查 API 地址是否正确注入
grep -r "api-dev.fei-yue.net" dist/

# 打包生产版本
npm run build:prod

# 检查 API 地址
grep -r "fei-yue.net" dist/
```

### 检查线上环境

```bash
# 检查 staging 环境
curl -I https://dev.fei-yue.net

# 检查生产环境
curl -I https://fei-yue.net
```

---

## CI/CD 工作流

GitHub Actions 配置文件：`.github/workflows/deploy-cloudflare.yml`

### 触发条件

| 触发方式 | 目标环境 |
|----------|----------|
| Push to `main` | Production |
| Push to `dev` | Staging |
| 手动触发 | 可选择环境 |

### 工作流程

```
1. Checkout 代码
2. 安装 Node.js 20
3. 安装依赖 (npm ci)
4. 判断目标环境
5. 执行对应的 build 命令
6. 使用 wrangler 部署到 Cloudflare Pages
```

---

## 常见问题

### Q: 部署后页面刷新 404？

Cloudflare Pages 默认支持 SPA，但需要确保配置正确。创建 `public/_redirects` 文件：

```
/*  /index.html  200
```

### Q: API 请求跨域失败？

确保后端配置了正确的 CORS 头：
- Production: 允许 `https://fei-yue.net`
- Staging: 允许 `https://dev.fei-yue.net`

### Q: 环境变量没有生效？

1. 确保变量以 `VITE_` 开头
2. 确保使用正确的 build 命令（`build:prod` 或 `build:staging`）
3. 重新运行 build

### Q: wrangler 登录失败？

```bash
# 清除登录状态
wrangler logout

# 重新登录
wrangler login
```

---

## 文件结构

```
├── .env.development      # 本地开发环境变量
├── .env.staging          # Staging 环境变量
├── .env.production       # 生产环境变量
├── .github/
│   └── workflows/
│       └── deploy-cloudflare.yml  # CI/CD 配置
├── src/
│   └── api/
│       └── config.ts     # API 配置（读取环境变量）
└── dist/                 # 打包输出目录
```

---

## 迁移自腾讯云 COS

如果之前使用腾讯云 COS，迁移步骤：

1. ✅ 创建 `.env.staging` 文件
2. ✅ 更新 `src/api/config.ts` 使用 `VITE_API_BASE_URL`
3. ✅ 更新 `package.json` 添加新的 npm scripts
4. ✅ 创建 `.github/workflows/deploy-cloudflare.yml`
5. ⏳ 在 Cloudflare 创建 Pages 项目
6. ⏳ 配置 GitHub Secrets
7. ⏳ 配置自定义域名 DNS
8. ⏳ 测试部署

旧的腾讯云配置文件 `.github/workflows/deploy.yml` 可以保留或删除。
