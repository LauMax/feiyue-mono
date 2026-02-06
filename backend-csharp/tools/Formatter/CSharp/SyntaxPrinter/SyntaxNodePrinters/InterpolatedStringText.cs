using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class InterpolatedStringText
{
    public static Doc Print(InterpolatedStringTextSyntax node, PrintingContext context) => Token.Print(node.TextToken, context);
}