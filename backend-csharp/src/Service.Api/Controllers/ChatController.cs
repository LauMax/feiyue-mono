using Microsoft.AspNetCore.Mvc;
using Service.Chat;
using Service.InternalContracts;

namespace Service.Api.Controllers;

/// <summary>聊天控制器</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class ChatController : ControllerBase
{
    private readonly ILogger<ChatController> _logger;
    private readonly IChatService _chatService;

    public ChatController(ILogger<ChatController> logger, IChatService chatService)
    {
        _logger = logger;
        _chatService = chatService;
    }

    /// <summary>获取聊天室</summary>
    [HttpGet("room/{roomId}")]
    public async Task<IActionResult> GetRoom(string roomId, CancellationToken cancellationToken)
    {
        var room = await _chatService.GetRoomAsync(roomId, cancellationToken);

        if (room is null)
            return NotFound(new { message = "Room not found" });

        return Ok(room);
    }

    /// <summary>获取用户的活跃聊天室</summary>
    [HttpGet("user/{userId}/active-room")]
    public async Task<IActionResult> GetActiveRoom(string userId, CancellationToken cancellationToken)
    {
        var room = await _chatService.GetActiveRoomForUserAsync(userId, cancellationToken);

        if (room is null)
            return NotFound(new { message = "No active room found" });

        return Ok(room);
    }

    /// <summary>关闭聊天室</summary>
    [HttpPost("room/{roomId}/close")]
    public async Task<IActionResult> CloseRoom(string roomId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Closing room {RoomId}", roomId);

        await _chatService.CloseRoomAsync(roomId, cancellationToken);

        return Ok(new { message = "Room closed successfully" });
    }

    /// <summary>发送消息</summary>
    [HttpPost("room/{roomId}/messages")]
    public async Task<IActionResult> SendMessage(string roomId, [FromBody] SendMessageRequest request, CancellationToken cancellationToken)
    {
        var message = await _chatService.SendMessageAsync(roomId, request.SenderId, request.Content, cancellationToken);

        return Ok(message);
    }

    /// <summary>获取聊天室消息</summary>
    [HttpGet("room/{roomId}/messages")]
    public async Task<IActionResult> GetMessages(string roomId, CancellationToken cancellationToken, [FromQuery] int limit = 50)
    {
        var room = await _chatService.GetRoomAsync(roomId, cancellationToken);
        var messages = await _chatService.GetMessagesAsync(roomId, limit, cancellationToken);

        // 转换为前端期望的格式: role/message/timestamp/isStoryClue
        var result = messages.Select(m => new
        {
            id = m.Id,
            role = MapSenderToRole(m.SenderId, room),
            message = m.Content,
            timestamp = m.SentAt.ToUnixTimeMilliseconds(),
            isStoryClue = m.MessageType == "system"
        });

        return Ok(result);
    }

    /// <summary>将 senderId 映射为前端角色 A/B/system</summary>
    private static string MapSenderToRole(string senderId, ChatRoom? room)
    {
        if (senderId == "system")
            return "system";
        if (room is null)
            return "A";
        return senderId == room.User1Id ? "A" : "B";
    }
}

// Request DTOs
public record SendMessageRequest(string SenderId, string Content);