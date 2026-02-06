using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class RecursivePattern
{
    public static Doc PrintWithOutType(RecursivePatternSyntax node, PrintingContext context) => Print(node, false, context);

    public static Doc Print(RecursivePatternSyntax node, PrintingContext context) => Print(node, true, context);

    private static Doc Print(RecursivePatternSyntax node, bool includeType, PrintingContext context)
    {
        DocListBuilder result = new(8);
        if (node.Type is not null && includeType)
            result.Add(Node.Print(node.Type, context));

        if (node.PositionalPatternClause is not null)
        {
            result.Add(
                node.Parent
                    is SwitchExpressionArmSyntax
                        or IsPatternExpressionSyntax
                        or CasePatternSwitchLabelSyntax
                        or BinaryPatternSyntax { Parent: SwitchExpressionArmSyntax or CasePatternSwitchLabelSyntax }
                    ? Doc.Null
                    : Doc.SoftLine,
                Doc.Group(
                    Token.Print(node.PositionalPatternClause.OpenParenToken, context),
                    Doc.Indent(
                        Doc.SoftLine,
                        SeparatedSyntaxList.Print(
                            node.PositionalPatternClause.Subpatterns,
                            (subpatternNode, _) =>
                                Doc.Concat(
                                    subpatternNode.NameColon is not null ? BaseExpressionColon.Print(subpatternNode.NameColon, context) : Doc.Null,
                                    Node.Print(subpatternNode.Pattern, context)),
                            Doc.Line,
                            context)),
                    Token.Print(node.PositionalPatternClause.CloseParenToken, context)));
        }

        if (node.PropertyPatternClause is not null)
        {
            if (!node.PropertyPatternClause.Subpatterns.Any())
            {
                if (node.Type is not null)
                    result.Add(" ");

                result.Add("{ }");
            }
            else
            {
                result.Add(
                    Doc.Group(
                        node.Type is not null && !node.PropertyPatternClause.OpenBraceToken.LeadingTrivia.Any(o => o.IsDirective || o.IsComment()) ? Doc.Line : Doc.Null,
                        Token.Print(node.PropertyPatternClause.OpenBraceToken, context),
                        Doc.Indent(
                            node.PropertyPatternClause.Subpatterns.Any() ? Doc.Line : Doc.Null,
                            SeparatedSyntaxList.Print(
                                node.PropertyPatternClause.Subpatterns,
                                (subpatternNode, _) =>
                                    Doc.Group(
                                        subpatternNode.ExpressionColon is not null ? Node.Print(subpatternNode.ExpressionColon, context) : Doc.Null,
                                        Node.Print(subpatternNode.Pattern, context)),
                                Doc.Line,
                                context)),
                        Doc.Line,
                        Token.Print(node.PropertyPatternClause.CloseBraceToken, context)));
            }
        }

        if (node.Designation is not null)
            result.Add(" ", Node.Print(node.Designation, context));

        return Doc.Concat(ref result);
    }
}