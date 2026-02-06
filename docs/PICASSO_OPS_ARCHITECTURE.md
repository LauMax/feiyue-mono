# Picasso Ops 架构分析 - Feiyue参考

## 📂 完整目录结构解读

```
ops/
├── 🔧 infra/                    # 基础设施即代码（IaC）
├── 🚢 clusterfleet/              # Kubernetes部署配置
├── 🔄 .pipelines/                # CI/CD流水线
├── 📦 applications/              # Kubernetes应用定义
├── 🔐 auth0/                     # 身份认证配置
├── 🌐 cloudflare/                # CDN和边缘计算
├── 🐳 containers/                # 容器镜像定义
├── 📚 modules/                   # Terraform可复用模块
├── 🔒 policies/                  # 访问策略
└── 🛠️ utils/                     # 工具脚本
```

---

## 1️⃣ 🔧 infra/ - 基础设施即代码

**用途：** Terraform配置，管理所有云资源

### 📁 结构模式

```
infra/
├── bootstrap/              # 初始化配置（Terraform Backend等）
│   ├── corp/              # 企业环境初始化
│   ├── pme-prod/          # 生产环境初始化
│   └── pme-staging/       # 预发布环境初始化
│
├── pme-prod/              # 生产环境（按资源类型分）
│   ├── prod-cosmosdb-account/      # CosmosDB账号
│   ├── prod-cosmosdb-containers/   # CosmosDB容器
│   ├── prod-eventhub/              # Event Hub
│   ├── prod-storage/               # Storage Account
│   ├── prod-cognitive-services/    # AI服务
│   ├── prod-appconfig/             # 应用配置
│   └── prod-shared/                # 共享资源
│
├── pme-staging/           # 预发布环境（结构同prod）
├── pme-testing/           # 测试环境
├── pme-prod-cn/           # 中国区生产
├── pme-staging-cn/        # 中国区预发布
└── corp/                  # 企业内部环境（开发用）
```

### 💡 关键设计原则

1. **环境隔离：** 每个环境独立目录
2. **资源分离：** 每个资源类型独立Root Module
3. **独立状态：** 每个Root Module有自己的tfstate
4. **命名规范：** `{env}-{resource-type}`

### 📝 每个Root Module包含

```
prod-cosmosdb-account/
├── main.tf              # 资源定义
├── terraform.tf         # Provider配置
├── tfbackend.hcl        # Backend配置（状态存储）
├── outputs.tf           # 输出值
└── .terraform.lock.hcl  # 依赖锁定
```

---

## 2️⃣ 📚 modules/ - 可复用模块

**用途：** 封装常用资源模式，供infra引用

### 🏗️ 模块类型

```
modules/
├── cosmos/                  # CosmosDB标准配置
├── cosmosdb/               # CosmosDB完整封装
├── storage-account/        # Storage标准配置
├── eventhub-namespace/     # Event Hub配置
├── cognitive-services/     # AI服务封装
├── kubernetes-cluster/     # AKS集群配置
├── keyvault/              # Key Vault配置
├── redis/                 # Redis缓存
└── ...                    # 30+模块
```

### 📖 使用示例

```hcl
# infra/pme-prod/prod-cosmosdb-account/main.tf
module "cosmos_account" {
  source = "../../../modules/cosmos"  # 引用可复用模块
  
  name                = "cosmos-picasso-prod"
  location            = "eastus2"
  resource_group_name = "rg-picasso-shared"
  
  backup_policy_type = "Continuous"
  # ...其他配置
}
```

---

## 3️⃣ 🚢 clusterfleet/ - Kubernetes部署

**用途：** Kubernetes资源定义（使用Kustomize）

### 📂 结构（Overlay模式）

```
clusterfleet/
├── base/                   # 基础配置（通用）
│   ├── agent/             # 每个微服务一个目录
│   ├── chat/
│   ├── memory/
│   ├── attachments/
│   └── ...                # 30+微服务
│
└── overlays/              # 环境特定覆盖
    ├── prod/              # 生产配置
    ├── staging/           # 预发布配置
    ├── testing/           # 测试配置
    ├── china/             # 中国区配置
    └── preview/           # 预览环境
```

### 🎯 Kustomize模式

```
base/agent/
├── kustomization.yaml     # 基础定义
├── deployment.yaml        # Deployment
├── service.yaml           # Service
├── hpa.yaml              # 自动伸缩
└── configmap.yaml        # 配置

overlays/prod/agent/
└── kustomization.yaml     # 生产环境覆盖（副本数、资源限制等）
```

**优势：**
- ✅ DRY原则：base定义一次，overlays覆盖差异
- ✅ 环境一致性：保证配置结构相同
- ✅ 易于比较：可以diff不同环境

---

## 4️⃣ 🔄 .pipelines/ - CI/CD流水线

**用途：** Azure DevOps Pipeline定义

### 📂 结构

```
.pipelines/
├── picasso/               # 主应用流水线
│   ├── china/            # 中国区部署
│   └── ...
├── activation/           # Activation服务流水线
├── computeruse/          # ComputerUse服务流水线
├── discovery/            # Discovery服务流水线
├── shopping/             # Shopping服务流水线
└── templates/            # 可复用Pipeline模板
    └── common/           # 通用步骤
```

