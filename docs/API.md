# API 文档

本文档描述 Feiyue Monorepo 中所有服务的 API 接口。

## 📋 目录

- [C# 后端 API](#c-后端-api)
- [Python AI 服务 API](#python-ai-服务-api)
- [WebSocket API](#websocket-api)

---

## C# 后端 API

Base URL: `http://localhost:5000/api`

### 匹配系统

#### POST /match/request
开始匹配请求

**Request:**
```json
{
  "userId": "string",
  "gender": "male|female",
  "ageGroup": "<18|18-23|23+",
  "tags": ["tag1", "tag2"]
}
```

**Response:**
```json
{
  "success": true,
  "status": "waiting|matched",
  "roomId": "string?",
  "errorMessage": "string?"
}
```

#### GET /match/status/{userId}
查询匹配状态

**Response:**
```json
{
  "status": "waiting|matched|failed",
  "position": 1,
  "roomId": "string?"
}
```

#### DELETE /match/cancel/{userId}
取消匹配

**Response:**
```json
{
  "success": true
}
```

---

## Python AI 服务 API

Base URL: `http://localhost:8000/api`

### 故事生成

#### POST /story/generate
生成故事背景

**Request:**
```json
{
  "userAGender": "male",
  "userBGender": "female",
  "userATags": ["文艺", "内向"],
  "userBTags": ["活泼", "外向"]
}
```

**Response:**
```json
{
  "title": "偶遇图书馆",
  "background": "在一个安静的下午...",
  "maleRoleName": "林墨",
  "maleRoleDescription": "文艺青年，喜欢阅读",
  "femaleRoleName": "苏晴",
  "femaleRoleDescription": "活泼开朗的女孩"
}
```

### 虚拟角色

#### POST /character/generate
生成虚拟角色消息

**Request:**
```json
{
  "characterName": "图书馆管理员",
  "personality": "温柔、细心",
  "context": "用户已经沉默了5分钟"
}
```

**Response:**
```json
{
  "message": "你们似乎陷入了沉默，需要我推荐一本书打破僵局吗？"
}
```

### ML 匹配评分

#### POST /match/score
计算匹配评分

**Request:**
```json
{
  "userAGender": "male",
  "userBGender": "female",
  "userATags": ["文艺", "内向"],
  "userBTags": ["活泼", "外向"]
}
```

**Response:**
```json
{
  "score": 0.85,
  "reasoning": "性格互补，有较好的交流潜力"
}
```

---

## WebSocket API

Base URL: `ws://localhost:5000/ws`

### 聊天连接

#### WS /chat/{roomId}
建立聊天连接

**连接参数:**
- `roomId`: 房间 ID

**发送消息:**
```json
{
  "message": "你好"
}
```

**接收消息:**
```json
{
  "id": "msg-123",
  "roomId": "room-456",
  "role": "A|B|system",
  "message": "你好",
  "timestamp": 1234567890,
  "isStoryClue": false
}
```

**系统消息（虚拟角色）:**
```json
{
  "id": "msg-124",
  "roomId": "room-456",
  "role": "system",
  "message": "图书馆即将关闭，你们打算继续聊天吗？",
  "timestamp": 1234567891,
  "isStoryClue": true,
  "triggerType": "silence|rounds|manual"
}
```

---

## 错误处理

所有 API 遵循统一的错误格式：

```json
{
  "success": false,
  "error": {
    "code": "ERROR_CODE",
    "message": "错误描述",
    "details": {}
  }
}
```

### 常见错误码

| 错误码 | HTTP 状态 | 说明 |
|--------|----------|------|
| INVALID_REQUEST | 400 | 请求参数无效 |
| UNAUTHORIZED | 401 | 未授权 |
| NOT_FOUND | 404 | 资源不存在 |
| CONFLICT | 409 | 资源冲突 |
| INTERNAL_ERROR | 500 | 服务器内部错误 |
| SERVICE_UNAVAILABLE | 503 | 服务不可用 |

---

## 完整 API 文档

### Swagger UI
- C# 后端: http://localhost:5000/swagger
- Python AI: http://localhost:8000/docs

### OpenAPI 规范
- C# 后端: http://localhost:5000/swagger/v1/swagger.json
- Python AI: http://localhost:8000/openapi.json
