using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class MemberAccessExpression
{
    public static Doc Print(MemberAccessExpressionSyntax node, PrintingContext context) => InvocationExpression.PrintMemberChain(node, context);
}