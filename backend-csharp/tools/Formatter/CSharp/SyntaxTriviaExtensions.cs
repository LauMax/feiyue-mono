using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Feiyue.Formatter.CSharp;

internal static class SyntaxTriviaExtensions
{
    public static bool IsComment(this SyntaxTrivia syntaxTrivia) => syntaxTrivia.RawSyntaxKind().IsComment();

    public static bool IsComment(this SyntaxKind syntaxKind) =>
        syntaxKind
            is SyntaxKind.SingleLineCommentTrivia
                or SyntaxKind.MultiLineCommentTrivia
                or SyntaxKind.SingleLineDocumentationCommentTrivia
                or SyntaxKind.MultiLineDocumentationCommentTrivia;

    public static SyntaxKind RawSyntaxKind(this SyntaxTrivia trivia) => (SyntaxKind)trivia.RawKind;
}