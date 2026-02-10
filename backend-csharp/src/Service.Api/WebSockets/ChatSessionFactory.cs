using System.Threading.Channels;
using Service.Chat;
using Service.InternalContracts;

namespace Service.Api.WebSockets;

/// <summary>
/// 聊天会话工厂 - 根据房间类型创建 HumanChatSession 或 VirtualChatSession。
/// 对应 Picasso 的 IChatHandlerFactory 模式。
/// </summary>
public sealed class ChatSessionFactory : IChatSessionFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IChatService _chatService;
    private readonly IChatProgressService _progressService;
    private readonly RoomConnectionManager _connectionManager;

    public ChatSessionFactory(
        ILoggerFactory loggerFactory,
        IChatService chatService,
        IChatProgressService progressService,
        RoomConnectionManager connectionManager)
    {
        _loggerFactory = loggerFactory;
        _chatService = chatService;
        _progressService = progressService;
        _connectionManager = connectionManager;
    }

    public IChatSession Create(
        string roomId,
        string userId,
        ChannelWriter<ChatServerEvent> outputChannel,
        bool isVirtual)
    {
        if (isVirtual)
        {
            return new VirtualChatSession(
                _loggerFactory.CreateLogger<VirtualChatSession>(),
                _chatService,
                roomId,
                userId,
                outputChannel);
        }

        return new HumanChatSession(
            _loggerFactory.CreateLogger<HumanChatSession>(),
            _chatService,
            _progressService,
            _connectionManager,
            roomId,
            userId,
            outputChannel);
    }
}
