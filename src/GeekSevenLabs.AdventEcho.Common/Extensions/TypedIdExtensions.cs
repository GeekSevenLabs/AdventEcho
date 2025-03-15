namespace GeekSevenLabs.AdventEcho.Common.Extensions;

public static class TypedIdExtensions
{
    public static TId Required<TId>(this TId? id) where TId : struct, ITypedId
    {
        if (!id.HasValue)
        {
            throw new ArgumentNullException(nameof(id));
        }

        return id.Value;
    }
}