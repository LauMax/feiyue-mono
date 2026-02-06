using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

internal static partial class CSharpierIgnore
{
    private static readonly Regex IgnoreRegex = IgnoreRegexGenerator();
    public static readonly Regex IgnoreStartRegex = IgnoreStartRegexGenerator();
    public static readonly Regex IgnoreEndRegex = IgnoreEndRegexGenerator();
    public static readonly Regex WhiteSpaceLineEndingsRegex = WhiteSpaceLineEndingsGenerator();

    public static bool HasIgnoreComment(SyntaxNode syntaxNode) => Token.HasLeadingCommentMatching(syntaxNode, IgnoreRegex);

    public static bool HasIgnoreComment(SyntaxToken syntaxToken) => Token.HasLeadingCommentMatching(syntaxToken, IgnoreRegex);

    public static bool IsNodeIgnored(SyntaxNode syntaxNode)
    {
        // this get handled in BaseMethodDeclaration and/or AttributeList
        if (syntaxNode is BaseMethodDeclarationSyntax baseMethodDeclarationSyntax && baseMethodDeclarationSyntax.AttributeLists.Any())
            return false;

        return syntaxNode.Parent?.Kind()
                is SyntaxKind.ClassDeclaration
                    or SyntaxKind.StructDeclaration
                    or SyntaxKind.InterfaceDeclaration
                    or SyntaxKind.RecordDeclaration
                    or SyntaxKind.RecordStructDeclaration
                    or SyntaxKind.EnumDeclaration
                    or SyntaxKind.Block
                    or SyntaxKind.CompilationUnit
                    or SyntaxKind.NamespaceDeclaration
            && HasIgnoreComment(syntaxNode);
    }

    [SkipLocalsInit]
    public static List<Doc> PrintNodesRespectingRangeIgnore<T>(SyntaxList<T> list, PrintingContext context)
        where T : SyntaxNode
    {
        List<Doc> statements = [];
        StringBuilder unFormattedCode = new();
        var printUnformatted = false;

        foreach (var node in list)
        {
            if (Token.HasLeadingCommentMatching(node, IgnoreEndRegex))
            {
                statements.Add(unFormattedCode.ToString().Trim());
                unFormattedCode.Clear();
                printUnformatted = false;
            }
            else if (Token.HasLeadingCommentMatching(node, IgnoreStartRegex))
            {
                printUnformatted = true;
            }

            if (printUnformatted)
                unFormattedCode.Append(PrintWithoutFormatting(node, context));
            else
                statements.Add(Node.Print(node, context));
        }

        if (unFormattedCode.Length > 0)
            statements.Add(unFormattedCode.ToString().Trim());

        return statements;
    }

    public static string PrintWithoutFormatting(SyntaxNode syntaxNode, PrintingContext context) => PrintWithoutFormatting(syntaxNode.GetText().ToString(), context);

    public static string PrintWithoutFormatting(string code, PrintingContext context) =>
        // trim trailing whitespace + replace only existing line endings
        WhiteSpaceLineEndingsRegex.Replace(code, context.Options.LineEnding);

    [GeneratedRegex("^// csharpier-ignore($| -)")]
    private static partial Regex IgnoreRegexGenerator();

    [GeneratedRegex("^// csharpier-ignore-start($| -)")]
    private static partial Regex IgnoreStartRegexGenerator();

    [GeneratedRegex("^// csharpier-ignore-end($| -)")]
    private static partial Regex IgnoreEndRegexGenerator();

    [GeneratedRegex(@"[\t\v\f ]*(\r\n?|\n)")]
    private static partial Regex WhiteSpaceLineEndingsGenerator();
}