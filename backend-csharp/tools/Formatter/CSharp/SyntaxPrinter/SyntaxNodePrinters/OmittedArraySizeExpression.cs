using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class OmittedArraySizeExpression
{
    public static Doc Print(OmittedArraySizeExpressionSyntax _, PrintingContext _1) => Doc.Null;
}