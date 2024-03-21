using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Pure Tone' block.
/// </summary>
public class PureToneBlock : IBlock
{
    /// <summary>
    /// Gets or sets the length of one pulse in T-states.
    /// </summary>
    [BlockProperty(Order = 0)]
    public Word PulseLength { get; set; }

    /// <summary>
    /// Gets or sets the number of pulses.
    /// </summary>
    [BlockProperty(Order = 1)]
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
    /// <param name="reader">A byte reader.</param>
    internal PureToneBlock(IByteStreamReader reader)
    {
        PulseLength = reader.ReadWord();
        PulseCount = reader.ReadWord();
    }
}
