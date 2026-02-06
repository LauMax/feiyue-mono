using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Feiyue.Formatter.CSharp;

internal sealed partial class SyntaxNodeComparer
{
    private string OriginalSourceCode { get; }
    private string NewSourceCode { get; }
    private SyntaxTree OriginalSyntaxTree { get; }
    private SyntaxTree NewSyntaxTree { get; }
    private bool ReorderedModifiers { get; }
    private bool ReorderedUsingsWithDisabledText { get; }
    private bool MovedTrailingTrivia { get; }

    public SyntaxNodeComparer(
        string originalSourceCode,
        string newSourceCode,
        bool reorderedModifiers,
        bool reorderedUsingsWithDisabledText,
        bool movedTrailingTrivia,
        SourceCodeKind sourceCodeKind,
        CancellationToken cancellationToken)
    {
        OriginalSourceCode = originalSourceCode;
        NewSourceCode = newSourceCode;
        ReorderedModifiers = reorderedModifiers;
        ReorderedUsingsWithDisabledText = reorderedUsingsWithDisabledText;
        MovedTrailingTrivia = movedTrailingTrivia;

        CSharpParseOptions cSharpParseOptions = new(CSharpFormatter.LanguageVersion, kind: sourceCodeKind);
        OriginalSyntaxTree = CSharpSyntaxTree.ParseText(OriginalSourceCode, cSharpParseOptions, cancellationToken: cancellationToken);
        NewSyntaxTree = CSharpSyntaxTree.ParseText(NewSourceCode, cSharpParseOptions, cancellationToken: cancellationToken);
        CompareFunc = Compare;
    }

    public async Task<string> CompareSourceAsync(CancellationToken cancellationToken)
    {
        // this seems almost impossible to figure out with the current way this is written
        // the usings could be in disabled text on namespaces, or on the modifiers of any base types.
        // parts of the #if or #endif could be leading trivia in different places
        if (ReorderedUsingsWithDisabledText)
            return string.Empty;

        var result = AreEqualIgnoringWhitespace(await OriginalSyntaxTree.GetRootAsync(cancellationToken), await NewSyntaxTree.GetRootAsync(cancellationToken));

        if (!result.IsInvalid)
            return string.Empty;

        var message = $"----------------------------- Original: {GetLine(result.OriginalSpan, OriginalSyntaxTree, OriginalSourceCode)}";

        message += $"----------------------------- Formatted: {GetLine(result.NewSpan, NewSyntaxTree, NewSourceCode)}";
        return message;
    }

    private static string GetLine(TextSpan? textSpan, SyntaxTree syntaxTree, string source)
    {
        if (!textSpan.HasValue)
            return "Missing";

        var line = syntaxTree.GetLineSpan(textSpan.Value).StartLinePosition.Line;
        var endLine = syntaxTree.GetLineSpan(textSpan.Value).EndLinePosition.Line;

        var result = $"Around Line {line} -----------------------------{Environment.NewLine}";

        using StringReader stringReader = new(source);
        var x = 0;
        var linesWritten = 0;
        var currentLine = stringReader.ReadLine();
        while (x <= endLine + 2 || linesWritten < 8)
        {
            if (x >= line - 2)
            {
                result += currentLine + Environment.NewLine;
                linesWritten++;
            }

            if (linesWritten > 15)
                break;

            currentLine = stringReader.ReadLine();
            if (currentLine is null)
                break;

            x++;
        }

        return result;
    }

    private readonly Stack<(SyntaxNode? Node, SyntaxNode? Parent)> originalStack = [];
    private readonly Stack<(SyntaxNode? Node, SyntaxNode? Parent)> formattedStack = [];

    private CompareResult AreEqualIgnoringWhitespace(SyntaxNode originalStart, SyntaxNode formattedStart)
    {
        originalStack.Push((originalStart, originalStart));
        formattedStack.Push((formattedStart, formattedStart));
        while (originalStack.Count > 0)
        {
            var result = Compare(originalStack.Pop(), formattedStack.Pop());
            if (result.IsInvalid)
                return result;
        }

        return default;
    }

#pragma warning disable CA1822
    private CompareResult CompareLists<T>(
        T originalList,
        T formattedList,
        Func<SyntaxToken, SyntaxToken, CompareResult> comparer,
        Func<SyntaxToken, TextSpan> getSpan,
        TextSpan originalParentSpan,
        TextSpan newParentSpan)
        where T : IReadOnlyList<SyntaxToken>
    {
        for (var x = 0; x < originalList.Count || x < formattedList.Count; x++)
        {
            if (x == originalList.Count)
                return NotEqual(originalParentSpan, getSpan(formattedList[x]));

            if (x == formattedList.Count)
                return NotEqual(getSpan(originalList[x]), newParentSpan);

            var result = comparer(originalList[x], formattedList[x]);
            if (result.IsInvalid)
                return result;
        }

        return default;
    }
#pragma warning restore CA1822

