using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ImplicitStackAllocArrayCreationExpression
{
    public static Doc Print(ImplicitStackAllocArrayCreationExpressionSyntax node, PrintingContext context) =>
        Doc.Concat(
            Token.Print(node.StackAllocKeyword, context),
            Token.Print(node.OpenBracketToken, context),
            Token.PrintWithSuffix(node.CloseBracketToken, " ", context),
            Node.Print(node.Initializer, context));
}