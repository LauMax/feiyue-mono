using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class LocalFunctionStatement
{
    public static Doc Print(LocalFunctionStatementSyntax node, PrintingContext context) => BaseMethodDeclaration.Print(node, context);
}