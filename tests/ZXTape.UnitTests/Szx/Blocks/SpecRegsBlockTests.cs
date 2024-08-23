using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Blocks;
using OldBit.ZXTape.Szx.Serialization;

namespace OldBit.ZXTape.UnitTests.Szx.Blocks;

public class SpecRegsBlockTests
{
    [Fact]
    public void SpecRegs_ShouldConvertToBytes()
    {
        var regs = GetSpecRegsBlock();
        var writer = new ByteWriter();

        regs.Write(writer);

        var data = writer.GetData();
        data.Length.Should().Be(8 + 8);

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x52435053);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(8);

        // Data
        data[8].Should().Be(1);
        data[9].Should().Be(2);
        data[10].Should().Be(3);
        data[11].Should().Be(4);
        data[12..16].ToArray().Should().BeEquivalentTo(new byte[] { 0, 0, 0, 0 });
    }

    [Fact]
    public void SpecRegs_ShouldConvertFromBytes()
    {
        var regsData = GetSpecRegsBlockData();
        using var memoryStream = new MemoryStream(regsData);
        var reader = new ByteStreamReader(memoryStream);

        var regs = SpecRegsBlock.Read(reader, regsData.Length);

        regs.Border.Should().Be(1);
        regs.Port7FFD.Should().Be(2);
        regs.Port1FFD.Should().Be(3);
        regs.PortFE.Should().Be(4);
        regs.Reserved.Should().BeEquivalentTo(new byte[] { 0, 0, 0, 0 });
    }

    private static byte[] GetSpecRegsBlockData()
    {
        var registers = GetSpecRegsBlock();
        var writer = new ByteWriter();

        registers.Write(writer);
        writer.GetData();

        return writer.GetData()[8..].ToArray();
    }

    private static SpecRegsBlock GetSpecRegsBlock() => new()
    {
        Border = 1,
        Port7FFD = 2,
        Port1FFD = 3,
        PortFE = 4,
    };
}