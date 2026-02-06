using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class QueryContinuation
{
    public static Doc Print(QueryContinuationSyntax node, PrintingContext context) =>
        Doc.Concat(Token.PrintWithSuffix(node.IntoKeyword, " ", context), Token.PrintWithSuffix(node.Identifier, Doc.Line, context), QueryBody.Print(node.Body, context));
}