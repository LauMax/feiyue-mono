#!/bin/bash

echo "🧪 Testing Feiyue Backend End-to-End"
echo ""

API_URL="http://localhost:5000"

# Colors
GREEN='\033[0;32m'
RED='\033[0;31m'
NC='\033[0m' # No Color

test_passed=0
test_failed=0

test_api() {
    local name=$1
    local method=$2
    local url=$3
    local data=$4
    
    echo -n "Testing: $name... "
    
    if [ "$method" = "GET" ]; then
        response=$(curl -s -w "\n%{http_code}" "$API_URL$url")
    else
        response=$(curl -s -w "\n%{http_code}" -X $method "$API_URL$url" \
            -H "Content-Type: application/json" \
            -d "$data")
    fi
    
    http_code=$(echo "$response" | tail -n 1)
    body=$(echo "$response" | sed '$d')
    
    if [ "$http_code" -ge 200 ] && [ "$http_code" -lt 300 ]; then
        echo -e "${GREEN}✓ PASSED${NC} (HTTP $http_code)"
        test_passed=$((test_passed + 1))
        echo "$body" | jq . 2>/dev/null || echo "$body"
    else
        echo -e "${RED}✗ FAILED${NC} (HTTP $http_code)"
        test_failed=$((test_failed + 1))
        echo "$body"
    fi
    echo ""
}

echo "==== 1. Health Check ===="
test_api "Health Check" "GET" "/health" ""

echo "==== 2. Create Users ===="
USER1_RESPONSE=$(curl -s -X POST "$API_URL/api/user/create" -H "Content-Type: application/json" -d '{}')
USER1_ID=$(echo $USER1_RESPONSE | jq -r '.id')
echo "User 1 ID: $USER1_ID"

USER2_RESPONSE=$(curl -s -X POST "$API_URL/api/user/create" -H "Content-Type: application/json" -d '{}')
USER2_ID=$(echo $USER2_RESPONSE | jq -r '.id')
echo "User 2 ID: $USER2_ID"
echo ""

echo "==== 3. Update User Profiles ===="
test_api "Update User 1 Profile" "POST" "/api/user/$USER1_ID/profile" \
    '{"gender":"male","age":25,"interests":["coding","reading"],"stories":[],"isVip":false}'

test_api "Update User 2 Profile" "POST" "/api/user/$USER2_ID/profile" \
    '{"gender":"female","age":23,"interests":["music","art"],"stories":[],"isVip":false}'

echo "==== 4. Enter Match Queue ===="
test_api "User 1 Enqueue" "POST" "/api/match/enqueue" \
    "{\"userId\":\"$USER1_ID\",\"genderPreference\":\"female\",\"isVip\":false}"

test_api "User 2 Enqueue" "POST" "/api/match/enqueue" \
    "{\"userId\":\"$USER2_ID\",\"genderPreference\":\"male\",\"isVip\":false}"

echo "==== 5. Check Queue Stats ===="
test_api "Queue Stats" "GET" "/api/match/stats" ""

echo "==== 6. Find Match ===="
MATCH_RESPONSE=$(curl -s -X POST "$API_URL/api/match/find" -H "Content-Type: application/json")
ROOM_ID=$(echo $MATCH_RESPONSE | jq -r '.roomId')
echo "Match Response: $MATCH_RESPONSE" | jq .
echo "Room ID: $ROOM_ID"
echo ""

if [ "$ROOM_ID" != "null" ] && [ -n "$ROOM_ID" ]; then
    echo "==== 7. Chat Room Tests ===="
    test_api "Get Room" "GET" "/api/chat/rooms/$ROOM_ID" ""
    
    test_api "Send Message 1" "POST" "/api/chat/rooms/$ROOM_ID/messages" \
        "{\"senderId\":\"$USER1_ID\",\"content\":\"Hello!\"}"
    
    test_api "Send Message 2" "POST" "/api/chat/rooms/$ROOM_ID/messages" \
        "{\"senderId\":\"$USER2_ID\",\"content\":\"Hi there!\"}"
    
    test_api "Get Messages" "GET" "/api/chat/rooms/$ROOM_ID/messages?limit=10" ""
    
    test_api "Close Room" "POST" "/api/chat/rooms/$ROOM_ID/close" ""
else
    echo "❌ No match found, skipping chat tests"
fi

echo ""
echo "===================================="
echo -e "Tests Passed: ${GREEN}$test_passed${NC}"
echo -e "Tests Failed: ${RED}$test_failed${NC}"
echo "===================================="

if [ $test_failed -eq 0 ]; then
    echo -e "${GREEN}🎉 All tests passed!${NC}"
    exit 0
else
    echo -e "${RED}❌ Some tests failed${NC}"
    exit 1
fi
