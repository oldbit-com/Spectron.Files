using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Pure Tone' block.
/// </summary>
public class PureToneBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [FileData(Order = 0)]
    public byte BlockId => BlockCode.PureTone;

    /// <summary>
    /// Gets or sets the length of one pulse in T-states.
    /// </summary>
    [FileData(Order = 1)]
    public Word PulseLength { get; set; }

    /// <summary>
    /// Gets or sets the number of pulses.
    /// </summary>
    [FileData(Order = 2)]
    public Word PulseCount { get; set; }

    /// <summary>
    /// Creates a new instance of the 'Pure Tone' block.
    /// </summary>
    public PureToneBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Pure Tone' block using the byte reader.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the PureToneBlock properties.</param>
    internal PureToneBlock(ByteStreamReader reader)
    {
        PulseLength = reader.ReadWord();
        PulseCount = reader.ReadWord();
    }
}