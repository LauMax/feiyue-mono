using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ImplicitElementAccess
{
    public static Doc Print(ImplicitElementAccessSyntax node, PrintingContext context) => Node.Print(node.ArgumentList, context);
}