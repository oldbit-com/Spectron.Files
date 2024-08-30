using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Serialization;

namespace OldBit.ZXTape.Szx.Blocks;

/// <summary>
/// This block ('R','A','M','P') contains a 16KB RAM page.
/// </summary>
public sealed class RamPageBlock
{
    /// <summary>
    /// Indicates the RAM page is compressed.
    /// </summary>
    public const Word FlagsCompressed = 1;

    /// <summary>
    /// Gets or sets the flags indicating the RAM page data is compressed.
    /// </summary>
    public Word Flags { get; init; }

    /// <summary>
    /// Gets or sets the memory page number, usually 0-7, Pentagon 512/1024
    /// and ZS Scorpion machines have additional memory.
    /// </summary>
    public byte PageNumber { get; init; }

    /// <summary>
    /// Gets the actual compressed or uncompressed memory page data.
    /// </summary>
    public byte[] Data { get; private set; }

    private RamPageBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the RamPageBlock class.
    /// </summary>
    /// <param name="data">The memory page data.</param>
    /// <param name="pageNumber">The memory page number.</param>
    /// <param name="compress">Specifies whether the data should be compressed or not.</param>
    public RamPageBlock(byte[] data, byte pageNumber, bool compress)
    {
        Flags = (Word)(compress ? FlagsCompressed : 0);
        PageNumber = pageNumber;
        Data = compress ? ZLibHelper.Compress(data) : data;
    }

    internal void Write(ByteWriter writer)
    {
        var header = new BlockHeader(BlockIds.RamPage, 3 + Data.Length);
        header.Write(writer);

        writer.WriteWord(Flags);
        writer.WriteByte(PageNumber);
        writer.WriteBytes(Data);
    }

    internal static RamPageBlock Read(ByteStreamReader reader, int size)
    {
        var ramPage = new RamPageBlock
        {
            Flags = reader.ReadWord(),
            PageNumber = reader.ReadByte()
        };

        var data = reader.ReadBytes(size - 3);
        ramPage.Data = ramPage.Flags == FlagsCompressed ? ZLibHelper.Decompress(data) : data;

        return ramPage;
    }
}