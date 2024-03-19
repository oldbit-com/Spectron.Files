using System.Collections;

namespace OldBit.ZXTape.Extensions;

internal static class TypeExtensions
{
    public static bool IsEnumerable(this Type type)
    {
        return type.GetInterface(nameof(IEnumerable)) != null;
    }
}
