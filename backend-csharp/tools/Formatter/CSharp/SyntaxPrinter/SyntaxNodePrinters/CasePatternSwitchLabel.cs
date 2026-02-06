using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class CasePatternSwitchLabel
{
    public static Doc Print(CasePatternSwitchLabelSyntax node, PrintingContext context) =>
        Doc.Group(
            ExtraNewLines.Print(node),
            Token.PrintWithSuffix(node.Keyword, " ", context),
            Node.Print(node.Pattern, context),
            node.WhenClause is not null ? WhenClause.Print(node.WhenClause, context) : Doc.Null,
            Token.Print(node.ColonToken, context));
}