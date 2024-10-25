namespace OldBit.Spectron.Files.Z80.Types;

/// <summary>
///     Bit 0-1: Interrupt mode (0, 1 or 2)
///     Bit 2  : 1=Issue 2 emulation
///     Bit 3  : 1=Double interrupt frequency
///     Bit 4-5: 1=High video synchronisation
///              3=Low video synchronisation
///              0,2=Normal
///     Bit 6-7: 0=Cursor / Protek / AGF joystick
///              1=Kempston joystick
///              2=Sinclair 2 Left joystick (or user defined, for version 3 .z80 files)
///              3=Sinclair 2 Right joystick
/// </summary>
public sealed class Flags2
{
    internal Flags2(byte value)
    {
        InterruptMode = (byte)(value & 0x03);
        Issue2Emulation = (value & 0x04) != 0;
        DoubleInterruptFrequency = (value & 0x08) != 0;
        VideoSynchronization = (byte)((value >> 4) & 0x03);
        JoystickType = (JoystickType)((value >> 6) & 0x03);
    }

    public byte InterruptMode { get; set; }

    public bool Issue2Emulation { get; set; }

    public bool DoubleInterruptFrequency { get; set; }

    public byte VideoSynchronization { get; set; }

    public JoystickType JoystickType { get; set; }

    public byte ToByte() => (byte)(
        (InterruptMode & 0x03) |
        (Issue2Emulation ? 0x04 : 0) |
        (DoubleInterruptFrequency ? 0x08 : 0) |
        ((VideoSynchronization & 0x03) << 4) |
        ((byte)JoystickType << 6));
}