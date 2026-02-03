import { fetchApi } from './config'

// 匹配相关类型
export interface MatchRequest {
  userId: string
  gender: string
  ageGroup: string
  tags: string[]
}

export interface MatchResponse {
  success: boolean
  status: string
  roomId?: string
  errorMessage?: string
}

export interface MatchStatus {
  status: string
  position: number
  roomId?: string
}

// 匹配 API - 参考 Studio 的 services 模式
export const matchApi = {
  async requestMatch(request: MatchRequest): Promise<MatchResponse> {
    return fetchApi('/api/match/request', {
      method: 'POST',
      body: JSON.stringify(request),
    })
  },

  async getMatchStatus(userId: string): Promise<MatchStatus> {
    return fetchApi(`/api/match/status/${userId}`)
  },

  async cancelMatch(userId: string): Promise<void> {
    return fetchApi(`/api/match/cancel/${userId}`, {
      method: 'DELETE',
    })
  },
}
