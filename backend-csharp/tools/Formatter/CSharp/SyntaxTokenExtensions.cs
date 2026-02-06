using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Feiyue.Formatter.CSharp;

internal static class SyntaxTokenExtensions
{
    public static SyntaxKind RawSyntaxKind(this SyntaxToken token) => (SyntaxKind)token.RawKind;
}