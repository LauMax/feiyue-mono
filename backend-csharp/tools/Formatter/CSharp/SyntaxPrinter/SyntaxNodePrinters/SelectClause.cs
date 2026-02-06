using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class SelectClause
{
    public static Doc Print(SelectClauseSyntax node, PrintingContext context) =>
        Doc.Concat(ExtraNewLines.Print(node), Token.PrintWithSuffix(node.SelectKeyword, " ", context), Node.Print(node.Expression, context));
}