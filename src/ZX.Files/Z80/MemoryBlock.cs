using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;

namespace OldBit.ZX.Files.Z80;

public class MemoryBlock : IDataSerializer
{
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public byte PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    public byte[] Data { get; set; } = [];

    internal static MemoryBlock? Load(ByteStreamReader reader)
    {
        if (!reader.TryReadWord(out var length))
        {
            return null;
        }

        var block = new MemoryBlock();
        var isDataCompressed = length != 0xFFFF;

        block.PageNumber = reader.ReadByte();

        block.Data = isDataCompressed ?
            DataCompressor.Decompress(reader.ReadBytes(length)) :
            reader.ReadBytes(16384);

        return block;
    }

    public byte[] Serialize()
    {
        var compressedData = DataCompressor.Compress(Data, false);
        var dataLength = (Word)(3 + compressedData.Count) ;

        var data = new List<byte>();
        data.AddRange(FileDataSerializer.SerializePrimitiveType(dataLength));
        data.Add(PageNumber);
        data.AddRange(compressedData);

        return data.ToArray();
    }
}