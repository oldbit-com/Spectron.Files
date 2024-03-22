using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the Pause (silence) or 'Stop the Tape' command.
/// </summary>
public class PauseBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [BlockProperty(Order = 0)]
    public byte BlockId => BlockCode.Pause;

    /// <summary>
    /// Gets or sets the pause duration in milliseconds.
    /// </summary>
    [BlockProperty(Order = 1)]
    public Word Duration { get; set; }

    /// <summary>
    /// Creates a new instance of the 'Pause' block.
    /// </summary>
    public PauseBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Pause' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    public PauseBlock(IByteStreamReader reader)
    {
        Duration = reader.ReadWord();
    }
}