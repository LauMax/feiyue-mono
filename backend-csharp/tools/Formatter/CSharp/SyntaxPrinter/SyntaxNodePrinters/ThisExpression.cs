using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ThisExpression
{
    public static Doc Print(ThisExpressionSyntax node, PrintingContext context) => Token.Print(node.Token, context);
}