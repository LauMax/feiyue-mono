using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class TypeOfExpression
{
    public static Doc Print(TypeOfExpressionSyntax node, PrintingContext context) =>
        Doc.Concat(Token.Print(node.Keyword, context), Token.Print(node.OpenParenToken, context), Node.Print(node.Type, context), Token.Print(node.CloseParenToken, context));
}