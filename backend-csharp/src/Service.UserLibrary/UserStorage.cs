using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Service.InternalContracts;

namespace Service.UserStorage;

public sealed class UserStorage : IUserStorage
{
    private readonly IMongoCollection<User> _users;
    private readonly IMongoCollection<UserProfile> _userProfiles;

    public UserStorage(IOptions<DatabaseOptions> options)
    {
        var mongoUrl = new MongoUrl(options.Value.ConnectionString);
        var client = new MongoClient(mongoUrl);
        var db = client.GetDatabase(mongoUrl.DatabaseName ?? "feiyue");
        _users = db.GetCollection<User>("users");
        _userProfiles = db.GetCollection<UserProfile>("user_profiles");
    }

    public async Task<User?> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _users.Find(x => x.Id == userId).FirstOrDefaultAsync(cancellationToken);
        return user;
    }

    public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _users.InsertOneAsync(user, null, cancellationToken);
        return user;
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        var result = await _users.ReplaceOneAsync(x => x.Id == user.Id, user, new ReplaceOptions { IsUpsert = true }, cancellationToken);
        return user;
    }

    public async Task<UserProfile?> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default)
    {
        var profile = await _userProfiles.Find(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        return profile;
    }

    public async Task<UserProfile> UpdateUserProfileAsync(UserProfile profile, CancellationToken cancellationToken = default)
    {
        var result = await _userProfiles.ReplaceOneAsync(x => x.UserId == profile.UserId, profile, new ReplaceOptions { IsUpsert = true }, cancellationToken);
        return profile;
    }
}

/// <summary>Database options</summary>
public sealed class DatabaseOptions
{
    public required string ConnectionString { get; set; }
}