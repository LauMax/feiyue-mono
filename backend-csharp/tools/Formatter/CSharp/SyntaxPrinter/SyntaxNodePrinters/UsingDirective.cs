using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class UsingDirective
{
    public static Doc Print(UsingDirectiveSyntax node, PrintingContext context, bool printExtraLines = true) =>
        Doc.Concat(
            printExtraLines ? ExtraNewLines.Print(node) : Doc.Null,
            Token.PrintWithSuffix(node.GlobalKeyword, " ", context, skipLeadingTrivia: true),
            Token.PrintWithSuffix(node.UsingKeyword, " ", context, skipLeadingTrivia: true),
            Token.PrintWithSuffix(node.UnsafeKeyword, " ", context, skipLeadingTrivia: true),
            Token.PrintWithSuffix(node.StaticKeyword, " ", context, skipLeadingTrivia: true),
            node.Alias is null ? Doc.Null : NameEquals.Print(node.Alias, context),
            Node.Print(node.NamespaceOrType, context),
            Token.Print(node.SemicolonToken, context));
}