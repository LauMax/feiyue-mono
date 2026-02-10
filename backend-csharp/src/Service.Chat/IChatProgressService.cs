using Service.InternalContracts;

namespace Service.Chat;

/// <summary>
/// 聊天进度服务接口 — 消息发送后检查是否应触发剧情线索。
/// </summary>
public interface IChatProgressService
{
    /// <summary>
    /// 处理消息发送后的进程更新 + 线索触发检查。
    /// 返回线索事件（如果触发），调用方负责广播。
    /// </summary>
    Task<ChatServerEvent?> ProcessMessageAsync(string roomId, string senderId, CancellationToken cancellationToken);
}
