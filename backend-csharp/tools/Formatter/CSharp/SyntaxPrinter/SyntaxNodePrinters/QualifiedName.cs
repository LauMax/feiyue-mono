using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class QualifiedName
{
    public static Doc Print(QualifiedNameSyntax node, PrintingContext context) =>
        Doc.Concat(Node.Print(node.Left, context), Token.Print(node.DotToken, context), Node.Print(node.Right, context));
}