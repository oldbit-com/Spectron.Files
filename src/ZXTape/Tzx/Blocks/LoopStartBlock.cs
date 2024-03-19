using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Loop Start' block.
/// </summary>
public class LoopStartBlock : IBlock
{
    /// <summary>
    /// Gets or sets the number of repetitions.
    /// </summary>
    [BlockProperty(Order = 0)]
    public Word Count { get; set; }

    /// <summary>
    /// Creates a new instance of the 'Loop Start' block.
    /// </summary>
    public LoopStartBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Loop Start' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal LoopStartBlock(IByteStreamReader reader)
    {
        Count = reader.ReadWord();
    }
}
