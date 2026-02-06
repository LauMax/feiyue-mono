using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class WhereClause
{
    public static Doc Print(WhereClauseSyntax node, PrintingContext context) =>
        Doc.Concat(ExtraNewLines.Print(node), Doc.Group(Token.Print(node.WhereKeyword, context), Doc.Indent(Doc.Line, Node.Print(node.Condition, context))));
}