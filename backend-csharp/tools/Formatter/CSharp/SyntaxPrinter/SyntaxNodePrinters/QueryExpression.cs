using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class QueryExpression
{
    public static Doc Print(QueryExpressionSyntax node, PrintingContext context) =>
        Doc.Concat(FromClause.Print(node.FromClause, context), Doc.Line, QueryBody.Print(node.Body, context));
}