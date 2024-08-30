namespace OldBit.ZXTape.Szx;

internal static class BlockIds
{
    internal const DWord Magic = 0x5453585A;        // 'Z','X','S','T'
    internal const DWord Creator = 0x52545243;      // 'C','R','T','R'
    internal const DWord Z80Regs = 0x5230385A;      // 'Z','8','0','R'
    internal const DWord SpecRegs = 0x52435053;     // 'S','P','C','R'
    internal const DWord ZxPrinter = 0x5250585A;    // Z','X','P','R'
    internal const DWord Keyboard = 0x4259454B;     // 'K','E','Y','B'
    internal const DWord Joystick = 0x00594F4A;     // 'J','O','Y',0
    internal const DWord CustomRom = 0x004D4F52;    // 'R','O','M',0
    internal const DWord RamPage = 0x504D4152;      // 'R','A','M','P'
}