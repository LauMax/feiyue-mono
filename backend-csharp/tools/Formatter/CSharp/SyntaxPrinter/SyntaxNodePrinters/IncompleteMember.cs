using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class IncompleteMember
{
    public static Doc Print(IncompleteMemberSyntax _, PrintingContext _1) => string.Empty;
}