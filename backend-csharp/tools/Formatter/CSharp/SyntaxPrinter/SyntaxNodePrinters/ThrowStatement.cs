using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ThrowStatement
{
    public static Doc Print(ThrowStatementSyntax node, PrintingContext context)
    {
        var expression = node.Expression is not null ? Doc.Concat(" ", Node.Print(node.Expression, context)) : string.Empty;
        return Doc.Concat(ExtraNewLines.Print(node), Token.Print(node.ThrowKeyword, context), expression, Token.Print(node.SemicolonToken, context));
    }
}