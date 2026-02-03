/**
 * API 统一接口
 * 直接使用真实 API
 */

import { realApi } from './real';
import type { 
  MatchRequestPayload, 
  MatchResponse, 
  MatchStatusResponse, 
  ChatMessage, 
  ChatSendPayload, 
  ChatMessagesResponse,
  ChatSendResponse,
  StoryClueMessage
} from './real';

interface Api {
  matchRequest(payload: MatchRequestPayload): Promise<any>;
  matchStatus(matchId: string): Promise<any>;
  matchCancel(matchId: string): Promise<any>;
  chatSend(payload: ChatSendPayload): Promise<any>;
  chatMessages(roomId: string): Promise<any>;
  roomLeave(roomId: string, userId: string): Promise<any>;
}

class ApiClient implements Api {
  private api: Api;

  constructor() {
    this.api = realApi as any;
    console.log(`[API] Using REAL API`);
    console.log(`[API] Base URL: ${import.meta.env.VITE_API_URL || 'https://api.fei-yue.net'}`);
  }

  /**
   * 创建匹配请求
   */
  async matchRequest(payload: MatchRequestPayload) {
    return this.api.matchRequest(payload);
  }

  /**
   * 查询匹配状态
   */
  async matchStatus(matchId: string) {
    return this.api.matchStatus(matchId);
  }

  /**
   * 取消匹配
   */
  async matchCancel(matchId: string) {
    return this.api.matchCancel(matchId);
  }

  /**
   * 发送聊天消息
   */
  async chatSend(payload: ChatSendPayload) {
    return this.api.chatSend(payload);
  }

  /**
   * 获取聊天历史
   */
  async chatMessages(roomId: string) {
    return this.api.chatMessages(roomId);
  }

  /**
   * 离开房间
   */
  async roomLeave(roomId: string, userId: string) {
    return this.api.roomLeave(roomId, userId);
  }
}

// 创建单例
const api = new ApiClient();

// 在开发环境下暴露到 window 对象，方便调试
if (import.meta.env.DEV) {
  (window as any).__api__ = api;
}

export default api;
export type { MatchRequestPayload, MatchResponse, MatchStatusResponse, ChatMessage, ChatSendPayload, ChatMessagesResponse };
