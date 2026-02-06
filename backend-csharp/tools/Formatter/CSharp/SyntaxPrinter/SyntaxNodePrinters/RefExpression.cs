using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class RefExpression
{
    public static Doc Print(RefExpressionSyntax node, PrintingContext context) =>
        Doc.Concat(Token.PrintWithSuffix(node.RefKeyword, " ", context), Node.Print(node.Expression, context));
}