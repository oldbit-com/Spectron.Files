using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Blocks;

public class GroupEndBlockTests
{
    [Fact]
    public void GroupEndBlock_ShouldSerializeToBytes()
    {
        var block = new GroupEndBlock();

        var serializer = new BlockSerializer();
        var result = serializer.Serialize(block);

        result.Should().Equal(0x22);
    }
}