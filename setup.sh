#!/bin/bash

# 绯悦 Monorepo 快速启动脚本

set -e

echo "🚀 欢迎使用绯悦 Feiyue Monorepo"
echo ""

# 检查依赖
check_dependencies() {
    echo "📦 检查依赖..."
    
    if ! command -v node &> /dev/null; then
        echo "❌ Node.js 未安装，请先安装 Node.js 20+"
        exit 1
    fi
    
    if ! command -v dotnet &> /dev/null; then
        echo "❌ .NET SDK 未安装，请先安装 .NET SDK 8.0+"
        exit 1
    fi
    
    if ! command -v python3 &> /dev/null; then
        echo "❌ Python 未安装，请先安装 Python 3.11+"
        exit 1
    fi
    
    if ! command -v docker &> /dev/null; then
        echo "❌ Docker 未安装，请先安装 Docker"
        exit 1
    fi
    
    echo "✅ 所有依赖已安装"
}

# 安装前端依赖
install_frontend() {
    echo ""
    echo "📦 安装前端依赖..."
    cd frontend
    npm install
    cd ..
    echo "✅ 前端依赖安装完成"
}

# 恢复 C# 依赖
restore_backend() {
    echo ""
    echo "📦 恢复 C# 后端依赖..."
    cd backend-csharp
    dotnet restore src/Feiyue.Api/Feiyue.Api.csproj
    cd ..
    echo "✅ C# 依赖恢复完成"
}

# 安装 Python 依赖
install_ai_service() {
    echo ""
    echo "📦 安装 Python AI 服务依赖..."
    cd ai-service
    pip3 install -r requirements.txt
    cd ..
    echo "✅ Python 依赖安装完成"
}

# 启动 Docker 服务
start_docker() {
    echo ""
    echo "🐳 启动 Docker 服务..."
    docker-compose up -d postgres redis
    echo "✅ PostgreSQL 和 Redis 已启动"
    echo "   - PostgreSQL: localhost:5432"
    echo "   - Redis: localhost:6379"
}

# 主函数
main() {
    check_dependencies
    
    echo ""
    read -p "是否安装所有依赖? (y/n) " -n 1 -r
    echo
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        install_frontend
        restore_backend
        install_ai_service
    fi
    
    echo ""
    read -p "是否启动 Docker 服务? (y/n) " -n 1 -r
    echo
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        start_docker
    fi
    
    echo ""
    echo "✨ 项目初始化完成！"
    echo ""
    echo "📖 下一步："
    echo "   1. 启动 C# 后端:    cd backend-csharp && dotnet run --project src/Feiyue.Api"
    echo "   2. 启动 Python AI:  cd ai-service && python3 -m uvicorn src.main:app --reload"
    echo "   3. 启动前端:        cd frontend && npm run dev"
    echo ""
    echo "   或使用 Docker:      docker-compose up"
    echo ""
}

main
