using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Blocks;

public class ReturnFromSequenceBlockTests
{
    [Fact]
    public void ReturnFromSequenceBlock_ShouldSerializeToBytes()
    {
        var block = new ReturnFromSequenceBlock();

        var serializer = new BlockSerializer();
        var result = serializer.Serialize(block);

        result.Should().Equal(0x27);
    }
}