using System.Collections;

namespace OldBit.ZX.Files.Extensions;

internal static class TypeExtensions
{
    public static bool IsEnumerable(this Type type) =>
        type.GetInterface(nameof(IEnumerable)) != null;
}
