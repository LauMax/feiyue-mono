#!/bin/bash

echo "🚀 Starting Feiyue Development Environment..."

# Start Docker services
echo "📦 Starting PostgreSQL and Redis..."
docker-compose up -d

# Wait for services to be healthy
echo "⏳ Waiting for services to be ready..."
sleep 5

# Check PostgreSQL
echo "🔍 Checking PostgreSQL..."
docker exec feiyue-postgres pg_isready -U postgres
if [ $? -eq 0 ]; then
    echo "✅ PostgreSQL is ready!"
else
    echo "❌ PostgreSQL failed to start"
    exit 1
fi

# Check Redis
echo "🔍 Checking Redis..."
docker exec feiyue-redis redis-cli ping
if [ $? -eq 0 ]; then
    echo "✅ Redis is ready!"
else
    echo "❌ Redis failed to start"
    exit 1
fi

echo ""
echo "🎉 Development environment is ready!"
echo ""
echo "📊 Database Info:"
echo "  PostgreSQL: localhost:5432"
echo "  Database: feiyue"
echo "  Username: postgres"
echo "  Password: postgres123"
echo ""
echo "  Redis: localhost:6379"
echo ""
echo "🔧 Useful Commands:"
echo "  - View logs: docker-compose logs -f"
echo "  - Stop services: docker-compose down"
echo "  - Access PostgreSQL: docker exec -it feiyue-postgres psql -U postgres -d feiyue"
echo "  - Access Redis: docker exec -it feiyue-redis redis-cli"
echo ""
echo "▶️  Run API: cd src/Feiyue.Api && dotnet run"
