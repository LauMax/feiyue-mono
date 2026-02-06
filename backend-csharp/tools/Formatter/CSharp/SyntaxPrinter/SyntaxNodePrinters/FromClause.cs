using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class FromClause
{
    public static Doc Print(FromClauseSyntax node, PrintingContext context) =>
        Doc.Concat(
            Token.PrintWithSuffix(node.FromKeyword, " ", context),
            node.Type is not null ? Doc.Concat(Node.Print(node.Type, context), " ") : Doc.Null,
            Token.PrintWithSuffix(node.Identifier, " ", context),
            Token.PrintWithSuffix(node.InKeyword, " ", context),
            Node.Print(node.Expression, context));
}