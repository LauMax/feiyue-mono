using Microsoft.Extensions.Logging;
using Service.Chat;
using Service.InternalContracts;
using Service.MatchStorage;
using Service.StoryGeneration;

namespace Service.Match;

/// <summary>匹配服务实现 - 参考Picasso模式</summary>
internal sealed class MatchService : IMatchService
{
    private readonly ILogger<MatchService> _logger;
    private readonly IMatchStorage _matchStorage;
    private readonly IChatService _chatService;
    private readonly IStoryGenerationService _storyService;

    public MatchService(ILogger<MatchService> logger, IMatchStorage matchStorage, IChatService chatService, IStoryGenerationService storyService)
    {
        _logger = logger;
        _matchStorage = matchStorage;
        _chatService = chatService;
        _storyService = storyService;
    }

    public async Task<MatchResult> CreateMatchRequestAsync(MatchUserProfile profile, Story? story, string selectedRole, CancellationToken cancellationToken = default)
    {
        // 生成用户ID和匿名ID
        var userId = Guid.NewGuid().ToString();
        var anonymousId = GenerateAnonymousId();
        var now = DateTimeOffset.UtcNow;
        var matchId = Guid.NewGuid().ToString();

        _logger.LogInformation("Creating match request for user {UserId} (gender: {Gender}, role: {Role})", userId, profile.Gender, selectedRole);

        // 创建匹配请求
        var request = new MatchRequest(
            Id: matchId,
            UserId: userId,
            AnonymousId: anonymousId,
            Gender: profile.Gender,
            GenderPreference: GetGenderPreference(profile.Gender),
            Profile: profile,
            Story: story,
            SelectedRole: selectedRole,
            Priority: 0,
            RequestedAt: now,
            Status: "waiting",
            RoomId: null,
            MatchedAt: null);

        // 保存到 MongoDB
        await _matchStorage.SaveMatchRequestAsync(request, cancellationToken);

        // 创建队列条目
        var entry = new MatchQueueEntry(
            UserId: userId,
            AnonymousId: anonymousId,
            Gender: profile.Gender,
            GenderPreference: GetGenderPreference(profile.Gender),
            Profile: profile,
            Story: story,
            SelectedRole: selectedRole,
            EnqueuedAt: now,
            Priority: 0);

        // 加入 Redis 队列
        await _matchStorage.EnqueueAsync(entry, cancellationToken);

        // 尝试匹配
        var oppositeGender = profile.Gender == "male" ? "female" : "male";
        var candidates = await _matchStorage.GetQueueByGenderAsync(oppositeGender, cancellationToken);

        var bestMatch = FindBestMatch(entry, candidates);

        if (bestMatch is not null)
        {
            // 找到匹配！
            _logger.LogInformation("Match found! User {UserId} matched with {MatchedUserId}", userId, bestMatch.UserId);

            // 获取对方的匹配请求
            var partnerRequest = await _matchStorage.GetMatchRequestAsync(bestMatch.UserId, cancellationToken);

            // 决定使用哪个 story（双方都没有时 AI 生成）
            var combinedTags = (profile.Tags ?? [])
                .Concat(partnerRequest?.Profile?.Tags ?? [])
                .Distinct()
                .ToArray();
            var finalStory = await DetermineStoryAsync(story, partnerRequest?.Story, combinedTags, cancellationToken);

            // 创建聊天室
            var room = await _chatService.CreateRoomAsync(userId, bestMatch.UserId, finalStory, isVirtual: false, cancellationToken);

            // 异步发送开场叙述（fire-and-forget，不阻塞匹配响应）
            _ = SendStorySeedAsync(room.Id, finalStory, cancellationToken);

            // 从 Redis 队列移除两个用户（不删除 MongoDB 记录）
            await _matchStorage.RemoveFromQueueAsync(userId, cancellationToken);
            await _matchStorage.RemoveFromQueueAsync(bestMatch.UserId, cancellationToken);

            // 更新匹配状态
            var matchedRequest = request with
            {
                Status = "matched",
                RoomId = room.Id,
                MatchedAt = DateTimeOffset.UtcNow,
                Story = finalStory
            };
            await _matchStorage.UpdateMatchRequestAsync(matchedRequest, cancellationToken);

            if (partnerRequest is not null)
            {
                var partnerMatched = partnerRequest with { Status = "matched", RoomId = room.Id, MatchedAt = DateTimeOffset.UtcNow, Story = finalStory };
                await _matchStorage.UpdateMatchRequestAsync(partnerMatched, cancellationToken);
            }

            // 获取角色信息
            var yourRoleInfo = selectedRole == "A" ? finalStory?.MaleRole : finalStory?.FemaleRole;

            return new MatchResult(
                Success: true,
                Status: "matched",
                UserId: userId,
                AnonymousId: anonymousId,
                MatchId: matchId,
                RoomId: room.Id,
                Story: finalStory,
                YourRole: selectedRole,
                YourRoleInfo: yourRoleInfo,
                PartnerRole: selectedRole == "A" ? "B" : "A",
                PartnerProfile: bestMatch.Profile,
                Message: "匹配成功！已进入聊天室。",
                QueuePosition: null);
        }

        // 未找到匹配，返回等待状态
        var stats = await _matchStorage.GetQueueStatsAsync(cancellationToken);
        var waitingCount = profile.Gender == "male" ? stats.MaleWaiting : stats.FemaleWaiting;

        return new MatchResult(
            Success: true,
            Status: "waiting",
            UserId: userId,
            AnonymousId: anonymousId,
            MatchId: matchId,
            RoomId: null,
            Story: null,
            YourRole: null,
            YourRoleInfo: null,
            PartnerRole: null,
            PartnerProfile: null,
            Message: $"已进入等待队列。",
            QueuePosition: new QueuePosition(profile.Gender, waitingCount));
    }

