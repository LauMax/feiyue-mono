using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class CheckedStatement
{
    public static Doc Print(CheckedStatementSyntax node, PrintingContext context) => Doc.Concat(Token.Print(node.Keyword, context), Block.Print(node.Block, context));
}