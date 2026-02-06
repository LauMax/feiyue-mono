using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ConstructorConstraint
{
    public static Doc Print(ConstructorConstraintSyntax node, PrintingContext context) =>
        Doc.Concat(Token.Print(node.NewKeyword, context), Token.Print(node.OpenParenToken, context), Token.Print(node.CloseParenToken, context));
}