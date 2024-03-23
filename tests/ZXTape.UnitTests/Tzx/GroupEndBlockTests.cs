using OldBit.ZXTape.Serialization;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class GroupEndBlockTests
{
    [Fact]
    public void GroupEndBlock_ShouldSerializeToBytes()
    {
        var block = new GroupEndBlock();

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x22);
    }
}