namespace Feiyue.Formatter;

internal sealed class PrinterOptions()
{
    public bool IncludeAST { get; init; }
    public bool IncludeDocTree { get; init; }
    public bool UseTabs { get; set; }

    public int IndentSize
    {
        get;
        set
        {
            if (value <= 0)
                throw new ArgumentException("An indent size of 0 is not valid");

            field = value;
        }
    } = 4;

    public int Width { get; set; } = 100;
    public EndOfLine EndOfLine { get; set; } = EndOfLine.Auto;
    public bool TrimInitialLines { get; init; } = true;
    public bool IncludeGenerated { get; set; }
    public Formatter Formatter { get; set; } = Formatter.CSharp;

    public const int WidthUsedByTests = 100;

    internal static string GetLineEnding(string code, PrinterOptions printerOptions)
    {
        if (printerOptions.EndOfLine != EndOfLine.Auto)
            return printerOptions.EndOfLine == EndOfLine.CRLF ? "\r\n" : "\n";

        var lineIndex = code.IndexOf('\n', StringComparison.Ordinal);
        if (lineIndex <= 0)
            return "\n";

        if (code[lineIndex - 1] == '\r')
            return "\r\n";

        return "\n";
    }

    public static Formatter GetFormatter(string filePath)
    {
        var possibleExtension = Path.GetExtension(filePath);
        if (string.IsNullOrEmpty(possibleExtension))
            return Formatter.Unknown;

        var extension = possibleExtension[1..].ToLower(CultureInfo.InvariantCulture);

        var formatter = extension switch
        {
            "cs" => Formatter.CSharp,
            "csx" => Formatter.CSharpScript,
            _ => Formatter.Unknown
        };
        return formatter;
    }
}

internal enum Formatter
{
    Unknown,
    CSharp,
    CSharpScript
}