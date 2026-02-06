using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

/// <summary>Formats XML documentation comments per RCS1253.</summary>
internal static partial class DocumentationCommentFormatter
{
    /// <summary>Formats a documentation comment trivia, applying RCS1253 (single-line summary format).</summary>
    public static string Format(SyntaxTrivia trivia)
    {
        if (trivia.RawSyntaxKind() != SyntaxKind.SingleLineDocumentationCommentTrivia)
            return trivia.ToFullString().TrimEnd('\n', '\r');

        var structure = trivia.GetStructure();
        if (structure is not DocumentationCommentTriviaSyntax docComment)
            return trivia.ToFullString().TrimEnd('\n', '\r');

        // Check if this is a simple summary that can be collapsed to single line
        var result = TryFormatSingleLineSummary(docComment);
        return result ?? trivia.ToFullString().TrimEnd('\n', '\r');
    }

    private static string? TryFormatSingleLineSummary(DocumentationCommentTriviaSyntax docComment)
    {
        // Look for a single <summary> element with simple text content
        var elements = docComment.Content.OfType<XmlElementSyntax>().ToList();

        // If there's only one element and it's a summary with simple text, we can collapse it
        if (elements.Count != 1)
            return null;

        var element = elements[0];
        if (element.StartTag.Name.ToString() != "summary")
            return null;

        // Check if content is simple text (no nested elements, no complex formatting)
        var textContent = ExtractSimpleTextContent(element);
        if (textContent == null)
            return null;

        // Don't collapse if the resulting line would be too long (considering "/// <summary></summary>" overhead)
        // RCS1253 expects single-line format only for short summaries
        if (textContent.Length > 80)
            return null;

        // Format as single-line: /// <summary>Text content.</summary>
        return $"/// <summary>{textContent}</summary>";
    }

    private static string? ExtractSimpleTextContent(XmlElementSyntax element)
    {
        var sb = new StringBuilder();

        foreach (var content in element.Content)
        {
            switch (content)
            {
                case XmlTextSyntax text:
                    foreach (var token in text.TextTokens)
                    {
                        var tokenText = token.Text;
                        // Skip the /// prefix and leading/trailing whitespace
                        if (token.RawSyntaxKind() == SyntaxKind.XmlTextLiteralToken)
                            sb.Append(tokenText);
                    }
                    break;

                case XmlEmptyElementSyntax emptyElement:
                    // Handle <see cref="..."/> and similar inline elements
                    sb.Append(emptyElement.ToFullString());
                    break;

                case XmlElementSyntax nestedElement:
                    // Handle inline elements like <c>code</c>, <paramref name="..."/>, etc.
                    var name = nestedElement.StartTag.Name.ToString();
                    if (name is "c" or "code" or "paramref" or "typeparamref" or "see" or "seealso")
                        sb.Append(nestedElement.ToFullString());
                    else
                        return null; // Complex nested content, don't collapse

                    break;

                default:
                    return null; // Unknown content type, don't collapse
            }
        }

        // Clean up the text: normalize whitespace, remove /// prefixes
        var result = CleanupSummaryText(sb.ToString());

        // If the cleaned result is too long, don't collapse
        if (result.Length > 100)
            return null;

        return result;
    }

    private static string CleanupSummaryText(string text)
    {
        // Remove XML doc comment prefixes (///)
        text = DocCommentPrefixRegex().Replace(text, " ");

        // Normalize whitespace
        text = WhitespaceRegex().Replace(text, " ");

        return text.Trim();
    }

    [GeneratedRegex(@"^\s*///\s*", RegexOptions.Multiline)]
    private static partial Regex DocCommentPrefixRegex();

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();
}