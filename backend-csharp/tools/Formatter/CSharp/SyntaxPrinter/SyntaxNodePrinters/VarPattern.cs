using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class VarPattern
{
    public static Doc Print(VarPatternSyntax node, PrintingContext context) =>
        Doc.Concat(Token.PrintWithSuffix(node.VarKeyword, " ", context), Node.Print(node.Designation, context));
}