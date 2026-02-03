/**
 * 真实 API 实现
 * 调用实际的后端 API
 */

import HttpClient from './http';
import apiConfig from './config';
import type { UserProfileData } from '../app/components/UserProfile';
import type { Story } from '../app/App';

// 匹配请求需要用户资料和角色
export interface MatchRequestPayload {
  profile: UserProfileData;
  selectedRole: 'A' | 'B';  // 根据性别自动设置：男=A, 女=B
}

// 后端期望的 profile 格式
interface BackendProfilePayload {
  gender: 'male' | 'female' | 'other';
  ageGroup: '<18' | '18-23' | '23+';
  height: string;
  weight: string;
  tags: string[];
  description: string;
}

export interface MatchResponse {
  userId: string;          // 真实用户ID（用于发送消息）
  anonymousId: string;     // 匿名ID（显示给用户）
  matchId: string;
  status: 'waiting' | 'matched';  // 匹配状态
  roomId?: string;         // 已匹配时返回
  story?: Story;           // 匹配成功后返回故事
  yourRole?: 'A' | 'B';    // 你的角色
  partnerProfile?: UserProfileData; // 对方资料
  message: string;
  queuePosition?: {
    gender: string;
    waitingCount: number;
  };
}

export interface MatchStatusResponse {
  status: 'waiting' | 'matched';
  waitTime?: number;
  roomId?: string;
  story?: Story;           // 匹配成功后返回故事
  yourRole?: 'A' | 'B';    // 你的角色
  partnerRole?: 'A' | 'B';
  partnerProfile?: UserProfileData; // 对方资料
}

export interface ChatMessage {
  id: string;
  roomId: string;
  role: 'A' | 'B' | 'system';
  message: string;
  timestamp: number;
  isStoryClue?: boolean;
}

export interface StoryClueMessage {
  id: string;
  roomId: string;
  role: 'system';
  message: string;
  timestamp: number;
  isStoryClue: true;
  triggerType: 'rounds' | 'silence' | 'manual';
  triggerData?: {
    roundCount?: number;
    silenceSeconds?: number;
  };
}

export interface ChatSendResponse {
  message: ChatMessage;
  storyClue?: StoryClueMessage;
  roomStats?: {
    totalMessages: number;
    conversationRounds: number;
    lastActivityTime: number;
  };
}

export interface ChatSendPayload {
  roomId: string;
  userId: string;
  message: string;
}

// 后端返回的消息可以是数组或 { messages: [] } 格式
export type ChatMessagesResponse = ChatMessage[] | { messages: ChatMessage[]; total?: number };

class RealApi {
  private client: HttpClient;

  constructor() {
    this.client = new HttpClient(apiConfig.baseURL);
    this.client.setDefaultTimeout(apiConfig.timeout.default);
  }

  /**
   * 创建匹配请求
   * POST /api/match/request
   */
  async matchRequest(payload: MatchRequestPayload): Promise<{
    success: boolean;
    data?: MatchResponse;
    error?: any;
  }> {
    // 转换并验证 profile 数据，确保符合后端期望的格式
    const profile = payload.profile;
    
    // 验证必填字段
    if (!profile.gender || !profile.ageGroup) {
      return {
        success: false,
        error: {
          code: 'VALIDATION_ERROR',
          message: '性别和年龄段为必填项'
        }
      };
    }

    // 构建符合后端格式的请求体
    const backendPayload = {
      profile: {
        gender: profile.gender,
        ageGroup: profile.ageGroup,
        height: profile.height?.toString() || '',  // 确保是字符串
        weight: profile.weight?.toString() || '',  // 确保是字符串
        tags: profile.tags || [],
        description: profile.description || ''
      } as BackendProfilePayload,
      selectedRole: payload.selectedRole
    };

    console.log('[API] 发送匹配请求:', JSON.stringify(backendPayload, null, 2));

    const response = await this.client.post<MatchResponse>(
      apiConfig.endpoints.matchRequest,
      backendPayload,
      { timeout: apiConfig.timeout.match }
    );
    return response;
  }

  /**
   * 查询匹配状态
   * GET /api/match/status/:matchId
   */
  async matchStatus(matchId: string): Promise<{
    success: boolean;
    data?: MatchStatusResponse;
    error?: any;
  }> {
    const url = apiConfig.endpoints.matchStatus.replace(':matchId', matchId);
    const response = await this.client.get<MatchStatusResponse>(url);
    return response;
  }

  /**
   * 取消匹配
   * POST /api/match/cancel?match_id=xxx
   */
  async matchCancel(matchId: string): Promise<{
    success: boolean;
    message?: string;
    error?: any;
  }> {
    const url = `${apiConfig.endpoints.matchCancel}?match_id=${matchId}`;
    const response = await this.client.post(url, {});
    return response;
  }

  /**
   * 发送聊天消息
   * POST /api/chat/send?room_id=...&user_id=...
   */
  async chatSend(payload: ChatSendPayload): Promise<{
    success: boolean;
    data?: ChatSendResponse;
    error?: any;
  }> {
    const url = `${apiConfig.endpoints.chatSend}?room_id=${payload.roomId}&user_id=${payload.userId}`;
    const response = await this.client.post<ChatSendResponse>(url, {
      message: payload.message,
    });
    return response;
  }

  /**
   * 获取聊天历史
   * GET /api/chat/messages/:roomId
   */
  async chatMessages(roomId: string): Promise<{
    success: boolean;
    data?: ChatMessagesResponse;
    error?: any;
  }> {
    const url = apiConfig.endpoints.chatMessages.replace(':roomId', roomId);
    const response = await this.client.get<ChatMessagesResponse>(url);
    return response;
  }

  /**
   * 离开房间
   * POST /api/room/leave?room_id=...&user_id=...
   */
  async roomLeave(
    roomId: string,
    userId: string
  ): Promise<{ success: boolean; message?: string; error?: any }> {
    const url = `${apiConfig.endpoints.roomLeave}?room_id=${roomId}&user_id=${userId}`;
    const response = await this.client.post(url, {});
    return response;
  }

  /**
   * 健康检查
   */
  async health(): Promise<{ success: boolean }> {
    const response = await this.client.get(apiConfig.endpoints.health);
    return response;
  }
}

export const realApi = new RealApi();
