using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Blocks;

public class LoopEndBlockTests
{
    [Fact]
    public void LoopEndBlock_ShouldSerializeToBytes()
    {
        var block = new LoopEndBlock();

        var serializer = new BlockSerializer();
        var result = serializer.Serialize(block);

        result.Should().Equal(0x25);
    }
}