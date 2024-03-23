using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Turbo Speed Data' block.
/// </summary>
public class TurboSpeedDataBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [BlockProperty(Order = 0)]
    public byte BlockId => BlockCode.TurboSpeedData;

    /// <summary>
    /// Gets or sets the length of the PILOT pulse.
    /// </summary>
    [BlockProperty(Order = 1)]
    public Word PilotPulseLength { get; set; }

    /// <summary>
    /// Gets or sets the length of the SYNC first pulse.
    /// </summary>
    [BlockProperty(Order = 2)]
    public Word SyncFirstPulseLength { get; set; }

    /// <summary>
    /// Gets or sets the length of the SYNC second pulse.
    /// </summary>
    [BlockProperty(Order = 3)]
    public Word SyncSecondPulseLength { get; set; }

    /// <summary>
    /// Gets or sets the length of the ZERO bit pulse.
    /// </summary>
    [BlockProperty(Order = 4)]
    public Word ZeroBitPulseLength { get; set; }

    /// <summary>
    /// Gets or sets the length of the ONE bit pulse.
    /// </summary>
    [BlockProperty(Order = 5)]
    public Word OneBitPulseLength { get; set; }

    /// <summary>
    /// Gets or sets the length of the PILOT tone (number of pulses).
    /// </summary>
    [BlockProperty(Order = 6)]
    public Word PilotToneLength { get; set; }

    /// <summary>
    /// Gets or sets the used bits in the last byte (other bits should be 0). E.g. if this is 6, then the bits used (x)
    /// in the last byte are: xxxxxx00, where MSb is the leftmost bit, LSb is the rightmost bit.
    /// </summary>
    [BlockProperty(Order = 7)]
    public byte UsedBitsLastByte { get; set; }

    /// <summary>
    /// Gets or sets the pause value (in millisecond) that should be applied after this block.
    /// </summary>
    [BlockProperty(Order = 8)]
    public Word PauseDuration { get; set; }

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the data. Property is only needed during the serialization.
    /// </summary>
    [BlockProperty(Order = 9, Size = 3)]
    private int Length => Data.Count;

    /// <summary>
    /// Gets or sets the data as in .TAP file.
    /// </summary>
    [BlockProperty(Order = 10)]
    public List<byte> Data { get; set; } = [];

    /// <summary>
    /// Creates a new instance of the 'Turbo Speed Data' block.
    /// </summary>
    public TurboSpeedDataBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Turbo Speed Data' block using the byte reader.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the TurboSpeedDataBlock properties.</param>
    internal TurboSpeedDataBlock(ByteStreamReader reader)
    {
        PilotPulseLength = reader.ReadWord();
        SyncFirstPulseLength = reader.ReadWord();
        SyncSecondPulseLength = reader.ReadWord();
        ZeroBitPulseLength = reader.ReadWord();
        OneBitPulseLength = reader.ReadWord();
        PilotToneLength = reader.ReadWord();
        UsedBitsLastByte = reader.ReadByte();
        PauseDuration = reader.ReadWord();
        var length = reader.ReadByte() | reader.ReadByte() << 8 | reader.ReadByte() << 16;
        Data.AddRange(reader.ReadBytes(length));
    }
}