using OldBit.Spectron.Files.IO;
using OldBit.Spectron.Files.Szx.Extensions;

namespace OldBit.Spectron.Files.Szx;

/// <summary>
/// The zx-state header appears right at the start of a zx-state (.szx) file.
/// </summary>
public sealed class SzxHeader
{
    public const byte MachineId16K = 0;
    public const byte MachineId48K = 1;
    public const byte MachineId128K = 2;
    public const byte MachineIdPlus2 = 3;
    public const byte MachineIdPlus2A = 4;
    public const byte MachineIdPlus3 = 5;
    public const byte MachineIdPlus3E = 6;
    public const byte MachineIdPentagon128 = 7;
    public const byte MachineIdTc2048 = 8;
    public const byte MachineIdTc2068 = 9;
    public const byte MachineIdScorpion = 10;
    public const byte MachineIdSe = 11;
    public const byte MachineIdTs2068 = 12;
    public const byte MachineIdPentagon512 = 13;
    public const byte MachineIdPentagon1024 = 14;
    public const byte MachineIdNtsc48K = 15;
    public const byte MachineId128Ke = 16;

    public const byte FlagsAlternateTimings = 1;

    /// <summary>
    /// Byte sequence of 'Z', 'X', 'S', 'T' to identify the file as a zx-state file.
    /// </summary>
    public DWord Magic { get; private set; } = BlockIds.Magic;

    /// <summary>
    /// Major version number of the file format. Currently, 1.
    /// </summary>
    public byte MajorVersion { get; set; } = 1;

    /// <summary>
    /// Minor version number of the file format. Currently, 4.
    /// </summary>
    public byte MinorVersion { get; set; } = 4;

    /// <summary>
    /// The model of ZX Spectrum (or clone) to switch to when loading the file.
    /// </summary>
    public byte MachineId { get; set; } = MachineId48K;

    /// <summary>
    /// Various flags.
    /// </summary>
    public byte Flags { get; set; }

    internal static SzxHeader Read(ByteStreamReader reader) => new()
    {
        Magic = reader.ReadDWord(),
        MajorVersion = reader.ReadByte(),
        MinorVersion = reader.ReadByte(),
        MachineId = reader.ReadByte(),
        Flags = reader.ReadByte(),
    };

    internal void Write(Stream writer)
    {
        writer.WriteDWord(Magic);
        writer.WriteByte(MajorVersion);
        writer.WriteByte(MinorVersion);
        writer.WriteByte(MachineId);
        writer.WriteByte(Flags);
    }
}