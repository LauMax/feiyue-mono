using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ParenthesizedPattern
{
    public static Doc Print(ParenthesizedPatternSyntax node, PrintingContext context) =>
        Doc.Group(Token.Print(node.OpenParenToken, context), Doc.Indent(Doc.SoftLine, Node.Print(node.Pattern, context)), Token.Print(node.CloseParenToken, context));
}