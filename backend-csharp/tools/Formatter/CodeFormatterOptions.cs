namespace Feiyue.Formatter;

internal sealed class CodeFormatterOptions
{
    public int Width { get; init; } = 180;
    public IndentStyle IndentStyle { get; init; } = IndentStyle.Spaces;
    public int IndentSize { get; init; } = 4;
    public EndOfLine EndOfLine { get; init; } = EndOfLine.Auto;
    public bool IncludeGenerated { get; init; }

    internal PrinterOptions ToPrinterOptions() =>
        new()
        {
            Width = Width,
            UseTabs = IndentStyle == IndentStyle.Tabs,
            IndentSize = IndentSize,
            EndOfLine = EndOfLine,
            IncludeGenerated = IncludeGenerated
        };
}

internal enum IndentStyle
{
    Spaces,
    Tabs
}