using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class DefaultConstraint
{
    public static Doc Print(DefaultConstraintSyntax node, PrintingContext context) => Token.Print(node.DefaultKeyword, context);
}