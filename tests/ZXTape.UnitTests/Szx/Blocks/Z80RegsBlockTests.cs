using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Blocks;
using OldBit.ZXTape.Szx.Serialization;

namespace OldBit.ZXTape.UnitTests.Szx.Blocks;

public class Z80RegsBlockTests
{
    [Fact]
    public void Z80Registers_ShouldConvertToBytes()
    {
        var regs = GetZ80RegsBlock();
        var writer = new ByteWriter();

        regs.Write(writer);

        var data = writer.GetData();
        data.Length.Should().Be(8 + 37);

        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x5230385A);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(37);
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
    public void Z80Registers_ShouldConvertFromBytes()
    {
        var registersData = GetZ80RegsBlockData();
        using var memoryStream = new MemoryStream(registersData);
        var reader = new ByteStreamReader(memoryStream);

        var registers = Z80RegsBlock.Read(reader, registersData.Length);

        registers.AF.Should().Be(0x1234);
        registers.BC.Should().Be(0x5678);
        registers.DE.Should().Be(0x9ABC);
        registers.HL.Should().Be(0xDEF0);
        registers.AF1.Should().Be(0x1234);
        registers.BC1.Should().Be(0x5678);
        registers.DE1.Should().Be(0x9ABC);
        registers.HL1.Should().Be(0xDEF0);
        registers.IX.Should().Be(0x1234);
        registers.IY.Should().Be(0x5678);
        registers.SP.Should().Be(0xDEF0);
        registers.PC.Should().Be(0x9ABC);
        registers.I.Should().Be(0x12);
        registers.R.Should().Be(0x34);
        registers.IFF1.Should().Be(0x01);
        registers.IFF2.Should().Be(0x01);
        registers.IM.Should().Be(0x02);
        registers.CyclesStart.Should().Be(1985);
        registers.HoldIntReqCycles.Should().Be(0x01);
        registers.Flags.Should().Be(Z80RegsBlock.FlagsHalted);
        registers.MemPtr.Should().Be(0x1234);
    }

    private static byte[] GetZ80RegsBlockData()
    {
        var registers = GetZ80RegsBlock();
        var writer = new ByteWriter();

        registers.Write(writer);
        writer.GetData();

        return writer.GetData()[8..].ToArray();
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