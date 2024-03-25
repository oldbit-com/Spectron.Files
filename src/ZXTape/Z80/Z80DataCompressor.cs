
namespace OldBit.ZXTape.Z80;

internal sealed class Z80DataCompressor
{
    private const byte Marker = 0xED;

    public static List<byte> Compress(byte[] data, bool appendEndMarker = true)
    {
        var compressed = new List<byte>();
        var position = 0;

        while (position < data.Length)
        {
            var start = position;
            var currentByte = data[position];

            // Count the number of repeated bytes
            while (position < data.Length && data[position] == currentByte)
            {
                position++;
            }

            var count = position - start;

            if (currentByte == Marker)
            {
                // If the byte is the marker, we need to handle it differently
                if (count == 1)
                {
                    compressed.Add(currentByte);
                    position++;
                    if (position < data.Length)
                    {
                        compressed.Add(data[position]);
                    }
                }
                else
                {
                    Repeat(compressed, count, Marker);
                }
            }
            else if (count >= 5 || (count > 1 && position < data.Length && data[position] == Marker))
            {
                // More than 5 bytes repeated or a sequence of more than 1 ED bytes
                Repeat(compressed, count, currentByte);
            }
            else
            {
                // If the sequence is less than 5 bytes long, just copy the bytes
                compressed.AddRange(data[start..(count+start)]);
            }
        }

        if (appendEndMarker)
        {
            // Append the end marker
            compressed.AddRange(new byte[] { 0x00, Marker, Marker, 0x00 });
        }

        return compressed;
    }

    public static List<byte> Decompress(byte[] data)
    {
        var decompressed = new List<byte>();
        var position = 0;

        while (position < data.Length - 1)
        {
            if (data[position] == Marker && data[position + 1] == Marker)
            {
                int count = data[position + 2];
                var value = data[position + 3];

                decompressed.AddRange(Enumerable.Repeat(value, count));
                position += 4;
            }
            else
            {
                decompressed.Add(data[position]);
                position++;
            }

            if (IsEndOfData(data, position))
            {
                break;
            }
        }

        return decompressed;
    }

    private static void Repeat(List<byte> compressed, int count, byte currentByte)
    {
        while (count > 0)
        {
            var repeat = Math.Min(count, 0x100);
            compressed.AddRange(new[] { Marker, Marker, (byte)repeat, currentByte });
            count -= repeat;
        }
    }

    private static bool IsEndOfData(IReadOnlyList<byte> data, int index)
    {
        return
            index < data.Count - 3 &&
            data[index] == 0x00 &&
            data[index + 1] == Marker &&
            data[index + 2] == Marker &&
            data[index + 3] == 0x00;
    }
}