namespace Feiyue.Formatter;

internal interface IFormatter
{
    Task<CodeFormatterResult> FormatAsync(string code, PrinterOptions printerOptions, CancellationToken cancellationToken);
}