using OldBit.SpeccyLib.Blocks;
using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Set Signal Level' block.
/// </summary>
public class SetSignalLevelBlock : IBlock
{
    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the block without these 4 bytes.
    /// </summary>
    [BlockProperty(Order = 0)]
    private int Length => 1;

    /// <summary>
    /// Gets or sets the signal level.
    /// </summary>
    [BlockProperty(Order = 1)]
    public SignalLevel Level { get; set; }

    /// <summary>
    /// Creates a new instance of the 'Set Signal Level' block.
    /// </summary>
    public SetSignalLevelBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Set Signal Level' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal SetSignalLevelBlock(IByteStreamReader reader)
    {
        Level = (SignalLevel)reader.ReadByte();
    }
}
