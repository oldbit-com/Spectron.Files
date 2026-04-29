using System.Text;

namespace OldBit.Spectron.Files.Extensions;

internal static class BytesExtensions
{
    internal static string ToAsciiString(this IEnumerable<byte> bytes)
    {
        var s = new StringBuilder();

        foreach (var b in bytes)
        {
            if (b == 0)
            {
                continue;
            }

            s.Append((char)b);
        }

        return s.ToString();
    }
}