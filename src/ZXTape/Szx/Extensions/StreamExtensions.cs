using System.Text;

namespace OldBit.ZXTape.Szx.Extensions;

internal static class StreamExtensions
{
    internal static void WriteWord(this Stream stream, int value)
    {
        stream.WriteByte((byte)value);
        stream.WriteByte((byte)(value >> 8));
    }

    internal static void WriteDWord(this Stream stream, DWord value)
    {
        stream.WriteByte((byte)value);
        stream.WriteByte((byte)(value >> 8));
        stream.WriteByte((byte)(value >> 16));
        stream.WriteByte((byte)(value >> 24));
    }

    internal static void WriteBytes(this Stream stream, byte[] value)
    {
        stream.Write(value, 0, value.Length);
    }

    internal static void WriteChars(this Stream stream, string value, int length)
    {
        var bytes = Encoding.ASCII.GetBytes(value);
        stream.Write(bytes, 0, Math.Min(bytes.Length, length));

        if (bytes.Length < length)
        {
            stream.Write(new byte[length - bytes.Length], 0, length - bytes.Length);
        }
    }
}