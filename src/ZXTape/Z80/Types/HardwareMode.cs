namespace OldBit.ZXTape.Z80.Types;

[Flags]
public enum HardwareMode
{
    None = 0x00,

    Spectrum48 = 0x01,

    Spectrum128 = 0x02,

    SamRam = 0x04,

    Mgt = 0x08,

    Interface1 = 0x10,

    SpectrumPlus3 = 0x20,

    Pentagon128 = 0x40,

    Scorpion256 = 0x80,

    DidaktikKompakt = 0x100,

    SpectrumPlus2 = 0x200,

    SpectrumPlus2A = 0x400,

    TC2048 = 0x800,

    TC2068 = 0x1000,

    TS2068 = 0x2000,
}