using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Sna;

/// <summary>
/// Represents the SNA data.
/// </summary>
public sealed class SnaData
{
    /// <summary>
    /// Gets or sets the SNA header.
    /// </summary>
    [BlockProperty(Order = 0)]
    public SnaHeader Header { get; set; } = new();

    /// <summary>
    /// Gets or sets the 48K RAM data.
    /// </summary>
    [BlockProperty(Order = 1)]
    public List<byte> Ram48 { get; set; } = [];

    /// <summary>
    /// Gets or sets the 128K RAM data.
    /// </summary>
    [BlockProperty(Order = 2)]
    public Sna128Ram? Ram128 { get; set; }

    /// <summary>
    /// Initializes a new instance of the SNA data.
    /// </summary>
    public SnaData()
    {
    }

    /// <summary>
    /// Creates a new instance of the SNA data.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the SnaData properties.</param>
    internal SnaData(ByteStreamReader reader)
    {
        Header = new SnaHeader(reader);
        Ram48 = reader.ReadBytes(0x4000).ToList();
        Ram128 = Sna128Ram.LoadIfExists(reader);
    }
}