using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class MakeRefExpression
{
    public static Doc Print(MakeRefExpressionSyntax node, PrintingContext context) =>
        Doc.Concat(Token.Print(node.Keyword, context), Token.Print(node.OpenParenToken, context), Node.Print(node.Expression, context), Token.Print(node.CloseParenToken, context));
}