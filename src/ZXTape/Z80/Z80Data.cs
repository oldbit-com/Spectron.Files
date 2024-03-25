using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Z80;

/// <summary>
/// Represents the Z80 data.
/// </summary>
public class Z80Data
{
    /// <summary>
    /// Gets or sets the header.
    /// </summary>
    [FileData(Order = 0)]
    public Z80Header Header { get; set; } = new();

    /// <summary>
    /// Gets or sets the 48K RAM data.
    /// </summary>
    [FileData(Order = 1)]
    public List<byte> Ram48 { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the Z80 data.
    /// </summary>
    public Z80Data()
    {
    }

    /// <summary>
    /// Creates a new instance of the Z80 data.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the Z80Data properties.</param>
    internal Z80Data(ByteStreamReader reader)
    {
        Header = new Z80Header(reader);
    }
}