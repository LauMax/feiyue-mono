using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class NullableType
{
    public static Doc Print(NullableTypeSyntax node, PrintingContext context) => Doc.Concat(Node.Print(node.ElementType, context), Token.Print(node.QuestionToken, context));
}