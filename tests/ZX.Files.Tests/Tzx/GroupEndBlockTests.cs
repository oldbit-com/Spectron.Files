using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.Tests.Tzx;

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