namespace OldBit.ZX.Files.Z80.Types;

/// <summary>
///     Bit 0: 1 if R register emulation on
///     Bit 1: 1 if LDIR emulation on
///     Bit 2: AY sound in use, even on 48K machines
///     Bit 6: (if bit 2 set) Fuller Audio Box emulation
///     Bit 7: Modify hardware (see below)
/// </summary>
public sealed class Flags3
{
    internal Flags3(byte value)
    {
        EmulateRegisterR = (value & 0x01) != 0;
        EmulateLdirInstruction = (value & 0x02) != 0;
        UseAYSound = (value & 0x04) != 0;
        EmulateFullerAudioBox = (value & 0x40) != 0;
        ModifyHardware = (value & 0x80) != 0;
    }

    public bool EmulateRegisterR { get; set; }

    public bool EmulateLdirInstruction { get; set; }

    public bool UseAYSound { get; set; }

    public bool EmulateFullerAudioBox { get; set; }

    public bool ModifyHardware { get; set; }

    public byte ToByte() => (byte)(
        (EmulateRegisterR ? 0x01 : 0) |
        (EmulateLdirInstruction ? 0x02 : 0) |
        (UseAYSound ? 0x04 : 0) |
        (EmulateFullerAudioBox ? 0x40 : 0) |
        (ModifyHardware ? 0x80 : 0));
}