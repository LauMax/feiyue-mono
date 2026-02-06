using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class GenericName
{
    public static Doc Print(GenericNameSyntax node, PrintingContext context) =>
        Doc.Concat(
            Token.PrintLeadingTrivia(node.Identifier, context),
            Doc.Group(Token.PrintWithoutLeadingTrivia(node.Identifier, context), Node.Print(node.TypeArgumentList, context)));
}