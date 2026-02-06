using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class RelationalPattern
{
    public static Doc Print(RelationalPatternSyntax node, PrintingContext context) =>
        Doc.Concat(Token.PrintWithSuffix(node.OperatorToken, " ", context), Node.Print(node.Expression, context));
}