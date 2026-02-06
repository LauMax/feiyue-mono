# Feiyue Terraform 基础设施配置

## 前置要求

1. **腾讯云账号**：需要有腾讯云账号并获取 API 密钥
2. **Terraform**：安装 Terraform >= 1.0
3. **配置密钥**：设置环境变量

```bash
export TENCENTCLOUD_SECRET_ID="your-secret-id"
export TENCENTCLOUD_SECRET_KEY="your-secret-key"
```

## 快速开始

### 1. 初始化 Terraform

```bash
cd terraform
terraform init
```

### 2. 配置变量

复制示例配置文件：

```bash
cp terraform.tfvars.example terraform.tfvars
```

编辑 `terraform.tfvars`，设置：
- `db_password`: PostgreSQL 密码
- `redis_password`: Redis 密码
- 其他自定义配置

### 3. 预览变更

```bash
terraform plan
```

### 4. 应用配置

```bash
terraform apply
```

### 5. 获取输出

```bash
terraform output
```

## 资源清单

### 网络资源
- VPC (10.0.0.0/16)
- 公有子网 (10.0.1.0/24)
- 私有子网 (10.0.2.0/24)
- 安全组（应用 + 数据库）

### 计算资源
- CVM 实例（应用服务器）
  - 默认：2核4G
  - Ubuntu 22.04
  - 自动安装 .NET 10 + Docker

### 数据库资源
- PostgreSQL 14.2
  - 默认：2核4G，50GB存储
  - 自动备份
- Redis 5.0
  - 默认：1GB内存
  - 主从复制

### 负载均衡（生产环境）
- CLB 实例（仅在 prod 环境创建）

## 环境管理

### 开发环境

```bash
terraform workspace new dev
terraform workspace select dev
terraform apply -var-file="dev.tfvars"
```

### 生产环境

```bash
terraform workspace new prod
terraform workspace select prod
terraform apply -var-file="prod.tfvars"
```

## 连接信息

部署完成后，获取连接信息：

```bash
# 应用服务器 IP
terraform output app_public_ip

# 数据库连接
terraform output -json connection_strings
```

## 成本估算

### 开发环境（每月）
- CVM S5.MEDIUM2: ~100元
- PostgreSQL 1核2G: ~150元
- Redis 1G: ~50元
- **总计**: ~300元/月

### 生产环境（每月）
- CVM S5.MEDIUM4: ~200元
- PostgreSQL 2核4G: ~300元
- Redis 1G: ~50元
- CLB: ~50元
- **总计**: ~600元/月

## 安全注意事项

1. **密码管理**：
   - 不要将密码提交到 Git
   - 使用环境变量或 Vault 管理密钥

2. **SSH 访问**：
   - 生产环境限制 SSH 访问 IP
   - 使用堡垒机或 VPN

3. **数据库**：
   - 放在私有子网
   - 定期备份
   - 启用 SSL 连接

## 销毁资源

⚠️ **警告**：这将删除所有资源和数据！

```bash
terraform destroy
```

## 故障排查

### 1. 初始化失败

```bash
# 清理并重新初始化
rm -rf .terraform
terraform init
```

### 2. 资源创建失败

检查腾讯云配额和权限：
- CVM 配额
- VPC 配额
- 区域可用性

### 3. 连接数据库失败

检查安全组规则和 VPC 配置。

## 更多信息

- [腾讯云 Terraform Provider 文档](https://registry.terraform.io/providers/tencentcloudstack/tencentcloud/latest/docs)
- [Feiyue 项目文档](../docs/BACKEND_MIGRATION_DESIGN.md)
