using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class TupleType
{
    public static Doc Print(TupleTypeSyntax node, PrintingContext context) =>
        Doc.Group(
            Token.Print(node.OpenParenToken, context),
            Doc.Indent(Doc.SoftLine, SeparatedSyntaxList.Print(node.Elements, Node.Print, Doc.Line, context)),
            Token.Print(node.CloseParenToken, context));
}