using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class BinaryPattern
{
    public static Doc Print(BinaryPatternSyntax node, PrintingContext context) =>
        Doc.IndentIf(
            node.Parent is SubpatternSyntax or IsPatternExpressionSyntax,
            Doc.Concat(Node.Print(node.Left, context), Doc.Line, Token.PrintWithSuffix(node.OperatorToken, " ", context), Node.Print(node.Right, context)));
}