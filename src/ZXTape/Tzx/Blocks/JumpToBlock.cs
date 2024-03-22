using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Jump To' block.
/// </summary>
public class JumpToBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [BlockProperty(Order = 0)]
    public byte BlockId => BlockCode.JumpToBlock;

    /// <summary>
    /// Gets or sets the relative jump value.
    /// </summary>
    [BlockProperty(Order = 1)]
    public short Jump { get; set; }

    /// <summary>
    /// Creates a new instance of the 'Jump To' block.
    /// </summary>
    public JumpToBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Jump To' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal JumpToBlock(IByteStreamReader reader)
    {
        Jump = (short)reader.ReadWord();
    }
}