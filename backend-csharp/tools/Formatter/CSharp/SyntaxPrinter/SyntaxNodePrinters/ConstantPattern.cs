using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ConstantPattern
{
    public static Doc Print(ConstantPatternSyntax node, PrintingContext context) => Node.Print(node.Expression, context);
}