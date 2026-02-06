using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class DoStatement
{
    public static Doc Print(DoStatementSyntax node, PrintingContext context) =>
        Doc.Concat(
            ExtraNewLines.Print(node),
            Token.PrintWithSuffix(node.DoKeyword, node.Statement is not BlockSyntax ? " " : Doc.Null, context),
            Node.Print(node.Statement, context),
            Doc.HardLine,
            Token.PrintWithSuffix(node.WhileKeyword, " ", context),
            Token.Print(node.OpenParenToken, context),
            Doc.Indent(Doc.SoftLine, Node.Print(node.Condition, context)),
            Token.Print(node.CloseParenToken, context),
            Token.Print(node.SemicolonToken, context));
}