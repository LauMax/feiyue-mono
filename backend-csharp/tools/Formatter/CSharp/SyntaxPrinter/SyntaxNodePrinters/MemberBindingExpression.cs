using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class MemberBindingExpression
{
    public static Doc Print(MemberBindingExpressionSyntax node, PrintingContext context) => Doc.Concat(Token.Print(node.OperatorToken, context), Node.Print(node.Name, context));
}