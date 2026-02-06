using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class BracketedParameterList
{
    public static Doc Print(BracketedParameterListSyntax node, PrintingContext context) => ParameterList.Print(node, node.OpenBracketToken, node.CloseBracketToken, context);
}