using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

internal static class Token
{
    public static Doc PrintWithoutLeadingTrivia(SyntaxToken syntaxToken, PrintingContext context) => PrintSyntaxToken(syntaxToken, context, skipLeadingTrivia: true);

    public static Doc PrintWithoutTrailingTrivia(SyntaxToken syntaxToken, PrintingContext context) => PrintSyntaxToken(syntaxToken, context, skipTrailingTrivia: true);

    public static Doc Print(SyntaxToken syntaxToken, PrintingContext context) => PrintSyntaxToken(syntaxToken, context);

    public static Doc PrintWithSuffix(SyntaxToken syntaxToken, Doc suffixDoc, PrintingContext context, bool skipLeadingTrivia = false) =>
        PrintSyntaxToken(syntaxToken, context, suffixDoc, skipLeadingTrivia);

    internal static readonly string[] LineSeparators = ["\r\n", "\r", "\n"];

    private static Doc PrintSyntaxToken(SyntaxToken syntaxToken, PrintingContext context, Doc? suffixDoc = null, bool skipLeadingTrivia = false, bool skipTrailingTrivia = false)
    {
        if (syntaxToken.RawSyntaxKind() == SyntaxKind.None)
            return Doc.Null;

        DocListBuilder docs = new(8);

        if (!skipLeadingTrivia)
        {
            var leadingTrivia = PrintLeadingTrivia(syntaxToken, context);
            if (leadingTrivia != Doc.Null)
                docs.Add(leadingTrivia);
        }

        if (
            (syntaxToken.RawSyntaxKind() == SyntaxKind.StringLiteralToken && syntaxToken.Text.StartsWith('@'))
            || (
                syntaxToken.RawSyntaxKind() == SyntaxKind.InterpolatedStringTextToken
                && syntaxToken.Parent!.Parent is InterpolatedStringExpressionSyntax { StringStartToken.RawKind: (int)SyntaxKind.InterpolatedVerbatimStringStartToken }))
        {
            var lines = syntaxToken.Text.Replace("\r", string.Empty, StringComparison.Ordinal).Split('\n');
            docs.Add(Doc.Join(Doc.LiteralLine, lines.Select(o => new StringDoc(o))));
        }
        else if (syntaxToken.RawSyntaxKind() is SyntaxKind.MultiLineRawStringLiteralToken)
        {
            var linesIncludingQuotes = syntaxToken.Text.Split(LineSeparators, StringSplitOptions.None);
            var lastLineIsIndented = linesIncludingQuotes[^1][0] is '\t' or ' ';
            List<Doc> contents = [linesIncludingQuotes[0], lastLineIsIndented ? Doc.HardLineNoTrim : Doc.LiteralLine];

            var lines = syntaxToken.ValueText.Split(LineSeparators, StringSplitOptions.None);
            foreach (var line in lines)
            {
                contents.Add(line);
                contents.Add(
                    lastLineIsIndented
                        ? string.IsNullOrEmpty(line)
                            ? Doc.HardLine
                            : Doc.HardLineNoTrim
                        : Doc.LiteralLine);
            }

            contents.Add(linesIncludingQuotes[^1].TrimStart());

            var hasArgumentParent = syntaxToken.Parent.HasParent(typeof(ArgumentSyntax));

            docs.Add(Doc.IndentIf(!hasArgumentParent, Doc.Concat(contents)));
        }
        else if (syntaxToken.RawSyntaxKind() is SyntaxKind.InterpolatedMultiLineRawStringStartToken or SyntaxKind.InterpolatedRawStringEndToken)
        {
            docs.Add(syntaxToken.Text.Trim());
        }
        else
        {
            docs.Add(StringDoc.Create(syntaxToken));
        }

        if (!skipTrailingTrivia)
        {
            var trailingTrivia = PrintTrailingTrivia(syntaxToken);
            if (trailingTrivia != Doc.Null)
                docs.Add(trailingTrivia);
        }

        if (suffixDoc is not null)
            docs.Add(suffixDoc);

        var returnDoc = Doc.Concat(ref docs);
        docs.Dispose();

        return returnDoc;
    }

