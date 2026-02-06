namespace Feiyue.Formatter.DocTypes;

internal sealed class Concat(IList<Doc> contents) : Doc
{
    public IList<Doc> Contents { get; set; } = contents;
}