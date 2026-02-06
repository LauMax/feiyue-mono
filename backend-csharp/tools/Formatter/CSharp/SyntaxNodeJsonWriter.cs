using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Feiyue.Formatter.CSharp;

internal static partial class SyntaxNodeJsonWriter
{
    public static void WriteSyntaxToken(StringBuilder builder, SyntaxToken syntaxNode)
    {
        builder.Append('{');
        List<string?> properties =
        [
            $"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"",
            $"\"kind\":\"{syntaxNode.Kind()}\"",
            WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia),
            WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia),
            WriteBoolean("isMissing", syntaxNode.IsMissing)
        ];
        List<string> leadingTrivia = [];
        foreach (var node in syntaxNode.LeadingTrivia)
        {
            StringBuilder innerBuilder = new();
            WriteSyntaxTrivia(innerBuilder, node);
            leadingTrivia.Add(innerBuilder.ToString());
        }
        properties.Add($"\"leadingTrivia\":[{string.Join(",", leadingTrivia)}]");
        properties.Add(WriteString("text", syntaxNode.Text));
        List<string> trailingTrivia = [];
        foreach (var node in syntaxNode.TrailingTrivia)
        {
            StringBuilder innerBuilder = new();
            WriteSyntaxTrivia(innerBuilder, node);
            trailingTrivia.Add(innerBuilder.ToString());
        }
        properties.Add($"\"trailingTrivia\":[{string.Join(",", trailingTrivia)}]");
        builder.AppendJoin(",", properties.Where(o => o is not null)).Append('}');
    }

    public static void WriteSyntaxTrivia(StringBuilder builder, SyntaxTrivia syntaxNode)
    {
        builder.Append('{');
        List<string?> properties =
        [
            $"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"",
            $"\"kind\":\"{syntaxNode.Kind()}\"",
            WriteString("text", syntaxNode.ToString()),
            WriteBoolean("hasStructure", syntaxNode.HasStructure),
            WriteBoolean("isDirective", syntaxNode.IsDirective)
        ];
        builder.AppendJoin(",", properties.Where(o => o is not null)).Append('}');
    }

    private static string? WriteBoolean(string name, bool value) => value ? $"\"{name}\":true" : null;

    private static string? WriteString(string name, string value) => !string.IsNullOrEmpty(value) ? $"\"{name}\":\"{JsonEncodedText.Encode(value)}\"" : null;

    private static string? WriteInt(string name, int value) => value != 0 ? $"\"{name}\":{value}" : null;

    private static string GetNodeType(Type type)
    {
        var name = type.Name;
        return name.EndsWith("Syntax", StringComparison.Ordinal) ? name[..^"Syntax".Length] : name;
    }
}