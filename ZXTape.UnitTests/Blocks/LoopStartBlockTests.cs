using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Blocks;

public class LoopStartBlockTests
{
    [Fact]
    public void LoopStartBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new LoopStartBlock();

        block.Count.Should().Be(0);
    }

    [Fact]
    public void LoopStartBlock_ShouldDeserializeFromStream()
    {
        var bytes = new byte[] { 0x05, 0x00 };
        using var stream = new MemoryStream(bytes);
        var block = new LoopStartBlock(new ByteStreamReader(stream));

        block.Count.Should().Be(5);
    }

    [Fact]
    public void LoopStartBlock_ShouldSerializeToBytes()
    {
        var block = new LoopStartBlock{ Count = 2 };

        var serializer = new BlockSerializer();
        var result = serializer.Serialize(block);

        result.Should().Equal(0x02, 0x00);
    }
}