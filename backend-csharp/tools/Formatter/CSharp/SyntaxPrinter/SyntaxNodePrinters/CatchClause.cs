using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class CatchClause
{
    public static Doc Print(CatchClauseSyntax node, PrintingContext context) =>
        Doc.Concat(
            Token.Print(node.CatchKeyword, context),
            Doc.Group(
                node.Declaration is not null
                    ? Doc.Concat(
                        " ",
                        Token.Print(node.Declaration.OpenParenToken, context),
                        Node.Print(node.Declaration.Type, context),
                        node.Declaration.Identifier.RawSyntaxKind() != SyntaxKind.None ? " " : Doc.Null,
                        Token.Print(node.Declaration.Identifier, context),
                        Token.Print(node.Declaration.CloseParenToken, context))
                    : Doc.Null,
                node.Filter is not null
                    ? Doc.Indent(
                        Doc.Line,
                        Token.PrintWithSuffix(node.Filter.WhenKeyword, " ", context),
                        Token.Print(node.Filter.OpenParenToken, context),
                        Doc.Indent(Node.Print(node.Filter.FilterExpression, context)),
                        Token.Print(node.Filter.CloseParenToken, context))
                    : Doc.Null),
            Block.Print(node.Block, context));
}