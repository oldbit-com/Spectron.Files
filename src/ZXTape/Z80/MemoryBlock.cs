using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Z80;

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

    public MemoryBlock()
    {

    }

    internal MemoryBlock(ByteStreamReader reader, bool isDataCompressed)
    {
        var dataLength = reader.ReadWord();
        PageNumber = reader.ReadByte();
        Data = isDataCompressed ?
            DataCompressor.Decompress(reader.ReadBytes(dataLength), false) :
            reader.ReadBytes(dataLength);
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