using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ArrowExpressionClause
{
    public static Doc Print(ArrowExpressionClauseSyntax node, PrintingContext context) =>
        Doc.Group(Doc.Indent(" ", Token.PrintWithSuffix(node.ArrowToken, Doc.Line, context), Node.Print(node.Expression, context)));
}