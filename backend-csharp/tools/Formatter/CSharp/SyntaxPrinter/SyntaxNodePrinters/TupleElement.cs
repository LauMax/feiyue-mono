using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class TupleElement
{
    public static Doc Print(TupleElementSyntax node, PrintingContext context) =>
        Doc.Concat(Node.Print(node.Type, context), node.Identifier.RawSyntaxKind() != SyntaxKind.None ? Doc.Concat(" ", Token.Print(node.Identifier, context)) : Doc.Null);
}