using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class Parameter
{
    public static Doc Print(ParameterSyntax node, PrintingContext context)
    {
        var hasAttribute = node.AttributeLists.Any();

        DocListBuilder docs = new(8);

        if (hasAttribute)
        {
            docs.Add(AttributeLists.Print(node, node.AttributeLists, context));
            if (node.AttributeLists.Count < 2 && (node.GetLeadingTrivia().Any(o => o.IsComment()) || node.Parent is ParameterListSyntax { Parameters.Count: 0 }))
                docs.Add(" ");
            else
                docs.Add(Doc.Indent(Doc.Line));
        }

        if (node.Modifiers.Any())
            docs.Add(Modifiers.Print(node.Modifiers, context));

        if (node.Type is not null)
        {
            docs.Add(Node.Print(node.Type, context));

            if (node.Identifier.RawSyntaxKind() is not SyntaxKind.None)
                docs.Add(" ");
        }

        docs.Add(Token.Print(node.Identifier, context));
        if (node.Default is not null)
            docs.Add(EqualsValueClause.Print(node.Default, context));

        return hasAttribute ? Doc.Group(docs.ToArray()) : Doc.Concat(ref docs);
    }
}