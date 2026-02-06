# CI/CD Pipeline 配置

本项目使用 GitHub Actions 实现自动化 CI/CD 流程。

## 📋 Workflows 说明

### 1. CI - Build and Test (`ci.yml`)

**触发条件**：
- Push 到 `main` 或 `develop` 分支
- Pull Request 到 `main` 或 `develop` 分支

**包含任务**：

#### 后端测试 (backend-test)
- ✅ 设置 .NET 10 环境
- ✅ PostgreSQL + Redis 服务容器
- ✅ 恢复依赖并编译
- ✅ 运行单元测试
- ✅ 生成代码覆盖率报告
- ✅ 上传测试结果到 Codecov

#### 前端测试 (frontend-test)
- ✅ 设置 Node.js 20 环境
- ✅ 安装依赖（使用缓存）
- ✅ Lint 检查
- ✅ TypeScript 类型检查
- ✅ 构建生产版本
- ✅ 上传构建产物

#### Terraform 验证 (terraform-validate)
- ✅ 格式检查
- ✅ 初始化并验证配置
- ✅ 确保 IaC 代码质量

#### Docker 镜像构建 (docker-build)
- ✅ 仅在 main 分支 push 时触发
- ✅ 构建后端和前端 Docker 镜像
- ✅ 使用 GitHub Actions 缓存加速

---

### 2. CD - Deploy to Production (`deploy-production.yml`)

**触发条件**：
- Push 到 `main` 分支
- 手动触发 (workflow_dispatch)

**部署流程**：

1. **构建后端镜像** → 推送到腾讯云容器镜像仓库
2. **构建前端镜像** → 推送到腾讯云容器镜像仓库
3. **SSH 部署到服务器**：
   - 拉取最新镜像
   - 使用 Docker Compose 重启服务
   - 清理旧镜像
   - 健康检查

**镜像标签**：
- `latest` - 最新稳定版本
- `{sha-short}` - 提交 SHA（7位）
- `{timestamp}` - 时间戳版本

---

### 3. CD - Deploy to Dev/Staging (`deploy-dev.yml`)

**触发条件**：
- Push 到 `develop` 分支
- PR 合并到 main
- 手动触发（可选择环境：dev/staging）

**特点**：
- 使用独立的 dev 镜像仓库
- 使用 `docker-compose.dev.yml`
- 更激进的镜像清理策略（24小时）

---

## 🔑 需要配置的 Secrets

在 GitHub 仓库设置中添加以下 Secrets：

### 腾讯云镜像仓库
```
TENCENT_REGISTRY_USERNAME      # 腾讯云容器镜像服务用户名
TENCENT_REGISTRY_PASSWORD      # 腾讯云容器镜像服务密码
```

### 生产环境服务器
```
TENCENT_SERVER_HOST           # 生产服务器 IP
TENCENT_SERVER_USER           # SSH 用户名 (ubuntu)
TENCENT_SSH_PRIVATE_KEY       # SSH 私钥
API_BASE_URL                  # 生产环境 API 地址
```

### 开发环境服务器
```
DEV_SERVER_HOST               # 开发服务器 IP
DEV_SERVER_USER               # SSH 用户名
DEV_SSH_PRIVATE_KEY           # SSH 私钥
DEV_API_BASE_URL              # 开发环境 API 地址
```

### 可选（代码覆盖率）
```
CODECOV_TOKEN                 # Codecov 上传令牌
```

---

## 🚀 使用说明

### 自动部署

1. **部署到开发环境**
   ```bash
   git checkout develop
   git push origin develop
   # 自动触发部署到开发服务器
   ```

2. **部署到生产环境**
   ```bash
   git checkout main
   git merge develop
   git push origin main
   # 自动触发部署到生产服务器
   ```

### 手动部署

1. 进入 GitHub Actions 页面
2. 选择对应的 workflow
3. 点击 "Run workflow"
4. 选择环境和分支
5. 点击 "Run workflow" 确认

---

## 🔍 监控和调试

### 查看部署日志

```bash
# 在服务器上查看日志
ssh ubuntu@<server-ip>
cd /opt/feiyue
docker-compose logs -f
```

### 检查服务状态

```bash
docker-compose ps
curl http://localhost:8000/health
```

### 回滚到上一个版本

```bash
# 查看可用版本
docker images | grep feiyue

# 使用特定版本
docker-compose down
docker tag feiyue-backend:{version} feiyue-backend:latest
docker-compose up -d
```

---

## 📊 CI/CD 流程图

```
┌─────────────┐
│  Push Code  │
└──────┬──────┘
       │
       ▼
┌─────────────────┐
│   CI Pipeline   │
├─────────────────┤
│ • Backend Test  │
│ • Frontend Test │
│ • Terraform Val │
│ • Docker Build  │
└──────┬──────────┘
       │ (main branch only)
       ▼
┌─────────────────┐
│   CD Pipeline   │
├─────────────────┤
│ • Build Images  │
│ • Push to CCR   │
│ • SSH Deploy    │
│ • Health Check  │
└─────────────────┘
```

---

## 🛠️ 故障排查

### 1. 构建失败

检查：
- .NET 版本是否正确
- Node.js 版本是否正确
- 依赖是否有冲突

### 2. 测试失败

检查：
- PostgreSQL/Redis 服务是否正常
- 环境变量是否配置正确
- 测试数据库是否初始化

### 3. 部署失败

检查：
- SSH 连接是否正常
- 服务器磁盘空间是否充足
- Docker 守护进程是否运行
- 镜像拉取是否成功

### 4. 服务启动失败

检查：
- 数据库连接字符串
- Redis 连接配置
- 端口是否被占用
- 环境变量是否完整

---

## 📈 性能优化

1. **使用 Docker 层缓存**
   - 利用 GitHub Actions 缓存
   - 优化 Dockerfile 层顺序

2. **并行构建**
   - 后端和前端独立构建
   - 利用 GitHub Actions 矩阵策略

3. **增量部署**
   - 仅在代码变更时重新构建
   - 使用镜像标签避免重复拉取

---

## 🔐 安全最佳实践

1. **Secrets 管理**
   - 不要在代码中硬编码敏感信息
   - 使用 GitHub Secrets 存储凭证
   - 定期轮换密钥

2. **镜像安全**
   - 使用官方基础镜像
   - 定期更新依赖
   - 扫描镜像漏洞

3. **网络安全**
   - 使用 SSH 密钥而非密码
   - 限制服务器入站规则
   - 使用 HTTPS 传输

---

## 📚 相关文档

- [Backend Migration Design](../docs/BACKEND_MIGRATION_DESIGN.md)
- [Terraform Configuration](../terraform/README.md)
- [Docker Compose Configuration](../docker-compose.yml)
