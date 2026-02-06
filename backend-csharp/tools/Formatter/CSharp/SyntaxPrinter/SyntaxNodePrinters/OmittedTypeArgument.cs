using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class OmittedTypeArgument
{
    public static Doc Print(OmittedTypeArgumentSyntax _, PrintingContext _1) => Doc.Null;
}