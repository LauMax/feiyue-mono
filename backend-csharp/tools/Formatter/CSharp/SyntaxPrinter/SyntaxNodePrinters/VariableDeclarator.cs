using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class VariableDeclarator
{
    public static Doc Print(VariableDeclaratorSyntax node, PrintingContext context)
    {
        DocListBuilder docs = new(3);
        docs.Add(Token.Print(node.Identifier, context));

        if (node.ArgumentList is not null)
            docs.Add(BracketedArgumentList.Print(node.ArgumentList, context));

        if (node.Initializer is not null)
            docs.Add(EqualsValueClause.Print(node.Initializer, context));

        return Doc.Concat(ref docs);
    }
}