    public async Task<MatchStatusResult> GetMatchStatusAsync(string matchId, CancellationToken cancellationToken = default)
    {
        var request = await _matchStorage.GetMatchRequestByIdAsync(matchId, cancellationToken);

        if (request is null)
        {
            return new MatchStatusResult(Success: false, Data: null, Error: new MatchError("NOT_FOUND", "匹配请求不存在"));
        }

        if (request.Status == "waiting")
        {
            var waitTime = (int)(DateTimeOffset.UtcNow - request.RequestedAt).TotalSeconds;

            // 超过60秒没匹配到 → 自动分配虚拟人
            if (waitTime >= 60)
            {
                _logger.LogInformation("Match {MatchId} waited {WaitTime}s, assigning virtual person", matchId, waitTime);
                return await MatchWithVirtualPersonAsync(request, cancellationToken);
            }

            // 检查是否超时（5分钟 → 彻底失败）
            if (waitTime > 300)
            {
                await CancelMatchAsync(matchId, cancellationToken);
                return new MatchStatusResult(Success: false, Data: null, Error: new MatchError("MATCH_TIMEOUT", "未能找到合适的匹配对象，请重试"));
            }

            return new MatchStatusResult(
                Success: true,
                Data: new MatchStatusData(
                    UserId: request.UserId,
                    AnonymousId: request.AnonymousId,
                    MatchId: request.Id,
                    Status: "waiting",
                    RoomId: null,
                    Story: null,
                    YourRole: null,
                    YourRoleInfo: null,
                    PartnerRole: null,
                    PartnerProfile: null,
                    WaitTime: waitTime,
                    Message: "等待匹配中..."),
                Error: null);
        }

        if (request.Status == "matched")
        {
            var yourRoleInfo = request.SelectedRole == "A" ? request.Story?.MaleRole : request.Story?.FemaleRole;

            // 获取对方 profile - 需要查找同一房间的另一个用户
            MatchUserProfile? partnerProfile = null;
            // TODO: 从 ChatRoom 获取 partner profile

            return new MatchStatusResult(
                Success: true,
                Data: new MatchStatusData(
                    UserId: request.UserId,
                    AnonymousId: request.AnonymousId,
                    MatchId: request.Id,
                    Status: "matched",
                    RoomId: request.RoomId,
                    Story: request.Story,
                    YourRole: request.SelectedRole,
                    YourRoleInfo: yourRoleInfo,
                    PartnerRole: request.SelectedRole == "A" ? "B" : "A",
                    PartnerProfile: partnerProfile,
                    WaitTime: null,
                    Message: "匹配成功！已进入聊天室。"),
                Error: null);
        }

        if (request.Status == "cancelled")
        {
            return new MatchStatusResult(Success: false, Data: null, Error: new MatchError("MATCH_CANCELLED", "匹配已被取消"));
        }

        return new MatchStatusResult(Success: false, Data: null, Error: new MatchError("UNKNOWN_STATUS", "未知的匹配状态"));
    }

