using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Szx.Blocks;

namespace OldBit.ZX.Files.UnitTests.Szx.Blocks;

public class Z80RegsBlockTests
{
    [Fact]
    public void Z80Regs_ShouldConvertToBytes()
    {
        var regs = GetZ80RegsBlock();
        using var writer = new MemoryStream();

        regs.Write(writer);

        var data = writer.ToArray();
        data.Length.Should().Be(8 + 37);

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x5230385A);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(37);

        // Data
        BitConverter.ToUInt16(data[8..10].ToArray()).Should().Be(0x1234);
        BitConverter.ToUInt16(data[10..12].ToArray()).Should().Be(0x5678);
        BitConverter.ToUInt16(data[12..14].ToArray()).Should().Be(0x9ABC);
        BitConverter.ToUInt16(data[14..16].ToArray()).Should().Be(0xDEF0);
        BitConverter.ToUInt16(data[16..18].ToArray()).Should().Be(0x1234);
        BitConverter.ToUInt16(data[18..20].ToArray()).Should().Be(0x5678);
        BitConverter.ToUInt16(data[20..22].ToArray()).Should().Be(0x9ABC);
        BitConverter.ToUInt16(data[22..24].ToArray()).Should().Be(0xDEF0);
        BitConverter.ToUInt16(data[24..26].ToArray()).Should().Be(0x1234);
        BitConverter.ToUInt16(data[26..28].ToArray()).Should().Be(0x5678);
        BitConverter.ToUInt16(data[28..30].ToArray()).Should().Be(0xDEF0);
        BitConverter.ToUInt16(data[30..32].ToArray()).Should().Be(0x9ABC);
        data[32].Should().Be(0x12);
        data[33].Should().Be(0x34);
        data[34].Should().Be(0x01);
        data[35].Should().Be(0x01);
        data[36].Should().Be(0x02);
        BitConverter.ToUInt32(data[37..41].ToArray()).Should().Be(1985);
        data[41].Should().Be(0x01);
        data[42].Should().Be(0x02);
        BitConverter.ToUInt16(data[43..45].ToArray()).Should().Be(0x1234);
    }

    [Fact]
    public void Z80Regs_ShouldConvertFromBytes()
    {
        var regsData = GetZ80RegsBlockData();
        using var memoryStream = new MemoryStream(regsData);
        var reader = new ByteStreamReader(memoryStream);

        var regs = Z80RegsBlock.Read(reader, regsData.Length);

        regs.AF.Should().Be(0x1234);
        regs.BC.Should().Be(0x5678);
        regs.DE.Should().Be(0x9ABC);
        regs.HL.Should().Be(0xDEF0);
        regs.AF1.Should().Be(0x1234);
        regs.BC1.Should().Be(0x5678);
        regs.DE1.Should().Be(0x9ABC);
        regs.HL1.Should().Be(0xDEF0);
        regs.IX.Should().Be(0x1234);
        regs.IY.Should().Be(0x5678);
        regs.SP.Should().Be(0xDEF0);
        regs.PC.Should().Be(0x9ABC);
        regs.I.Should().Be(0x12);
        regs.R.Should().Be(0x34);
        regs.IFF1.Should().Be(0x01);
        regs.IFF2.Should().Be(0x01);
        regs.IM.Should().Be(0x02);
        regs.CyclesStart.Should().Be(1985);
        regs.HoldIntReqCycles.Should().Be(0x01);
        regs.Flags.Should().Be(Z80RegsBlock.FlagsHalted);
        regs.MemPtr.Should().Be(0x1234);
    }

    private static byte[] GetZ80RegsBlockData()
    {
        var registers = GetZ80RegsBlock();
        using var writer = new MemoryStream();

        registers.Write(writer);

        return writer.ToArray()[8..].ToArray();
    }

    private static Z80RegsBlock GetZ80RegsBlock() => new()
    {
        AF = 0x1234,
        BC = 0x5678,
        DE = 0x9ABC,
        HL = 0xDEF0,
        AF1 = 0x1234,
        BC1 = 0x5678,
        DE1 = 0x9ABC,
        HL1 = 0xDEF0,
        IX = 0x1234,
        IY = 0x5678,
        SP = 0xDEF0,
        PC = 0x9ABC,
        I = 0x12,
        R = 0x34,
        IFF1 = 0x01,
        IFF2 = 0x01,
        IM = 0x02,
        CyclesStart = 1985,
        HoldIntReqCycles = 0x01,
        Flags = Z80RegsBlock.FlagsHalted,
        MemPtr = 0x1234
    };
}