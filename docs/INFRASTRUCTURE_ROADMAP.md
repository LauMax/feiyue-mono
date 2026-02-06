# Feiyue Infrastructure Roadmap

基于Picasso最佳实践的实施路线图

## 🎯 总体目标

建立生产级基础设施，支持：
- ✅ 多环境部署（dev/prod）
- ✅ 基础设施即代码（IaC）
- ✅ 自动化CI/CD
- ✅ 高可用性和灾备

---

## 📅 实施计划

### Phase 1: 基础设施代码化（Week 1-2）

**目标：** 使用Terraform管理腾讯云资源

#### 1.1 初始化Terraform Backend
```bash
terraform/
└── bootstrap/
    └── tencent-cos/
        ├── main.tf          # COS Bucket用于存储tfstate
        ├── backend.tf       # 本地backend（冷启动用）
        └── README.md
```

**输出：** 远程状态存储Bucket

#### 1.2 创建数据库模块
```bash
terraform/
└── modules/
    ├── postgresql/
    │   ├── main.tf
    │   ├── variables.tf
    │   └── outputs.tf
    └── redis/
        ├── main.tf
        ├── variables.tf
        └── outputs.tf
```

**输出：** 可复用数据库模块

#### 1.3 部署开发环境数据库
```bash
terraform/
└── dev/
    └── database/
        ├── main.tf
        ├── backend.hcl      # 远程backend配置
        └── terraform.tf
```

**命令：**
```bash
cd terraform/dev/database
terraform init -backend-config=backend.hcl
terraform plan
terraform apply
```

**输出：**
- PostgreSQL实例（开发）
- Redis实例（开发）
- 连接字符串（输出到tfstate）

---

### Phase 2: 容器化和编排（Week 3-4）

**目标：** Kubernetes配置和部署

#### 2.1 创建Base配置
```bash
k8s/
└── base/
    ├── api/
    │   ├── kustomization.yaml
    │   ├── deployment.yaml
    │   ├── service.yaml
    │   └── configmap.yaml
    └── ...其他服务
```

#### 2.2 创建环境Overlay
```bash
k8s/
└── overlays/
    ├── dev/
    │   ├── kustomization.yaml
    │   └── patches/
    └── prod/
        ├── kustomization.yaml
        └── patches/
```

**输出：** 可部署的Kubernetes配置

---

### Phase 3: CI/CD自动化（Week 5-6）

**目标：** GitHub Actions流水线

#### 3.1 Terraform自动化
```yaml
# .github/workflows/terraform-plan.yml
name: Terraform Plan
on:
  pull_request:
    paths:
      - 'terraform/**'
```

#### 3.2 应用部署流水线
```yaml
# .github/workflows/deploy-dev.yml
name: Deploy to Dev
on:
  push:
    branches:
      - main
```

**输出：**
- 自动基础设施审查
- 自动应用部署
- 部署通知（Slack/邮件）

---

### Phase 4: 生产环境准备（Week 7-8）

**目标：** 生产级配置

#### 4.1 生产数据库
```bash
terraform/
└── prod/
    └── database/
        ├── main.tf          # 高可用配置
        ├── backend.hcl
        └── backup.tf        # 备份策略
```

**关键配置：**
- 主备复制
- 自动备份
- 监控告警

#### 4.2 灾备方案
```bash
terraform/
└── prod/
    └── disaster-recovery/
        ├── backup-policy.tf
        └── failover.tf
```

---

## 🛠️ 技术选型

### 当前决策

| 组件 | 选择 | 理由 |
|------|------|------|
| **云平台** | 腾讯云 | 国内访问速度 |
| **IaC** | Terraform | 行业标准，跨云支持 |
| **容器编排** | 待定 | TKE vs 云托管 |
| **CI/CD** | GitHub Actions | 与代码库集成 |
| **配置管理** | Kustomize | Kubernetes标准 |
| **状态存储** | 腾讯云COS | 高可用，版本控制 |

### 待决策

- [ ] 容器编排：TKE（Kubernetes）vs 云托管（Serverless）
- [ ] 监控方案：腾讯云监控 vs Prometheus+Grafana
- [ ] 日志方案：CLS vs ELK Stack
- [ ] 密钥管理：腾讯云KMS vs HashiCorp Vault

---

## 📊 成本估算（开发环境）

| 资源 | 规格 | 月费用（估算） |
|------|------|---------------|
| PostgreSQL | 1核2GB | ¥100 |
| Redis | 1GB | ¥30 |
| 云托管 | 1实例 | ¥150 |
| COS存储 | 10GB | ¥2 |
| **总计** | | **¥282/月** |

**生产环境：** 约¥1000-2000/月（包含高可用和备份）

---

## 🚀 快速开始

### 1. Clone仓库
```bash
cd /path/to/feiyue-mono
```

### 2. 初始化Terraform
```bash
cd terraform/bootstrap/tencent-cos
terraform init
terraform apply
```

### 3. 部署开发数据库
```bash
cd ../../dev/database

# 配置连接信息
cp terraform.tfvars.example terraform.tfvars
# 编辑 terraform.tfvars，填入腾讯云密钥

terraform init -backend-config=backend.hcl
terraform plan
terraform apply
```

### 4. 更新应用配置
```bash
# 获取数据库连接字符串
terraform output -json > connections.json

# 更新 backend-csharp/AppHost/appsettings.Development.json
```

### 5. 测试连接
```bash
cd ../../../backend-csharp
USE_REMOTE_DB=true aspire run --project AppHost/Feiyue.AppHost.csproj --no-build
```

---

## 📚 参考文档

- [Picasso Ops架构分析](./PICASSO_OPS_ARCHITECTURE.md)
- [Terraform官方文档](https://www.terraform.io/docs)
- [腾讯云Terraform Provider](https://registry.terraform.io/providers/tencentcloudstack/tencentcloud/latest/docs)
- [Kustomize官方文档](https://kustomize.io/)

---

## 🔄 持续改进

### 下一步优化

1. **监控和告警**
   - 集成Prometheus
   - 配置告警规则
   - 创建Grafana Dashboard

2. **安全加固**
   - 密钥轮换策略
   - 网络隔离
   - 审计日志

3. **性能优化**
   - CDN加速
   - 数据库索引优化
   - 缓存策略

4. **成本优化**
   - 资源自动伸缩
   - 预留实例
   - 存储生命周期策略

---

## 🤝 团队协作

### 权限管理
- **开发人员：** 只读访问生产环境
- **DevOps：** 管理员权限
- **自动化：** Service Account

### 变更流程
1. 创建PR（包含Terraform plan）
2. Code Review
3. 合并后自动部署到dev
4. 手动审批后部署到prod

---

**状态：** 🟡 规划中
**下一步：** 实现Phase 1 - Terraform基础设施
