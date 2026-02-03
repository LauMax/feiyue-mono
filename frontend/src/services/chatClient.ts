import { config } from './config'

// WebSocket 消息类型
export interface ChatMessage {
  id: string
  roomId: string
  role: string
  message: string
  timestamp: number
  isStoryClue?: boolean
}

// WebSocket 客户端 - 参考 Studio 的 chat-client-v2.ts
export class ChatClient {
  private ws: WebSocket | null = null
  private reconnectAttempts = 0
  private maxReconnectAttempts = 5
  private reconnectDelay = 1000
  private messageHandlers: Set<(message: ChatMessage) => void> = new Set()
  private errorHandlers: Set<(error: Error) => void> = new Set()
  private closeHandlers: Set<() => void> = new Set()

  constructor(private roomId: string) {}

  connect(): void {
    const wsUrl = `${config.wsBaseUrl}/ws/chat/${this.roomId}`
    this.ws = new WebSocket(wsUrl)

    this.ws.onopen = () => {
      console.log('WebSocket connected')
      this.reconnectAttempts = 0
    }

    this.ws.onmessage = (event) => {
      try {
        const message: ChatMessage = JSON.parse(event.data)
        this.messageHandlers.forEach(handler => handler(message))
      } catch (error) {
        console.error('Failed to parse message:', error)
      }
    }

    this.ws.onerror = (event) => {
      const error = new Error('WebSocket error')
      this.errorHandlers.forEach(handler => handler(error))
    }

    this.ws.onclose = () => {
      console.log('WebSocket closed')
      this.closeHandlers.forEach(handler => handler())
      this.attemptReconnect()
    }
  }

  private attemptReconnect(): void {
    if (this.reconnectAttempts < this.maxReconnectAttempts) {
      this.reconnectAttempts++
      setTimeout(() => {
        console.log(`Reconnecting... (attempt ${this.reconnectAttempts})`)
        this.connect()
      }, this.reconnectDelay * this.reconnectAttempts)
    }
  }

  sendMessage(message: string): void {
    if (this.ws?.readyState === WebSocket.OPEN) {
      this.ws.send(JSON.stringify({ message }))
    } else {
      throw new Error('WebSocket is not connected')
    }
  }

  onMessage(handler: (message: ChatMessage) => void): void {
    this.messageHandlers.add(handler)
  }

  onError(handler: (error: Error) => void): void {
    this.errorHandlers.add(handler)
  }

  onClose(handler: () => void): void {
    this.closeHandlers.add(handler)
  }

  disconnect(): void {
    this.ws?.close()
    this.ws = null
  }
}
