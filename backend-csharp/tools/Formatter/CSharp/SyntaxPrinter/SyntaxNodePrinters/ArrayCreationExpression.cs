using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ArrayCreationExpression
{
    public static Doc Print(ArrayCreationExpressionSyntax node, PrintingContext context) =>
        Doc.Group(
            Token.PrintWithSuffix(node.NewKeyword, " ", context),
            Node.Print(node.Type, context),
            node.Initializer is not null ? Doc.Concat(Doc.Line, Node.Print(node.Initializer, context)) : Doc.Null);
}