using OldBit.Spectron.Files.IO;
using OldBit.Spectron.Files.Serialization;
using OldBit.Spectron.Files.Tzx.Blocks;

namespace OldBit.Spectron.Files.Tests.Tzx;

public class HardwareTypeBlockTests
{
    [Fact]
    public void HardwareTypeBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new HardwareTypeBlock();

        block.Infos.Count.Should().Be(0);
    }

    [Fact]
    public void HardwareTypeBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([
            0x03, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09
        ]);
        var block = new HardwareTypeBlock(new ByteStreamReader(stream));

        block.Infos.Count.Should().Be(3);
        block.Infos[0].HardwareTypeId.Should().Be(0x01);
        block.Infos[0].HardwareId.Should().Be(0x02);
        block.Infos[0].Info.Should().Be(0x03);
        block.Infos[1].HardwareTypeId.Should().Be(0x04);
        block.Infos[1].HardwareId.Should().Be(0x05);
        block.Infos[1].Info.Should().Be(0x06);
        block.Infos[2].HardwareTypeId.Should().Be(0x07);
        block.Infos[2].HardwareId.Should().Be(0x08);
        block.Infos[2].Info.Should().Be(0x09);
    }

    [Fact]
    public void HardwareTypeBlock_ShouldSerializeToBytes()
    {
        var block = new HardwareTypeBlock
        {
            Infos =
            [
                new HardwareTypeBlock.HardwareInfo { HardwareTypeId = 0x01, HardwareId = 0x02, Info = 0x03 },
                new HardwareTypeBlock.HardwareInfo { HardwareTypeId = 0x04, HardwareId = 0x05, Info = 0x06 },
                new HardwareTypeBlock.HardwareInfo { HardwareTypeId = 0x07, HardwareId = 0x08, Info = 0x09 }
            ]
        };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x33, 0x03, 0x01, 0x02, 0x03, 0x04, 0x05,
            0x06, 0x07, 0x08, 0x09);
    }
}