    public async Task<bool> CancelMatchAsync(string matchId, CancellationToken cancellationToken = default)
    {
        var request = await _matchStorage.GetMatchRequestByIdAsync(matchId, cancellationToken);

        if (request is null)
            return false;

        // 从 Redis 队列移除（不删除 MongoDB 记录）
        await _matchStorage.RemoveFromQueueAsync(request.UserId, cancellationToken);

        // 更新状态
        var cancelledRequest = request with
        {
            Status = "cancelled"
        };
        await _matchStorage.UpdateMatchRequestAsync(cancelledRequest, cancellationToken);

        _logger.LogInformation("Match {MatchId} cancelled", matchId);
        return true;
    }

    public async Task EnqueueMatchAsync(string userId, string gender, string genderPreference, int priority, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("User {UserId} (gender: {Gender}) entering match queue with preference {Preference}", userId, gender, genderPreference);

        var now = DateTimeOffset.UtcNow;
        var entry = new MatchQueueEntry(
            UserId: userId,
            AnonymousId: GenerateAnonymousId(),
            Gender: gender,
            GenderPreference: genderPreference,
            Profile: null,
            Story: null,
            SelectedRole: gender == "male" ? "A" : "B",
            EnqueuedAt: now,
            Priority: priority);

        // Save to Redis queue
        await _matchStorage.EnqueueAsync(entry, cancellationToken);

        // Save to MongoDB for persistence
        var request = new MatchRequest(
            Id: Guid.NewGuid().ToString(),
            UserId: userId,
            AnonymousId: entry.AnonymousId,
            Gender: gender,
            GenderPreference: genderPreference,
            Profile: null,
            Story: null,
            SelectedRole: entry.SelectedRole,
            Priority: priority,
            RequestedAt: now,
            Status: "waiting",
            RoomId: null,
            MatchedAt: null);
        await _matchStorage.SaveMatchRequestAsync(request, cancellationToken);
    }

