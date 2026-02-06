using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class SpreadElement
{
    public static Doc Print(SpreadElementSyntax node, PrintingContext context) => Doc.Group(Token.Print(node.OperatorToken, context), " ", Node.Print(node.Expression, context));
}