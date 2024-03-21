using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Blocks;

public class PauseBlockTests
{
    [Fact]
    public void PauseBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new PauseBlock();

        block.Duration.Should().Be(0);
    }

    [Fact]
    public void PauseBlock_ShouldDeserializeFromStream()
    {
        var bytes = new byte[] { 0x01, 0x02 };
        using var stream = new MemoryStream(bytes);
        var block = new PauseBlock(new ByteStreamReader(stream));

        block.Duration.Should().Be(0x0201);
    }

    [Fact]
    public void PauseBlock_ShouldSerializeToBytes()
    {
        var block = new PauseBlock{ Duration = 1234 };

        var serializer = new BlockSerializer();
        var result = serializer.Serialize(block);

        result.Should().Equal(0xD2, 0x04);
    }
}