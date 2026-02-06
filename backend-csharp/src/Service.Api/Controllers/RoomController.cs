using Microsoft.AspNetCore.Mvc;
using Service.Chat;

namespace Service.Api.Controllers;

/// <summary>房间控制器 - 对齐 Python 后端 /api/room 路由</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IChatService _chatService;

    public RoomController(ILogger<RoomController> logger, IChatService chatService)
    {
        _logger = logger;
        _chatService = chatService;
    }

    /// <summary>离开聊天室</summary>
    /// <remarks>POST /api/room/leave?room_id=xxx&amp;user_id=xxx</remarks>
    [HttpPost("leave")]
    public async Task<IActionResult> LeaveRoom([FromQuery(Name = "room_id")] string roomId, [FromQuery(Name = "user_id")] string userId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {UserId} leaving room {RoomId}", userId, roomId);

        // 检查用户是否在房间内
        var isInRoom = await _chatService.IsUserInRoomAsync(roomId, userId, cancellationToken);
        if (!isInRoom)
            return StatusCode(403, new { success = false, error = new { code = "NOT_IN_ROOM", message = "用户不在此房间内" } });

        // 结束聊天室
        var success = await _chatService.EndRoomAsync(roomId, cancellationToken);
        if (!success)
            return NotFound(new { success = false, error = new { code = "NOT_FOUND", message = "房间不存在" } });

        return Ok(new { success = true, message = "已退出聊天室" });
    }
}