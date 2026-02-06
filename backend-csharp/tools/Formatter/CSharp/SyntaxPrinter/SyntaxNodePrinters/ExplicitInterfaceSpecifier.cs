using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ExplicitInterfaceSpecifier
{
    public static Doc Print(ExplicitInterfaceSpecifierSyntax node, PrintingContext context) => Doc.Concat(Node.Print(node.Name, context), Token.Print(node.DotToken, context));
}