#!/bin/bash
set -e

# 颜色输出
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

NAMESPACE="feiyue-prod"

echo -e "${GREEN}=====================================${NC}"
echo -e "${GREEN}部署到 Kubernetes${NC}"
echo -e "${GREEN}=====================================${NC}"
echo ""

# 检查kubectl是否安装
if ! command -v kubectl &> /dev/null; then
    echo -e "${RED}kubectl 未安装，请先安装 kubectl${NC}"
    exit 1
fi

# 检查是否已连接到集群
echo -e "${YELLOW}检查集群连接...${NC}"
if ! kubectl cluster-info &> /dev/null; then
    echo -e "${RED}无法连接到 Kubernetes 集群${NC}"
    echo -e "${YELLOW}请先配置 kubeconfig${NC}"
    exit 1
fi

echo -e "${GREEN}✓ 已连接到集群${NC}"
echo ""

# 部署所有资源
echo -e "${YELLOW}部署资源到命名空间: ${NAMESPACE}${NC}"
cd k8s

# 1. 创建命名空间
echo -e "${BLUE}→ 创建命名空间...${NC}"
kubectl apply -f namespace.yaml

# 2. 创建ConfigMap
echo -e "${BLUE}→ 创建 ConfigMap...${NC}"
kubectl apply -f configmap.yaml

# 3. 创建Secret（提醒用户检查）
echo -e "${YELLOW}⚠️  注意：请确保 secret.yaml 中的密码已替换为真实值！${NC}"
read -p "是否继续部署 Secret? (y/n) " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]; then
    echo -e "${BLUE}→ 创建 Secret...${NC}"
    kubectl apply -f secret.yaml
else
    echo -e "${RED}已跳过 Secret 部署${NC}"
    exit 1
fi

# 4. 创建Service
echo -e "${BLUE}→ 创建 Service...${NC}"
kubectl apply -f service.yaml

# 5. 创建Deployment
echo -e "${BLUE}→ 创建 Deployment...${NC}"
kubectl apply -f deployment.yaml

# 6. 可选：创建HPA
echo ""
read -p "是否启用自动扩缩容(HPA)? (y/n) " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]; then
    echo -e "${BLUE}→ 创建 HPA...${NC}"
    kubectl apply -f hpa.yaml
    echo -e "${GREEN}✓ HPA 已启用${NC}"
fi

echo ""
echo -e "${GREEN}=====================================${NC}"
echo -e "${GREEN}部署完成！${NC}"
echo -e "${GREEN}=====================================${NC}"
echo ""

# 显示部署状态
echo -e "${YELLOW}查看部署状态：${NC}"
echo ""
kubectl get pods -n "${NAMESPACE}"
echo ""
kubectl get svc -n "${NAMESPACE}"
echo ""

# 等待LoadBalancer分配外网IP
echo -e "${YELLOW}等待 LoadBalancer 分配外网IP...${NC}"
echo -e "${BLUE}提示：这可能需要1-2分钟${NC}"
echo ""

for i in {1..30}; do
    EXTERNAL_IP=$(kubectl get svc feiyue-api -n "${NAMESPACE}" -o jsonpath='{.status.loadBalancer.ingress[0].ip}' 2>/dev/null || echo "")
    if [ -n "$EXTERNAL_IP" ] && [ "$EXTERNAL_IP" != "<pending>" ]; then
        echo -e "${GREEN}✓ 获得外网IP: ${EXTERNAL_IP}${NC}"
        echo ""
        echo -e "${GREEN}访问地址: http://${EXTERNAL_IP}/swagger${NC}"
        exit 0
    fi
    echo -n "."
    sleep 2
done

echo ""
echo -e "${YELLOW}LoadBalancer IP 分配较慢，请稍后手动检查：${NC}"
echo -e "${BLUE}kubectl get svc feiyue-api -n ${NAMESPACE}${NC}"
