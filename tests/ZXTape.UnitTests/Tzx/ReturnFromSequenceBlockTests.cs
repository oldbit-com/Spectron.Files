using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class ReturnFromSequenceBlockTests
{
    [Fact]
    public void ReturnFromSequenceBlock_ShouldSerializeToBytes()
    {
        var block = new ReturnFromSequenceBlock();

        var result = BlockSerializer.Serialize(block);

        result.Should().Equal(0x27);
    }
}