using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Glue' block.
/// </summary>
public class GlueBlock : IBlock
{
    /// <summary>
    /// Gets or sets the data object.
    /// </summary>
    [BlockProperty(Order = 0, Size = 9)]
    public List<byte> Data { get; set; }

    /// <summary>
    /// Creates a new instance of the 'Glue' block.
    /// </summary>
    public GlueBlock()
    {
        Data = new List<byte> { 0x58, 0x54, 0x61, 0x70, 0x65, 0x21, 0x1A, HeaderBlock.TzxVerMajor, HeaderBlock.TzxVerMinor };
    }

    /// <summary>
    /// Creates a new instance of the 'Glue' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal GlueBlock(IByteStreamReader reader)
    {
        Data = new List<byte>(reader.ReadBytes(9));
    }
}
