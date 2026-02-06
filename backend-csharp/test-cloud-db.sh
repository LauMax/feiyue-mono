#!/bin/bash
#
# test-cloud-db.sh - 测试腾讯云数据库连接
#
# 使用方法:
#   ./test-cloud-db.sh

set -e

echo "🧪 测试腾讯云数据库连接"
echo "================================"

# ===== 配置信息（替换为你的实际值） =====
POSTGRES_HOST="your-postgres.tencentcdb.com"
POSTGRES_PORT="5432"
POSTGRES_DB="feiyue"
POSTGRES_USER="postgres"
POSTGRES_PASSWORD="your-password"

REDIS_HOST="your-redis.tencentcdb.com"
REDIS_PORT="6379"
REDIS_PASSWORD="your-password"

# ===== 测试PostgreSQL =====
echo ""
echo "📊 测试PostgreSQL连接..."
if command -v psql &> /dev/null; then
    PGPASSWORD=$POSTGRES_PASSWORD psql \
        -h $POSTGRES_HOST \
        -p $POSTGRES_PORT \
        -U $POSTGRES_USER \
        -d postgres \
        -c "SELECT version();" && \
    echo "✅ PostgreSQL连接成功！"
else
    echo "⚠️  未安装psql，跳过PostgreSQL测试"
    echo "   安装: brew install postgresql (macOS)"
fi

# ===== 测试Redis =====
echo ""
echo "🔴 测试Redis连接..."
if command -v redis-cli &> /dev/null; then
    redis-cli \
        -h $REDIS_HOST \
        -p $REDIS_PORT \
        -a $REDIS_PASSWORD \
        PING && \
    echo "✅ Redis连接成功！"
else
    echo "⚠️  未安装redis-cli，跳过Redis测试"
    echo "   安装: brew install redis (macOS)"
fi

echo ""
echo "================================"
echo "✨ 测试完成！"
