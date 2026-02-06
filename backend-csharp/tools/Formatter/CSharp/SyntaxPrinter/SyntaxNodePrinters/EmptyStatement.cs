using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class EmptyStatement
{
    public static Doc Print(EmptyStatementSyntax node, PrintingContext context) => Token.Print(node.SemicolonToken, context);
}