    private CompareResult CompareLists<T>(
        T originalList,
        T formattedList,
        Func<SyntaxNode, SyntaxNode, CompareResult> _,
        Func<SyntaxNode, TextSpan> getSpan,
        TextSpan originalParentSpan,
        TextSpan newParentSpan)
        where T : IReadOnlyList<SyntaxNode>
    {
        for (var x = 0; x < originalList.Count || x < formattedList.Count; x++)
        {
            if (x == originalList.Count)
                return NotEqual(originalParentSpan, getSpan(formattedList[x]));

            if (x == formattedList.Count)
                return NotEqual(getSpan(originalList[x]), newParentSpan);

            var originalNode = originalList[x];
            var formattedNode = formattedList[x];
            originalStack.Push((originalNode, originalNode.Parent));
            formattedStack.Push((formattedNode, formattedNode.Parent));
        }

        return default;
    }

    private static SyntaxToken[] AllSeparatorsButLast(in SeparatedSyntaxList<SyntaxNode> list)
    {
        if (list.Count <= 1)
            return [];

        var tokens = new SyntaxToken[list.Count - 1];
        var tokenIndex = 0;

        foreach (var element in list.GetWithSeparators())
        {
            if (element.IsToken)
            {
                tokens[tokenIndex++] = element.AsToken();
                if (tokenIndex == tokens.Length)
                    break;
            }
        }

        return tokens;
    }

    private static CompareResult NotEqual(SyntaxNode? originalNode, SyntaxNode? formattedNode) =>
        new()
        {
            IsInvalid = true,
            OriginalSpan = originalNode?.Span,
            NewSpan = formattedNode?.Span
        };

    private static CompareResult NotEqual(TextSpan? originalSpan, TextSpan? formattedSpan) =>
        new()
        {
            IsInvalid = true,
            OriginalSpan = originalSpan,
            NewSpan = formattedSpan
        };

    private Func<SyntaxToken, SyntaxToken, CompareResult> CompareFunc { get; }

    private CompareResult Compare(SyntaxToken originalToken, SyntaxToken formattedToken) => Compare(originalToken, formattedToken, null, null);

    private CompareResult Compare(SyntaxToken originalToken, SyntaxToken formattedToken, SyntaxNode? originalNode, SyntaxNode? formattedNode)
    {
        if (
            ReorderedModifiers
            && (
                (formattedNode is NamespaceDeclarationSyntax nd && nd.NamespaceKeyword == formattedToken)
                || (formattedNode is FileScopedNamespaceDeclarationSyntax fsnd && fsnd.NamespaceKeyword == formattedToken))
            && formattedNode.GetLeadingTrivia().ToFullString().Contains("#endif", StringComparison.Ordinal))
        {
            return default;
        }

        if (originalToken.Parent is InterpolatedStringExpressionSyntax && originalToken.Kind() is SyntaxKind.InterpolatedRawStringEndToken)
        {
            // this detects if we added indentation when there was none, or removed all indentation when there was some
            // and handles the case of changing /t to " "
            var originalFirst = originalToken.ValueText.TrimStart(['\r', '\n'])[0];
            var formattedFirst = formattedToken.ValueText.TrimStart(['\r', '\n'])[0];
            if (originalFirst != formattedFirst && (originalFirst is not (' ' or '\t') || formattedFirst is not (' ' or '\t')))
                return NotEqual(originalToken.Span, formattedNode!.Span);
        }
        // when a verbatim string contains mismatched line endings they will become consistent
        // this validation will fail unless we also get them consistent here
        // adding a semi-complicated if check to determine when to do the string replacement
        // did not appear to have any performance benefits
        else if (originalToken.ValueText.Replace("\r", string.Empty, StringComparison.Ordinal) != formattedToken.ValueText.Replace("\r", string.Empty, StringComparison.Ordinal))
        {
            return NotEqual(
                originalToken.RawSyntaxKind() == SyntaxKind.None ? originalNode?.Span : originalToken.Span,
                formattedToken.RawSyntaxKind() == SyntaxKind.None ? formattedNode?.Span : formattedToken.Span);
        }

        var result = Compare(originalToken.LeadingTrivia, formattedToken.LeadingTrivia);
        if (result.IsInvalid)
            return result;

        var result2 = MovedTrailingTrivia ? default : Compare(originalToken.TrailingTrivia, formattedToken.TrailingTrivia);

        return result2.IsInvalid ? result2 : default;
    }

    private CompareResult Compare(SyntaxTrivia originalTrivia, SyntaxTrivia formattedTrivia)
    {
        if (originalTrivia.RawSyntaxKind() is SyntaxKind.DisabledTextTrivia)
        {
            if (ReorderedModifiers)
                return default;

            return DisabledTextComparer.IsCodeBasicallyEqual(originalTrivia.ToString(), formattedTrivia.ToString()) ? default : NotEqual(originalTrivia.Span, formattedTrivia.Span);
        }

        if (originalTrivia.IsComment())
            return CompareComment(originalTrivia.ToString(), formattedTrivia.ToString(), originalTrivia, formattedTrivia);

        return originalTrivia.ToString().TrimEnd() == formattedTrivia.ToString().TrimEnd() ? default : NotEqual(originalTrivia.Span, formattedTrivia.Span);
    }

