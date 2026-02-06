using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

internal static class SeparatedSyntaxList
{
    public static Doc Print<T>(SeparatedSyntaxList<T> list, Func<T, PrintingContext, Doc> printFunc, Doc afterSeparator, PrintingContext context, int startingIndex = 0)
        where T : SyntaxNode => PrintCore(list, printFunc, afterSeparator, context, startingIndex);

    public static Doc PrintWithTrailingComma<T>(
        SeparatedSyntaxList<T> list,
        Func<T, PrintingContext, Doc> printFunc,
        Doc afterSeparator,
        PrintingContext context,
        SyntaxToken? _ = null)
        where T : SyntaxNode => PrintCore(list, printFunc, afterSeparator, context, 0);

    [SkipLocalsInit]
    private static Doc PrintCore<T>(in SeparatedSyntaxList<T> list, Func<T, PrintingContext, Doc> printFunc, Doc afterSeparator, PrintingContext context, int startingIndex)
        where T : SyntaxNode
    {
        var docs = list.Count <= 3 ? new DocListBuilder(8) : new DocListBuilder(list.Count * 3);
        StringBuilder unFormattedCode = new();
        var printUnformatted = false;
        for (var x = startingIndex; x < list.Count; x++)
        {
            var member = list[x];

            if (Token.HasLeadingCommentMatching(member, CSharpierIgnore.IgnoreEndRegex))
            {
                docs.Add(unFormattedCode.ToString().Trim());
                unFormattedCode.Clear();
                printUnformatted = false;
            }
            else if (Token.HasLeadingCommentMatching(member, CSharpierIgnore.IgnoreStartRegex))
            {
                if (!printUnformatted && x > 0)
                    docs.Add(Doc.HardLine);

                printUnformatted = true;
            }

            if (printUnformatted)
            {
                unFormattedCode.Append(CSharpierIgnore.PrintWithoutFormatting(member, context));
                if (x < list.SeparatorCount)
                    unFormattedCode.Append(list.GetSeparator(x).ToFullString().Trim()).Append(Environment.NewLine);

                continue;
            }

            docs.Add(printFunc(list[x], context));

            if (x < list.SeparatorCount)
            {
                docs.Add(Token.Print(list.GetSeparator(x), context));
                if (x < list.Count - 1)
                    docs.Add(afterSeparator);
            }
        }

        if (unFormattedCode.Length > 0)
            docs.Add(unFormattedCode.ToString().Trim());

        var output = Doc.Concat(ref docs);
        docs.Dispose();

        return output;
    }
}