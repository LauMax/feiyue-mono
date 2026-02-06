using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class PrefixUnaryExpression
{
    public static Doc Print(PrefixUnaryExpressionSyntax node, PrintingContext context) => Doc.Concat(Token.Print(node.OperatorToken, context), Node.Print(node.Operand, context));
}