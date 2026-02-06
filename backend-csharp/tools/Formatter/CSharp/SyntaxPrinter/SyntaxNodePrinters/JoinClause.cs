using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class JoinClause
{
    public static Doc Print(JoinClauseSyntax node, PrintingContext context) =>
        Doc.Concat(
            ExtraNewLines.Print(node),
            Doc.Group(
                Token.PrintWithSuffix(node.JoinKeyword, " ", context),
                node.Type is not null ? Doc.Concat(Node.Print(node.Type, context), " ") : Doc.Null,
                Token.PrintWithSuffix(node.Identifier, " ", context),
                Token.PrintWithSuffix(node.InKeyword, " ", context),
                Node.Print(node.InExpression, context),
                Doc.Indent(
                    Doc.Line,
                    Token.PrintWithSuffix(node.OnKeyword, " ", context),
                    Node.Print(node.LeftExpression, context),
                    " ",
                    Token.PrintWithSuffix(node.EqualsKeyword, " ", context),
                    Node.Print(node.RightExpression, context),
                    node.Into is not null
                        ? Doc.Concat(Doc.Line, Token.PrintWithSuffix(node.Into.IntoKeyword, " ", context), Token.Print(node.Into.Identifier, context))
                        : Doc.Null)));
}