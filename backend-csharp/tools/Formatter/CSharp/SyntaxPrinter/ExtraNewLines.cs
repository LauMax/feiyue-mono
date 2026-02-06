using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

internal static class ExtraNewLines
{
    public static Doc Print(CSharpSyntaxNode node)
    {
        if (node.Parent is GlobalStatementSyntax)
            return Doc.Null;

        // RCS0001: Add blank line after embedded statement
        if (PreviousSiblingHasEmbeddedStatement(node))
            return Doc.HardLine;

        return Print(node.GetLeadingTrivia());
    }

    public static Doc Print(SyntaxTriviaList syntaxTriviaList)
    {
        foreach (var leadingTrivia in syntaxTriviaList)
        {
            if (leadingTrivia.RawSyntaxKind() == SyntaxKind.EndOfLineTrivia)
            {
                // ensures we only print a single new line
                return Doc.HardLine;
            }

            if (leadingTrivia.RawSyntaxKind() != SyntaxKind.WhitespaceTrivia)
                return Doc.Null;
        }

        return Doc.Null;
    }

    /// <summary>
    /// RCS0001: Checks if the previous sibling statement is a control flow statement
    /// with an embedded statement (no braces).
    /// </summary>
    private static bool PreviousSiblingHasEmbeddedStatement(SyntaxNode node)
    {
        // Only applies to statements
        if (node is not StatementSyntax)
            return false;

        // Get the previous sibling
        var parent = node.Parent;
        if (parent is null)
            return false;

        SyntaxNode? previousSibling = null;
        foreach (var child in parent.ChildNodes())
        {
            if (child == node)
                break;

            previousSibling = child;
        }

        if (previousSibling is null)
            return false;

        return HasEmbeddedStatement(previousSibling);
    }

    /// <summary>Checks if a statement has an embedded statement (body without braces).</summary>
    private static bool HasEmbeddedStatement(SyntaxNode node) =>
        node switch
        {
            IfStatementSyntax ifStmt => ifStmt.Statement is not BlockSyntax || (ifStmt.Else?.Statement is not null and not BlockSyntax and not IfStatementSyntax),
            WhileStatementSyntax whileStmt => whileStmt.Statement is not BlockSyntax,
            ForStatementSyntax forStmt => forStmt.Statement is not BlockSyntax,
            ForEachStatementSyntax foreachStmt => foreachStmt.Statement is not BlockSyntax,
            ForEachVariableStatementSyntax foreachVarStmt => foreachVarStmt.Statement is not BlockSyntax,
            DoStatementSyntax doStmt => doStmt.Statement is not BlockSyntax,
            UsingStatementSyntax usingStmt => usingStmt.Statement is not BlockSyntax,
            LockStatementSyntax lockStmt => lockStmt.Statement is not BlockSyntax,
            FixedStatementSyntax fixedStmt => fixedStmt.Statement is not BlockSyntax,
            _ => false,
        };
}