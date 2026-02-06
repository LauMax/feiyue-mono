using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

/// <summary>
/// Handles printing statements that can optionally have braces.
/// Implements RCS1001 (add braces when multiline), RCS1002 (remove braces when single-line),
/// and SA1520 (use braces consistently in if-else chains).
/// </summary>
internal static class OptionalBraces
{
    public static Doc Print(StatementSyntax node, PrintingContext context)
    {
        // If it's not a block, check if we need to ADD braces (RCS1001)
        if (node is not BlockSyntax blockSyntax)
        {
            // RCS1001: Add braces if statement spans multiple lines
            if (!IsSingleLine(node))
                return PrintWithBraces(node, context);

            // RCS1001: Add braces if the parent condition spans multiple lines
            if (ParentConditionIsMultiLine(node))
                return PrintWithBraces(node, context);

            // SA1520: Add braces if this is part of an if-else chain and any branch needs braces
            if (node.Parent is IfStatementSyntax or ElseClauseSyntax && AnyBranchInIfElseChainNeedsBracesForStatement(node))
                return PrintWithBraces(node, context);

            // No braces needed - single-line statement with single-line condition
            return DocUtilities.RemoveInitialDoubleHardLine(Doc.Indent(Doc.HardLine, Node.Print(node, context)));
        }

        // If block has more than one statement, keep the braces
        if (blockSyntax.Statements.Count != 1)
            return Block.Print(blockSyntax, context);

        var singleStatement = blockSyntax.Statements[0];

        // RCS1001: Keep braces if the single statement spans multiple lines
        if (!IsSingleLine(singleStatement))
            return Block.Print(blockSyntax, context);

        // RCS1001: Keep braces if the parent statement's condition spans multiple lines
        if (ParentConditionIsMultiLine(blockSyntax))
            return Block.Print(blockSyntax, context);

        // SA1520: Keep braces if this is part of an if-else chain and any branch needs braces
        if (blockSyntax.Parent is IfStatementSyntax or ElseClauseSyntax && AnyBranchInIfElseChainNeedsBraces(blockSyntax))
            return Block.Print(blockSyntax, context);

        // Keep braces to avoid dangling else: if with else inside an if without else
        if (singleStatement is IfStatementSyntax { Else: not null } && blockSyntax.Parent is IfStatementSyntax { Else: null })
            return Block.Print(blockSyntax, context);

        // Keep braces if there are comments on the braces, inside the block, or on the statement
        if (Token.HasComments(blockSyntax.OpenBraceToken) || Token.HasComments(blockSyntax.CloseBraceToken))
            return Block.Print(blockSyntax, context);

        // Keep braces if the statement has leading comments
        if (HasLeadingComments(singleStatement))
            return Block.Print(blockSyntax, context);

        // RCS1002: Remove braces - single statement is single-line and condition is single-line
        return DocUtilities.RemoveInitialDoubleHardLine(Doc.Indent(Doc.HardLine, Node.Print(singleStatement, context)));
    }

    /// <summary>Prints a statement wrapped in braces (adding braces that weren't there before).</summary>
    private static Doc PrintWithBraces(StatementSyntax statement, PrintingContext context) =>
        Doc.Concat(Doc.HardLine, "{", Doc.Indent(Doc.HardLine, Node.Print(statement, context)), Doc.HardLine, "}");

    /// <summary>
    /// Checks if the parent statement's condition spans multiple lines.
    /// RCS1001 requires braces when the condition is multi-line.
    /// </summary>
    private static bool ParentConditionIsMultiLine(Microsoft.CodeAnalysis.SyntaxNode node)
    {
        // Find the parent statement - could be directly the parent, or for else clauses, the grandparent
        var parent = node.Parent;
        if (parent is ElseClauseSyntax elseClause)
            parent = elseClause.Parent;

        return parent switch
        {
            IfStatementSyntax ifStmt => !IsSingleLine(ifStmt.Condition),
            WhileStatementSyntax whileStmt => !IsSingleLine(whileStmt.Condition),
            ForStatementSyntax forStmt => forStmt.Condition is not null && !IsSingleLine(forStmt.Condition),
            ForEachStatementSyntax forEachStmt => !IsSingleLine(forEachStmt.Expression),
            ForEachVariableStatementSyntax forEachVarStmt => !IsSingleLine(forEachVarStmt.Expression),
            DoStatementSyntax doStmt => !IsSingleLine(doStmt.Condition),
            LockStatementSyntax lockStmt => !IsSingleLine(lockStmt.Expression),
            UsingStatementSyntax usingStmt => usingStmt.Expression is not null && !IsSingleLine(usingStmt.Expression),
            _ => false,
        };
    }

