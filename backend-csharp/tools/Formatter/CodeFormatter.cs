using Microsoft.CodeAnalysis;
using Feiyue.Formatter.CSharp;

namespace Feiyue.Formatter;

internal static class CodeFormatter
{
    internal static async Task<CodeFormatterResult> FormatAsync(string fileContents, PrinterOptions options, CancellationToken cancellationToken) =>
        options.Formatter switch
        {
            Formatter.CSharp => await CSharpFormatter.FormatAsync(fileContents, options, SourceCodeKind.Regular, cancellationToken),
            Formatter.CSharpScript => await CSharpFormatter.FormatAsync(fileContents, options, SourceCodeKind.Script, cancellationToken),
            _ => new CodeFormatterResult { FailureMessage = "Is an unsupported file type." }
        };
}