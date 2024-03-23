using OldBit.ZXTape.Serialization;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class LoopEndBlockTests
{
    [Fact]
    public void LoopEndBlock_ShouldSerializeToBytes()
    {
        var block = new LoopEndBlock();

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x25);
    }
}