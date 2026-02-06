using Microsoft.CodeAnalysis;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

internal static class UnhandledNode
{
    public static Doc Print(SyntaxNode node, PrintingContext _) =>
        // full string includes comments/directives but also any whitespace, which we need to strip
        node.ToFullString().Trim();
}