using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class CustomInfoBlockTests
{
    [Fact]
    public void CustomInfoBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new CustomInfoBlock();

        block.Identification.Should().BeEmpty();
        block.Info.Count.Should().Be(0);
    }

    [Fact]
    public void CustomInfoBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([
            0x5A, 0x58, 0x53, 0x70, 0x65, 0x63, 0x74, 0x72,
            0x75, 0x6D, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x02, 0x00, 0x00, 0x00, 0x04, 0x05
        ]);
        var block = new CustomInfoBlock(new ByteStreamReader(stream));

        block.Identification.Should().Be("ZXSpectrum");
        block.Info.Count.Should().Be(2);
        block.Info[0].Should().Be(0x04);
        block.Info[1].Should().Be(0x05);
    }

    [Fact]
    public void CustomInfoBlock_ShouldSerializeToBytes()
    {
        var block = new CustomInfoBlock
        {
            Identification = "CustomInfo",
            Info = [0x01, 0x02, 0x03, 0x04]
        };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x35, 0x43, 0x75, 0x73, 0x74, 0x6f, 0x6d, 0x49,
            0x6e, 0x66, 0x6f, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x04, 0x00,
            0x00, 0x00, 0x01, 0x02, 0x03, 0x04);
    }
}