using System.Text;

namespace OldBit.ZX.Files.Extensions;

internal static class BytesExtensions
{
    internal static string ToAsciiString(this IEnumerable<byte> bytes)
    {
        var s = new StringBuilder();
        foreach (var b in bytes)
        {
            s.Append((char)b);
        }
        return s.ToString();
    }
}