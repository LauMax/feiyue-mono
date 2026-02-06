using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ExpressionElement
{
    public static Doc Print(ExpressionElementSyntax node, PrintingContext context) => Node.Print(node.Expression, context);
}