using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class LetClause
{
    public static Doc Print(LetClauseSyntax node, PrintingContext context) =>
        Doc.Concat(
            ExtraNewLines.Print(node),
            Token.PrintWithSuffix(node.LetKeyword, " ", context),
            Token.PrintWithSuffix(node.Identifier, " ", context),
            Token.PrintWithSuffix(node.EqualsToken, " ", context),
            Node.Print(node.Expression, context));
}