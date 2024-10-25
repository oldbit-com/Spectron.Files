using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.Tests.Tzx;

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