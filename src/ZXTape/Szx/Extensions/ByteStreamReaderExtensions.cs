using System.Text;
using OldBit.ZXTape.IO;

namespace OldBit.ZXTape.Szx.Extensions;

internal static class ByteStreamReaderExtensions
{
    internal static string ReadChars(this ByteStreamReader reader, int count)
    {
        var buffer = reader.ReadBytes(count);

        return Encoding.ASCII.GetString(buffer).Trim('\0');
    }
}