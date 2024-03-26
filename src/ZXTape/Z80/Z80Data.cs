using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Z80;

/// <summary>
/// Represents the Z80 data.
/// </summary>
public class Z80Data
{
    /// <summary>
    /// Gets or sets the header.
    /// </summary>
    [FileData(Order = 0)]
    public Z80Header Header { get; set; } = new();

    /// <summary>
    /// Gets or sets the 48K RAM data.
    /// </summary>
    [FileData(Order = 1)]
    public byte[] Ram { get; set; } = [];

    [FileData(Order = 2)]
    public List<MemoryBlock> MemoryBlocks { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the Z80 data.
    /// </summary>
    public Z80Data()
    {
    }

    /// <summary>
    /// Creates a new instance of the Z80 data.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the Z80Data properties.</param>
    internal Z80Data(ByteStreamReader reader)
    {
        Header = new Z80Header(reader);
        if (Header.Version == 1)
        {
            Ram = IsDataCompressed ?
                DataCompressor.Decompress(reader.ReadToEnd(), true) :
                reader.ReadToEnd();
        }
        else
        {
            var memoryBlock = new MemoryBlock(reader, true);
            MemoryBlocks.Add(memoryBlock);
            memoryBlock = new MemoryBlock(reader, true);
            MemoryBlocks.Add(memoryBlock);
        }
    }

    private bool IsDataCompressed => (Header.RawData[12] & 0b00100000) != 0;
}