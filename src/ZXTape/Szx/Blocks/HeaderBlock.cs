namespace OldBit.ZXTape.Szx.Blocks;

public class HeaderBlock
{
    /// <summary>
    /// Byte sequence of 'Z', 'X', 'S', 'T' to identify the file as a zx-state file.
    /// </summary>
    public Magic Magic { get; set; } = new();

    /// <summary>
    /// Major version number of the file format. Currently 1.
    /// </summary>
    public byte MajorVersion { get; set; } = 1;

    /// <summary>
    /// Minor version number of the file format. Currently 4.
    /// </summary>
    public byte MinorVersion { get; set; } = 4;

    /// <summary>
    /// The model of ZX Spectrum (or clone) to switch to when loading the file.
    /// </summary>
    public byte MachineId { get; set; }

    /// <summary>
    /// Various flags.
    /// </summary>
    public byte Flags { get; set; }
}