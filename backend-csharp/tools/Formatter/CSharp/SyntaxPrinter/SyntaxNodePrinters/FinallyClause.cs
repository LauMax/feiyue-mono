using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class FinallyClause
{
    public static Doc Print(FinallyClauseSyntax node, PrintingContext context) => Doc.Concat(Token.Print(node.FinallyKeyword, context), Node.Print(node.Block, context));
}