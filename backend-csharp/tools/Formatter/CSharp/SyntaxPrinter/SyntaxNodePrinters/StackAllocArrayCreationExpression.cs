using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class StackAllocArrayCreationExpression
{
    public static Doc Print(StackAllocArrayCreationExpressionSyntax node, PrintingContext context) =>
        Doc.Concat(
            Token.PrintWithSuffix(node.StackAllocKeyword, " ", context),
            Node.Print(node.Type, context),
            node.Initializer is not null ? Doc.Concat(" ", InitializerExpression.Print(node.Initializer, context)) : string.Empty);
}