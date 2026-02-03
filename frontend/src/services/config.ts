// API 基础配置 - 参考 Studio 的 services/shared 模式
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000'
const WS_BASE_URL = import.meta.env.VITE_WS_URL || 'ws://localhost:5000'

export const config = {
  apiBaseUrl: API_BASE_URL,
  wsBaseUrl: WS_BASE_URL,
}

// HTTP 客户端配置
export async function fetchApi<T>(
  endpoint: string,
  options?: RequestInit
): Promise<T> {
  const response = await fetch(`${config.apiBaseUrl}${endpoint}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...options?.headers,
    },
  })

  if (!response.ok) {
    throw new Error(`API Error: ${response.statusText}`)
  }

  return response.json()
}
