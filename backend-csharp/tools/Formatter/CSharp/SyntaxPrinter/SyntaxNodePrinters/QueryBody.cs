using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class QueryBody
{
    public static Doc Print(QueryBodySyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(5);
        docs.Add(Doc.Join(Doc.Line, node.Clauses.Select(o => Node.Print(o, context))));

        if (node.Clauses.Count > 0)
            docs.Add(Doc.Line);

        docs.Add(Node.Print(node.SelectOrGroup, context));
        if (node.Continuation is not null)
            docs.Add(" ", QueryContinuation.Print(node.Continuation, context));

        return Doc.Concat(ref docs);
    }
}