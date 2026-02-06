using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class AnonymousObjectMemberDeclarator
{
    public static Doc Print(AnonymousObjectMemberDeclaratorSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(2);
        if (node.Parent is AnonymousObjectCreationExpressionSyntax parent && node != parent.Initializers.First())
            docs.Add(ExtraNewLines.Print(node));

        if (node.NameEquals is not null)
        {
            docs.Add(Token.PrintWithSuffix(node.NameEquals.Name.Identifier, " ", context));
            docs.Add(Token.PrintWithSuffix(node.NameEquals.EqualsToken, " ", context));
        }
        docs.Add(Node.Print(node.Expression, context));
        return Doc.Concat(ref docs);
    }
}