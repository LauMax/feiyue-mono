using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class PointerType
{
    public static Doc Print(PointerTypeSyntax node, PrintingContext context) => Doc.Concat(Node.Print(node.ElementType, context), Token.Print(node.AsteriskToken, context));
}