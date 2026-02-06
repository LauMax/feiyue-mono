using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class NameEquals
{
    public static Doc Print(NameEqualsSyntax node, PrintingContext context) =>
        Doc.Concat(Node.Print(node.Name, context), " ", Token.PrintWithSuffix(node.EqualsToken, " ", context));
}