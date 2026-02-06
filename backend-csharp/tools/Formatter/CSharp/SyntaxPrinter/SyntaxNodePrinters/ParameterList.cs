using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ParameterList
{
    public static Doc Print(ParameterListSyntax node, PrintingContext context) => Print(node, node.OpenParenToken, node.CloseParenToken, context);

    public static Doc Print(BaseParameterListSyntax node, SyntaxToken openToken, SyntaxToken closeToken, PrintingContext context) =>
        Doc.Group(
            Token.Print(openToken, context),
            node.Parameters.Count > 0 ? Doc.Indent(Doc.SoftLine, SeparatedSyntaxList.Print(node.Parameters, Parameter.Print, Doc.Line, context)) : Doc.Null,
            Token.Print(closeToken, context));
}