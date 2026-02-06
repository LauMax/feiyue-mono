namespace Feiyue.Formatter.CSharp;

internal sealed class BooleanExpression
{
    public required List<string> Parameters { get; init; }
    public required Func<Dictionary<string, bool>, bool> Function { get; init; }
}