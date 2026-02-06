using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ArgumentList
{
    public static Doc Print(ArgumentListSyntax node, PrintingContext context) =>
        Doc.Group(
            Doc.IndentIf(
                node.Parent
                    is InvocationExpressionSyntax
                    {
                        Expression: IdentifierNameSyntax
                            or GenericNameSyntax
                            or MemberAccessExpressionSyntax { Expression: ThisExpressionSyntax or PredefinedTypeSyntax or IdentifierNameSyntax { Identifier.Text.Length: <= 4 } },
                        Parent: { Parent: InvocationExpressionSyntax } or PostfixUnaryExpressionSyntax { Parent.Parent: InvocationExpressionSyntax }
                    },
                ArgumentListLike.Print(node.OpenParenToken, node.Arguments, node.CloseParenToken, context)));
}