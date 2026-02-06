using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ReturnStatement
{
    public static Doc Print(ReturnStatementSyntax node, PrintingContext context)
    {
        if (node.Expression is null)
            return Doc.Concat(ExtraNewLines.Print(node), Token.Print(node.ReturnKeyword, context), Token.Print(node.SemicolonToken, context));

        // Check if the original code has the expression on a new line after 'return'
        var returnLine = node.ReturnKeyword.GetLocation().GetLineSpan().EndLinePosition.Line;
        var exprLine = node.Expression.GetLocation().GetLineSpan().StartLinePosition.Line;
        var wasOnNewLine = exprLine > returnLine;

        if (wasOnNewLine)
        {
            // Preserve the line break - the developer intentionally split it
            return Doc.Concat(
                ExtraNewLines.Print(node),
                Token.Print(node.ReturnKeyword, context),
                Doc.Indent(Doc.HardLine, Node.Print(node.Expression, context)),
                Token.Print(node.SemicolonToken, context));
        }

        // Normal case - try to keep on one line, break if needed
        return Doc.Group(
            ExtraNewLines.Print(node),
            Token.PrintWithSuffix(node.ReturnKeyword, " ", context),
            node.Expression is BinaryExpressionSyntax or QueryExpressionSyntax ? Doc.Indent(Node.Print(node.Expression, context)) : Node.Print(node.Expression, context),
            Token.Print(node.SemicolonToken, context));
    }
}