    private bool CompareFullSpan(SyntaxNode originalStart, SyntaxNode formattedStart)
    {
        var originalSpan = OriginalSourceCode.AsSpan().Slice(originalStart.FullSpan.Start, originalStart.FullSpan.Length);
        var formattedSpan = NewSourceCode.AsSpan().Slice(formattedStart.FullSpan.Start, formattedStart.FullSpan.Length);
        return originalSpan == formattedSpan;
    }

    private static CompareResult CompareComment(string originalComment, string formattedComment, SyntaxTrivia originalTrivia, SyntaxTrivia formattedTrivia)
    {
        using StringReader originalStringReader = new(originalComment);
        using StringReader formattedStringReader = new(formattedComment);
        var originalLine = originalStringReader.ReadLine();
        var formattedLine = formattedStringReader.ReadLine();
        while (originalLine is not null)
        {
            if (formattedLine is null || originalLine.Trim() != formattedLine.Trim())
                return NotEqual(originalTrivia.Span, formattedTrivia.Span);

            originalLine = originalStringReader.ReadLine();
            formattedLine = formattedStringReader.ReadLine();
        }

        // After the loop, originalLine is always null. Check if formatted has extra lines.
        if (formattedLine is not null)
            return NotEqual(originalTrivia.Span, formattedTrivia.Span);

        return default;
    }

    private CompareResult Compare(SyntaxTriviaList originalList, SyntaxTriviaList formattedList)
    {
        static SyntaxTrivia? FindNextSyntaxTrivia(SyntaxTriviaList list, ref int next)
        {
            SyntaxTrivia result;
            do
            {
                if (next >= list.Count)
                    return null;

                result = list[next];
                next++;
            }
            while (
                result.RawSyntaxKind() is SyntaxKind.EndOfLineTrivia or SyntaxKind.WhitespaceTrivia
                || (result.RawSyntaxKind() is SyntaxKind.DisabledTextTrivia && string.IsNullOrWhiteSpace(result.ToString())));

            return result;
        }

        static string? BuildComment(SyntaxTriviaList list, ref int next)
        {
            string? result = null;
            while (next < list.Count && list[next].RawSyntaxKind() is SyntaxKind.EndOfLineTrivia or SyntaxKind.WhitespaceTrivia or SyntaxKind.SingleLineDocumentationCommentTrivia)
            {
                if (next >= list.Count)
                    return result;

                if (list[next].RawSyntaxKind() is SyntaxKind.SingleLineDocumentationCommentTrivia)
                    result += list[next].ToFullString();

                next++;
            }

            return result;
        }

        var nextOriginal = 0;
        var nextFormatted = 0;
        var original = FindNextSyntaxTrivia(originalList, ref nextOriginal);
        var formatted = FindNextSyntaxTrivia(formattedList, ref nextFormatted);
        while (original is not null && formatted is not null)
        {
            var result = Compare(original.Value, formatted.Value);
            if (
                original.Value.RawSyntaxKind() is SyntaxKind.SingleLineDocumentationCommentTrivia
                && formatted.Value.RawSyntaxKind() is SyntaxKind.SingleLineDocumentationCommentTrivia)
            {
                var originalCommentValue = original.Value.ToFullString();
                originalCommentValue += BuildComment(originalList, ref nextOriginal);

                var formattedCommentValue = formatted.Value.ToFullString();
                formattedCommentValue += BuildComment(formattedList, ref nextFormatted);

                result = CompareComment(originalCommentValue, formattedCommentValue, original.Value, formatted.Value);
            }

            if (result.IsInvalid)
                return result;

            original = FindNextSyntaxTrivia(originalList, ref nextOriginal);
            formatted = FindNextSyntaxTrivia(formattedList, ref nextFormatted);
        }

        if (original != formatted)
            return NotEqual(originalList.Span, formattedList.Span);

        return default;
    }

    private CompareResult CompareUsingDirectives(
        SyntaxList<UsingDirectiveSyntax> original,
        SyntaxList<UsingDirectiveSyntax> formatted,
        SyntaxNode originalParent,
        SyntaxNode formattedParent)
    {
        if (original.Count > 0 && original.First().GetLeadingTrivia().Any())
            return default;

        if (original.Count != formatted.Count)
            return NotEqual(originalParent, formattedParent);

        var sortedOriginal = original.OrderBy(o => o.ToFullString().Trim()).ToList();
        var sortedFormatted = formatted.OrderBy(o => o.ToFullString().Trim()).ToList();

        for (var x = 0; x < original.Count; x++)
        {
            var result = Compare((sortedOriginal[x], originalParent), (sortedFormatted[x], formattedParent));

            if (result.IsInvalid)
                return result;
        }

        return default;
    }
}

internal struct CompareResult
{
    public bool IsInvalid;
    public TextSpan? OriginalSpan;
    public TextSpan? NewSpan;
}