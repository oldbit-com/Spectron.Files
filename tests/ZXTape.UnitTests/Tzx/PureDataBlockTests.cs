using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class PureDataBlockTests
{
    [Fact]
    public void PureDataBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new PureDataBlock();

        block.ZeroBitPulseLength.Should().Be(0);
        block.OneBitPulseLength.Should().Be(0);
        block.UsedBitsLastByte.Should().Be(0);
        block.PauseDuration.Should().Be(0);
        block.Data.Count.Should().Be(0);
    }

    [Fact]
    public void PureDataBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([
            0x07, 0x08, 0x09, 0x0A, 0x0D, 0x0E, 0x0F, 0x03, 0x00, 0x00, 0xA0, 0xA1, 0xA2
        ]);
        var block = new PureDataBlock(new ByteStreamReader(stream));

        block.ZeroBitPulseLength.Should().Be(0x0807);
        block.OneBitPulseLength.Should().Be(0x0A09);
        block.UsedBitsLastByte.Should().Be(0x0D);
        block.Data.Count.Should().Be(3);
        block.Data[0].Should().Be(0xA0);
        block.Data[1].Should().Be(0xA1);
        block.Data[2].Should().Be(0xA2);
    }

    [Fact]
    public void PureDataBlock_ShouldSerializeToBytes()
    {
        var block = new PureDataBlock
        {
            ZeroBitPulseLength = 1000,
            OneBitPulseLength = 2000,
            UsedBitsLastByte = 1,
            PauseDuration = 3000,
            Data = [0x01, 0x02, 0x03, 0x04]
        };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x14, 0xE8, 0x03, 0xD0, 0x07, 0x01, 0xB8, 0x0B,
            0x04, 0x00, 0x00, 0x01, 0x02, 0x03, 0x04);
    }
}