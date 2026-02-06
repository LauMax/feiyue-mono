using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class GroupClause
{
    public static Doc Print(GroupClauseSyntax node, PrintingContext context) =>
        Doc.Concat(
            ExtraNewLines.Print(node),
            Token.PrintWithSuffix(node.GroupKeyword, " ", context),
            Node.Print(node.GroupExpression, context),
            " ",
            Token.PrintWithSuffix(node.ByKeyword, " ", context),
            Node.Print(node.ByExpression, context));
}