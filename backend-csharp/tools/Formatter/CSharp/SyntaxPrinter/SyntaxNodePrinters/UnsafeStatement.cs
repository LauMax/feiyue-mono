using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class UnsafeStatement
{
    public static Doc Print(UnsafeStatementSyntax node, PrintingContext context) =>
        Doc.Concat(ExtraNewLines.Print(node), Token.Print(node.UnsafeKeyword, context), Node.Print(node.Block, context));
}