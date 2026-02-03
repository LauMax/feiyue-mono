# 飞跃前端部署指南

## 目录
- [本地开发](#本地开发)
- [构建生产版本](#构建生产版本)
- [部署方案](#部署方案)
  - [方案一：腾讯云 COS + CDN（推荐）](#方案一腾讯云-cos--cdn推荐)
  - [方案二：腾讯云轻量服务器](#方案二腾讯云轻量服务器)
  - [方案三：腾讯云 CloudBase](#方案三腾讯云-cloudbase)
- [环境变量配置](#环境变量配置)
- [常见问题](#常见问题)

---

## 本地开发

### 安装依赖
```bash
npm install
```

### 启动开发服务器
```bash
# 连接本地后端 (localhost:8000)
npm run local

# 或指定后端地址
VITE_API_URL=http://your-backend-url npm run dev
```

访问 http://localhost:5173

---

## 构建生产版本

### 1. 配置生产环境变量

创建 `.env.production` 文件：
```env
VITE_API_MODE=real
VITE_API_URL=https://your-backend-api.com
```

### 2. 执行构建
```bash
npm run build
```

构建产物输出到 `dist/` 目录。

### 3. 本地预览（可选）
```bash
npm run preview
```

---

## 部署方案

### 方案一：腾讯云 COS + CDN（推荐）

**优点**：成本低、配置简单、自动扩容、全球加速

#### 步骤 1：创建 COS 存储桶

1. 登录 [腾讯云控制台](https://console.cloud.tencent.com/cos)
2. 点击「创建存储桶」
3. 配置：
   - **名称**：feiyue-frontend（自定义）
   - **地域**：选择离用户近的地域（如：上海）
   - **访问权限**：公有读私有写
4. 创建完成

#### 步骤 2：开启静态网站

1. 进入存储桶 → 「基础配置」→「静态网站」
2. 开启静态网站功能
3. 配置：
   - **索引文档**：index.html
   - **错误文档**：index.html（SPA 路由需要）
4. 记录访问域名

#### 步骤 3：上传文件

**方式一：控制台上传**
1. 进入存储桶 → 「文件列表」
2. 上传 `dist/` 目录下所有文件

**方式二：COSCMD 命令行上传**
```bash
# 安装
pip install coscmd

# 配置（从控制台获取密钥）
coscmd config -a YOUR_SECRET_ID -s YOUR_SECRET_KEY -b feiyue-frontend-xxxxxx -r ap-shanghai

# 上传
coscmd upload -r dist/ /
```

#### 步骤 4：配置 CDN（推荐）

1. 进入 [CDN 控制台](https://console.cloud.tencent.com/cdn)
2. 点击「添加域名」
3. 配置：
   - **加速域名**：your-domain.com
   - **源站类型**：COS 源
   - **源站地址**：选择刚创建的存储桶
4. 配置 HTTPS（推荐）
5. 到域名服务商添加 CNAME 解析

---

### 方案二：腾讯云轻量服务器

**优点**：完全控制、可部署多个项目、适合有运维经验的团队

#### 步骤 1：购买服务器

1. 登录 [轻量应用服务器](https://console.cloud.tencent.com/lighthouse)
2. 购买配置：
   - **镜像**：Ubuntu 22.04 LTS
   - **配置**：2核2G 即可
   - **地域**：选择离用户近的

#### 步骤 2：安装 Nginx

```bash
# SSH 登录服务器
ssh root@YOUR_SERVER_IP

# 更新系统
apt update && apt upgrade -y

# 安装 Nginx
apt install nginx -y

# 启动并设置开机自启
systemctl start nginx
systemctl enable nginx
```

#### 步骤 3：配置 Nginx

编辑 `/etc/nginx/sites-available/default`：

```nginx
server {
    listen 80;
    server_name your-domain.com;
    root /var/www/feiyue;
    index index.html;

    # Gzip 压缩
    gzip on;
    gzip_types text/plain text/css application/json application/javascript text/xml application/xml;

    # SPA 路由支持
    location / {
        try_files $uri $uri/ /index.html;
    }

    # 静态资源缓存
    location ~* \.(js|css|png|jpg|jpeg|gif|ico|svg|woff|woff2)$ {
        expires 1y;
        add_header Cache-Control "public, immutable";
    }

    # API 代理（可选）
    location /api/ {
        proxy_pass https://your-backend-api.com/api/;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

```bash
# 测试配置
nginx -t

# 重载配置
systemctl reload nginx
```

#### 步骤 4：上传文件

```bash
# 本地执行
npm run build

# 创建目录
ssh root@YOUR_SERVER_IP "mkdir -p /var/www/feiyue"

# 上传文件
scp -r dist/* root@YOUR_SERVER_IP:/var/www/feiyue/
```

#### 步骤 5：配置 HTTPS（推荐）

```bash
# 安装 Certbot
apt install certbot python3-certbot-nginx -y

# 申请证书
certbot --nginx -d your-domain.com

# 自动续期测试
certbot renew --dry-run
```

---

### 方案三：腾讯云 CloudBase

**优点**：一键部署、无需运维、自动扩缩容

#### 步骤 1：安装 CLI

```bash
npm install -g @cloudbase/cli
```

#### 步骤 2：登录

```bash
tcb login
```

#### 步骤 3：初始化

```bash
tcb init
```

选择「静态网站托管」模板。

#### 步骤 4：部署

```bash
# 构建
npm run build

# 部署
tcb hosting deploy ./dist -e YOUR_ENV_ID
```

#### 步骤 5：配置自定义域名

在 CloudBase 控制台 → 静态网站托管 → 设置 → 自定义域名

---

## 环境变量配置

### 开发环境 (.env.local)
```env
VITE_API_MODE=real
VITE_API_URL=http://localhost:8000
```

### 生产环境 (.env.production)
```env
VITE_API_MODE=real
VITE_API_URL=https://api.your-domain.com
```

### 测试环境 (.env.staging)
```env
VITE_API_MODE=real
VITE_API_URL=https://staging-api.your-domain.com
```

构建时指定环境：
```bash
# 生产环境（默认）
npm run build

# 测试环境
npm run build -- --mode staging
```

---

## 自动化部署（CI/CD）

### GitHub Actions 示例

创建 `.github/workflows/deploy.yml`：

```yaml
name: Deploy to Tencent Cloud

on:
  push:
    branches: [main]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Setup Node.js
        uses: actions/setup-node@v4
        with:
          node-version: '20'
          cache: 'npm'

      - name: Install dependencies
        run: npm ci

      - name: Build
        run: npm run build
        env:
          VITE_API_URL: ${{ secrets.VITE_API_URL }}

      - name: Deploy to COS
        uses: TencentCloud/cos-action@v1
        with:
          secret_id: ${{ secrets.TENCENT_SECRET_ID }}
          secret_key: ${{ secrets.TENCENT_SECRET_KEY }}
          cos_bucket: ${{ secrets.COS_BUCKET }}
          cos_region: ${{ secrets.COS_REGION }}
          local_path: dist
          remote_path: /
          clean: true
```

在 GitHub 仓库设置中添加 Secrets：
- `TENCENT_SECRET_ID`
- `TENCENT_SECRET_KEY`
- `COS_BUCKET`
- `COS_REGION`
- `VITE_API_URL`

---

## 常见问题

### 1. 页面刷新 404

**原因**：SPA 路由问题，服务器找不到对应的文件

**解决**：
- COS：设置错误文档为 `index.html`
- Nginx：配置 `try_files $uri $uri/ /index.html`

### 2. API 跨域错误

**解决方案**：
1. 后端配置 CORS 允许前端域名
2. 或使用 Nginx 反向代理

### 3. 资源加载失败（路径错误）

**解决**：检查 `vite.config.ts` 中的 `base` 配置
```ts
export default defineConfig({
  base: '/', // 根路径部署
  // base: '/app/', // 子路径部署
})
```

### 4. HTTPS 混合内容警告

**原因**：HTTPS 页面请求 HTTP 资源

**解决**：确保 `VITE_API_URL` 使用 HTTPS

---

## 成本估算（腾讯云）

| 方案 | 月成本 | 适用场景 |
|------|--------|----------|
| COS + CDN | ¥10-50 | 中小流量网站 |
| 轻量服务器 | ¥50-100 | 需要完全控制 |
| CloudBase | ¥0-30 | 小型项目、快速上线 |

---

## 联系支持

如有部署问题，请联系技术团队。
