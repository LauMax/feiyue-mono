using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class FunctionPointerType
{
    public static Doc Print(FunctionPointerTypeSyntax node, PrintingContext context) =>
        Doc.Concat(
            Token.Print(node.DelegateKeyword, context),
            Token.PrintWithSuffix(node.AsteriskToken, " ", context),
            node.CallingConvention is not null ? PrintCallingConvention(node.CallingConvention, context) : Doc.Null,
            Token.Print(node.ParameterList.LessThanToken, context),
            Doc.Indent(
                Doc.Group(
                    Doc.SoftLine,
                    SeparatedSyntaxList.Print(
                        node.ParameterList.Parameters,
                        (o, _) => Doc.Concat(AttributeLists.Print(o, o.AttributeLists, context), Modifiers.Print(o.Modifiers, context), Node.Print(o.Type, context)),
                        Doc.Line,
                        context))),
            Token.Print(node.ParameterList.GreaterThanToken, context));

    private static Doc PrintCallingConvention(FunctionPointerCallingConventionSyntax node, PrintingContext context) =>
        Doc.Concat(
            Token.Print(node.ManagedOrUnmanagedKeyword, context),
            node.UnmanagedCallingConventionList is not null
                ? Doc.Concat(
                    Token.Print(node.UnmanagedCallingConventionList.OpenBracketToken, context),
                    SeparatedSyntaxList.Print(node.UnmanagedCallingConventionList.CallingConventions, (o, _) => Token.Print(o.Name, context), " ", context),
                    Token.Print(node.UnmanagedCallingConventionList.CloseBracketToken, context))
                : Doc.Null);
}