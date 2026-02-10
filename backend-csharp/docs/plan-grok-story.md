# 计划：在 C# Backend 集成 Grok API 生成故事种子与剧情推进

## 目标
- 在后端新增 Grok API 调用能力，用于：
  1) 生成**故事种子**（标题、背景、角色A/B信息）
  2) 生成**剧情推进**（根据现有故事与对话/设定生成下一段剧情或提示）

## 现状调研
- 现有后端包含 `Story` 与 `Role` 的内部模型：[src/Service.InternalContracts/StoryModels.cs](../src/Service.InternalContracts/StoryModels.cs)
- 现有 API 层为 `Service.Api`，包含 `MatchController` 并接受 `Story`（用户自填/选择）
- 尚未发现现有 LLM/AI 客户端或故事生成服务

## 设计方案（最小改动、可扩展）

### 1) 新增 Grok 调用基础设施
- 新增 `GrokOptions`：
  - `BaseUrl`（例如 https://api.x.ai/v1/chat/completions 或兼容 OpenAI 的 endpoint）
  - `ApiKey`
  - `ModelForSeed`、`ModelForPlot`
  - `Temperature`、`MaxTokens`
- 新增 `IGrokClient` + `GrokClient`（基于 `IHttpClientFactory`）
  - 统一封装请求、响应解析、错误处理、超时、日志

### 2) 新增业务服务层
- 新增 `IStoryGenerationService` + `StoryGenerationService`
  - `GenerateSeedAsync(SeedRequest, CancellationToken)`
  - `GeneratePlotAsync(PlotRequest, CancellationToken)`
- 在服务层组织 prompt 与输出约束（要求输出可解析 JSON）

### 3) 新增 API 端点
- 新增 `StoryController`
  - `POST /api/story/seed`
  - `POST /api/story/plot`
- 使用 DTO 显式声明输入/输出结构，避免客户端猜字段

### 4) 模型输出格式
- 要求 Grok 返回 JSON（例如 `{ "title": ..., "background": ..., "maleRole": {...}, "femaleRole": {...} }`）
- 后端解析 JSON -> `Story` / DTO
- 对剧情推进要求输出 JSON（例如 `{ "nextPlot": "...", "suggestedActions": [...] }`）

### 5) 配置与环境变量
- 在 `Service.Api/appsettings*.json` 加入 `Grok` 配置占位
- 支持环境变量覆盖 `Grok:ApiKey`

## 实施步骤
1. 新建 `Service.Story`（或在 `Service.Chat` 内新增 Story 相关服务）
2. 添加 `GrokOptions`、`IGrokClient`、`GrokClient`
3. 添加 `IStoryGenerationService` 与实现
4. 添加 `StoryController` 及请求/响应 DTO
5. 在 `Program.cs` 注册依赖与 `HttpClient`
6. 简单端到端测试（调用 Grok 返回 JSON 并映射到 DTO）

## 风险与待确认
- **Grok API 具体地址与鉴权方式**是否为 OpenAI 兼容格式
- **返回结构**：是否支持强制 JSON 输出（若不支持，需要增加 JSON 提取/修复逻辑）
- **模型名称**：`grok-2`、`grok-2-mini` 或其他

## 需要你的确认
- Grok API 的 base URL、模型名、鉴权方式
- 种子与剧情推进的期望字段与示例输出

---

确认后我开始实现。