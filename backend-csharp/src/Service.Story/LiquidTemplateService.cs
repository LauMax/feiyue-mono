using System.Collections.Concurrent;
using Fluid;
using Fluid.Values;
using Microsoft.Extensions.Logging;

namespace Service.StoryGeneration;

/// <summary>
/// Fluid 引擎实现的 Liquid 模板渲染服务。
/// 从 LiquidTemplates/story/ 目录加载 .liquid 文件，解析后缓存。
/// </summary>
internal sealed class LiquidTemplateService : ILiquidTemplateService
{
    private readonly ILogger<LiquidTemplateService> _logger;
    private readonly FluidParser _parser = new();
    private readonly ConcurrentDictionary<string, IFluidTemplate> _cache = new();
    private readonly string _templateDir;

    public LiquidTemplateService(ILogger<LiquidTemplateService> logger)
    {
        _logger = logger;

        // 模板目录：输出目录下的 LiquidTemplates/story/
        _templateDir = Path.Combine(AppContext.BaseDirectory, "LiquidTemplates", "story");
    }

    public Task<string> RenderAsync(string templateName, object context, CancellationToken cancellationToken = default)
    {
        var template = GetOrParseTemplate(templateName);

        var templateContext = new TemplateContext(context, TemplateOptions);
        var result = template.Render(templateContext);

        return Task.FromResult(result.Trim());
    }

    private IFluidTemplate GetOrParseTemplate(string templateName)
    {
        return _cache.GetOrAdd(templateName, name =>
        {
            var path = Path.Combine(_templateDir, $"{name}.liquid");
            if (!File.Exists(path))
            {
                _logger.LogError("Liquid template not found: {Path}.", path);
                throw new FileNotFoundException($"Liquid template not found: {path}");
            }

            var source = File.ReadAllText(path);
            if (!_parser.TryParse(source, out var template, out var error))
            {
                _logger.LogError("Failed to parse Liquid template {Name}: {Error}.", name, error);
                throw new InvalidOperationException($"Failed to parse template '{name}': {error}");
            }

            _logger.LogInformation("Loaded Liquid template: {Name}.", name);
            return template;
        });
    }

    private static readonly TemplateOptions TemplateOptions = new()
    {
        MemberAccessStrategy = new UnsafeMemberAccessStrategy(),
    };
}
