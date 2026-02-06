namespace Feiyue.Formatter.DocTypes;

internal sealed class AlwaysFits(Doc printedTrivia) : Doc
{
    public Doc Contents => printedTrivia;
}