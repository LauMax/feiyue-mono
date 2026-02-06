namespace Feiyue.Formatter.DocPrinter;

internal sealed class Indent
{
    public string Value { get; set; } = string.Empty;
    public int Length { get; set; }
}

internal sealed class Indenter(PrinterOptions printerOptions)
{
    private readonly PrinterOptions printerOptions = printerOptions;
    private readonly Dictionary<string, Indent> increaseIndentCache = [];

    public static Indent GenerateRoot() => new();

    public Indent IncreaseIndent(Indent indent)
    {
        if (increaseIndentCache.TryGetValue(indent.Value, out var increasedIndent))
            return increasedIndent;

        var nextIndent = printerOptions.UseTabs
            ? new Indent { Value = indent.Value + "\t", Length = indent.Length + printerOptions.IndentSize }
            : new Indent { Value = indent.Value.PadRight(indent.Value.Length + printerOptions.IndentSize), Length = indent.Length + printerOptions.IndentSize };

        increaseIndentCache[indent.Value] = nextIndent;
        return nextIndent;
    }
}