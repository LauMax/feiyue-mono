using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class UnaryPattern
{
    public static Doc Print(UnaryPatternSyntax node, PrintingContext context) =>
        Doc.Concat(Token.PrintWithSuffix(node.OperatorToken, " ", context), Node.Print(node.Pattern, context));
}