using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class AliasQualifiedName
{
    public static Doc Print(AliasQualifiedNameSyntax node, PrintingContext context) =>
        Doc.Concat(Node.Print(node.Alias, context), Token.Print(node.ColonColonToken, context), Node.Print(node.Name, context));
}