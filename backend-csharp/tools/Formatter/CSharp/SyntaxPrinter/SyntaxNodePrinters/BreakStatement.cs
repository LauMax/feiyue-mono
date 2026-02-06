using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class BreakStatement
{
    public static Doc Print(BreakStatementSyntax node, PrintingContext context) =>
        Doc.Concat(ExtraNewLines.Print(node), Token.Print(node.BreakKeyword, context), Token.Print(node.SemicolonToken, context));
}