using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ThrowExpression
{
    public static Doc Print(ThrowExpressionSyntax node, PrintingContext context) =>
        Doc.Concat(Token.PrintWithSuffix(node.ThrowKeyword, " ", context), Node.Print(node.Expression, context));
}