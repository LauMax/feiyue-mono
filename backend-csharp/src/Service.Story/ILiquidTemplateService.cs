namespace Service.StoryGeneration;

/// <summary>
/// Liquid 模板渲染服务 — 加载 .liquid 文件并用上下文渲染
/// </summary>
public interface ILiquidTemplateService
{
    /// <summary>
    /// 渲染指定模板
    /// </summary>
    /// <param name="templateName">模板名称（不含扩展名），如 "generate_complete"</param>
    /// <param name="context">模板上下文对象，属性会映射为模板变量</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>渲染后的字符串</returns>
    Task<string> RenderAsync(string templateName, object context, CancellationToken cancellationToken = default);
}