    public static Doc PrintLeadingTrivia(SyntaxToken syntaxToken, PrintingContext context)
    {
        if (context.State.SkipNextLeadingTrivia)
        {
            context.State.SkipNextLeadingTrivia = false;
            return Doc.Null;
        }

        var isClosingBrace =
            syntaxToken.RawSyntaxKind() == SyntaxKind.CloseBraceToken
            || (syntaxToken.Parent is CollectionExpressionSyntax && syntaxToken.RawSyntaxKind() == SyntaxKind.CloseBracketToken);

        var printedTrivia = PrivatePrintLeadingTrivia(syntaxToken.LeadingTrivia, context, skipLastHardline: isClosingBrace);

        var leadingTrivia = syntaxToken.LeadingTrivia;
        var hasDirective = leadingTrivia.Any(o => o.IsDirective);

        if (hasDirective)
        {
            // the leading trivia "always fits" for purposes of deciding when to break Lines, so the method call after a false #if directive doesn't break when it actually fits
            /*
            #if CONDITION
                        if (true)
                        {
                            return;
                        }
            #endif
            SomeObject.CallMethod().CallOtherMethod(shouldNotBreak);
            */
            printedTrivia = Doc.AlwaysFits(printedTrivia);
        }

        if (syntaxToken.RawSyntaxKind() != SyntaxKind.CloseBraceToken)
            return printedTrivia;

        Doc extraNewLines = Doc.Null;

        if (hasDirective || leadingTrivia.Any(o => o.IsComment()))
            extraNewLines = ExtraNewLines.Print(syntaxToken.LeadingTrivia);

        return printedTrivia != Doc.Null || extraNewLines != Doc.Null
            ? Doc.Concat(extraNewLines, Doc.IndentIf(printedTrivia != Doc.Null, printedTrivia), Doc.HardLine)
            : printedTrivia;
    }

    public static Doc PrintLeadingTrivia(SyntaxTriviaList leadingTrivia, PrintingContext context) => PrivatePrintLeadingTrivia(leadingTrivia, context);

    public static Doc PrintLeadingTriviaWithNewLines(SyntaxTriviaList leadingTrivia, PrintingContext context) =>
        PrivatePrintLeadingTrivia(leadingTrivia, context, includeInitialNewLines: true);

    private static Doc PrivatePrintLeadingTrivia(SyntaxTriviaList leadingTrivia, PrintingContext context, bool includeInitialNewLines = false, bool skipLastHardline = false)
    {
        if (leadingTrivia.Count == 0)
            return Doc.Null;

        List<Doc> docs = [];

        // we don't print any new lines until we run into a comment or directive
        // the PrintExtraNewLines method takes care of printing the initial new lines for a given node
        var printNewLines = includeInitialNewLines;

        for (var x = 0; x < leadingTrivia.Count; x++)
        {
            var trivia = leadingTrivia[x];
            var kind = trivia.RawSyntaxKind();

            if (printNewLines && kind == SyntaxKind.EndOfLineTrivia)
            {
                if (docs.Count > 0 && docs[^1] == Doc.HardLineSkipBreakIfFirstInGroup)
                    printNewLines = false;

                if (!(docs.Count > 1 && docs[^1] == Doc.HardLineSkipBreakIfFirstInGroup && docs[^2] is LeadingComment { Type: CommentType.SingleLine }))
                    docs.Add(Doc.HardLineSkipBreakIfFirstInGroup);
            }
            if (kind is not (SyntaxKind.EndOfLineTrivia or SyntaxKind.WhitespaceTrivia))
                printNewLines = true;

            void AddLeadingComment(CommentType commentType, bool isDocComment = false)
            {
                string comment;
                if (isDocComment && kind == SyntaxKind.SingleLineDocumentationCommentTrivia)
                    comment = DocumentationCommentFormatter.Format(trivia);
                else
                    comment = trivia.ToFullString().TrimEnd('\n', '\r');

                if (commentType == CommentType.MultiLine && x > 0 && leadingTrivia[x - 1].RawSyntaxKind() is SyntaxKind.WhitespaceTrivia)
                    comment = leadingTrivia[x - 1] + comment;

                docs.Add(Doc.LeadingComment(comment, commentType));
            }

            if (IsSingleLineComment(kind))
            {
                AddLeadingComment(CommentType.SingleLine, isDocComment: kind == SyntaxKind.SingleLineDocumentationCommentTrivia);
                docs.Add(kind == SyntaxKind.SingleLineDocumentationCommentTrivia ? Doc.HardLineSkipBreakIfFirstInGroup : Doc.Null);
            }
            else if (IsMultiLineComment(kind))
            {
                AddLeadingComment(CommentType.MultiLine);
            }
            else if (kind == SyntaxKind.DisabledTextTrivia)
            {
                docs.Add(Doc.Trim, trivia.ToString());
            }
            else if (IsRegion(kind))
            {
                var triviaText = trivia.ToString();
                docs.Add(Doc.HardLineIfNoPreviousLine);
                docs.Add(Doc.Trim);
                docs.Add(kind == SyntaxKind.RegionDirectiveTrivia ? Doc.BeginRegion(triviaText) : Doc.EndRegion(triviaText));
                docs.Add(Doc.HardLine);
            }
            else if (trivia.IsDirective)
            {
                var triviaText = trivia.ToString();

                docs.Add(
                    Doc.HardLineIfNoPreviousLineSkipBreakIfFirstInGroup,
                    Doc.HardLineIfNoPreviousLineSkipBreakIfFirstInGroup,
                    Doc.Trim,
                    Doc.Directive(triviaText),
                    Doc.HardLineSkipBreakIfFirstInGroup);

                // keep one line after an #endif if there is at least one
                if (kind is SyntaxKind.EndIfDirectiveTrivia)
                {
                    if (x + 1 < leadingTrivia.Count && leadingTrivia[x + 1].RawSyntaxKind() is SyntaxKind.EndOfLineTrivia)
                    {
                        x++;
                        docs.Add(Doc.HardLineSkipBreakIfFirstInGroup);
                    }
                    printNewLines = false;
                }
            }
        }

        while (skipLastHardline && docs.Count != 0 && docs[^1] is HardLine or NullDoc)
            docs.RemoveAt(docs.Count - 1);

        if (context.State.NextTriviaNeedsLine)
        {
            if (leadingTrivia.Any(o => o.RawSyntaxKind() is SyntaxKind.IfDirectiveTrivia))
            {
                docs.Insert(0, Doc.HardLineSkipBreakIfFirstInGroup);
            }
            else
            {
                var index = docs.Count - 1;
                while (index >= 0 && (docs[index] is HardLine or LeadingComment || docs[index] == Doc.Null))
                    index--;

                // this handles an edge case where we get here but already added the line
                // it relates to the fact that single line comments include new line directives
                if (index + 2 >= docs.Count || !(docs[index + 1] is HardLine && docs[index + 2] is HardLine))
                    docs.Insert(index + 1, Doc.HardLineSkipBreakIfFirstInGroup);
            }
            context.State.NextTriviaNeedsLine = false;
        }

        return docs.Count > 0 ? Doc.Concat(docs) : Doc.Null;
    }

