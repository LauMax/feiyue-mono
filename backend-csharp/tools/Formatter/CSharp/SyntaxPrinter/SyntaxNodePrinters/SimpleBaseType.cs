using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class SimpleBaseType
{
    public static Doc Print(SimpleBaseTypeSyntax node, PrintingContext context) => Node.Print(node.Type, context);
}