using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ImplicitObjectCreationExpression
{
    public static Doc Print(ImplicitObjectCreationExpressionSyntax node, PrintingContext context) =>
        ObjectCreationExpression.BreakParentIfNested(
            node,
            Doc.Group(
                Token.Print(node.NewKeyword, context),
                ArgumentList.Print(node.ArgumentList, context),
                node.Initializer is not null ? Doc.Concat(Doc.Line, InitializerExpression.Print(node.Initializer, context)) : Doc.Null));
}