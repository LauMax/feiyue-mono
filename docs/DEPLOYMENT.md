# 部署指南

本文档描述如何将 Feiyue Monorepo 部署到生产环境。

## 🎯 部署架构

```
用户浏览器
    ↓
Cloudflare CDN / Nginx
    ↓
Kubernetes Ingress
    ├─→ Frontend (3 replicas)
    ├─→ Backend C# (5 replicas)
    └─→ AI Service (3 replicas)
         ↓
    PostgreSQL (Primary + 2 Replicas)
    Redis Cluster (6 nodes)
```

## 📋 前置要求

### 生产环境
- Kubernetes 1.28+
- Helm 3.x
- Docker Registry
- PostgreSQL 15+ (或云数据库)
- Redis 7+ (或云 Redis)

### 推荐云服务
- **Azure**: AKS + Azure Database for PostgreSQL + Azure Cache for Redis
- **AWS**: EKS + RDS PostgreSQL + ElastiCache
- **阿里云**: ACK + RDS PostgreSQL + Redis 企业版

## 🐳 Docker 镜像构建

### 1. 构建所有镜像

```bash
# 设置镜像仓库地址
export REGISTRY=your-registry.com
export VERSION=1.0.0

# 构建 C# 后端
cd backend-csharp
docker build -t $REGISTRY/feiyue-backend:$VERSION .
docker push $REGISTRY/feiyue-backend:$VERSION

# 构建 Python AI
cd ../ai-service
docker build -t $REGISTRY/feiyue-ai:$VERSION .
docker push $REGISTRY/feiyue-ai:$VERSION

# 构建前端
cd ../frontend
docker build -t $REGISTRY/feiyue-frontend:$VERSION .
docker push $REGISTRY/feiyue-frontend:$VERSION
```

### 2. 多阶段构建优化

所有 Dockerfile 已经使用多阶段构建，最终镜像大小：
- C# 后端: ~200MB
- Python AI: ~800MB (包含 ML 库)
- 前端: ~20MB (nginx + 静态文件)

## ☸️ Kubernetes 部署

### 1. 创建 Namespace

```bash
kubectl create namespace feiyue-prod
kubectl label namespace feiyue-prod env=production
```

### 2. 创建 Secrets

```bash
# PostgreSQL 连接字符串
kubectl create secret generic postgres-secret \
  --from-literal=connection-string="Host=xxx;Port=5432;Database=feiyue;Username=xxx;Password=xxx" \
  -n feiyue-prod

# Redis 连接字符串
kubectl create secret generic redis-secret \
  --from-literal=connection-string="redis-host:6379,password=xxx" \
  -n feiyue-prod

# Grok API Key
kubectl create secret generic grok-secret \
  --from-literal=api-key="your-grok-api-key" \
  -n feiyue-prod
```

### 3. 部署服务

#### 后端 Deployment

```yaml
# k8s/backend-deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: feiyue-backend
  namespace: feiyue-prod
spec:
  replicas: 5
  selector:
    matchLabels:
      app: feiyue-backend
  template:
    metadata:
      labels:
        app: feiyue-backend
    spec:
      containers:
      - name: backend
        image: your-registry.com/feiyue-backend:1.0.0
        ports:
        - containerPort: 8080
        env:
        - name: ASPNETCORE_ENVIRONMENT
          value: "Production"
        - name: ConnectionStrings__PostgreSQL
          valueFrom:
            secretKeyRef:
              name: postgres-secret
              key: connection-string
        - name: ConnectionStrings__Redis
          valueFrom:
            secretKeyRef:
              name: redis-secret
              key: connection-string
        resources:
          requests:
            memory: "512Mi"
            cpu: "500m"
          limits:
            memory: "1Gi"
            cpu: "1000m"
        livenessProbe:
          httpGet:
            path: /health
            port: 8080
          initialDelaySeconds: 30
          periodSeconds: 10
        readinessProbe:
          httpGet:
            path: /health
            port: 8080
          initialDelaySeconds: 10
          periodSeconds: 5
---
apiVersion: v1
kind: Service
metadata:
  name: feiyue-backend-service
  namespace: feiyue-prod
spec:
  selector:
    app: feiyue-backend
  ports:
  - port: 80
    targetPort: 8080
  type: ClusterIP
```

