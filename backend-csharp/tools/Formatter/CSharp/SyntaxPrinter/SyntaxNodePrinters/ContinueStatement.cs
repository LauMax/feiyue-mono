using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ContinueStatement
{
    public static Doc Print(ContinueStatementSyntax node, PrintingContext context) =>
        Doc.Concat(ExtraNewLines.Print(node), Token.Print(node.ContinueKeyword, context), Token.Print(node.SemicolonToken, context));
}