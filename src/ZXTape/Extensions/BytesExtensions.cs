using System.Text;

namespace OldBit.ZXTape.Extensions;

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

    internal static Word GetWord(this byte[] data, int index) =>
        (Word)((data[index + 1] << 8) | data[index]);

    internal static void SetWord(this byte[] data, int index, Word value)
    {
        data[index] = (byte)value;
        data[index + 1] = (byte)(value >> 8);
    }
}