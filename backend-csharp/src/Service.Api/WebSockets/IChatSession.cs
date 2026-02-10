using System.Threading.Channels;
using Service.InternalContracts;

namespace Service.Api.WebSockets;

/// <summary>
/// 聊天会话接口 - 借鉴 Picasso 的 Channel 驱动架构。
/// 不同的实现处理不同的聊天模式（真人 vs 虚拟人）。
/// </summary>
public interface IChatSession : IAsyncDisposable
{
    /// <summary>处理用户发来的消息</summary>
    Task HandleMessageAsync(string content, CancellationToken cancellationToken);
}

/// <summary>
/// 聊天会话工厂 - 根据房间类型创建不同的会话。
/// </summary>
public interface IChatSessionFactory
{
    /// <summary>根据房间信息创建会话</summary>
    IChatSession Create(
        string roomId,
        string userId,
        ChannelWriter<ChatServerEvent> outputChannel,
        bool isVirtual);
}
