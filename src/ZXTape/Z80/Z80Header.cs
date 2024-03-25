using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Z80;

/// <summary>
/// Represents the .z80 file header.
/// </summary>
public sealed class Z80Header
{
    /// <summary>
    /// Gets or sets the A register.
    /// </summary>
    [FileData(Order = 0)]
    public byte A { get; set; }

    /// <summary>
    /// Gets or sets the F register.
    /// </summary>
    [FileData(Order = 1)]
    public byte F { get; set; }

    /// <summary>
    /// Gets or sets the BC register.
    /// </summary>
    [FileData(Order = 2)]
    public Word BC { get; set; }

    /// <summary>
    /// Gets or sets the HL register.
    /// </summary>
    [FileData(Order = 3)]
    public Word HL { get; set; }

    /// <summary>
    /// Gets or sets the PC register.
    /// </summary>
    [FileData(Order = 4)]
    public Word PC { get; set; }

    /// <summary>
    /// Gets or sets the SP register.
    /// </summary>
    [FileData(Order = 5)]
    public Word SP { get; set; }

    /// <summary>
    /// Gets or sets the I register.
    /// </summary>
    [FileData(Order = 6)]
    public byte I { get; set; }

    /// <summary>
    /// Gets or sets the R register.
    /// </summary>
    [FileData(Order = 7)]
    public byte R { get; set; }

    /// <summary>
    /// Gets or sets the miscellaneous bits setting.
    /// <remarks>
    ///     Bit 0  : Bit 7 of the R-register
    ///     Bit 1-3: Border colour
    ///     Bit 4  : 1=Basic SamRom switched in
    ///     Bit 5  : 1=Block of data is compressed
    ///     Bit 6-7: No meaning
    /// </remarks>
    /// </summary>
    [FileData(Order = 8)]
    public byte Flags1 { get; set; }

    /// <summary>
    /// Gets or sets the DE register.
    /// </summary>
    [FileData(Order = 9)]
    public Word DE { get; set; }

    /// <summary>
    /// Gets or sets the BC' register.
    /// </summary>
    [FileData(Order = 10)]
    public Word BCPrime { get; set; }

    /// <summary>
    /// Gets or sets the DE' register.
    /// </summary>
    [FileData(Order = 11)]
    public Word DEPrime { get; set; }

    /// <summary>
    /// Gets or sets the HL' register.
    /// </summary>
    [FileData(Order = 12)]
    public Word HLPrime { get; set; }

    /// <summary>
    /// Gets or sets the A' register.
    /// </summary>
    [FileData(Order = 13)]
    public byte APrime { get; set; }

    /// <summary>
    /// Gets or sets the F' register.
    /// </summary>
    [FileData(Order = 14)]
    public byte FPrime { get; set; }

    /// <summary>
    /// Gets or sets the IY register.
    /// </summary>
    [FileData(Order = 15)]
    public Word IY { get; set; }

    /// <summary>
    /// Gets or sets the IX register.
    /// </summary>
    [FileData(Order = 16)]
    public Word IX { get; set; }

    /// <summary>
    /// Gets or sets the Interrupt flip-flop: 0=DI, otherwise EI.
    /// </summary>
    [FileData(Order = 17)]
    public byte Interrupt { get; set; }

    /// <summary>
    /// Gets or sets the IFF2 value.
    /// </summary>
    [FileData(Order = 18)]
    public byte IFF2 { get; set; }

    /// <summary>
    /// Gets or sets the miscellaneous bits setting.
    /// <remarks>
    ///     Bit 0-1: Interrupt mode (0, 1 or 2)
    ///     Bit 2  : 1=Issue 2 emulation
    ///     Bit 3  : 1=Double interrupt frequency
    ///     Bit 4-5: 1=High video synchronisation
    ///              3=Low video synchronisation
    ///              0,2=Normal
    ///     Bit 6-7: 0=Cursor/Protek/AGF joystick
    ///              1=Kempston joystick
    ///              2=Sinclair 2 Left joystick (or user defined, for version 3 .z80 files)
    ///              3=Sinclair 2 Right joystick
    /// </remarks>
    /// </summary>
    [FileData(Order = 19)]
    public byte Flags2 { get; set; }

    /// <summary>
    /// Creates a new instance of the Z80Header header.
    /// </summary>
    public Z80Header()
    {
    }

    /// <summary>
    /// Creates a new instance of the Z80Header header.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the Z80Header properties.</param>
    internal Z80Header(ByteStreamReader reader)
    {
        A = reader.ReadByte();
        F = reader.ReadByte();
        BC = reader.ReadWord();
        HL = reader.ReadWord();
        PC = reader.ReadWord();
        SP = reader.ReadWord();
        I = reader.ReadByte();
        R = reader.ReadByte();
        Flags1 = reader.ReadByte();
        DE = reader.ReadWord();
        BCPrime = reader.ReadWord();
        DEPrime = reader.ReadWord();
        HLPrime = reader.ReadWord();
        APrime = reader.ReadByte();
        FPrime = reader.ReadByte();
        IY = reader.ReadWord();
        IX = reader.ReadWord();
        Interrupt = reader.ReadByte();
        IFF2 = reader.ReadByte();
        Flags2 = reader.ReadByte();
    }
}