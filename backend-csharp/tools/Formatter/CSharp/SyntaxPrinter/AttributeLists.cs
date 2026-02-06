using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

internal static class AttributeLists
{
    public static Doc Print(SyntaxNode node, SyntaxList<AttributeListSyntax> attributeLists, PrintingContext context)
    {
        if (attributeLists.Count == 0)
            return Doc.Null;

        DocListBuilder docs = new(2);
        Doc separator = node is TypeParameterSyntax or ParameterSyntax or ParenthesizedLambdaExpressionSyntax or AccessorDeclarationSyntax ? Doc.Line : Doc.HardLine;

        docs.Add(Doc.Join(separator, attributeLists.Select(o => AttributeList.Print(o, context))));

        if (node is not (ParameterSyntax or TypeParameterSyntax))
            docs.Add(separator);

        return Doc.Concat(ref docs);
    }
}