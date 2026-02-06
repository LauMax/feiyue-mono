using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ElementAccessExpression
{
    public static Doc Print(ElementAccessExpressionSyntax node, PrintingContext context) =>
        Doc.Concat(Node.Print(node.Expression, context), Node.Print(node.ArgumentList, context));
}