using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class Interpolation
{
    public static Doc Print(InterpolationSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(8);
        docs.Add(Token.Print(node.OpenBraceToken, context));
        docs.Add(Node.Print(node.Expression, context));

        if (node.AlignmentClause is not null)
            docs.Add(Token.PrintWithSuffix(node.AlignmentClause.CommaToken, " ", context), Node.Print(node.AlignmentClause.Value, context));

        if (node.FormatClause is not null)
            docs.Add(Token.Print(node.FormatClause.ColonToken, context), Token.Print(node.FormatClause.FormatStringToken, context));

        docs.Add(Token.Print(node.CloseBraceToken, context));
        return Doc.Concat(ref docs);
    }
}