using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Pure Data' block.
/// </summary>
public class PureDataBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [BlockProperty(Order = 0)]
    public byte BlockId => BlockCode.PureData;

    /// <summary>
    ///  Gets or sets the length of the ZERO bit pulse.
    /// </summary>
    [BlockProperty(Order = 1)]
    public Word ZeroBitPulseLength { get; set; }

    /// <summary>
    /// Gets or sets the length of the ONE bit pulse.
    /// </summary>
    [BlockProperty(Order = 2)]
    public Word OneBitPulseLength { get; set; }

    /// <summary>
    /// Gets or sets the used bits in the last byte (other bits should be 0). E.g. if this is 6, then the bits used (x)
    /// in the last byte are: xxxxxx00, where MSb is the leftmost bit, LSb is the rightmost bit.
    /// </summary>
    [BlockProperty(Order = 3)]
    public byte UsedBitsLastByte { get; set; }

    /// <summary>
    /// Gets or sets the pause value (in millisecond) that should be applied after this block.
    /// </summary>
    [BlockProperty(Order = 4)]
    public Word PauseDuration { get; set; }

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the data.
    /// </summary>
    [BlockProperty(Order = 5, Size = 3)]
    private int Length => Data.Count();

    /// <summary>
    /// Gets or sets the data as in .TAP file.
    /// </summary>
    [BlockProperty(Order = 6)]
    public List<byte> Data { get; set; } = [];

    /// <summary>
    /// Creates a new instance of the 'Pure Data' block.
    /// </summary>
    public PureDataBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Pure Data' block using the byte reader.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the PureDataBlock properties.</param>
    internal PureDataBlock(ByteStreamReader reader)
    {
        ZeroBitPulseLength = reader.ReadWord();
        OneBitPulseLength = reader.ReadWord();
        UsedBitsLastByte = reader.ReadByte();
        PauseDuration = reader.ReadWord();
        var length = reader.ReadByte() | reader.ReadByte() << 8 | reader.ReadByte() << 16;
        Data.AddRange(reader.ReadBytes(length));
    }
}