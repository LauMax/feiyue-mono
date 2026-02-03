# 第一个功能测试指南

## ✅ 已实现的功能

### 后端 (C# - Picasso 风格)
- ✅ `MatchController.cs` - 完整的匹配 API
- ✅ External 模型（用于 JSON 序列化）
- ✅ ToInternal/ToExternal 转换
- ✅ 参考 Picasso 的完整代码风格

### AI 服务 (Python - Grok 集成)
- ✅ `story_api.py` - 故事生成 API
- ✅ Grok API 集成（支持开发模式模拟数据）
- ✅ 完整的错误处理

### 前端 (React - Studio 风格)
- ✅ `MatchPage.tsx` - 匹配页面
- ✅ React Query 数据获取
- ✅ 轮询匹配状态
- ✅ 完整的 UI 交互

## 🚀 测试步骤

### 1. 启动数据库（必需）

```bash
cd /Users/liuxiaosheng/Desktop/Repos/feiyue-mono
docker-compose up -d postgres redis
```

等待启动完成（约10秒）。

### 2. 启动 Python AI 服务

```bash
# 终端 1
cd /Users/liuxiaosheng/Desktop/Repos/feiyue-mono/ai-service

# 安装依赖（首次需要）
pip3 install -r requirements.txt

# 启动服务
python3 -m uvicorn src.main:app --reload --port 8000
```

访问 http://localhost:8000/docs 查看 Swagger 文档。

**测试 AI 接口：**
```bash
curl -X POST http://localhost:8000/api/story/generate \
  -H "Content-Type: application/json" \
  -d '{
    "user_a_gender": "male",
    "user_b_gender": "female",
    "user_a_tags": ["文艺", "内向"],
    "user_b_tags": ["活泼", "外向"]
  }'
```

应该返回模拟的故事数据（因为没有配置 GROK_API_KEY）。

### 3. 启动 C# 后端

```bash
# 终端 2
cd /Users/liuxiaosheng/Desktop/Repos/feiyue-mono/backend-csharp

# 恢复依赖（首次需要）
dotnet restore

# 启动服务
dotnet run --project src/Feiyue.Api/Feiyue.Api.csproj
```

访问 http://localhost:5000/swagger 查看 Swagger 文档。

**测试匹配接口：**
```bash
curl -X POST http://localhost:5000/api/match/request \
  -H "Content-Type: application/json" \
  -d '{
    "userId": "test-user-1",
    "gender": "male",
    "ageGroup": "18-23",
    "tags": ["文艺", "音乐"]
  }'
```

### 4. 启动前端

```bash
# 终端 3
cd /Users/liuxiaosheng/Desktop/Repos/feiyue-mono/frontend

# 安装依赖（首次需要）
npm install

# 启动服务
npm run dev
```

访问 http://localhost:3000

### 5. 完整测试流程

1. **打开前端**: http://localhost:3000
2. **点击首页的"开始匹配"** 或直接访问 http://localhost:3000/match
3. **选择性别、年龄段、标签**
4. **点击"开始匹配"**
5. **观察**:
   - 前端显示"正在匹配中..."
   - 每2秒轮询一次状态
   - 可以点击"取消匹配"

### 6. 查看日志

**C# 后端日志:**
```
info: Feiyue.Api.Controllers.MatchController[0]
      Match request received for user test-user-1
info: Feiyue.Match.MatchService[...]
      Requesting match for user test-user-1 with gender male.
```

**Python AI 日志:**
```
INFO:     127.0.0.1:xxxxx - "POST /api/story/generate HTTP/1.1" 200 OK
```

## 🐛 常见问题

### C# 编译错误

**问题**: `The type or namespace name 'Match' could not be found`

**解决**:
```bash
cd backend-csharp
dotnet clean
dotnet restore
dotnet build
```

### Python 模块找不到

**问题**: `ModuleNotFoundError: No module named 'src'`

**解决**: 确保在 `ai-service` 目录下运行，使用 `-m` 参数：
```bash
python3 -m uvicorn src.main:app --reload
```

### 前端路由错误

**问题**: 点击"开始匹配"没有反应

**解决**: 检查 `routeTree.gen.ts` 中的 async import：
```typescript
component: async () => {
  const { MatchPage } = await import('./pages/MatchPage')
  return <MatchPage />
}
```

### CORS 错误

**问题**: 前端请求被 CORS 阻止

**解决**: 检查 `backend-csharp/src/Feiyue.Api/Program.cs` 中的 CORS 配置：
```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
```

## 📊 预期结果

### 1. AI 服务响应示例

```json
{
  "title": "图书馆的偶遇",
  "background": "在一个阳光明媚的下午，两位年轻人在图书馆不期而遇...",
  "male_role": {
    "name": "林墨",
    "description": "文艺青年，喜欢阅读和写作",
    "personality": "内敛、细腻、善于观察"
  },
  "female_role": {
    "name": "苏晴",
    "description": "活泼开朗的大学生",
    "personality": "外向、热情、喜欢交友"
  }
}
```

### 2. C# 后端响应示例

```json
{
  "success": true,
  "status": "waiting",
  "roomId": null,
  "errorMessage": null
}
```

### 3. 前端界面

- 性别选择按钮（男/女）
- 年龄段下拉框
- 兴趣标签选择（最多5个）
- "开始匹配"按钮
- 匹配中显示状态和位置
- "取消匹配"按钮

## 🎯 下一步

测试通过后，可以继续：

1. **完善匹配逻辑** - 实现真正的队列匹配算法
2. **集成 Grok API** - 配置真实的 API Key
3. **实现聊天功能** - WebSocket 聊天室
4. **添加更多页面** - 聊天界面、个人资料

## 🔧 配置 Grok API（可选）

如果你有 Grok API Key：

```bash
export GROK_API_KEY="your-api-key-here"
```

或者创建 `.env` 文件：
```bash
cd /Users/liuxiaosheng/Desktop/Repos/feiyue-mono/ai-service
echo "GROK_API_KEY=your-api-key-here" > .env
```

然后重启 Python AI 服务。
