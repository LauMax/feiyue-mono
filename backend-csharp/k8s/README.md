# Feiyue Kubernetes 部署指南

本目录包含Feiyue后端API部署到腾讯云TKE的所有配置文件。

## 📁 文件说明

| 文件 | 说明 |
|-----|------|
| `namespace.yaml` | 命名空间配置 |
| `configmap.yaml` | 应用配置（非敏感） |
| `secret.yaml` | 密钥配置（数据库密码等） |
| `deployment.yaml` | 部署配置（Pod模板） |
| `service.yaml` | 负载均衡服务 |
| `hpa.yaml` | 自动扩缩容配置（可选） |

## 🚀 快速开始

### 1. 前置条件

- 已安装 `kubectl`
- 已配置 TKE 集群的 kubeconfig
- 已创建腾讯云容器镜像仓库

### 2. 配置密钥

编辑 `secret.yaml`，替换以下占位符：

```yaml
# PostgreSQL连接字符串
postgres-connection: "Host=YOUR_DB_HOST;Database=feiyue;..."

# Redis连接字符串  
redis-connection: "YOUR_REDIS_HOST:6379,password=..."

# JWT密钥
jwt-secret: "YOUR_JWT_SECRET_KEY"
```

### 3. 构建并推送镜像

```bash
# 登录腾讯云容器镜像服务
docker login ccr.ccs.tencentyun.com

# 构建并推送镜像
cd ../
./scripts/build-and-push.sh v1.0.0
```

### 4. 部署到K8s

```bash
# 使用自动化脚本
./scripts/deploy.sh

# 或手动部署
kubectl apply -f k8s/
```

### 5. 验证部署

```bash
# 查看Pod状态
kubectl get pods -n feiyue-prod

# 查看Service（获取外网IP）
kubectl get svc -n feiyue-prod

# 查看日志
kubectl logs -f deployment/feiyue-api -n feiyue-prod
```

## 📈 扩容操作

### 手动扩缩容

```bash
# 扩容到3个实例
./scripts/scale.sh 3

# 或使用kubectl
kubectl scale deployment feiyue-api --replicas=3 -n feiyue-prod
```

### 自动扩缩容（HPA）

```bash
# 启用HPA
kubectl apply -f k8s/hpa.yaml

# 查看HPA状态
kubectl get hpa -n feiyue-prod

# HPA会根据CPU/内存自动调整副本数（1-10个）
```

## 🔄 更新应用

### 滚动更新（零停机）

```bash
# 1. 构建新版本镜像
./scripts/build-and-push.sh v1.0.1

# 2. 更新Deployment
kubectl set image deployment/feiyue-api \
  api=ccr.ccs.tencentyun.com/feiyue/api:v1.0.1 \
  -n feiyue-prod

# 3. 查看更新进度
kubectl rollout status deployment/feiyue-api -n feiyue-prod
```

### 回滚

```bash
# 回滚到上一个版本
kubectl rollout undo deployment/feiyue-api -n feiyue-prod

# 回滚到指定版本
kubectl rollout undo deployment/feiyue-api --to-revision=2 -n feiyue-prod
```

## 🔍 监控和调试

### 查看日志

```bash
# 实时日志
kubectl logs -f deployment/feiyue-api -n feiyue-prod

# 查看所有Pod日志
kubectl logs -l app=feiyue-api -n feiyue-prod --tail=100

# 查看特定Pod日志
kubectl logs feiyue-api-xxx-yyy -n feiyue-prod
```

### 进入容器调试

```bash
# 进入Pod容器
kubectl exec -it deployment/feiyue-api -n feiyue-prod -- /bin/bash

# 执行命令
kubectl exec deployment/feiyue-api -n feiyue-prod -- env | grep Connection
```

### 查看资源使用

```bash
# 查看Pod资源使用
kubectl top pods -n feiyue-prod

# 查看节点资源使用
kubectl top nodes
```

## 💰 成本估算

### TKE Serverless

| 配置 | 副本数 | 月成本 |
|-----|-------|--------|
| 0.5核1GB | 1 | ¥150 |
| 0.5核1GB | 3 | ¥450 |
| 0.5核1GB | 5 | ¥750 |

### TKE标准版

| 配置 | 节点数 | 月成本 |
|-----|-------|--------|
| 2核4GB节点 | 2 | ¥300 |
| 4核8GB节点 | 2 | ¥600 |

## 🎯 最佳实践

### 1. 资源配置

- **开发环境**: `replicas: 1`, CPU: 500m, Memory: 512Mi
- **生产环境**: `replicas: 3`, CPU: 1000m, Memory: 1Gi
- **高峰期**: 启用HPA，自动扩展到5-10个实例

### 2. 健康检查

- `livenessProbe`: 检测容器是否存活，失败则重启
- `readinessProbe`: 检测容器是否就绪，失败则从负载均衡移除
- `startupProbe`: 保护慢启动应用

### 3. 滚动更新策略

```yaml
strategy:
  type: RollingUpdate
  rollingUpdate:
    maxSurge: 1        # 最多多1个新Pod
    maxUnavailable: 0  # 保证至少1个可用（零停机）
```

### 4. 密钥管理

- ❌ 不要将真实密码提交到git
- ✅ 使用环境变量或腾讯云密钥管理服务
- ✅ 定期轮换密钥

## 🆘 常见问题

### Pod无法启动

```bash
# 查看Pod详细信息
kubectl describe pod <pod-name> -n feiyue-prod

# 常见原因：
# 1. 镜像拉取失败 - 检查镜像地址和权限
# 2. 资源不足 - 增加节点或减少资源请求
# 3. 配置错误 - 检查环境变量和Secret
```

### 无法获取外网IP

```bash
# 检查Service状态
kubectl get svc feiyue-api -n feiyue-prod

# 如果长时间Pending：
# 1. 检查TKE控制台LoadBalancer配额
# 2. 检查命名空间是否有权限创建LB
```

### 健康检查失败

```bash
# 查看容器日志
kubectl logs deployment/feiyue-api -n feiyue-prod

# 调整健康检查参数：
# - 增加 initialDelaySeconds
# - 增加 failureThreshold
# - 检查 /health 接口是否正常
```

## 📚 参考资料

- [TKE官方文档](https://cloud.tencent.com/document/product/457)
- [Kubernetes官方文档](https://kubernetes.io/docs/)
- [腾讯云容器镜像服务](https://cloud.tencent.com/document/product/1141)
