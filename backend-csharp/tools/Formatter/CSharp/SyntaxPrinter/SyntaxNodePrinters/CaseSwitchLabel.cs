using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class CaseSwitchLabel
{
    public static Doc Print(CaseSwitchLabelSyntax node, PrintingContext context) =>
        Doc.Concat(ExtraNewLines.Print(node), Token.PrintWithSuffix(node.Keyword, " ", context), Doc.Group(Node.Print(node.Value, context)), Token.Print(node.ColonToken, context));
}