using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ArrayType
{
    public static Doc Print(ArrayTypeSyntax node, PrintingContext context) =>
        Doc.Concat(Node.Print(node.ElementType, context), Doc.Concat(node.RankSpecifiers.Select(o => Node.Print(o, context)).ToArray()));
}