    /// <summary>Checks if the syntax node is on a single line in the original source.</summary>
    private static bool IsSingleLine(Microsoft.CodeAnalysis.SyntaxNode node)
    {
        var lineSpan = node.GetLocation().GetLineSpan();
        return lineSpan.StartLinePosition.Line == lineSpan.EndLinePosition.Line;
    }

    /// <summary>Checks if the statement has leading comments (single-line or multi-line).</summary>
    private static bool HasLeadingComments(StatementSyntax statement)
    {
        foreach (var trivia in statement.GetLeadingTrivia())
        {
            if (
                trivia.RawSyntaxKind()
                is Microsoft.CodeAnalysis.CSharp.SyntaxKind.SingleLineCommentTrivia
                    or Microsoft.CodeAnalysis.CSharp.SyntaxKind.MultiLineCommentTrivia
                    or Microsoft.CodeAnalysis.CSharp.SyntaxKind.SingleLineDocumentationCommentTrivia
                    or Microsoft.CodeAnalysis.CSharp.SyntaxKind.MultiLineDocumentationCommentTrivia)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// SA1520: In an if-else chain, if any branch needs braces, all branches need braces.
    /// For use when the current node is NOT a block (no braces currently).
    /// </summary>
    private static bool AnyBranchInIfElseChainNeedsBracesForStatement(StatementSyntax statement)
    {
        // Find the root if statement of this chain
        var rootIf = GetRootIfStatementFromStatement(statement);
        if (rootIf is null)
            return false;

        // Check all branches in the chain
        return AnyBranchNeedsBraces(rootIf);
    }

    private static IfStatementSyntax? GetRootIfStatementFromStatement(StatementSyntax statement)
    {
        var current = statement.Parent;

        // Walk up to find the if statement this statement belongs to
        while (current is ElseClauseSyntax elseClause)
            current = elseClause.Parent;

        if (current is not IfStatementSyntax ifStmt)
            return null;

        // Walk up to find the root if (in case this is an else-if)
        while (ifStmt.Parent is ElseClauseSyntax { Parent: IfStatementSyntax parentIf })
            ifStmt = parentIf;

        return ifStmt;
    }

    /// <summary>
    /// SA1520: In an if-else chain, if any branch needs braces, all branches need braces.
    /// This walks up to find the root if statement, then checks all branches.
    /// </summary>
    private static bool AnyBranchInIfElseChainNeedsBraces(BlockSyntax block)
    {
        // Find the root if statement of this chain
        var rootIf = GetRootIfStatement(block);
        if (rootIf is null)
            return false;

        // Check all branches in the chain
        return AnyBranchNeedsBraces(rootIf);
    }

    private static IfStatementSyntax? GetRootIfStatement(BlockSyntax block)
    {
        var current = block.Parent;

        // Walk up to find the if statement this block belongs to
        while (current is ElseClauseSyntax elseClause)
            current = elseClause.Parent;

        if (current is not IfStatementSyntax ifStmt)
            return null;

        // Walk up to find the root if (in case this is an else-if)
        while (ifStmt.Parent is ElseClauseSyntax { Parent: IfStatementSyntax parentIf })
            ifStmt = parentIf;

        return ifStmt;
    }

    private static bool AnyBranchNeedsBraces(IfStatementSyntax ifStatement)
    {
        // Check if the if condition is multi-line
        if (!IsSingleLine(ifStatement.Condition))
            return true;

        // Check the 'if' branch body
        if (BranchNeedsBraces(ifStatement.Statement))
            return true;

        // Check the 'else' branch (which could be another if or a statement)
        var elseClause = ifStatement.Else;
        while (elseClause is not null)
        {
            if (elseClause.Statement is IfStatementSyntax elseIf)
            {
                // Check if the else-if condition is multi-line
                if (!IsSingleLine(elseIf.Condition))
                    return true;

                // else-if: check the if branch body
                if (BranchNeedsBraces(elseIf.Statement))
                    return true;

                elseClause = elseIf.Else;
            }
            else
            {
                // final else
                if (BranchNeedsBraces(elseClause.Statement))
                    return true;

                break;
            }
        }

        return false;
    }

    private static bool BranchNeedsBraces(StatementSyntax statement)
    {
        // If it's a block with a single statement, check if that statement is multi-line
        if (statement is BlockSyntax { Statements.Count: 1 } block)
            return !IsSingleLine(block.Statements[0]);

        // If it's not a block, check if the statement is multi-line
        return !IsSingleLine(statement);
    }
}