#### AI 服务 Deployment

```yaml
# k8s/ai-deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: feiyue-ai
  namespace: feiyue-prod
spec:
  replicas: 3
  selector:
    matchLabels:
      app: feiyue-ai
  template:
    metadata:
      labels:
        app: feiyue-ai
    spec:
      containers:
      - name: ai-service
        image: your-registry.com/feiyue-ai:1.0.0
        ports:
        - containerPort: 8000
        env:
        - name: ENVIRONMENT
          value: "production"
        - name: GROK_API_KEY
          valueFrom:
            secretKeyRef:
              name: grok-secret
              key: api-key
        - name: REDIS_URL
          valueFrom:
            secretKeyRef:
              name: redis-secret
              key: connection-string
        resources:
          requests:
            memory: "1Gi"
            cpu: "500m"
          limits:
            memory: "2Gi"
            cpu: "1000m"
---
apiVersion: v1
kind: Service
metadata:
  name: feiyue-ai-service
  namespace: feiyue-prod
spec:
  selector:
    app: feiyue-ai
  ports:
  - port: 80
    targetPort: 8000
  type: ClusterIP
```

#### 前端 Deployment

```yaml
# k8s/frontend-deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: feiyue-frontend
  namespace: feiyue-prod
spec:
  replicas: 3
  selector:
    matchLabels:
      app: feiyue-frontend
  template:
    metadata:
      labels:
        app: feiyue-frontend
    spec:
      containers:
      - name: frontend
        image: your-registry.com/feiyue-frontend:1.0.0
        ports:
        - containerPort: 80
        resources:
          requests:
            memory: "128Mi"
            cpu: "100m"
          limits:
            memory: "256Mi"
            cpu: "200m"
---
apiVersion: v1
kind: Service
metadata:
  name: feiyue-frontend-service
  namespace: feiyue-prod
spec:
  selector:
    app: feiyue-frontend
  ports:
  - port: 80
    targetPort: 80
  type: ClusterIP
```

### 4. Ingress 配置

```yaml
# k8s/ingress.yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: feiyue-ingress
  namespace: feiyue-prod
  annotations:
    cert-manager.io/cluster-issuer: "letsencrypt-prod"
    nginx.ingress.kubernetes.io/ssl-redirect: "true"
    nginx.ingress.kubernetes.io/websocket-services: "feiyue-backend-service"
spec:
  ingressClassName: nginx
  tls:
  - hosts:
    - fei-yue.net
    - api.fei-yue.net
    secretName: feiyue-tls
  rules:
  - host: fei-yue.net
    http:
      paths:
      - path: /
        pathType: Prefix
        backend:
          service:
            name: feiyue-frontend-service
            port:
              number: 80
  - host: api.fei-yue.net
    http:
      paths:
      - path: /api
        pathType: Prefix
        backend:
          service:
            name: feiyue-backend-service
            port:
              number: 80
      - path: /ws
        pathType: Prefix
        backend:
          service:
            name: feiyue-backend-service
            port:
              number: 80
```

### 5. 水平自动扩缩容 (HPA)

```yaml
# k8s/hpa.yaml
apiVersion: autoscaling/v2
kind: HorizontalPodAutoscaler
metadata:
  name: feiyue-backend-hpa
  namespace: feiyue-prod
spec:
  scaleTargetRef:
    apiVersion: apps/v1
    kind: Deployment
    name: feiyue-backend
  minReplicas: 3
  maxReplicas: 20
  metrics:
  - type: Resource
    resource:
      name: cpu
      target:
        type: Utilization
        averageUtilization: 70
  - type: Resource
    resource:
      name: memory
      target:
        type: Utilization
        averageUtilization: 80
```

## 🗄️ 数据库部署

### PostgreSQL

推荐使用云数据库服务，或使用以下配置：

```yaml
# 使用 Bitnami PostgreSQL Helm Chart
helm install feiyue-postgres bitnami/postgresql \
  --namespace feiyue-prod \
  --set auth.database=feiyue_db \
  --set auth.username=feiyue \
  --set auth.password=your-password \
  --set primary.persistence.size=100Gi \
  --set readReplicas.replicaCount=2
```