### 🔧 典型Pipeline内容

```yaml
# .pipelines/picasso/azure-pipelines.yml
trigger:
  branches:
    include:
      - main
  paths:
    include:
      - Service.Chat/**

stages:
  - stage: Build
    jobs:
      - job: BuildAndTest
        steps:
          - task: DotNetCoreCLI@2
            inputs:
              command: 'build'
          
  - stage: Deploy_Staging
    dependsOn: Build
    jobs:
      - deployment: DeployToStaging
        environment: 'picasso-staging'
```

---

## 5️⃣ 📦 applications/ - Kubernetes应用

**用途：** 第三方应用和工具的K8s配置

```
applications/
├── 3rdparty/
│   ├── argo-cd/          # GitOps工具
│   └── tailscale-operator/  # VPN
└── kube-oidc-proxy/      # OIDC认证代理
```

---

## 6️⃣ 🔐 auth0/ - 身份认证

**用途：** Auth0身份提供商配置

```
auth0/
├── data/
│   ├── Auth0Dev/         # 开发环境配置
│   └── Auth0Prod/        # 生产环境配置
└── scripts/              # 配置脚本
```

---

## 7️⃣ 🌐 cloudflare/ - CDN和边缘

**用途：** Cloudflare Worker和CDN配置

```
cloudflare/
└── copilotstaging-worker/
    ├── src/              # Worker代码
    └── misc/             # 配置文件
```

---

## 8️⃣ 🐳 containers/ - 容器定义

**用途：** 特殊用途容器镜像

```
containers/
└── dynamic-sessions-tasks/  # 动态会话任务容器
```

---

## 9️⃣ 🔒 policies/ - 访问策略

**用途：** RBAC和访问控制

```
policies/
├── access/               # 访问权限定义
└── plan/                 # 计划策略
```

---

## 🔟 🛠️ utils/ - 工具脚本

**用途：** 运维工具和自动化脚本

---

## 🎯 给Feiyue的建议架构

基于Picasso的最佳实践，我建议Feiyue采用以下结构：

```
feiyue-mono/
├── terraform/                      # 对应Picasso的infra/
│   ├── bootstrap/                 # Terraform Backend初始化
│   │   └── tencent-cos/          # 腾讯云COS存储状态
│   │
│   ├── modules/                   # 可复用模块
│   │   ├── postgresql/           # PostgreSQL标准配置
│   │   ├── redis/                # Redis标准配置
│   │   └── cos-storage/          # COS对象存储
│   │
│   ├── dev/                       # 开发环境
│   │   ├── database/             # PostgreSQL + Redis
│   │   ├── storage/              # COS存储
│   │   └── compute/              # 云托管/TKE
│   │
│   └── prod/                      # 生产环境
│       ├── database/
│       ├── storage/
│       └── compute/
│
├── k8s/                            # 对应Picasso的clusterfleet/
│   ├── base/                      # 基础配置
│   │   ├── api/
│   │   ├── match/
│   │   └── chat/
│   │
│   └── overlays/                  # 环境覆盖
│       ├── dev/
│       └── prod/
│
├── .github/workflows/              # 对应Picasso的.pipelines/
│   ├── deploy-dev.yml
│   ├── deploy-prod.yml
│   └── terraform-plan.yml
│
└── ops/                            # 运维脚本
    ├── scripts/
    └── monitoring/
```

---

## 📊 关键对比

| 概念 | Picasso | Feiyue建议 | 说明 |
|------|---------|-----------|------|
| **云平台** | Azure | 腾讯云 | 不同但模式相同 |
| **IaC工具** | Terraform | Terraform | ✅ 相同 |
| **状态存储** | Azure Storage | 腾讯云COS | 功能相同 |
| **容器编排** | AKS | TKE或云托管 | Kubernetes标准 |
| **CI/CD** | Azure DevOps | GitHub Actions | 流程相似 |
| **配置管理** | Kustomize | Kustomize | ✅ 相同 |
| **环境隔离** | 目录分离 | 目录分离 | ✅ 相同 |

---

## 🚀 实施步骤

**Phase 1: 基础设施（Terraform）**
```bash
# 1. 初始化Backend
cd terraform/bootstrap/tencent-cos
terraform init && terraform apply

# 2. 部署数据库
cd ../../dev/database
terraform init -backend-config=backend.hcl
terraform plan
terraform apply
```

**Phase 2: 应用部署（K8s）**
```bash
# 1. 构建基础配置
cd k8s/base/api
kustomize build

# 2. 部署开发环境
cd ../../overlays/dev
kustomize build | kubectl apply -f -
```

**Phase 3: CI/CD自动化**
```bash
# GitHub Actions自动执行上述步骤
git push origin main  # 自动触发部署
```

---

## 💡 核心经验

1. **模块化设计** - 可复用、可测试、可维护
2. **环境隔离** - 避免配置泄露
3. **状态分离** - 降低爆炸半径
4. **版本锁定** - 保证可重现性
5. **自动化优先** - 减少人为错误

**需要我帮你实现这套架构吗？**
