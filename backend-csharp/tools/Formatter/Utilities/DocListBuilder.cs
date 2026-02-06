using System.Buffers;
using System.Runtime.CompilerServices;
using Feiyue.Formatter.DocTypes;

namespace Feiyue.Formatter.Utilities;

// From https://github.com/dotnet/runtime/blob/f5c73447ca9dcb3407d0143829bbf708c04170c1/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/ValueListBuilder.cs#L10
internal ref struct DocListBuilder
{
    private Span<Doc> span;
    private Doc[]? arrayFromPool;

    public DocListBuilder(int capacity) => Grow(capacity);

    public int Length { readonly get; set; }

    public ref Doc this[int index] => ref span[index];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() => Length = 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(Doc item)
    {
        var pos = Length;

        // Workaround for https://github.com/dotnet/runtime/issues/72004
        var span = this.span;
        if ((uint)pos < (uint)span.Length)
        {
            span[pos] = item;
            Length = pos + 1;
        }
        else
        {
            AddWithResize(item);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(params ReadOnlySpan<Doc> source)
    {
        var pos = Length;
        var span = this.span;
        if (source.Length == 1 && (uint)pos < (uint)span.Length)
        {
            span[pos] = source[0];
            Length = pos + 1;
        }
        else
        {
            AppendMultiChar(source);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void AppendMultiChar(scoped ReadOnlySpan<Doc> source)
    {
        if ((uint)(Length + source.Length) > (uint)span.Length)
            Grow(span.Length - Length + source.Length);

        source.CopyTo(span[Length..]);
        Length += source.Length;
    }

    public void Insert(int index, scoped ReadOnlySpan<Doc> source)
    {
        Debug.Assert(index == 0, "Implementation currently only supports index == 0");

        if ((uint)(Length + source.Length) > (uint)span.Length)
            Grow(source.Length);

        span[..Length].CopyTo(span[source.Length..]);
        source.CopyTo(span);
        Length += source.Length;
    }

    // Hide uncommon path
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void AddWithResize(Doc item)
    {
        var pos = Length;
        Grow(1);
        span[pos] = item;
        Length = pos + 1;
    }

    public readonly ReadOnlySpan<Doc> AsSpan() => span[..Length];

    public readonly Doc[] ToArray() => AsSpan().ToArray();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        var toReturn = arrayFromPool;
        if (toReturn is null)
            return;

        arrayFromPool = null;
        ArrayPool<Doc>.Shared.Return(toReturn);
    }

    public override readonly string ToString() => DocSerializer.Serialize(Doc.Concat(AsSpan().ToArray()));

    // Note that consuming implementations depend on the list only growing if it's absolutely
    // required.  If the list is already large enough to hold the additional items be added,
    // it must not grow. The list is used in a number of places where the reference is checked
    // and it's expected to match the initial reference provided to the constructor if that
    // span was sufficiently large.
    private void Grow(int additionalCapacityRequired = 1)
    {
        const int ArrayMaxLength = 0x7FFFFFC7; // same as Array.MaxLength

        // Double the size of the span.  If it's currently empty, default to size 4,
        // although it'll be increased in Rent to the pool's minimum bucket size.
        var nextCapacity = Math.Max(span.Length != 0 ? span.Length * 2 : 4, span.Length + additionalCapacityRequired);

        // If the computed doubled capacity exceeds the possible length of an array, then we
        // want to downgrade to either the maximum array length if that's large enough to hold
        // an additional item, or the current length + 1 if it's larger than the max length, in
        // which case it'll result in an OOM when calling Rent below.  In the exceedingly rare
        // case where _span.Length is already int.MaxValue (in which case it couldn't be a managed
        // array), just use that same value again and let it OOM in Rent as well.
        if ((uint)nextCapacity > ArrayMaxLength)
            nextCapacity = Math.Max(Math.Max(span.Length + 1, ArrayMaxLength), span.Length);

        var array = ArrayPool<Doc>.Shared.Rent(nextCapacity);
        span.CopyTo(array);

        var toReturn = arrayFromPool;
        span = arrayFromPool = array;
        if (toReturn is not null)
            ArrayPool<Doc>.Shared.Return(toReturn);
    }
}