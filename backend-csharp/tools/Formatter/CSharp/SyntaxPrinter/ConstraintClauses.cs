using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

internal static class ConstraintClauses
{
    public static Doc Print(SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, PrintingContext context)
    {
        if (constraintClauses.Count == 0)
            return Doc.Null;

        var body = Doc.Join(Doc.HardLine, constraintClauses.Select(o => TypeParameterConstraintClause.Print(o, context)));

        return Doc.Group(Doc.Indent(Doc.HardLine, body));
    }
}