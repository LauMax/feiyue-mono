using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ForStatement
{
    public static Doc Print(ForStatementSyntax node, PrintingContext context) =>
        Doc.Concat(
            ExtraNewLines.Print(node),
            Doc.Group(
                Token.Print(node.ForKeyword, context),
                " ",
                Token.Print(node.OpenParenToken, context),
                Doc.Indent(
                    Doc.SoftLine,
                    Doc.Group(
                        node.Declaration is not null ? VariableDeclaration.Print(node.Declaration, context) : Doc.Null,
                        SeparatedSyntaxList.Print(node.Initializers, Node.Print, " ", context),
                        Token.Print(node.FirstSemicolonToken, context)),
                    node.Condition is not null ? Doc.Concat(Doc.Line, Node.Print(node.Condition, context)) : Doc.Line,
                    Token.Print(node.SecondSemicolonToken, context),
                    Doc.Line,
                    Doc.Group(Doc.Indent(SeparatedSyntaxList.Print(node.Incrementors, Node.Print, Doc.Line, context)))),
                Token.Print(node.CloseParenToken, context)),
            node.Statement switch
            {
                ForStatementSyntax => Doc.Group(Doc.HardLine, Node.Print(node.Statement, context)),
                _ => OptionalBraces.Print(node.Statement, context)
            });
}