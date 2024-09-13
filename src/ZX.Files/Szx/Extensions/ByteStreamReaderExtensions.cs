using System.Text;
using OldBit.ZX.Files.IO;

namespace OldBit.ZX.Files.Szx.Extensions;

internal static class ByteStreamReaderExtensions
{
    internal static string ReadChars(this ByteStreamReader reader, int count)
    {
        var buffer = reader.ReadBytes(count);

        return Encoding.ASCII.GetString(buffer).Trim('\0');
    }
}