### Redis

```yaml
# 使用 Bitnami Redis Cluster Helm Chart
helm install feiyue-redis bitnami/redis-cluster \
  --namespace feiyue-prod \
  --set password=your-password \
  --set cluster.nodes=6 \
  --set persistence.size=20Gi
```

## 📊 监控和日志

### Prometheus + Grafana

```bash
# 安装 Prometheus Operator
helm install prometheus prometheus-community/kube-prometheus-stack \
  --namespace monitoring \
  --create-namespace

# 配置 ServiceMonitor
kubectl apply -f k8s/servicemonitor.yaml
```

### ELK Stack

```bash
# 安装 Elasticsearch + Kibana
helm install elastic elastic/elasticsearch \
  --namespace logging \
  --create-namespace

helm install kibana elastic/kibana \
  --namespace logging
```

## 🚀 部署流程

### 自动化部署脚本

```bash
#!/bin/bash
# deploy.sh

set -e

VERSION=$1
ENVIRONMENT=${2:-production}

echo "🚀 Deploying Feiyue v$VERSION to $ENVIRONMENT"

# 1. 构建镜像
echo "📦 Building images..."
docker build -t $REGISTRY/feiyue-backend:$VERSION ./backend-csharp
docker build -t $REGISTRY/feiyue-ai:$VERSION ./ai-service
docker build -t $REGISTRY/feiyue-frontend:$VERSION ./frontend

# 2. 推送镜像
echo "⬆️  Pushing images..."
docker push $REGISTRY/feiyue-backend:$VERSION
docker push $REGISTRY/feiyue-ai:$VERSION
docker push $REGISTRY/feiyue-frontend:$VERSION

# 3. 更新 Kubernetes
echo "☸️  Updating Kubernetes..."
kubectl set image deployment/feiyue-backend \
  backend=$REGISTRY/feiyue-backend:$VERSION \
  -n feiyue-$ENVIRONMENT

kubectl set image deployment/feiyue-ai \
  ai-service=$REGISTRY/feiyue-ai:$VERSION \
  -n feiyue-$ENVIRONMENT

kubectl set image deployment/feiyue-frontend \
  frontend=$REGISTRY/feiyue-frontend:$VERSION \
  -n feiyue-$ENVIRONMENT

# 4. 等待部署完成
echo "⏳ Waiting for rollout..."
kubectl rollout status deployment/feiyue-backend -n feiyue-$ENVIRONMENT
kubectl rollout status deployment/feiyue-ai -n feiyue-$ENVIRONMENT
kubectl rollout status deployment/feiyue-frontend -n feiyue-$ENVIRONMENT

echo "✅ Deployment complete!"
```

## 🔄 CI/CD 集成

### GitHub Actions

```yaml
# .github/workflows/deploy.yml
name: Deploy to Production

on:
  push:
    tags:
      - 'v*'

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Set up Docker Buildx
      uses: docker/setup-buildx-action@v2
    
    - name: Login to Registry
      uses: docker/login-action@v2
      with:
        registry: ${{ secrets.REGISTRY }}
        username: ${{ secrets.REGISTRY_USERNAME }}
        password: ${{ secrets.REGISTRY_PASSWORD }}
    
    - name: Build and Push
      run: |
        ./deploy.sh ${{ github.ref_name }} production
    
    - name: Deploy to Kubernetes
      uses: azure/k8s-deploy@v4
      with:
        manifests: |
          k8s/backend-deployment.yaml
          k8s/ai-deployment.yaml
          k8s/frontend-deployment.yaml
```

## 📋 生产环境检查清单

- [ ] 所有 Secrets 已创建
- [ ] PostgreSQL 已配置主从复制
- [ ] Redis 已配置集群模式
- [ ] TLS 证书已配置
- [ ] 监控和告警已配置
- [ ] 日志收集已配置
- [ ] 备份策略已实施
- [ ] HPA 已配置
- [ ] 负载测试已完成
- [ ] 灾难恢复计划已准备