    private static bool IsSingleLineComment(SyntaxKind kind) => kind is SyntaxKind.SingleLineDocumentationCommentTrivia or SyntaxKind.SingleLineCommentTrivia;

    private static bool IsMultiLineComment(SyntaxKind kind) => kind is SyntaxKind.MultiLineCommentTrivia or SyntaxKind.MultiLineDocumentationCommentTrivia;

    private static bool IsRegion(SyntaxKind kind) => kind is SyntaxKind.RegionDirectiveTrivia or SyntaxKind.EndRegionDirectiveTrivia;

    public static Doc PrintTrailingTrivia(SyntaxToken node) => PrintTrailingTrivia(node.TrailingTrivia);

    private static Doc PrintTrailingTrivia(in SyntaxTriviaList trailingTrivia)
    {
        if (trailingTrivia.Count == 0)
            return Doc.Null;

        DocListBuilder docs = new(8);
        foreach (var trivia in trailingTrivia)
        {
            if (trivia.RawSyntaxKind() == SyntaxKind.SingleLineCommentTrivia)
                docs.Add(Doc.TrailingComment(trivia.ToString(), CommentType.SingleLine));
            else if (trivia.RawSyntaxKind() == SyntaxKind.MultiLineCommentTrivia)
                docs.Add(" ", Doc.TrailingComment(trivia.ToString(), CommentType.MultiLine));
        }

        return Doc.Concat(ref docs);
    }

    public static bool HasComments(SyntaxToken syntaxToken) =>
        syntaxToken.LeadingTrivia.Any(o => o.RawSyntaxKind() is not (SyntaxKind.WhitespaceTrivia or SyntaxKind.EndOfLineTrivia))
        || syntaxToken.TrailingTrivia.Any(o => o.RawSyntaxKind() is not (SyntaxKind.WhitespaceTrivia or SyntaxKind.EndOfLineTrivia));

    public static bool HasLeadingCommentMatching(SyntaxNode node, Regex regex)
    {
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var o in node.GetLeadingTrivia())
        {
            if (o.RawSyntaxKind() is SyntaxKind.SingleLineCommentTrivia && regex.IsMatch(o.ToString()))
                return true;
        }

        return false;
    }

    public static bool HasLeadingCommentMatching(SyntaxToken token, Regex regex) =>
        token.LeadingTrivia.Any(o => o.RawSyntaxKind() is SyntaxKind.SingleLineCommentTrivia && regex.IsMatch(o.ToString()));
}