using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class EnumMemberDeclaration
{
    public static Doc Print(EnumMemberDeclarationSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(4);
        docs.Add(AttributeLists.Print(node, node.AttributeLists, context));
        docs.Add(Modifiers.Print(node.Modifiers, context));
        docs.Add(Token.Print(node.Identifier, context));

        if (node.EqualsValue is not null)
            docs.Add(EqualsValueClause.Print(node.EqualsValue, context));

        return Doc.Concat(ref docs);
    }
}