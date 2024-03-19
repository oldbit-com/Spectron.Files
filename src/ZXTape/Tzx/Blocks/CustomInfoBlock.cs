using OldBit.ZXTape.Extensions;
using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Custom Info' block.
/// </summary>
public class CustomInfoBlock : IBlock
{
    /// <summary>
    /// Gets or sets the identification string (in ASCII).
    /// </summary>
    [BlockProperty(Order = 0, Size = 0x10)]
    public string Identification { get; set; } = string.Empty;

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the custom info.
    /// </summary>
    [BlockProperty(Order = 1)]
    private int Length => (byte)Info.Count;

    /// <summary>
    /// Gets or sets a list with custom info data.
    /// </summary>
    [BlockProperty(Order = 2)]
    public List<byte> Info { get; set; } = [];

    /// <summary>
    /// Creates a new instance of the 'Custom Info' block.
    /// </summary>
    public CustomInfoBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Custom Info' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal CustomInfoBlock(IByteStreamReader reader)
    {
        Identification = reader.ReadBytes(0x10).ToAsciiString().TrimEnd(' ');
        var length = reader.ReadDWord();
        Info.AddRange(reader.ReadBytes((int)length));
    }
}
