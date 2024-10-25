using OldBit.Spectron.Files.Serialization;
using OldBit.Spectron.Files.Tzx.Blocks;

namespace OldBit.Spectron.Files.Tests.Tzx;

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