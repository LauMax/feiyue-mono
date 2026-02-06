using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

internal static class ArgumentListLike
{
    public static Doc Print(SyntaxToken openParenToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken, PrintingContext context)
    {
        Doc? args;
        if (arguments is [{ Expression: SimpleLambdaExpressionSyntax }])
        {
            var lambda1 = (SimpleLambdaExpressionSyntax)arguments[0].Expression;
            var groupId = context.GroupFor("LambdaArguments");
            args = Doc.Concat(
                Doc.GroupWithId(groupId, Doc.Indent(Doc.SoftLine, Argument.PrintModifiers(arguments[0], context), SimpleLambdaExpression.PrintHead(lambda1, context))),
                Doc.IfBreak(Doc.Indent(Doc.Group(SimpleLambdaExpression.PrintBody(lambda1, context))), SimpleLambdaExpression.PrintBody(lambda1, context), groupId));
        }
        else if (arguments is [{ Expression: ParenthesizedLambdaExpressionSyntax { ParameterList.Parameters: [] } }])
        {
            var lambda = (ParenthesizedLambdaExpressionSyntax)arguments[0].Expression;
            var groupId = context.GroupFor("LambdaArguments");
            args = Doc.Concat(
                Doc.GroupWithId(groupId, Doc.Indent(Doc.SoftLine, Argument.PrintModifiers(arguments[0], context), ParenthesizedLambdaExpression.PrintHead(lambda, context))),
                Doc.IndentIfBreak(ParenthesizedLambdaExpression.PrintBody(lambda, context), groupId));
        }
        else if (arguments is [{ Expression: CollectionExpressionSyntax, NameColon: null }])
        {
            args = SeparatedSyntaxList.Print(arguments, Argument.Print, Doc.Line, context);
        }
        else if (arguments.Count > 0)
        {
            args = Doc.Indent(Doc.SoftLine, SeparatedSyntaxList.Print(arguments, Argument.Print, Doc.Line, context));
        }
        else
        {
            args = Doc.Null;
        }

        return Doc.Concat(Token.Print(openParenToken, context), args, Token.Print(closeParenToken, context));
    }
}