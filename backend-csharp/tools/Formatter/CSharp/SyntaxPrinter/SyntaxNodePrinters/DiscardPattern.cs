using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class DiscardPattern
{
    public static Doc Print(DiscardPatternSyntax node, PrintingContext context) => Token.Print(node.UnderscoreToken, context);
}