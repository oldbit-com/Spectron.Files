using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.UnitTests.Tzx;

public class CswRecordingBlockTests
{
    [Fact]
    public void CswRecordingBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new CswRecordingBlock();

        block.PauseDuration.Should().Be(0);
        block.CompressionType.Should().Be(CompressionType.Rle);
        block.Data.Count.Should().Be(0);
    }

    [Fact]
    public void CswRecordingBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([
            0x0E, 0x00, 0x00, 0x00, 0x01, 0x02, 0xAA, 0xBB,
            0xCC, 0x02, 0xC1, 0xC2, 0xC3, 0xC4, 0x11, 0x12,
            0x13, 0x14
        ]);
        var block = new CswRecordingBlock(new ByteStreamReader(stream));

        block.PauseDuration.Should().Be(0x0201);
        block.SamplingRate.Should().Be(0xCCBBAA);
        block.CompressionType.Should().Be(CompressionType.ZRle);
        block.StoredPulsesCount.Should().Be(0xC4C3C2C1);
        block.Data.Count.Should().Be(4);
        block.Data[0].Should().Be(0x11);
        block.Data[1].Should().Be(0x12);
        block.Data[2].Should().Be(0x13);
        block.Data[3].Should().Be(0x14);
    }

    [Fact]
    public void CswRecordingBlock_ShouldSerializeToBytes()
    {
        var block = new CswRecordingBlock
        {
            CompressionType = CompressionType.Rle,
            PauseDuration = 1000,
            SamplingRate = 2000,
            StoredPulsesCount = 3000,
            Data = [0x01, 0x02, 0x03, 0x04]
        };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x18, 0x0E, 0x00, 0x00, 0x00, 0xE8, 0x03,
            0xD0, 0x07, 0x00, 0x01, 0xB8, 0x0B, 0x00, 0x00, 0x01, 0x02, 0x03, 0x04);
    }
}