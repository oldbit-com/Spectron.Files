using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Blocks;

public class JumpToBlockTests
{
    [Fact]
    public void JumpToBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new JumpToBlock();

        block.Jump.Should().Be(0);
    }

    [Fact]
    public void JumpToBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([0xE7, 0xFF]);
        var block = new JumpToBlock(new ByteStreamReader(stream));

        block.Jump.Should().Be(-25);
    }

    [Fact]
    public void JumpToBlock_ShouldSerializeToBytes()
    {
        var block = new JumpToBlock { Jump = -1 };

        var serializer = new BlockSerializer();
        var result = serializer.Serialize(block);

        result.Should().Equal(0xFF, 0xFF);
    }
}