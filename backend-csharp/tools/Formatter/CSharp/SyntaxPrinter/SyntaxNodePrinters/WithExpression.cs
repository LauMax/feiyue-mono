using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class WithExpression
{
    public static Doc Print(WithExpressionSyntax node, PrintingContext context) =>
        Doc.Concat(Node.Print(node.Expression, context), " ", Token.PrintWithSuffix(node.WithKeyword, Doc.Line, context), Node.Print(node.Initializer, context));
}