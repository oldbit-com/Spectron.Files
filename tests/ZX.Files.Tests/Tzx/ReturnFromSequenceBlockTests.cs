using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.Tests.Tzx;

public class ReturnFromSequenceBlockTests
{
    [Fact]
    public void ReturnFromSequenceBlock_ShouldSerializeToBytes()
    {
        var block = new ReturnFromSequenceBlock();

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x27);
    }
}