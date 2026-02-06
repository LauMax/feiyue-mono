using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class AttributeList
{
    public static Doc Print(AttributeListSyntax node, PrintingContext context)
    {
        if (node.Parent is BaseMethodDeclarationSyntax && CSharpierIgnore.HasIgnoreComment(node))
            return CSharpierIgnore.PrintWithoutFormatting(node, context).Trim();

        DocListBuilder docs = new(8);
        if (node.Parent is CompilationUnitSyntax compilationUnitSyntax && compilationUnitSyntax.AttributeLists.First() != node)
            docs.Add(ExtraNewLines.Print(node));

        docs.Add(Token.Print(node.OpenBracketToken, context));
        if (node.Target is not null)
            docs.Add(Token.Print(node.Target.Identifier, context), Token.PrintWithSuffix(node.Target.ColonToken, " ", context));

        var printSeparatedSyntaxList = SeparatedSyntaxList.Print(
            node.Attributes,
            (attributeNode, _) =>
            {
                var name = Node.Print(attributeNode.Name, context);
                if (attributeNode.ArgumentList is null)
                    return name;

                var singleCollectionExpression = attributeNode.ArgumentList.Arguments is [{ Expression: CollectionExpressionSyntax, NameColon: null, NameEquals: null }];

                return Doc.Group(
                    name,
                    Token.Print(attributeNode.ArgumentList.OpenParenToken, context),
                    Doc.IndentIf(
                        !singleCollectionExpression,
                        Doc.Concat(
                            singleCollectionExpression ? Doc.Null : Doc.SoftLine,
                            SeparatedSyntaxList.Print(
                                attributeNode.ArgumentList.Arguments,
                                (attributeArgumentNode, _) =>
                                    Doc.Concat(
                                        attributeArgumentNode.NameEquals is not null ? NameEquals.Print(attributeArgumentNode.NameEquals, context) : Doc.Null,
                                        attributeArgumentNode.NameColon is not null ? BaseExpressionColon.Print(attributeArgumentNode.NameColon, context) : Doc.Null,
                                        Node.Print(attributeArgumentNode.Expression, context)),
                                Doc.Line,
                                context))),
                    Token.Print(attributeNode.ArgumentList.CloseParenToken, context));
            },
            Doc.Line,
            context);

        docs.Add(node.Attributes.Count > 1 ? Doc.Indent(Doc.SoftLine, printSeparatedSyntaxList) : printSeparatedSyntaxList);

        if (node.Attributes.Count > 1)
            docs.Add(Doc.SoftLine);

        docs.Add(Token.Print(node.CloseBracketToken, context));

        var result = Doc.Group(docs.ToArray());
        docs.Dispose();

        return result;
    }
}