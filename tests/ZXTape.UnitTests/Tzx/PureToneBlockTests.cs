using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class PureToneBlockTests
{

    [Fact]
    public void PureToneBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new PureToneBlock();

        block.PulseLength.Should().Be(0);
        block.PulseCount.Should().Be(0);
    }

    [Fact]
    public void PureToneBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([0x01, 0x02, 0x02, 0x04]);
        var block = new PureToneBlock(new ByteStreamReader(stream));

        block.PulseLength.Should().Be(0x0201);
        block.PulseCount.Should().Be(0x0402);
    }

    [Fact]
    public void PureToneBlock_ShouldSerializeToBytes()
    {
        var block = new PureToneBlock { PulseCount = 10, PulseLength = 1923 };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x12, 0x83, 0x07, 0x0A, 0x00);
    }
}