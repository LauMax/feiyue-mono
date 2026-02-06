using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class SwitchSection
{
    public static Doc Print(SwitchSectionSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(2);
        docs.Add(Doc.Join(Doc.HardLine, node.Labels.Select(o => Node.Print(o, context))));
        if (node.Statements is [BlockSyntax blockSyntax])
        {
            docs.Add(Block.Print(blockSyntax, context));
        }
        else
        {
            docs.Add(
                Doc.Indent(
                    node.Statements.First() is BlockSyntax ? Doc.Null : Doc.HardLine,
                    Doc.Join(Doc.HardLine, node.Statements.Select(o => Node.Print(o, context)).ToArray())));
        }
        return Doc.Concat(ref docs);
    }
}