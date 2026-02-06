using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class GlobalStatement
{
    public static Doc Print(GlobalStatementSyntax node, PrintingContext context) =>
        Doc.Concat(AttributeLists.Print(node, node.AttributeLists, context), Modifiers.Print(node.Modifiers, context), Node.Print(node.Statement, context));
}