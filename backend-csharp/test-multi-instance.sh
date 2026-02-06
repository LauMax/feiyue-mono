#!/bin/bash

echo "🔍 Testing Multi-Instance Redis Sharing"
echo ""

# Check if Redis is accessible
echo "1️⃣ Testing Redis connection..."
docker exec feiyue-redis redis-cli ping
if [ $? -ne 0 ]; then
    echo "❌ Redis is not running. Start with: ./dev-start.sh"
    exit 1
fi
echo "✅ Redis is accessible"
echo ""

# Start two API instances
echo "2️⃣ Starting API instances..."
echo "   Instance 1: http://localhost:5000"
echo "   Instance 2: http://localhost:5001"
echo ""

# Start instance 1
cd src/Feiyue.Api
dotnet run --urls "http://localhost:5000" > /tmp/feiyue-api-1.log 2>&1 &
PID1=$!

# Start instance 2
dotnet run --urls "http://localhost:5001" > /tmp/feiyue-api-2.log 2>&1 &
PID2=$!

echo "   PIDs: $PID1, $PID2"
echo "   Waiting 5 seconds for startup..."
sleep 5

# Test health
echo ""
echo "3️⃣ Testing health endpoints..."
curl -s http://localhost:5000/health | jq .
curl -s http://localhost:5001/health | jq .

# Test queue sharing
echo ""
echo "4️⃣ Testing queue sharing..."

# Add user to queue via instance 1
echo "   Adding user1 via instance 1..."
USER1=$(curl -s -X POST http://localhost:5000/api/user/create \
  -H "Content-Type: application/json" \
  -d '{}' | jq -r '.id')

curl -s -X POST http://localhost:5000/api/match/enqueue \
  -H "Content-Type: application/json" \
  -d "{\"userId\":\"$USER1\",\"genderPreference\":\"female\",\"isVip\":false}" | jq .

# Check queue stats from instance 2
echo ""
echo "   Checking queue stats via instance 2..."
curl -s http://localhost:5001/api/match/stats | jq .

# Cleanup
echo ""
echo "5️⃣ Cleanup..."
kill $PID1 $PID2 2>/dev/null
echo "✅ Test complete!"
echo ""
echo "📊 Logs:"
echo "   Instance 1: /tmp/feiyue-api-1.log"
echo "   Instance 2: /tmp/feiyue-api-2.log"
