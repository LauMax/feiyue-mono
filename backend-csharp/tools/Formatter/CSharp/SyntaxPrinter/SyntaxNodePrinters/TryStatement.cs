using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class TryStatement
{
    public static Doc Print(TryStatementSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(8);
        docs.Add(ExtraNewLines.Print(node));
        docs.Add(AttributeLists.Print(node, node.AttributeLists, context));
        docs.Add(Token.Print(node.TryKeyword, context));
        docs.Add(Block.Print(node.Block, context));
        docs.Add(node.Catches.Any() ? Doc.HardLine : Doc.Null);
        docs.Add(Doc.Join(Doc.HardLine, node.Catches.Select(o => CatchClause.Print(o, context))));

        if (node.Finally is not null)
            docs.Add(Doc.HardLine, FinallyClause.Print(node.Finally, context));

        return Doc.Concat(ref docs);
    }
}