using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class BracketedArgumentList
{
    public static Doc Print(BracketedArgumentListSyntax node, PrintingContext context) =>
        Doc.Group(
            Token.Print(node.OpenBracketToken, context),
            Doc.Indent(Doc.SoftLine, SeparatedSyntaxList.Print(node.Arguments, Node.Print, Doc.Line, context)),
            Token.Print(node.CloseBracketToken, context));
}