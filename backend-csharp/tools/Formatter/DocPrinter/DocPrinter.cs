using System.Text;
using Feiyue.Formatter.DocTypes;
using Feiyue.Formatter.Utilities;

namespace Feiyue.Formatter.DocPrinter;

internal sealed class DocPrinter
{
    private readonly Stack<PrintCommand> remainingCommands = [];
    private readonly Dictionary<string, PrintMode> groupModeMap = [];
    private readonly StringBuilder output = new();

    private readonly string endOfLine;
    private readonly PrinterOptions printerOptions;
    private readonly Indenter indenter;
    private readonly Stack<Indent> regionIndents = [];

    // Reusable collection types for use in DocFitter
    private readonly Stack<PrintCommand> docFitterNewCommands = [];
    private readonly StringBuilder docFitterOutput = new();

    private bool shouldRemeasure;
    private bool newLineNextStringValue;
    private bool skipNextNewLine;
    private int currentWidth;

    private DocPrinter(Doc doc, PrinterOptions printerOptions, string endOfLine)
    {
        this.endOfLine = endOfLine;
        this.printerOptions = printerOptions;
        indenter = new(printerOptions);
        remainingCommands.Push(new PrintCommand(Indenter.GenerateRoot(), PrintMode.Break, doc));
    }

    public static string Print(Doc document, PrinterOptions printerOptions, string endOfLine)
    {
        PropagateBreaks.RunOn(document);

        return new DocPrinter(document, printerOptions, endOfLine).Print();
    }

    public string Print()
    {
        while (remainingCommands.Count > 0)
            ProcessNextCommand();

        EnsureOutputEndsWithSingleNewLine();

        if (printerOptions.TrimInitialLines)
            output.TrimStart('\n', '\r');

        var result = output.ToString();

        return result;
    }

    private void EnsureOutputEndsWithSingleNewLine()
    {
        var trimmed = 0;
        for (; trimmed < output.Length; trimmed++)
        {
            if (output[^(trimmed + 1)] is not '\r' and not '\n')
                break;
        }

        output.Length -= trimmed;
    }

    private void ProcessNextCommand()
    {
        var (indent, mode, doc) = remainingCommands.Pop();
        if (doc == Doc.Null)
            return;

        if (doc is StringDoc stringDoc)
        {
            ProcessString(stringDoc, indent);
        }
        else if (doc is Concat concat)
        {
            for (var x = concat.Contents.Count - 1; x >= 0; x--)
                Push(concat.Contents[x], mode, indent);
        }
        else if (doc is IndentDoc indentDoc)
        {
            Push(indentDoc.Contents, mode, indenter.IncreaseIndent(indent));
        }
        else if (doc is Trim)
        {
            currentWidth -= output.TrimTrailingWhitespace();
            newLineNextStringValue = false;
        }
        else if (doc is Group group)
        {
            ProcessGroup(group, mode, indent);
        }
        else if (doc is IfBreak ifBreak)
        {
            var groupMode = mode;
            if (ifBreak.GroupId is not null && !groupModeMap.TryGetValue(ifBreak.GroupId, out groupMode))
                throw new("You cannot use an ifBreak before the group it targets.");

            var contents = groupMode == PrintMode.Break ? ifBreak.BreakContents : ifBreak.FlatContents;
            Push(contents, mode, indent);
        }
        else if (doc is LineDoc line)
        {
            ProcessLine(line, mode, indent);
        }
        else if (doc is BreakParent) { }
        else if (doc is LeadingComment leadingComment)
        {
            output.TrimTrailingWhitespace();
            if ((output.Length != 0 && output[^1] != '\n') || newLineNextStringValue)
                output.Append(endOfLine);

            AppendComment(leadingComment, indent);

            currentWidth = indent.Length;
            newLineNextStringValue = false;
            skipNextNewLine = false;
        }
        else if (doc is TrailingComment trailingComment)
        {
            output.TrimTrailingWhitespace();
            output.Append(' ').Append(trailingComment.Comment);
            currentWidth = indent.Length;
            if (mode != PrintMode.ForceFlat)
            {
                newLineNextStringValue = true;
                skipNextNewLine = true;
            }
        }
        else if (doc is ForceFlat forceFlat)
        {
            Push(forceFlat.Contents, PrintMode.ForceFlat, indent);
        }
        else if (doc is Region region)
        {
            if (region.IsEnd)
            {
                // in the case where regions are combined with ignored ranges, the start region
                // ends up printing inside the unformatted nodes, so we don't have a matching
                // start region to go with this end region
                if (regionIndents.TryPop(out var regionIndent))
                    output.Append(regionIndent.Value);
                else
                    output.Append(indent.Value);
            }
            else
            {
                output.Append(indent.Value);
                regionIndents.Push(indent);
            }

            output.Append(region.Text);
        }
        else if (doc is AlwaysFits temp)
        {
            Push(temp.Contents, mode, indent);
        }
        else
        {
            throw new("didn't handle " + doc);
        }
    }

