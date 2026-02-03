/**
 * API 配置文件
 * 直接使用真实 API
 * 
 * 环境配置：
 * - 生产环境：https://api.fei-yue.net
 * - Staging/Dev 环境：https://api-dev.fei-yue.net
 * - 本地开发：http://localhost:8000
 */

// 从环境变量读取 API 地址，本地开发默认使用 localhost
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:8000';

export const apiConfig = {
  baseURL: API_BASE_URL,
  isDevelopment: import.meta.env.DEV,
  
  // API 端点（后端需要 /api 前缀）
  endpoints: {
    // 匹配系统
    matchRequest: '/api/match/request',
    matchStatus: '/api/match/status/:matchId',
    matchCancel: '/api/match/cancel',
    
    // 聊天系统
    chatSend: '/api/chat/send',
    chatMessages: '/api/chat/messages/:roomId',
    roomLeave: '/api/room/leave',
    
    // 健康检查
    health: '/health',
  },
  
  // 超时配置（毫秒）
  timeout: {
    default: 30000,
    match: 300000,  // 匹配最多等待 5 分钟
  },
  
  // 轮询配置
  polling: {
    matchStatus: 2000,  // 每 2 秒查询一次匹配状态
    maxRetries: 150,    // 最多轮询 150 次 = 5 分钟
  }
};

export default apiConfig;
