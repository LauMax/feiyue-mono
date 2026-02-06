using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class BaseFieldDeclaration
{
    public static Doc Print(BaseFieldDeclarationSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(5);
        docs.Add(AttributeLists.Print(node, node.AttributeLists, context));
        docs.Add(Modifiers.PrintSorted(node.Modifiers, context));
        if (node is EventFieldDeclarationSyntax eventFieldDeclarationSyntax)
            docs.Add(Token.PrintWithSuffix(eventFieldDeclarationSyntax.EventKeyword, " ", context));

        docs.Add(VariableDeclaration.Print(node.Declaration, context), Token.Print(node.SemicolonToken, context));
        return Doc.Concat(ref docs);
    }
}