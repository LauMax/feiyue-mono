using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class Argument
{
    public static Doc Print(ArgumentSyntax node, PrintingContext context)
    {
        var modifiers = PrintModifiers(node, context);

        return modifiers == Doc.Null ? Node.Print(node.Expression, context) : Doc.Concat(modifiers, Node.Print(node.Expression, context));
    }

    public static Doc PrintModifiers(ArgumentSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(2);
        if (node.NameColon is not null)
            docs.Add(BaseExpressionColon.Print(node.NameColon, context));

        if (node.RefKindKeyword.RawSyntaxKind() != SyntaxKind.None)
            docs.Add(Token.PrintWithSuffix(node.RefKindKeyword, " ", context));

        return Doc.Concat(ref docs);
    }
}