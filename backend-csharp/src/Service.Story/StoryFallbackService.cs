using Service.InternalContracts;

namespace Service.StoryGeneration;

/// <summary>
/// 降级故事模板服务 — 当 Grok API 不可用时提供确定性模板。
/// 从 Python 后端移植的 6 类故事模板 + 线索模板。
/// </summary>
internal static class StoryFallbackService
{
    // ========== 故事模板 ==========

    private static readonly IReadOnlyDictionary<string, FallbackCategory> StoryTemplates =
        new Dictionary<string, FallbackCategory>(StringComparer.OrdinalIgnoreCase)
        {
            ["dominant"] = new(
                ["Dom", "霸道", "刺激", "野性", "控制", "主导"],
                [
                    new Story(
                        "权力游戏",
                        "夜幕降临，城市的霓虹灯开始闪烁。在顶层会所的私密包厢里，灯光昏暗而暧昧。一场关于权力与臣服的游戏即将展开，空气中弥漫着紧张与期待...",
                        new Role("陆景琛", "商业帝国的掌控者，习惯了主导一切", "霸道、深沉、占有欲强"),
                        new Role("苏念", "表面柔弱，内心却有着不为人知的倔强", "外柔内刚、聪慧、不轻易屈服")),
                    new Story(
                        "猎物与猎手",
                        "月光洒落在别墅的落地窗前，整个房间笼罩在一片暧昧的氛围中。他的目光如同猎手锁定猎物，而她，是否愿意成为这场追逐游戏的参与者？",
                        new Role("顾承泽", "习惯掌控全局的男人，眼神中透着危险的魅力", "强势、神秘、令人捉摸不透"),
                        new Role("林晚", "看似温顺的外表下藏着一颗不安分的心", "聪明、有主见、敢于挑战")),
                ]),
            ["romantic"] = new(
                ["温柔", "浪漫", "小清新", "文艺", "甜蜜", "治愈"],
                [
                    new Story(
                        "咖啡与诗",
                        "午后的阳光透过咖啡馆的落地窗洒落，空气中弥漫着咖啡与书页的香气。在这个充满文艺气息的角落，两个热爱生活的灵魂即将相遇...",
                        new Role("沈言", "温润如玉的作家，用文字编织浪漫", "温柔、细腻、浪漫"),
                        new Role("苏晴", "喜欢读书和咖啡的文艺女孩", "温柔、善解人意、内心丰富")),
                    new Story(
                        "星空下的约定",
                        "夏夜的微风轻轻吹过，繁星点点的天空下，露台上的烛光摇曳。这是一个适合倾诉心事的夜晚，也是一个浪漫故事开始的时刻...",
                        new Role("江屿", "温暖阳光的大男孩，笑容治愈人心", "阳光、体贴、专情"),
                        new Role("唐诗", "喜欢仰望星空的浪漫女孩", "可爱、浪漫、容易害羞")),
                ]),
            ["mysterious"] = new(
                ["神秘", "悬疑", "角色扮演", "制服", "禁忌"],
                [
                    new Story(
                        "迷雾之约",
                        "古老庄园的走廊里，烛光在墙上投下摇曳的影子。她收到了一封神秘的邀请函，而在走廊的尽头，一个神秘的身影正在等待...",
                        new Role("霍绛", "庄园的神秘主人，身份成谜", "高深莫测、危险、充满魅力"),
                        new Role("叶知秋", "被神秘邀请而来的访客", "好奇、勇敢、直觉敏锐")),
                ]),
            ["playful"] = new(
                ["活泼", "可爱", "调皮", "开朗", "青春", "Switch", "软萌", "撒娇", "宠爱", "甜宠"],
                [
                    new Story(
                        "宠溺日常",
                        "阳光明媚的周末，舒适的公寓里充满了温馨的气息。他看着她软萌的样子，嘴角不自觉地上扬。这是属于他们的甜蜜时光...",
                        new Role("傅司珩", "表面高冷实则是个宠妻狂魔", "外冷内热、专一、宠溺"),
                        new Role("乔安", "软萌可爱，喜欢撒娇的小女生", "软萌、爱撒娇、小任性")),
                    new Story(
                        "校园青春",
                        "大学校园的梧桐叶正黄，图书馆里学习的氛围正浓。一个意外的碰撞让两本书掉在地上，也让两颗心开始靠近...",
                        new Role("夏阳", "阳光大男孩，学生会的活跃分子", "开朗、幽默、充满活力"),
                        new Role("小鹿", "活泼可爱的学妹，总是充满好奇心", "天真、好奇、喜欢冒险")),
                ]),
            ["gentle"] = new(
                ["温柔", "暖心", "Sub"],
                [
                    new Story(
                        "午后咖啡馆",
                        "阳光透过百叶窗洒在木质桌面上，咖啡馆里弥漫着淡淡的咖啡香。她坐在角落里安静地看书，他端着刚煮好的咖啡走来，这个午后注定不平凡...",
                        new Role("季暖", "温文尔雅的咖啡师，总是面带微笑", "温柔、细心、善解人意"),
                        new Role("苏小暖", "爱读书的文艺女孩，眼中有星光", "安静、敏感、内心丰富")),
                    new Story(
                        "雨夜书店",
                        "雨水打在书店的玻璃窗上，店内温暖的灯光驱散了夜晚的寒意。两个陌生人在同一本书前停下脚步，这场意外的相遇即将改写两个人的故事...",
                        new Role("林深", "安静的书店老板，熟悉每一本书的故事", "内敛、博学、温和"),
                        new Role("米小雨", "在雨夜寻找温暖的女孩", "感性、纯真、渴望被理解")),
                ]),
        };

