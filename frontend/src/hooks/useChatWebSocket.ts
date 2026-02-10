import { useEffect, useRef, useState, useCallback } from 'react';

interface Message {
  id: string;
  roomId: string;
  senderId: string;
  content: string;
  messageType: string;
  sentAt: number;
}

interface WebSocketMessage {
  type: 'connected' | 'message' | 'user_left' | 'error';
  data?: Message | { userId: string; message: string };
  message?: string;
}

interface UseChatWebSocketOptions {
  roomId: string;
  userId: string;
  onMessage: (message: Message) => void;
  onConnect?: () => void;
  onPartnerLeft?: () => void;
  onError?: (error: string) => void;
}

export function useChatWebSocket({
  roomId,
  userId,
  onMessage,
  onConnect,
  onPartnerLeft,
  onError,
}: UseChatWebSocketOptions) {
  const wsRef = useRef<WebSocket | null>(null);
  const [isConnected, setIsConnected] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const reconnectTimeoutRef = useRef<NodeJS.Timeout>();
  const intentionalCloseRef = useRef(false);

  // 用 ref 稳定回调引用，避免 connect 函数每次渲染都变化导致重连循环
  const onMessageRef = useRef(onMessage);
  const onConnectRef = useRef(onConnect);
  const onPartnerLeftRef = useRef(onPartnerLeft);
  const onErrorRef = useRef(onError);
  onMessageRef.current = onMessage;
  onConnectRef.current = onConnect;
  onPartnerLeftRef.current = onPartnerLeft;
  onErrorRef.current = onError;

  const connect = useCallback(() => {
    // 防止重复连接
    if (wsRef.current?.readyState === WebSocket.OPEN || wsRef.current?.readyState === WebSocket.CONNECTING) {
      return;
    }

    intentionalCloseRef.current = false;
    const wsBaseUrl = import.meta.env.VITE_WS_URL || 'ws://localhost:5050';
    const wsUrl = `${wsBaseUrl}/ws/chat?roomId=${roomId}&userId=${userId}`;
    console.log('[WebSocket] Connecting to:', wsUrl);

    const ws = new WebSocket(wsUrl);

    ws.onopen = () => {
      console.log('[WebSocket] Connected');
      setIsConnected(true);
      setError(null);
      onConnectRef.current?.();
    };

    ws.onmessage = (event) => {
      try {
        const wsMessage: WebSocketMessage = JSON.parse(event.data);
        console.log('[WebSocket] Received:', wsMessage);

        if (wsMessage.type === 'message' && wsMessage.data) {
          onMessageRef.current(wsMessage.data as Message);
        } else if (wsMessage.type === 'user_left') {
          console.log('[WebSocket] Partner left the chat');
          onPartnerLeftRef.current?.();
        } else if (wsMessage.type === 'error') {
          const errorMsg = wsMessage.message || 'Unknown error';
          setError(errorMsg);
          onErrorRef.current?.(errorMsg);
        }
      } catch (err) {
        console.error('[WebSocket] Failed to parse message:', err);
      }
    };

    ws.onerror = (event) => {
      console.error('[WebSocket] Error:', event);
      setError('WebSocket connection error');
      onErrorRef.current?.('WebSocket connection error');
    };

    ws.onclose = () => {
      console.log('[WebSocket] Disconnected');
      setIsConnected(false);

      // 只有非主动关闭才重连
      if (!intentionalCloseRef.current) {
        reconnectTimeoutRef.current = setTimeout(() => {
          console.log('[WebSocket] Attempting to reconnect...');
          connect();
        }, 3000);
      }
    };

    wsRef.current = ws;
  }, [roomId, userId]); // 只依赖 roomId 和 userId，回调通过 ref 读取

  const disconnect = useCallback(() => {
    intentionalCloseRef.current = true;

    if (reconnectTimeoutRef.current) {
      clearTimeout(reconnectTimeoutRef.current);
    }

    if (wsRef.current) {
      wsRef.current.close();
      wsRef.current = null;
    }

    setIsConnected(false);
  }, []);

  const sendMessage = useCallback((content: string) => {
    if (!wsRef.current || wsRef.current.readyState !== WebSocket.OPEN) {
      console.error('[WebSocket] Not connected, cannot send message');
      return false;
    }

    const envelope = {
      type: 'message',
      content,
    };

    wsRef.current.send(JSON.stringify(envelope));
    console.log('[WebSocket] Sent:', envelope);
    return true;
  }, []);

  useEffect(() => {
    connect();

    return () => {
      disconnect();
    };
  }, [connect, disconnect]);

  return {
    isConnected,
    error,
    sendMessage,
    reconnect: connect,
  };
}
