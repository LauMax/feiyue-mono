using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class LabeledStatement
{
    public static Doc Print(LabeledStatementSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(5);
        docs.Add(ExtraNewLines.Print(node));
        docs.Add(AttributeLists.Print(node, node.AttributeLists, context));
        docs.Add(Token.Print(node.Identifier, context));
        docs.Add(Token.Print(node.ColonToken, context));

        if (node.Statement is BlockSyntax blockSyntax)
            docs.Add(Block.Print(blockSyntax, context));
        else
            docs.Add(Doc.HardLine, Node.Print(node.Statement, context));

        return Doc.Concat(ref docs);
    }
}