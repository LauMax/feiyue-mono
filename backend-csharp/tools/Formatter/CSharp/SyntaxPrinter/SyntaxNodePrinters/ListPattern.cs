using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ListPattern
{
    public static Doc Print(ListPatternSyntax node, PrintingContext context) =>
        Doc.Group(
            Token.Print(node.OpenBracketToken, context),
            Doc.Indent(Doc.SoftLine, SeparatedSyntaxList.PrintWithTrailingComma(node.Patterns, Node.Print, Doc.Line, context, node.CloseBracketToken)),
            Token.Print(node.CloseBracketToken, context),
            node.Designation is not null ? " " : Doc.Null,
            Node.Print(node.Designation, context));
}