using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class RangeExpression
{
    public static Doc Print(RangeExpressionSyntax node, PrintingContext context) =>
        Doc.Concat(
            node.LeftOperand is not null ? Node.Print(node.LeftOperand, context) : Doc.Null,
            Token.Print(node.OperatorToken, context),
            node.RightOperand is not null ? Node.Print(node.RightOperand, context) : Doc.Null);
}