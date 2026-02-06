using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Feiyue.Formatter.CSharp;

internal sealed class PreprocessorSymbols : CSharpSyntaxWalker
{
    private readonly List<string[]> symbolSets = [];
    private readonly HashSet<string> squashedSymbolSets = [];
    private SymbolContext currentContext = new() { ParentContext = new() { ParentContext = null! } };

    private PreprocessorSymbols()
        : base(SyntaxWalkerDepth.Trivia) { }

    public static List<string[]> GetSets(string code) => GetSets(CSharpSyntaxTree.ParseText(code));

    public static List<string[]> GetSets(SyntaxTree syntaxTree) => new PreprocessorSymbols().GetSymbolSets(syntaxTree);

    private List<string[]> GetSymbolSets(SyntaxTree syntaxTree)
    {
        Visit(syntaxTree.GetRoot());

        return symbolSets;
    }

    public override void VisitLeadingTrivia(SyntaxToken token)
    {
        if (!token.HasLeadingTrivia)
            return;

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var syntaxTrivia in token.LeadingTrivia)
        {
            if (syntaxTrivia.RawSyntaxKind() is SyntaxKind.IfDirectiveTrivia or SyntaxKind.ElifDirectiveTrivia or SyntaxKind.ElseDirectiveTrivia or SyntaxKind.EndIfDirectiveTrivia)
                Visit((CSharpSyntaxNode)syntaxTrivia.GetStructure()!);
        }
    }

    public override void VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
    {
        // TODO in this or the elif, if node.Condition is IdentifierNameSyntax, we know the symbol already
        // and don't need to parse it, but there isn't a good way to deal with that
        currentContext = new() { ParentContext = currentContext };

        ParseExpression(node.Condition.ToFullString());
    }

    public override void VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node) => ParseExpression(node.Condition.ToFullString());

    public override void VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
    {
        var allParameters = currentContext.BooleanExpressions.SelectMany(o => o.Parameters).Distinct().ToList();
        var combinations = GenerateCombinations(allParameters);
        var functions = currentContext.BooleanExpressions.ConvertAll(o => o.Function);

        var combination = combinations.FirstOrDefault(combination => functions.All(o => !o(combination)));

        if (combination is null)
            return;

        // TODO it would be more efficient to not add a new boolean expression, because we know which
        // symbols we need
        currentContext.BooleanExpressions.Add(
            new BooleanExpression { Parameters = combination.Where(o => o.Value).Select(o => o.Key).ToList(), Function = o => o.All(p => p.Value) });
    }

    public override void VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
    {
        var parentSymbols = Array.Empty<string>();
        if (currentContext.ParentContext.BooleanExpressions.Count != 0)
            parentSymbols = GetSymbols(currentContext.ParentContext.BooleanExpressions[^1]);

        foreach (var booleanExpression in currentContext.BooleanExpressions)
        {
            var symbolSet = GetSymbols(booleanExpression).Concat(parentSymbols).Distinct().ToArray();

            if (symbolSet.Length == 0)
                continue;

            var squashedSymbolSet = string.Join(",", symbolSet);
            if (!squashedSymbolSets.Contains(squashedSymbolSet))
            {
                symbolSets.Add(symbolSet);
                squashedSymbolSets.Add(squashedSymbolSet);
            }
        }

        currentContext = currentContext.ParentContext;
    }

    private static string[] GetSymbols(BooleanExpression booleanExpression)
    {
        // TODO some type of caching on finding the symbols from the expression would speed things up
        // maybe we can solve these when constructing them, so they are all stored in the same spot
        // but the else works a bit different
        var combinations = GenerateCombinations(booleanExpression.Parameters);

        var possibleParameters = combinations.FirstOrDefault(possibleParameters => booleanExpression.Function(possibleParameters));

        return possibleParameters is null ? [] : possibleParameters.Where(o => o.Value).Select(o => o.Key).OrderBy(o => o, StringComparer.Ordinal).ToArray();
    }

    private void ParseExpression(string expression)
    {
        var indexOf = expression.IndexOf('/', StringComparison.Ordinal);
        if (indexOf > 0)
            expression = expression[..indexOf];

        var booleanExpression = BooleanExpressionParser.Parse(expression);
        currentContext.BooleanExpressions.Add(booleanExpression);
    }

    private static List<Dictionary<string, bool>> GenerateCombinations(List<string> parameterNames)
    {
        if (parameterNames.Count == 0)
        {
            return

                [
                    []
                ];
        }

        var subCombinations = GenerateCombinations(parameterNames.Skip(1).ToList());
        List<Dictionary<string, bool>> combinations = [];

        foreach (var subCombination in subCombinations)
        {
            Dictionary<string, bool> falseCombination = new(subCombination) { { parameterNames[0], false } };
            Dictionary<string, bool> trueCombination = new(subCombination) { { parameterNames[0], true } };

            combinations.Add(falseCombination);
            combinations.Add(trueCombination);
        }

        return combinations;
    }

    private sealed class SymbolContext
    {
        public required SymbolContext ParentContext { get; init; }
        public List<BooleanExpression> BooleanExpressions { get; } = [];
    }
}