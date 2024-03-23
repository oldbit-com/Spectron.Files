using System.Diagnostics.CodeAnalysis;
using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Sna;

/// <summary>
/// Represents the SNA 128K extension data.
/// </summary>
public sealed class Sna128Ram
{
    /// <summary>
    /// Gets or sets the PC register.
    /// </summary>
    [FileData(Order = 0)]
    public Word PC { get; set; }

    /// <summary>
    /// Gets or sets the page mode (port 7FFD value).
    /// </summary>
    [FileData(Order = 1)]
    public byte PageMode { get; set; }

    /// <summary>
    /// Gets or sets the TR-DOS ROM paged flag (1=yes, 0=no).
    /// </summary>
    [FileData(Order = 2)]
    public byte TrDosRomPaged { get; set; }

    /// <summary>
    /// Gets or sets the remaining banks of the 128K RAM.
    /// </summary>
    [FileData(Order = 3)]
    public List<List<byte>> RemainingBanks { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the Sna128ExtensionData class.
    /// </summary>
    public Sna128Ram()
    {
    }

    /// <summary>
    /// Attempts to load the SNA 128K extension data from the provided ByteStreamReader into a new Sna128Ram instance.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to read the SNA 128K extension data.</param>
    /// <returns>A Sna128Ram instance created from the read data if the read operation succeeds; otherwise, null.</returns>
    /// <throws cref="EndOfStreamException">Thrown when not enough data is in the stream.</throws>
    internal static Sna128Ram? LoadIfExists(ByteStreamReader reader)
    {
        var pc = new byte[2];
        var readCount = reader.ReadAtLeast(pc, pc.Length);

        if (readCount == 0)
        {
            return null;    // No 128K data available, 48K SNA file format
        }
        if (readCount != pc.Length)
        {
            throw new EndOfStreamException("Not enough data to read the 128K SNA file format.");
        }

        var sna128Ram = new Sna128Ram
        {
            PC = (Word)(pc[0] | (pc[1] << 8)),
            PageMode = reader.ReadByte(),
            TrDosRomPaged = reader.ReadByte()
        };

        for (var bank = 0; bank < 8; bank++)
        {
            if (bank == 2 || bank == 5 || bank == (sna128Ram.PageMode & 0x07))
            {
                continue;   // These banks are included in 48K SNA file format, skip them
            }

            var bankData = reader.ReadBytes(0x4000).ToList();
            sna128Ram.RemainingBanks.Add(bankData);
        }

        return sna128Ram;
    }
}