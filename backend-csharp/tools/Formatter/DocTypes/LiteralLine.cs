namespace Feiyue.Formatter.DocTypes;

internal sealed class LiteralLine : LineDoc, IBreakParent
{
    public LiteralLine()
    {
        Type = LineType.Hard;
        IsLiteral = true;
    }
}