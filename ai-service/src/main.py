from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
import os

app = FastAPI(
    title="Feiyue AI Service",
    description="AI 服务 - 故事生成、虚拟角色、ML 匹配",
    version="1.0.0"
)

# CORS 配置
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # 开发环境，生产环境需要限制
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

@app.get("/health")
async def health_check():
    """健康检查端点"""
    return {"status": "healthy", "service": "ai-service"}

@app.get("/")
async def root():
    """根端点"""
    return {
        "service": "Feiyue AI Service",
        "version": "1.0.0",
        "endpoints": {
            "story": "/api/story/*",
            "character": "/api/character/*",
            "match": "/api/match/*",
        }
    }

# 导入 API 路由
from .api import story_api
app.include_router(story_api.router)

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)
