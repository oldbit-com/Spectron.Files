using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Pulse Sequence' block.
/// </summary>
public class PulseSequenceBlock : IBlock
{
    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the pulses' array.
    /// </summary>
    [BlockProperty(Order = 0, Size = 1)]
    private int PulseCount => PulseLengths.Count;

    /// <summary>
    /// Gets or sets an array of pulses' lengths.
    /// </summary>
    [BlockProperty(Order = 1)]
    public List<Word> PulseLengths { get; set; } = new();

    /// <summary>
    /// Creates a new instance of the 'Pulse Sequence' block.
    /// </summary>
    public PulseSequenceBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Pulse Sequence' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal PulseSequenceBlock(IByteStreamReader reader)
    {
        var length = reader.ReadByte();
        PulseLengths.AddRange(reader.ReadWords(length));
    }
}
