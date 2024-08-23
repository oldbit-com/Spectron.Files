using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Serialization;

namespace OldBit.ZXTape.Szx.Blocks;

/// <summary>
/// This block (Z80R) contains the Z80 registers and other internal state values.
/// It does not contain any specific model registers.
/// </summary>
public sealed class Z80RegsBlock
{
    /// <summary>
    /// The last instruction executed was an EI instruction or an invalid $DD or $FD prefix.
    /// </summary>
    /// <remarks>ZXSTZF_EILAST</remarks>
    public const byte FlagsEiLast = 1;

    /// <summary>
    /// The last instruction executed was a HALT instruction. The CPU is currently executing NOPs and
    /// will continue to do so until the next interrupt occurs.
    /// This flag is mutually exclusive with ZXSTZF_EILAST.
    /// </summary>
    /// <remarks>ZXSTZF_HALTED</remarks>
    public const byte FlagsHalted = 2;

    public Word AF { get; set; }
    public Word BC { get; set; }
    public Word DE { get; set; }
    public Word HL { get; set; }
    public Word AF1 { get; set; }
    public Word BC1 { get; set; }
    public Word DE1 { get; set; }
    public Word HL1 { get; set; }
    public Word IX { get; set; }
    public Word IY { get; set; }
    public Word SP { get; set; }
    public Word PC { get; set; }
    public byte I { get; set; }
    public byte R { get; set; }
    public byte IFF1 { get; set; }
    public byte IFF2 { get; set; }
    public byte IM { get; set; }
    public DWord CyclesStart { get; set; }
    public byte HoldIntReqCycles { get; set; }
    public byte Flags { get; set; }
    public Word MemPtr { get; set; }

    internal void Write(ByteWriter writer)
    {
        var header = new BlockHeader(BlockIds.Z80Regs, 37);
        header.Write(writer);

        writer.WriteWord(AF);
        writer.WriteWord(BC);
        writer.WriteWord(DE);
        writer.WriteWord(HL);
        writer.WriteWord(AF1);
        writer.WriteWord(BC1);
        writer.WriteWord(DE1);
        writer.WriteWord(HL1);
        writer.WriteWord(IX);
        writer.WriteWord(IY);
        writer.WriteWord(SP);
        writer.WriteWord(PC);
        writer.WriteByte(I);
        writer.WriteByte(R);
        writer.WriteByte(IFF1);
        writer.WriteByte(IFF2);
        writer.WriteByte(IM);
        writer.WriteDWord(CyclesStart);
        writer.WriteByte(HoldIntReqCycles);
        writer.WriteByte(Flags);
        writer.WriteWord(MemPtr);
    }

    internal static Z80RegsBlock Read(ByteStreamReader reader, int size)
    {
        if (size != 37)
        {
            throw new InvalidDataException("Invalid Z80Regs block size.");
        }

        var registers = new Z80RegsBlock
        {
            AF = reader.ReadWord(),
            BC = reader.ReadWord(),
            DE = reader.ReadWord(),
            HL = reader.ReadWord(),
            AF1 = reader.ReadWord(),
            BC1 = reader.ReadWord(),
            DE1 = reader.ReadWord(),
            HL1 = reader.ReadWord(),
            IX = reader.ReadWord(),
            IY = reader.ReadWord(),
            SP = reader.ReadWord(),
            PC = reader.ReadWord(),
            I = reader.ReadByte(),
            R = reader.ReadByte(),
            IFF1 = reader.ReadByte(),
            IFF2 = reader.ReadByte(),
            IM = reader.ReadByte(),
            CyclesStart = reader.ReadDWord(),
            HoldIntReqCycles = reader.ReadByte(),
            Flags = reader.ReadByte(),
            MemPtr = reader.ReadWord()
        };

        return registers;
    }
}