using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Standard Speed Data' block.
/// </summary>
public class StandardSpeedDataBlock : IBlock
{
    /// <summary>
    /// Gets or sets the pause value (in millisecond) that should be applied after this block.
    /// </summary>
    [BlockProperty(Order = 0)]
    public Word PauseDuration { get; set; }

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the data.
    /// </summary>
    [BlockProperty(Order = 1, Size = 2)]
    private int Length => Data.Count;

    /// <summary>
    /// Gets or sets the data as in .TAP file.
    /// </summary>
    [BlockProperty(Order = 2)]
    public List<byte> Data { get; set; } = [];

    /// <summary>
    /// Creates a new instance of the 'Standard Speed Data' block.
    /// </summary>
    public StandardSpeedDataBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Standard Speed Data' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal StandardSpeedDataBlock(IByteStreamReader reader)
    {
        PauseDuration = reader.ReadWord();
        var length = reader.ReadWord();
        Data.AddRange(reader.ReadBytes(length));
    }
}