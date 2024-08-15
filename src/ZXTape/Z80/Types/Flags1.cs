namespace OldBit.ZXTape.Z80.Types;

/// <summary>
///     Bit 0  : Bit 7 of the R-register
///     Bit 1-3: Border colour
///     Bit 4  : 1=Basic SamRom switched in
///     Bit 5  : 1=Block of data is compressed
///     Bit 6-7: No meaning
/// </summary>
public sealed class Flags1
{
    internal Flags1(byte value)
    {
        Bit7R = (byte)(value & 0x01);
        BorderColor = (byte)((value >> 1) & 0x07);
        IsSamRam = (value & 0x10) != 0;
        IsDataCompressed = (value & 0x20) != 0;
    }

    public byte Bit7R { get; set; }

    public byte BorderColor { get; set; }

    public bool IsSamRam { get; set; }

    public bool IsDataCompressed { get; set; }

    public byte ToByte() => (byte)(
        (Bit7R & 0x01) |
        ((BorderColor & 0x07) << 1) |
        (IsSamRam ? 0x10 : 0) |
        (IsDataCompressed ? 0x20 : 0));
}