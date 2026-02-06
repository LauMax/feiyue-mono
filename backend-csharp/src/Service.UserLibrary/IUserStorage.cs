using Service.InternalContracts;

namespace Service.UserStorage;

/// <summary>Storage layer for user data using Supabase PostgreSQL.</summary>
public interface IUserStorage
{
    Task<User?> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default);
    Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    Task<UserProfile?> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default);
    Task<UserProfile> UpdateUserProfileAsync(UserProfile profile, CancellationToken cancellationToken = default);
}