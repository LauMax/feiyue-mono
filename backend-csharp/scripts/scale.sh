#!/bin/bash
set -e

# 颜色输出
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

NAMESPACE="feiyue-prod"
DEPLOYMENT="feiyue-api"
REPLICAS=$1

if [ -z "$REPLICAS" ]; then
    echo -e "${RED}用法: ./scale.sh <副本数>${NC}"
    echo -e "示例: ./scale.sh 3"
    echo ""
    echo -e "${YELLOW}当前部署状态：${NC}"
    kubectl get deployment "${DEPLOYMENT}" -n "${NAMESPACE}" 2>/dev/null || echo "部署不存在"
    exit 1
fi

echo -e "${GREEN}=====================================${NC}"
echo -e "${GREEN}扩缩容操作${NC}"
echo -e "${GREEN}=====================================${NC}"
echo -e "命名空间: ${YELLOW}${NAMESPACE}${NC}"
echo -e "部署名称: ${YELLOW}${DEPLOYMENT}${NC}"
echo -e "目标副本数: ${YELLOW}${REPLICAS}${NC}"
echo ""

# 显示当前状态
echo -e "${YELLOW}当前状态：${NC}"
kubectl get deployment "${DEPLOYMENT}" -n "${NAMESPACE}"
echo ""

# 执行扩缩容
echo -e "${BLUE}执行扩缩容...${NC}"
kubectl scale deployment "${DEPLOYMENT}" --replicas="${REPLICAS}" -n "${NAMESPACE}"

echo -e "${GREEN}✓ 扩缩容命令已发送${NC}"
echo ""

# 等待扩缩容完成
echo -e "${YELLOW}等待 Pod 就绪...${NC}"
kubectl rollout status deployment "${DEPLOYMENT}" -n "${NAMESPACE}" --timeout=5m

echo ""
echo -e "${GREEN}=====================================${NC}"
echo -e "${GREEN}扩缩容完成！${NC}"
echo -e "${GREEN}=====================================${NC}"
echo ""

# 显示最终状态
echo -e "${YELLOW}最终状态：${NC}"
kubectl get pods -n "${NAMESPACE}" -l app="${DEPLOYMENT}"
