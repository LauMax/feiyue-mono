using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using Service.Chat;
using Service.InternalContracts;

namespace Service.Api.WebSockets;

/// <summary>
/// WebSocket 聊天处理器 - 借鉴 Picasso 的 Channel 驱动架构。
///
/// 核心设计：
/// 1. 读循环：从 WebSocket 读取客户端事件 → 分发给 IChatSession
/// 2. 写循环：从 Channel 消费服务端事件 → 写入 WebSocket 发给客户端
/// 3. IChatSession 负责业务逻辑（真人转发 or 虚拟人回复），产出事件写入 Channel
///
/// 这样实现了传输层和业务逻辑的完全解耦。
/// </summary>
public sealed class ChatWebSocketHandler
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    private readonly ILogger<ChatWebSocketHandler> _logger;
    private readonly IChatSessionFactory _sessionFactory;
    private readonly RoomConnectionManager _connectionManager;
    private readonly IChatService _chatService;

    public ChatWebSocketHandler(
        ILogger<ChatWebSocketHandler> logger,
        IChatSessionFactory sessionFactory,
        RoomConnectionManager connectionManager,
        IChatService chatService)
    {
        _logger = logger;
        _sessionFactory = sessionFactory;
        _connectionManager = connectionManager;
        _chatService = chatService;
    }

    public async Task HandleWebSocketAsync(WebSocket webSocket, string roomId, string userId, CancellationToken cancellationToken)
    {
        // 检查房间是否存在，确定是否是虚拟人房间
        var room = await _chatService.GetRoomAsync(roomId, cancellationToken);
        var isVirtual = room?.IsVirtual ?? false;

        _logger.LogInformation("WebSocket connected: User {UserId} joined room {RoomId} (virtual: {IsVirtual})", userId, roomId, isVirtual);

        // 创建事件 Channel - 借鉴 Picasso 的无界队列设计
        var outputChannel = Channel.CreateUnbounded<ChatServerEvent>(new UnboundedChannelOptions
        {
            AllowSynchronousContinuations = false,
            SingleReader = true,
            SingleWriter = false
        });

        // 注册 Channel 到连接管理器（广播时通过 Channel 投递，而非直接写 WebSocket）
        _connectionManager.AddConnection(roomId, userId, outputChannel.Writer);

        // 创建会话（真人 or 虚拟人）
        await using var session = _sessionFactory.Create(roomId, userId, outputChannel.Writer, isVirtual);

        // 发送欢迎消息
        outputChannel.Writer.TryWrite(new ChatServerEvent("connected", new { message = "Successfully connected to chat room", isVirtual }));

        // 启动读写双循环 - 借鉴 Picasso 的 ChatController 模式
        var writeTask = WriteEventsAsync(webSocket, outputChannel.Reader, cancellationToken);
        try
        {
            await ReadEventsAsync(webSocket, session, cancellationToken);
        }
        finally
        {
            outputChannel.Writer.Complete();
            await writeTask;
            _connectionManager.RemoveConnection(roomId, userId);
            _logger.LogInformation("WebSocket disconnected: User {UserId} left room {RoomId}", userId, roomId);
        }
    }

    /// <summary>
    /// 读循环：从 WebSocket 读取客户端消息 → 分发给 IChatSession 处理。
    /// 对应 Picasso ChatController.ReadEventsAsync
    /// </summary>
    private async Task ReadEventsAsync(WebSocket webSocket, IChatSession session, CancellationToken cancellationToken)
    {
        var buffer = new byte[1024 * 4];
        try
        {
            while (webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
                    break;
                }

                if (result.MessageType != WebSocketMessageType.Text)
                    continue;

                var messageJson = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var envelope = JsonSerializer.Deserialize<ChatClientEvent>(messageJson, JsonOptions);
                if (envelope?.Content is null)
                    continue;

                await session.HandleMessageAsync(envelope.Content, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Read loop cancelled");
        }
        catch (WebSocketException ex)
        {
            _logger.LogWarning(ex, "WebSocket read error");
        }
    }

    /// <summary>
    /// 写循环：从 Channel 消费事件 → 序列化后通过 WebSocket 发给客户端。
    /// 对应 Picasso ChatController.WriteEventsAsync
    /// </summary>
    private async Task WriteEventsAsync(WebSocket webSocket, ChannelReader<ChatServerEvent> reader, CancellationToken cancellationToken)
    {
        try
        {
            await foreach (var serverEvent in reader.ReadAllAsync(cancellationToken))
            {
                if (webSocket.State != WebSocketState.Open)
                    break;

                var json = JsonSerializer.Serialize(serverEvent, JsonOptions);
                var bytes = Encoding.UTF8.GetBytes(json);
                await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            // 正常关闭
        }
        catch (WebSocketException)
        {
            // Socket 已关闭
        }
    }
}