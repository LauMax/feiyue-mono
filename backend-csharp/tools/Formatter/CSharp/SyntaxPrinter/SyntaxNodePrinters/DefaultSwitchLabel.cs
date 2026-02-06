using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class DefaultSwitchLabel
{
    public static Doc Print(DefaultSwitchLabelSyntax node, PrintingContext context) =>
        Doc.Concat(ExtraNewLines.Print(node), Token.Print(node.Keyword, context), Token.Print(node.ColonToken, context));
}