using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class DeclarationExpression
{
    public static Doc Print(DeclarationExpressionSyntax node, PrintingContext context) => Doc.Concat(Node.Print(node.Type, context), " ", Node.Print(node.Designation, context));
}