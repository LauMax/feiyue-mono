using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class AnonymousMethodExpression
{
    public static Doc Print(AnonymousMethodExpressionSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(4);
        docs.Add(Modifiers.Print(node.Modifiers, context));
        docs.Add(Token.Print(node.DelegateKeyword, context));

        if (node.ParameterList is not null)
            docs.Add(ParameterList.Print(node.ParameterList, context));

        docs.Add(Block.Print(node.Block, context));

        return Doc.Concat(ref docs);
    }
}