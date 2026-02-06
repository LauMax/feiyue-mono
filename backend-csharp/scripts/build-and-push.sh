#!/bin/bash
set -e

# 颜色输出
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# 配置变量
REGISTRY="ccr.ccs.tencentyun.com"
NAMESPACE="feiyue"
IMAGE_NAME="api"
VERSION=${1:-"latest"}  # 默认使用latest，可以通过参数指定版本

FULL_IMAGE="${REGISTRY}/${NAMESPACE}/${IMAGE_NAME}:${VERSION}"

echo -e "${GREEN}=====================================${NC}"
echo -e "${GREEN}构建并推送 Docker 镜像${NC}"
echo -e "${GREEN}=====================================${NC}"
echo -e "镜像: ${YELLOW}${FULL_IMAGE}${NC}"
echo ""

# 检查是否已登录
echo -e "${YELLOW}检查登录状态...${NC}"
if ! docker info | grep -q "Username"; then
    echo -e "${RED}未登录到Docker仓库，请先登录：${NC}"
    echo -e "${YELLOW}docker login ${REGISTRY}${NC}"
    exit 1
fi

# 构建镜像
echo -e "${YELLOW}开始构建镜像...${NC}"
docker build -f Dockerfile.prod -t "${FULL_IMAGE}" .

if [ $? -ne 0 ]; then
    echo -e "${RED}构建失败！${NC}"
    exit 1
fi

echo -e "${GREEN}✓ 构建成功${NC}"

# 推送镜像
echo -e "${YELLOW}推送镜像到仓库...${NC}"
docker push "${FULL_IMAGE}"

if [ $? -ne 0 ]; then
    echo -e "${RED}推送失败！${NC}"
    exit 1
fi

echo -e "${GREEN}✓ 推送成功${NC}"

# 同时打上latest标签（如果指定了版本号）
if [ "${VERSION}" != "latest" ]; then
    LATEST_IMAGE="${REGISTRY}/${NAMESPACE}/${IMAGE_NAME}:latest"
    echo -e "${YELLOW}同时推送 latest 标签...${NC}"
    docker tag "${FULL_IMAGE}" "${LATEST_IMAGE}"
    docker push "${LATEST_IMAGE}"
    echo -e "${GREEN}✓ latest 标签推送成功${NC}"
fi

echo ""
echo -e "${GREEN}=====================================${NC}"
echo -e "${GREEN}完成！镜像已推送${NC}"
echo -e "${GREEN}=====================================${NC}"
echo -e "镜像: ${YELLOW}${FULL_IMAGE}${NC}"
echo ""
echo -e "下一步部署到K8s："
echo -e "${YELLOW}cd k8s && kubectl apply -f .${NC}"
