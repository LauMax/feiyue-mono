using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ClassOrStructConstraint
{
    public static Doc Print(ClassOrStructConstraintSyntax node, PrintingContext context) =>
        Doc.Concat(Token.Print(node.ClassOrStructKeyword, context), Token.Print(node.QuestionToken, context));
}