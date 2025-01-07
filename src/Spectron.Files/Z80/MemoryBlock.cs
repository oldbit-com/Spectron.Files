using OldBit.Spectron.Files.IO;
using OldBit.Spectron.Files.Serialization;

namespace OldBit.Spectron.Files.Z80;

public class MemoryBlock : IDataSerializer
{
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public byte PageNumber { get; private set; }

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    public byte[] Data { get; private set; } = [];

    internal MemoryBlock()
    {
    }

    public MemoryBlock(byte[] data, byte pageNumber)
    {
        if (data.Length != 0x4000)
        {
            throw new ArgumentOutOfRangeException(nameof(data), "Memory data must be 16k long.");
        }

        Data = data;
        PageNumber = pageNumber;
    }

    internal static MemoryBlock? Load(ByteStreamReader reader)
    {
        if (!reader.TryReadWord(out var length))
        {
            return null;
        }

        var block = new MemoryBlock();
        var isDataCompressed = length != 0xffff;

        block.PageNumber = reader.ReadByte();

        block.Data = isDataCompressed ?
            DataCompressor.Decompress(reader.ReadBytes(length)) :
            reader.ReadBytes(16384);

        return block;
    }

    public byte[] Serialize()
    {
        var compressedData = DataCompressor.Compress(Data, false);

        var length = FileDataSerializer.SerializePrimitiveType((Word)compressedData.Count).ToArray();

        compressedData.Insert(0, PageNumber);
        compressedData.Insert(0, length[1]);
        compressedData.Insert(0, length[0]);

        return compressedData.ToArray();
    }
}