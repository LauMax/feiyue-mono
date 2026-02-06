using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class BasePropertyDeclaration
{
    public static Doc Print(BasePropertyDeclarationSyntax node, PrintingContext context)
    {
        EqualsValueClauseSyntax? initializer = null;
        ExplicitInterfaceSpecifierSyntax? explicitInterfaceSpecifierSyntax = null;
        Func<Doc>? identifier = null;
        Doc eventKeyword = Doc.Null;
        ArrowExpressionClauseSyntax? expressionBody = null;
        SyntaxToken? semicolonToken = null;

        if (node is PropertyDeclarationSyntax propertyDeclarationSyntax)
        {
            expressionBody = propertyDeclarationSyntax.ExpressionBody;
            initializer = propertyDeclarationSyntax.Initializer;
            explicitInterfaceSpecifierSyntax = propertyDeclarationSyntax.ExplicitInterfaceSpecifier;
            identifier = () => Token.Print(propertyDeclarationSyntax.Identifier, context);
            semicolonToken = propertyDeclarationSyntax.SemicolonToken;
        }
        else if (node is IndexerDeclarationSyntax indexerDeclarationSyntax)
        {
            expressionBody = indexerDeclarationSyntax.ExpressionBody;
            explicitInterfaceSpecifierSyntax = indexerDeclarationSyntax.ExplicitInterfaceSpecifier;
            identifier = () => Doc.Concat(Token.Print(indexerDeclarationSyntax.ThisKeyword, context), Node.Print(indexerDeclarationSyntax.ParameterList, context));
            semicolonToken = indexerDeclarationSyntax.SemicolonToken;
        }
        else if (node is EventDeclarationSyntax eventDeclarationSyntax)
        {
            eventKeyword = Token.PrintWithSuffix(eventDeclarationSyntax.EventKeyword, " ", context);
            explicitInterfaceSpecifierSyntax = eventDeclarationSyntax.ExplicitInterfaceSpecifier;
            identifier = () => Token.Print(eventDeclarationSyntax.Identifier, context);
            semicolonToken = eventDeclarationSyntax.SemicolonToken;
        }

        return Doc.Group(
            Doc.Concat(
                AttributeLists.Print(node, node.AttributeLists, context),
                Modifiers.PrintSorted(node.Modifiers, context),
                eventKeyword,
                Node.Print(node.Type, context),
                " ",
                explicitInterfaceSpecifierSyntax is not null
                    ? Doc.Concat(Node.Print(explicitInterfaceSpecifierSyntax.Name, context), Token.Print(explicitInterfaceSpecifierSyntax.DotToken, context))
                    : Doc.Null,
                identifier is not null ? identifier() : Doc.Null,
                Contents(node, expressionBody, context),
                initializer is not null ? EqualsValueClause.Print(initializer, context) : Doc.Null,
                semicolonToken.HasValue ? Token.Print(semicolonToken.Value, context) : Doc.Null));
    }

    private static Doc Contents(BasePropertyDeclarationSyntax node, ArrowExpressionClauseSyntax? expressionBody, PrintingContext context)
    {
        Doc contents = string.Empty;
        if (node.AccessorList is not null)
        {
            Doc separator = " ";
            if (node.AccessorList.Accessors.Any(o => o.Body is not null || o.ExpressionBody is not null || o.Modifiers.Any() || o.AttributeLists.Any()))
                separator = Doc.Line;

            contents = Doc.Group(
                Doc.Concat(
                    separator,
                    Token.Print(node.AccessorList.OpenBraceToken, context),
                    Doc.Indent(node.AccessorList.Accessors.Select(o => PrintAccessorDeclarationSyntax(o, separator, context)).ToArray()),
                    separator,
                    Token.Print(node.AccessorList.CloseBraceToken, context)));
        }
        else if (expressionBody is not null)
        {
            contents = ArrowExpressionClause.Print(expressionBody, context);
        }

        return contents;
    }

    private static Doc PrintAccessorDeclarationSyntax(AccessorDeclarationSyntax node, Doc separator, PrintingContext context)
    {
        DocListBuilder docs = new(6);
        if (
            node.AttributeLists.Count > 1
            || node.Body is not null
            || node.ExpressionBody is not null
            || (node.AttributeLists.FirstOrDefault() is { Attributes: [{ ArgumentList.Arguments.Count: > 0 }] }))
        {
            docs.Add(Doc.HardLine);
        }
        else
        {
            docs.Add(separator);
        }

        docs.Add(AttributeLists.Print(node, node.AttributeLists, context));
        docs.Add(Modifiers.PrintSorted(node.Modifiers, context));
        docs.Add(Token.Print(node.Keyword, context));

        if (node.Body is not null)
            docs.Add(Block.Print(node.Body, context));
        else if (node.ExpressionBody is not null)
            docs.Add(ArrowExpressionClause.Print(node.ExpressionBody, context));

        docs.Add(Token.Print(node.SemicolonToken, context));

        return Doc.Concat(ref docs);
    }
}