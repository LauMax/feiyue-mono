using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Feiyue.Formatter;

internal sealed class XmlProjectFormatter
{
    private static readonly IReadOnlyList<string> ItemGroupOrder =
    [
        "Sdk",
        "FrameworkReference",
        "ProjectReference",
        "PackageVersion",
        "PackageReference",
        "Using",
        "None",
        "Compile",
        "Protobuf",
        "Content",
        "EmbeddedResource",
        "BicepFiles",
        "BicepParamFiles",
        "InternalsVisibleTo"
    ];

    public static string FormatContent(string content)
    {
        var preserveTrailingNewline = content.EndsWith('\n');

        var root = XElement.Parse(content, LoadOptions.PreserveWhitespace);

        SortProjectElements(root);

        StripWhitespaceNodes(root);

        var sb = new StringBuilder();
        var settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            Indent = true,
            IndentChars = "  "
        };

        using (var writer = XmlWriter.Create(sb, settings))
        {
            root.Save(writer);
        }

        var lines = sb.ToString().Split('\n').Select(line => FixSelfClosingTag(line.TrimEnd())).Where(line => !string.IsNullOrWhiteSpace(line)).ToList();

        var result = string.Join("\n", lines);

        if (preserveTrailingNewline)
            result += "\n";

        return result;
    }

    private static void StripWhitespaceNodes(XElement element)
    {
        var whitespaceNodes = element.DescendantNodes().OfType<XText>().Where(t => string.IsNullOrWhiteSpace(t.Value)).ToList();

        foreach (var node in whitespaceNodes)
            node.Remove();
    }

    private static string FixSelfClosingTag(string line)
    {
        var trimmed = line.TrimEnd();
        if (trimmed.EndsWith("/>", StringComparison.Ordinal) && !trimmed.EndsWith(" />", StringComparison.Ordinal))
            return trimmed[..^2] + " />";

        return trimmed;
    }

    private static void SortProjectElements(XElement project)
    {
        foreach (var propGroup in project.Descendants("PropertyGroup"))
            SortElementsInPlace(propGroup);

        ConsolidateItemGroups(project);
        MoveTargetsToEnd(project);

        foreach (var tagName in new[] { "PropertyGroup", "ItemGroup" })
        {
            var emptyGroups = project.Descendants(tagName).Where(g => !g.Elements().Any()).ToList();

            foreach (var group in emptyGroups)
                group.Remove();
        }
    }

    private static void MoveTargetsToEnd(XElement project)
    {
        var targets = project.Elements("Target").ToList();

        foreach (var target in targets)
            target.Remove();

        foreach (var target in targets)
            project.Add(target);
    }

    private static void ConsolidateItemGroups(XElement project)
    {
        var allNodes = project.Nodes().ToList();
        var allItemGroups = project.Elements("ItemGroup").ToList();
        if (allItemGroups.Count == 0)
            return;

        var itemsByType = new Dictionary<string, List<(XComment? Comment, XElement Element)>>();

        foreach (var itemGroup in allItemGroups)
        {
            var nodes = itemGroup.Nodes().ToList();
            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] is not XElement child || child.Name.LocalName == "Folder")
                    continue;

                NormalizePaths(child);

                XComment? precedingComment = null;
                for (var j = i - 1; j >= 0; j--)
                {
                    if (nodes[j] is XComment comment)
                    {
                        precedingComment = comment;
                        break;
                    }

                    if (nodes[j] is XText text && string.IsNullOrWhiteSpace(text.Value))
                        continue;

                    break;
                }

                var tagName = child.Name.LocalName;
                if (!itemsByType.TryGetValue(tagName, out var list))
                {
                    list = [];
                    itemsByType[tagName] = list;
                }

                list.Add((precedingComment, child));
            }
        }

        var firstItemGroupIndex = allNodes.IndexOf(allItemGroups[0]);

        foreach (var itemGroup in allItemGroups)
            itemGroup.Remove();

        var itemGroupOrderSet = ItemGroupOrder.ToHashSet();
        var knownTypes = ItemGroupOrder.Where(itemsByType.ContainsKey);
        var unknownTypes = itemsByType.Keys.Where(k => !itemGroupOrderSet.Contains(k)).Order();
        var allTypes = knownTypes.Concat(unknownTypes).ToList();

        var newGroups = new List<XElement>();
        foreach (var itemType in allTypes)
        {
            var sortedItems = itemsByType[itemType]
                .OrderBy(item => item.Element.Attribute("Condition") is not null ? 1 : 0)
                .ThenBy(item => (item.Element.Attribute("Include")?.Value ?? item.Element.Attribute("Update")?.Value ?? string.Empty).ToLowerInvariant())
                .ToList();

            var newGroup = new XElement("ItemGroup");
            foreach (var (comment, element) in sortedItems)
            {
                if (comment is not null)
                    newGroup.Add(new XComment(comment.Value));

                newGroup.Add(new XElement(element));
            }

            newGroups.Add(newGroup);
        }

        var currentNodes = project.Nodes().ToList();
        var insertIndex = Math.Min(firstItemGroupIndex, currentNodes.Count);

        if (insertIndex == 0)
        {
            foreach (var group in newGroups.AsEnumerable().Reverse())
                project.AddFirst(group);
        }
        else if (insertIndex >= currentNodes.Count)
        {
            foreach (var group in newGroups)
                project.Add(group);
        }
        else
        {
            var insertAfter = currentNodes[insertIndex - 1];
            foreach (var group in newGroups.AsEnumerable().Reverse())
                insertAfter.AddAfterSelf(group);
        }
    }

    private static void NormalizePaths(XElement element)
    {
        foreach (var attrName in new[] { "Include", "Update", "Exclude", "Remove" })
        {
            if (element.Attribute(attrName) is { } attr)
                attr.Value = attr.Value.Replace('\\', '/');
        }
    }

    private static void SortElementsInPlace(XElement parent)
    {
        var elements = parent.Elements().ToList();

        if (elements.Count <= 1)
            return;

        var sortedElements = elements.OrderBy(el => el.Name.LocalName.ToLowerInvariant()).ToList();

        if (elements.SequenceEqual(sortedElements))
            return;

        parent.RemoveNodes();

        foreach (var element in sortedElements)
            parent.Add(element);
    }
}