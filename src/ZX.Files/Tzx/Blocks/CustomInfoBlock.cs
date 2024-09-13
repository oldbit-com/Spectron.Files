using OldBit.ZX.Files.Extensions;
using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;

namespace OldBit.ZX.Files.Tzx.Blocks;

/// <summary>
/// Represents the 'Custom Info' block.
/// </summary>
public class CustomInfoBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [FileData(Order = 0)]
    public byte BlockId => BlockCode.CustomInfo;

    /// <summary>
    /// Gets or sets the identification string (in ASCII).
    /// </summary>
    [FileData(Order = 1, Size = 0x10)]
    public string Identification { get; set; } = string.Empty;

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the custom info.
    /// </summary>
    [FileData(Order = 2)]
    private int Length => Info.Count;

    /// <summary>
    /// Gets or sets a list with custom info data.
    /// </summary>
    [FileData(Order = 3)]
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
    /// <param name="reader">The ByteStreamReader used to initialize the CustomInfoBlock properties.</param>
    internal CustomInfoBlock(ByteStreamReader reader)
    {
        Identification = reader.ReadBytes(0x10).ToAsciiString().TrimEnd(' ');
        var length = reader.ReadDWord();
        Info.AddRange(reader.ReadBytes((int)length));
    }
}