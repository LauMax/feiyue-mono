using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class PrimaryConstructorBaseType
{
    public static Doc Print(PrimaryConstructorBaseTypeSyntax node, PrintingContext context) =>
        Doc.Concat(Node.Print(node.Type, context), ArgumentList.Print(node.ArgumentList, context));
}