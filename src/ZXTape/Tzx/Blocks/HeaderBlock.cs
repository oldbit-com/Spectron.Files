using System.Text;
using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'TZX Header' block.
/// </summary>
public class HeaderBlock : IBlock
{
    /// <summary>
    /// Expected TZX header signature.
    /// </summary>
    public const string TzxSignature = "ZXTape!";

    /// <summary>
    /// Expected TZX header signature end of text marker.
    /// </summary>
    public const byte TzxEotMarker = 0x1A;

    /// <summary>
    /// Current major TZX version.
    /// </summary>
    public const byte TzxVerMajor = 1;

    /// <summary>
    /// Current minor TZX version.
    /// </summary>
    public const byte TzxVerMinor = 20;

    /// <summary>
    /// Creates a new instance of the 'TZX Header' block.
    /// </summary>
    public HeaderBlock()
    {
        Signature = TzxSignature;
        EotMarker = TzxEotMarker;
        VerMajor = TzxVerMajor;
        VerMinor = TzxVerMinor;
    }

    /// <summary>
    /// Creates a new instance of the 'TZX Header' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal HeaderBlock(IByteStreamReader reader)
    {
        Signature = Encoding.ASCII.GetString(reader.ReadBytes(7));
        EotMarker = reader.ReadByte();
        VerMajor = reader.ReadByte();
        VerMinor = reader.ReadByte();
    }

    /// <summary>
    /// Gets the TZX header signature.
    /// </summary>
    [BlockProperty(Order = 0, Size = 7)]
    public string Signature { get; private set; }

    /// <summary>
    /// Gets or sets the end of text file marker.
    /// </summary>
    [BlockProperty(Order = 1)]
    public byte EotMarker { get; private set; }

    /// <summary>
    /// Gets or sets the TZX major revision number.
    /// </summary>
    [BlockProperty(Order = 2)]
    public byte VerMajor { get; set; }

    /// <summary>
    /// Gets or sets the TZX minor revision number.
    /// </summary>
    [BlockProperty(Order = 3)]
    public byte VerMinor { get; set; }
}