    private void AppendComment(LeadingComment leadingComment, Indent indent)
    {
        int CalculateIndentLength(string line) => line.CalculateCurrentLeadingIndentation(printerOptions.IndentSize);

        using StringReader stringReader = new(leadingComment.Comment);
        var line = stringReader.ReadLine();
        var numberOfSpacesToAddOrRemove = 0;
        if (leadingComment.Type == CommentType.MultiLine && line is not null)
        {
            // in order to maintain the formatting inside of a multiline comment
            // we calculate how much the indentation of the first line is changing
            // and then change the indentation of all other lines the same amount
            var firstLineIndentLength = CalculateIndentLength(line);
            var currentIndent = CalculateIndentLength(indent.Value);
            numberOfSpacesToAddOrRemove = currentIndent - firstLineIndentLength;
        }

        while (line is not null)
        {
            if (leadingComment.Type == CommentType.SingleLine)
            {
                output.Append(indent.Value);
            }
            else
            {
                var spacesToAppend = CalculateIndentLength(line) + numberOfSpacesToAddOrRemove;
                if (printerOptions.UseTabs)
                {
                    var indentLength = CalculateIndentLength(indent.Value);
                    if (spacesToAppend >= indentLength)
                    {
                        output.Append(indent.Value);
                        spacesToAppend -= indentLength;
                    }

                    while (spacesToAppend > 0 && spacesToAppend >= printerOptions.IndentSize)
                    {
                        output.Append('\t');
                        spacesToAppend -= printerOptions.IndentSize;
                    }
                }
                if (spacesToAppend > 0)
                    output.Append(' ', spacesToAppend);
            }

            output.Append(line.Trim());
            line = stringReader.ReadLine();
            if (line is null)
                return;

            output.Append(endOfLine);
        }
    }

    private void ProcessString(StringDoc stringDoc, Indent indent)
    {
        if (string.IsNullOrEmpty(stringDoc.Value))
            return;

        // this ensures we don't print extra spaces after a trailing comment
        // newLineNextStringValue & skipNextNewLine are set to true when we print a trailing comment
        // when they are set we new line the next string we find. If we new line and then print a " " we end up with an extra space
        if (newLineNextStringValue && skipNextNewLine && stringDoc.Value == " ")
            return;

        if (newLineNextStringValue)
        {
            output.TrimTrailingWhitespace();
            output.Append(endOfLine).Append(indent.Value);
            currentWidth = indent.Length;
            newLineNextStringValue = false;
        }

        output.Append(stringDoc.Value);
        currentWidth += stringDoc.Value.GetPrintedWidth();
    }

    private void ProcessLine(LineDoc line, PrintMode mode, Indent indent)
    {
        if (mode is PrintMode.Flat or PrintMode.ForceFlat)
        {
            if (line.Type == LineDoc.LineType.Soft)
                return;

            if (line.Type == LineDoc.LineType.Normal)
            {
                output.Append(' ');
                currentWidth++;
                return;
            }

            // This line was forced into the output even if we were in flattened mode, so we need to tell the next
            // group that no matter what, it needs to remeasure because the previous measurement didn't accurately
            // capture the entire expression (this is necessary for nested groups)
            shouldRemeasure = true;
        }

        if (line.Squash && output.Length > 0 && output.EndsWithNewLineAndWhitespace())
            return;

        if (line.IsLiteral)
        {
            if (output.Length > 0)
            {
                output.Append(endOfLine);
                currentWidth = 0;
            }
        }
        else
        {
            if (!skipNextNewLine || !newLineNextStringValue)
            {
                if (line is not HardLineNoTrim)
                    output.TrimTrailingWhitespace();

                output.Append(endOfLine).Append(indent.Value);
                currentWidth = indent.Length;
            }

            if (skipNextNewLine)
                skipNextNewLine = false;
        }
    }

    private void ProcessGroup(Group group, PrintMode mode, Indent indent)
    {
        if (mode is PrintMode.Flat or PrintMode.ForceFlat && !shouldRemeasure)
        {
            Push(group.Contents, group.Break ? PrintMode.Break : mode, indent);
        }
        else
        {
            shouldRemeasure = false;
            PrintCommand possibleCommand = new(indent, PrintMode.Flat, group.Contents);

            if (!group.Break && Fits(possibleCommand))
            {
                remainingCommands.Push(possibleCommand);
            }
            else if (group is ConditionalGroup conditionalGroup)
            {
                if (group.Break)
                {
                    Push(conditionalGroup.Options[^1], PrintMode.Break, indent);
                }
                else
                {
                    var foundSomethingThatFits = false;
                    foreach (var option in conditionalGroup.Options.Skip(1))
                    {
                        possibleCommand = new(indent, mode, option);
                        if (!Fits(possibleCommand))
                            continue;

                        remainingCommands.Push(possibleCommand);
                        foundSomethingThatFits = true;
                        break;
                    }

                    if (!foundSomethingThatFits)
                        remainingCommands.Push(possibleCommand);
                }
            }
            else
            {
                Push(group.Contents, PrintMode.Break, indent);
            }
        }

        if (group.GroupId is not null)
            groupModeMap[group.GroupId] = remainingCommands.Peek().Mode;
    }

    private bool Fits(PrintCommand possibleCommand) =>
        DocFitter.Fits(possibleCommand, remainingCommands, printerOptions.Width - currentWidth, groupModeMap, indenter, docFitterNewCommands, docFitterOutput);

    private void Push(Doc doc, PrintMode printMode, Indent indent) => remainingCommands.Push(new PrintCommand(indent, printMode, doc));
}

internal record struct PrintCommand(Indent Indent, PrintMode Mode, Doc Doc);

internal enum PrintMode
{
    Flat,
    Break,
    ForceFlat
}