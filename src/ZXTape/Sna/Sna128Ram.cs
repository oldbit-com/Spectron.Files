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
    /// Gets or sets the RAM bank 5.
    /// </summary>
    [FileData(Order = 0)]
    public List<byte> RamBank5 { get; set; } = [];

    /// <summary>
    /// Gets or sets the RAM bank 2.
    /// </summary>
    [FileData(Order = 1)]
    public List<byte> RamBank2 { get; set; } = [];

    /// <summary>
    /// Gets or sets the currently paged RAM bank.
    /// </summary>
    [FileData(Order = 2)]
    public List<byte> RamBankN { get; set; } = [];

    /// <summary>
    /// Gets or sets the PC register.
    /// </summary>
    [FileData(Order = 3)]
    public Word PC { get; set; }

    /// <summary>
    /// Gets or sets the page mode (port 7FFD value).
    /// </summary>
    [FileData(Order = 4)]
    public byte PageMode { get; set; }

    /// <summary>
    /// Gets or sets the TR-DOS ROM paged flag (1=yes, 0=no).
    /// </summary>
    [FileData(Order = 5)]
    public byte TrDosRomPaged { get; set; }

    [FileData(Order = 6)]
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
        var ramBank5 = new byte[0x4000];
        var readCount = reader.ReadAtLeast(ramBank5, ramBank5.Length);

        if (readCount == 0)
        {
            return null;    // No 128K data available, 48K SNA file format
        }
        if (readCount != ramBank5.Length)
        {
            throw new EndOfStreamException("Not enough data to read the 128K SNA file format.");
        }

        var sna128Ram = new Sna128Ram
        {
            RamBank5 = ramBank5.ToList(),
            RamBank2 = reader.ReadBytes(0x4000).ToList(),
            RamBankN = reader.ReadBytes(0x4000).ToList(),
            PC = reader.ReadWord(),
            PageMode = reader.ReadByte(),
            TrDosRomPaged = reader.ReadByte()
        };

        for (var bank = 0; bank < 7; bank++)
        {
            if (bank == 2 || bank == 5 || bank == (sna128Ram.PageMode & 0x07))
            {
                continue;   // These banks are included in 48K SNA file format, skip them
            }

            sna128Ram.RemainingBanks.Add([..reader.ReadBytes(0x4000)]);
        }

        return sna128Ram;
    }
}