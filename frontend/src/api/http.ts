/**
 * HTTP 客户端
 * 支持 fetch API，无需额外依赖
 */

interface RequestOptions extends RequestInit {
  timeout?: number;
}

interface ApiResponse<T = any> {
  success: boolean;
  data?: T;
  error?: {
    code: string;
    message: string;
  };
}

class HttpClient {
  private baseURL: string;
  private defaultTimeout: number = 30000;

  constructor(baseURL: string = '') {
    this.baseURL = baseURL;
  }

  private async fetchWithTimeout(
    url: string,
    options: RequestOptions = {},
    timeout: number = this.defaultTimeout
  ): Promise<Response> {
    const controller = new AbortController();
    const timeoutId = setTimeout(() => controller.abort(), timeout);

    try {
      const response = await fetch(url, {
        ...options,
        signal: controller.signal,
      });
      return response;
    } finally {
      clearTimeout(timeoutId);
    }
  }

  private buildUrl(path: string, params?: Record<string, any>): string {
    let url = path.startsWith('http') ? path : `${this.baseURL}${path}`;
    
    if (params) {
      const queryString = new URLSearchParams();
      Object.entries(params).forEach(([key, value]) => {
        if (value !== null && value !== undefined) {
          queryString.append(key, String(value));
        }
      });
      const query = queryString.toString();
      url += query ? `?${query}` : '';
    }
    
    return url;
  }

  async get<T = any>(
    path: string,
    params?: Record<string, any>,
    options?: RequestOptions
  ): Promise<ApiResponse<T>> {
    const url = this.buildUrl(path, params);
    const timeout = options?.timeout || this.defaultTimeout;

    try {
      const response = await this.fetchWithTimeout(
        url,
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            ...options?.headers,
          },
        },
        timeout
      );

      return this.handleResponse<T>(response);
    } catch (error) {
      return this.handleError(error);
    }
  }

  async post<T = any>(
    path: string,
    data?: any,
    options?: RequestOptions
  ): Promise<ApiResponse<T>> {
    const url = this.buildUrl(path);
    const timeout = options?.timeout || this.defaultTimeout;

    try {
      const response = await this.fetchWithTimeout(
        url,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            ...options?.headers,
          },
          body: data ? JSON.stringify(data) : undefined,
        },
        timeout
      );

      return this.handleResponse<T>(response);
    } catch (error) {
      return this.handleError(error);
    }
  }

  private async handleResponse<T>(response: Response): Promise<ApiResponse<T>> {
    const contentType = response.headers.get('content-type');
    let data: any;

    if (contentType?.includes('application/json')) {
      data = await response.json();
    } else {
      data = await response.text();
    }

    if (!response.ok) {
      return {
        success: false,
        error: {
          code: data?.error?.code || `HTTP_${response.status}`,
          message: data?.error?.message || data?.message || response.statusText,
        },
      };
    }

    // 如果响应已经是 ApiResponse 格式，直接返回
    if (data && typeof data === 'object' && 'success' in data) {
      return data;
    }

    // 否则包装成 ApiResponse
    return {
      success: true,
      data: data,
    };
  }

  private handleError(error: any): ApiResponse {
    if (error.name === 'AbortError') {
      return {
        success: false,
        error: {
          code: 'TIMEOUT',
          message: '请求超时，请检查网络连接',
        },
      };
    }

    return {
      success: false,
      error: {
        code: 'NETWORK_ERROR',
        message: error.message || '网络错误，请稍后重试',
      },
    };
  }

  setDefaultTimeout(timeout: number): void {
    this.defaultTimeout = timeout;
  }
}

export default HttpClient;