    private static readonly Story DefaultStory = new(
        "命运的邂逅",
        "在繁华都市的一角，命运悄然安排了一场相遇。霓虹灯下，两个陌生人的目光交汇，仿佛整个世界都在这一刻静止...",
        new Role("他", "神秘而有魅力的男人", "成熟、稳重、有担当"),
        new Role("她", "温柔而独立的女人", "温柔、独立、有魅力"));

    // ========== 线索模板 ==========

    private static readonly IReadOnlyList<string> ConversationClues =
    [
        "故事在悄悄转向，命运的齿轮开始转动...",
        "你们的对话越来越深入，仿佛发现了彼此未曾展露的一面...",
        "气氛在发生微妙的变化，原本陌生的两个人似乎越来越默契...",
        "故事朝着意想不到的方向发展，你们都感受到了某种特别的连接...",
        "节奏在加快，在这个故事中，你们变成了彼此最重要的角色...",
        "或许这一刻，命运在编织某些有趣的线索...",
        "故事的节奏在加快，你们之间的互动变得越来越有趣...",
        "一股微妙的吸引力在两个灵魂之间涌动...",
        "这个故事好像在朝着某个特别的方向发展...",
        "你能感受到，这次邂逅不是巧合...",
    ];

    private static readonly IReadOnlyList<string> SilenceClues =
    [
        "突然，眼神的碰撞打破了沉默，空气中弥漫着一种微妙的气氛...",
        "窗外传来的某个声音打断了静寂，你们的注意力重新汇聚...",
        "一丝不易觉察的微笑浮现在彼此的嘴角，沉默中好像有什么在悄悄发生变化...",
        "屋内的温度仿佛升高了，寂静让彼此的心跳声变得更加清晰...",
        "对方的一个小动作，让你突然察觉到原来这份沉默从来都不是冷漠...",
    ];

    private static readonly IReadOnlyList<string> SeedTemplates =
    [
        "突然，{0}...",
        "就在这一刻，{0}...",
        "不知道你是否注意到，{0}...",
        "命运安排了这一时刻，{0}...",
        "气氛变得微妙，{0}...",
        "时间仿佛停顿了，{0}...",
    ];

    // ========== 公开方法 ==========

    /// <summary>
    /// 根据标签匹配 fallback 故事模板
    /// </summary>
    public static Story GetFallbackStory(IReadOnlyList<string> tags)
    {
        if (tags.Count == 0)
            return DefaultStory;

        foreach (var (_, category) in StoryTemplates)
        {
            var hasMatch = tags.Any(tag =>
                category.Tags.Any(ct => ct.Equals(tag, StringComparison.OrdinalIgnoreCase)));

            if (hasMatch)
            {
                return category.Stories[Random.Shared.Next(category.Stories.Count)];
            }
        }

        return DefaultStory;
    }

    /// <summary>
    /// 获取 fallback 开场叙述
    /// </summary>
    public static string GetFallbackSeed(string background)
    {
        var template = SeedTemplates[Random.Shared.Next(SeedTemplates.Count)];
        return string.Format(template, background);
    }

    /// <summary>
    /// 获取 fallback 剧情线索
    /// </summary>
    public static string GetFallbackClue(string triggerType)
    {
        var clues = triggerType.Equals("silence", StringComparison.OrdinalIgnoreCase)
            ? SilenceClues
            : ConversationClues;

        return clues[Random.Shared.Next(clues.Count)];
    }

    /// <summary>
    /// 获取 fallback 对话建议
    /// </summary>
    public static string GetFallbackDialogue()
    {
        return "故事在继续，等待你们的下一步...";
    }

    // ========== 内部类型 ==========

    private sealed record FallbackCategory(IReadOnlyList<string> Tags, IReadOnlyList<Story> Stories);
}