    public async Task LeaveQueueAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("User {UserId} leaving match queue", userId);
        await _matchStorage.DequeueAsync(userId, cancellationToken);
    }

    public async Task<QueueStats> GetQueueStatsAsync(CancellationToken cancellationToken = default)
    {
        return await _matchStorage.GetQueueStatsAsync(cancellationToken);
    }

    public async Task<string?> TryMatchAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to match user {UserId}", userId);

        // 获取当前用户的请求
        var request = await _matchStorage.GetMatchRequestAsync(userId, cancellationToken);
        if (request is null)
        {
            _logger.LogWarning("User {UserId} not in queue", userId);
            return null;
        }

        // 简单匹配逻辑：找到第一个兼容的用户
        var allEntries = await _matchStorage.GetAllEntriesAsync(cancellationToken);
        var compatibleEntry = allEntries.FirstOrDefault(e => e.UserId != userId && IsCompatible(request.Gender, request.GenderPreference, e.Gender, e.GenderPreference));

        if (compatibleEntry is null)
        {
            _logger.LogInformation("No compatible match found for user {UserId}", userId);
            return null;
        }

        // 从 Redis 队列移除两个用户（不删除 MongoDB 记录）
        await _matchStorage.RemoveFromQueueAsync(userId, cancellationToken);
        await _matchStorage.RemoveFromQueueAsync(compatibleEntry.UserId, cancellationToken);

        _logger.LogInformation("Matched users {UserId1} and {UserId2}", userId, compatibleEntry.UserId);

        // 返回匹配的用户ID
        return compatibleEntry.UserId;
    }

    private static string GenerateAnonymousId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var suffix = new string(Enumerable.Range(0, 20).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        return $"anonymous_{suffix}";
    }

    private static string GetGenderPreference(string gender)
    {
        // 默认：男找女，女找男
        return gender == "male" ? "female" : "male";
    }

    private async Task<Story?> DetermineStoryAsync(Story? story1, Story? story2, IReadOnlyList<string> combinedTags, CancellationToken cancellationToken)
    {
        // 优先使用有效的故事
        if (story1 is not null && !string.IsNullOrEmpty(story1.Background))
            return story1;

        if (story2 is not null && !string.IsNullOrEmpty(story2.Background))
            return story2;

        // 双方都没有故事 → AI 生成（失败会自动 fallback）
        _logger.LogInformation("No user story found, generating via AI. Tags: {Tags}.", string.Join(',', combinedTags));
        var context = new StoryGenerationContext(
            AgeGroup: null,
            GenderPreference: null,
            Tags: combinedTags,
            Description: null,
            MaleRole: null,
            FemaleRole: null);
        return await _storyService.GenerateCompleteStoryAsync(context, cancellationToken);
    }

    /// <summary>
    /// 异步发送开场叙述 — 匹配成功后作为第一条 system 消息。
    /// </summary>
    private async Task SendStorySeedAsync(string roomId, Story? story, CancellationToken cancellationToken)
    {
        if (story is null)
            return;

        try
        {
            var seedContext = new StorySeedContext(
                story.Background,
                Array.Empty<string>(),
                story.MaleRole,
                story.FemaleRole);

            var seed = await _storyService.GenerateStorySeedAsync(seedContext, cancellationToken);
            await _chatService.SendSystemMessageAsync(roomId, seed, "seed", cancellationToken);
            _logger.LogInformation("Story seed sent to room {RoomId}.", roomId);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to send story seed for room {RoomId}.", roomId);
        }
    }

    private MatchQueueEntry? FindBestMatch(MatchQueueEntry newUser, IReadOnlyList<MatchQueueEntry> candidates)
    {
        MatchQueueEntry? bestMatch = null;
        var bestScore = -1;

        foreach (var candidate in candidates)
        {
            if (candidate.UserId == newUser.UserId)
                continue;

            // 检查年龄兼容性
            if (!CanMatchAgeGroups(newUser.Profile?.AgeGroup, candidate.Profile?.AgeGroup))
                continue;

            // 检查角色互补
            if (newUser.SelectedRole == candidate.SelectedRole)
                continue;

            // 计算匹配分数
            var score = CalculateMatchScore(newUser, candidate);

            // 等待时间奖励
            var waitTime = (DateTimeOffset.UtcNow - candidate.EnqueuedAt).TotalSeconds;
            score += (int)(waitTime / 10);

            if (score > bestScore)
            {
                bestScore = score;
                bestMatch = candidate;
            }
        }

        return bestMatch;
    }

    private static int CalculateMatchScore(MatchQueueEntry user1, MatchQueueEntry user2)
    {
        var score = 0;

        // 相同故事: +100
        if (user1.Story?.Title is not null && user1.Story.Title == user2.Story?.Title)
            score += 100;

        // 角色互补: +50
        if (user1.SelectedRole != user2.SelectedRole)
            score += 50;

        // 共同标签: 每个+10
        var tags1 = user1.Profile?.Tags ?? [];
        var tags2 = user2.Profile?.Tags ?? [];
        var commonTags = tags1.Intersect(tags2).Count();
        score += commonTags * 10;

        // 年龄段匹配: +20
        if (CanMatchAgeGroups(user1.Profile?.AgeGroup, user2.Profile?.AgeGroup))
            score += 20;

        return score;
    }

    private static bool CanMatchAgeGroups(string? ageGroup1, string? ageGroup2)
    {
        if (ageGroup1 == "<18")
            return ageGroup2 == "<18";

        return ageGroup2 is "18-23" or "23+";
    }

    private static bool IsCompatible(string gender1, string pref1, string gender2, string pref2)
    {
        // User1 的性别偏好检查：User1 想找的性别是否匹配 User2 的实际性别
        var user1Compatible = pref1 == "any" || pref1 == gender2;

        // User2 的性别偏好检查：User2 想找的性别是否匹配 User1 的实际性别
        var user2Compatible = pref2 == "any" || pref2 == gender1;

        // 双方都满足才能匹配
        return user1Compatible && user2Compatible;
    }

    /// <summary>
    /// 匹配虚拟人 - 等待超过 20 秒后自动分配。
    ///
    /// 虚拟人不占用真实用户队列，直接创建一个 IsVirtual=true 的房间。
    /// 前端通过 WebSocket 连接后，ChatWebSocketHandler 会根据 IsVirtual
    /// 路由到 VirtualChatSession（当前 Mock，后续接入 Python AI 后端）。
    /// </summary>
    private async Task<MatchStatusResult> MatchWithVirtualPersonAsync(MatchRequest request, CancellationToken cancellationToken)
    {
        // 生成虚拟人信息
        var oppositeGender = request.Gender == "male" ? "female" : "male";
        var virtualProfile = GenerateVirtualProfile(oppositeGender);

        // 创建虚拟人房间（IsVirtual = true）
        var room = await _chatService.CreateRoomAsync(request.UserId, virtualProfile.Id, story: null, isVirtual: true, cancellationToken);

        // 从 Redis 队列移除（不删除 MongoDB 记录）
        await _matchStorage.RemoveFromQueueAsync(request.UserId, cancellationToken);

        // 更新匹配状态
        var matchedRequest = request with
        {
            Status = "matched",
            RoomId = room.Id,
            MatchedAt = DateTimeOffset.UtcNow
        };
        await _matchStorage.UpdateMatchRequestAsync(matchedRequest, cancellationToken);

        _logger.LogInformation("User {UserId} matched with virtual person {VirtualId}", request.UserId, virtualProfile.Id);

        // 构造虚拟人的 MatchUserProfile 以返回给前端
        var virtualPartnerProfile = new MatchUserProfile(
            Gender: oppositeGender,
            AgeGroup: virtualProfile.AgeGroup,
            Height: null,
            Weight: null,
            Tags: ["虚拟人"],
            Description: virtualProfile.Personality);

        return new MatchStatusResult(
            Success: true,
            Data: new MatchStatusData(
                UserId: request.UserId,
                AnonymousId: request.AnonymousId,
                MatchId: request.Id,
                Status: "matched",
                RoomId: room.Id,
                Story: request.Story,
                YourRole: request.SelectedRole,
                YourRoleInfo: request.SelectedRole == "A" ? request.Story?.MaleRole : request.Story?.FemaleRole,
                PartnerRole: request.SelectedRole == "A" ? "B" : "A",
                PartnerProfile: virtualPartnerProfile,
                WaitTime: null,
                Message: "已为你匹配到聊天对象，开始聊天吧！"),
            Error: null);
    }

    /// <summary>生成虚拟人档案 - 后续可从数据库读取预设虚拟人列表</summary>
    private static VirtualPersonProfile GenerateVirtualProfile(string gender)
    {
        var id = $"virtual_{Guid.NewGuid().ToString()[..8]}";
        var names = gender == "female"
            ? new[] { "小雪", "小月", "小星", "小雨", "小梦" }
            : new[] { "小明", "小杰", "小辉", "小宇", "小风" };

        var name = names[Random.Shared.Next(names.Length)];

        return new VirtualPersonProfile(
            Id: id,
            Name: name,
            Gender: gender,
            AgeGroup: "18-23",
            Personality: "温柔体贴，善于倾听",
            AvatarUrl: "");
    }
}