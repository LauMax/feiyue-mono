from fastapi import APIRouter, HTTPException
from pydantic import BaseModel, Field
from typing import Optional
import os
import httpx

router = APIRouter(prefix="/api/story", tags=["story"])

# ==================== Models ====================

class RoleData(BaseModel):
    name: str
    description: str
    personality: str

class StoryRequest(BaseModel):
    user_a_gender: str = Field(..., description="male or female")
    user_b_gender: str = Field(..., description="male or female")
    user_a_tags: list[str] = Field(default_factory=list)
    user_b_tags: list[str] = Field(default_factory=list)

class StoryResponse(BaseModel):
    title: str
    background: str
    male_role: RoleData
    female_role: RoleData


# ==================== Grok API Integration ====================

GROK_API_KEY = os.getenv("GROK_API_KEY", "")
GROK_API_URL = "https://api.x.ai/v1/chat/completions"

async def call_grok_api(prompt: str) -> str:
    """调用 Grok API"""
    if not GROK_API_KEY:
        # 开发模式：返回模拟数据
        return """
{
  "title": "图书馆的偶遇",
  "background": "在一个阳光明媚的下午，两位年轻人在图书馆不期而遇...",
  "maleRole": {
    "name": "林墨",
    "description": "文艺青年，喜欢阅读和写作",
    "personality": "内敛、细腻、善于观察"
  },
  "femaleRole": {
    "name": "苏晴",
    "description": "活泼开朗的大学生",
    "personality": "外向、热情、喜欢交友"
  }
}
"""
    
    try:
        async with httpx.AsyncClient() as client:
            response = await client.post(
                GROK_API_URL,
                headers={
                    "Authorization": f"Bearer {GROK_API_KEY}",
                    "Content-Type": "application/json"
                },
                json={
                    "model": "grok-beta",
                    "messages": [
                        {
                            "role": "system",
                            "content": "你是一个创意故事生成助手，擅长创作浪漫的角色扮演背景故事。"
                        },
                        {
                            "role": "user",
                            "content": prompt
                        }
                    ],
                    "temperature": 0.8,
                    "max_tokens": 1000
                },
                timeout=30.0
            )
            
            if response.status_code == 200:
                data = response.json()
                return data["choices"][0]["message"]["content"]
            else:
                raise HTTPException(
                    status_code=response.status_code,
                    detail=f"Grok API error: {response.text}"
                )
                
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Failed to call Grok API: {str(e)}")


def build_story_prompt(request: StoryRequest) -> str:
    """构建 Grok 提示词"""
    tags_a = ", ".join(request.user_a_tags) if request.user_a_tags else "无特定标签"
    tags_b = ", ".join(request.user_b_tags) if request.user_b_tags else "无特定标签"
    
    return f"""
请为一对匿名聊天的用户生成一个浪漫的角色扮演故事背景。

用户信息：
- 用户A：{request.user_a_gender}，标签：{tags_a}
- 用户B：{request.user_b_gender}，标签：{tags_b}

要求：
1. 创作一个吸引人的故事标题
2. 写一段150字左右的故事背景，描述两个角色相遇的场景
3. 为男性角色设计：姓名、外貌描述、性格特点
4. 为女性角色设计：姓名、外貌描述、性格特点
5. 角色设定要符合用户标签，并且有一定的互补性

请以 JSON 格式返回，格式如下：
{{
  "title": "故事标题",
  "background": "故事背景描述",
  "maleRole": {{
    "name": "男性角色名",
    "description": "外貌描述",
    "personality": "性格特点"
  }},
  "femaleRole": {{
    "name": "女性角色名",
    "description": "外貌描述",
    "personality": "性格特点"
  }}
}}
"""


# ==================== API Endpoints ====================

@router.post("/generate", response_model=StoryResponse)
async def generate_story(request: StoryRequest):
    """
    生成故事背景
    
    - **user_a_gender**: 用户A性别 (male/female)
    - **user_b_gender**: 用户B性别 (male/female)
    - **user_a_tags**: 用户A标签列表
    - **user_b_tags**: 用户B标签列表
    """
    try:
        # 构建提示词
        prompt = build_story_prompt(request)
        
        # 调用 Grok API
        grok_response = await call_grok_api(prompt)
        
        # 解析 JSON（Grok 可能返回 Markdown 包裹的 JSON）
        import json
        import re
        
        # 提取 JSON（去除可能的 markdown 代码块标记）
        json_match = re.search(r'\{[\s\S]*\}', grok_response)
        if json_match:
            story_data = json.loads(json_match.group(0))
        else:
            story_data = json.loads(grok_response)
        
        # 转换为响应模型
        return StoryResponse(
            title=story_data["title"],
            background=story_data["background"],
            male_role=RoleData(**story_data["maleRole"]),
            female_role=RoleData(**story_data["femaleRole"])
        )
        
    except json.JSONDecodeError as e:
        raise HTTPException(
            status_code=500,
            detail=f"Failed to parse Grok response: {str(e)}"
        )
    except Exception as e:
        raise HTTPException(
            status_code=500,
            detail=f"Failed to generate story: {str(e)}"
        )


@router.get("/health")
async def health_check():
    """健康检查"""
    return {
        "status": "healthy",
        "grok_api_configured": bool(GROK_API_KEY)
    }
