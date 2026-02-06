using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class GotoStatement
{
    public static Doc Print(GotoStatementSyntax node, PrintingContext context)
    {
        var expression = node.Expression is not null ? Doc.Concat(" ", Node.Print(node.Expression, context)) : string.Empty;
        return Doc.Concat(
            ExtraNewLines.Print(node),
            Token.Print(node.GotoKeyword, context),
            node.CaseOrDefaultKeyword.RawSyntaxKind() != SyntaxKind.None ? " " : Doc.Null,
            Token.Print(node.CaseOrDefaultKeyword, context),
            expression,
            Token.Print(node.SemicolonToken, context));
    }
}