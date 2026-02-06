using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class IfStatement
{
    public static Doc Print(IfStatementSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(8);
        if (node.Parent is not ElseClauseSyntax)
            docs.Add(ExtraNewLines.Print(node));

        docs.Add(
            Token.Print(node.IfKeyword, context),
            " ",
            Doc.Group(Token.Print(node.OpenParenToken, context), Doc.Indent(Doc.IfBreak(Doc.SoftLine, Doc.Null), Node.Print(node.Condition, context))),
            Token.Print(node.CloseParenToken, context),
            OptionalBraces.Print(node.Statement, context));

        if (node.Else is not null)
            docs.Add(Doc.HardLine, Node.Print(node.Else, context));

        return Doc.Concat(ref docs);
    }
}