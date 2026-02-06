using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ParenthesizedExpression
{
    public static Doc Print(ParenthesizedExpressionSyntax node, PrintingContext context) =>
        Doc.Group(Token.Print(node.OpenParenToken, context), Doc.Indent(Doc.SoftLine, Node.Print(node.Expression, context)), Token.Print(node.CloseParenToken, context));
}