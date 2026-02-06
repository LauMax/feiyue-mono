using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class RefType
{
    public static Doc Print(RefTypeSyntax node, PrintingContext context) =>
        Doc.Concat(Token.PrintWithSuffix(node.RefKeyword, " ", context), Token.PrintWithSuffix(node.ReadOnlyKeyword, " ", context), Node.Print(node.Type, context));
}