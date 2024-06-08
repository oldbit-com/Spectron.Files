using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Sna;

/// <summary>
/// Represents the SNA data.
/// </summary>
public sealed class SnaData
{
    /// <summary>
    /// Gets or sets the header.
    /// </summary>
    [FileData(Order = 0)]
    public SnaHeader Header { get; set; } = new();

    /// <summary>
    /// Gets or sets the 48K RAM data.
    /// </summary>
    [FileData(Order = 1)]
    public List<byte> Ram48 { get; set; } = [];

    /// <summary>
    /// Gets or sets the SNA header.
    /// </summary>
    [FileData(Order = 2)]
    public SnaHeader128? Header128 { get; set; }

    /// <summary>
    /// Gets or sets the remaining banks of the 128K RAM.
    /// </summary>
    [FileData(Order = 3)]
    public List<List<byte>>? RamBanks { get; set; }

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
        Ram48 = reader.ReadBytes(0xC000).ToList();

        var sna128HeaderData = new byte[4];
        var readCount = reader.ReadAtLeast(sna128HeaderData, sna128HeaderData.Length);

        if (readCount == 0)
        {
            return;    // No 128K data available, 48K SNA file format
        }

        if (readCount != sna128HeaderData.Length)
        {
            throw new EndOfStreamException("Not enough data to read the 128K SNA file format.");
        }

        Header128 = new SnaHeader128
        {
            PC = (Word)(sna128HeaderData[0] | (sna128HeaderData[1] << 8)),
            PageMode = sna128HeaderData[2],
            TrDosRom = sna128HeaderData[3]
        };

        RamBanks = new List<List<byte>>();
        for (var bank = 0; bank < 8; bank++)
        {
            if (bank == 2 || bank == 5 || bank == (Header128.PageMode & 0x07))
            {
                continue;   // These banks are included in the 48K SNA file format, skip them
            }

            var bankData = reader.ReadBytes(0x4000).ToList();
            RamBanks.Add(bankData);
        }
    }
}