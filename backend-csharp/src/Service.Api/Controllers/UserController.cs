using Microsoft.AspNetCore.Mvc;
using Service.InternalContracts;
using Service.UserStorage;

namespace Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserStorage _userStorage;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserStorage userStorage, ILogger<UserController> logger)
    {
        _userStorage = userStorage;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var userId = Guid.NewGuid().ToString();
        var user = new User(Id: userId, AnonymousId: userId, CreatedAt: DateTimeOffset.UtcNow);

        var created = await _userStorage.CreateUserAsync(user);
        return Ok(created);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        var user = await _userStorage.GetUserByIdAsync(userId);
        if (user is null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost("{userId}/profile")]
    public async Task<IActionResult> UpdateProfile(string userId, [FromBody] UpdateProfileRequest request)
    {
        var profile = new UserProfile(
            UserId: userId,
            Gender: request.Gender,
            Age: request.Age,
            Interests: request.Interests,
            Stories: request.Stories,
            IsVip: request.IsVip,
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow);

        var updated = await _userStorage.UpdateUserProfileAsync(profile);
        return Ok(updated);
    }

    [HttpGet("{userId}/profile")]
    public async Task<IActionResult> GetProfile(string userId)
    {
        var profile = await _userStorage.GetUserProfileAsync(userId);
        if (profile is null)
            return NotFound();
        return Ok(profile);
    }
}

public sealed record CreateUserRequest();

public sealed record UpdateProfileRequest(string Gender, int Age, IReadOnlyList<string> Interests, IReadOnlyList<Story> Stories, bool IsVip);