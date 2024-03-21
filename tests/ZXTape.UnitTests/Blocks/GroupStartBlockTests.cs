using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Blocks;

public class GroupStartBlockTests
{
    [Fact]
    public void GroupStartBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new GroupStartBlock();

        block.Name.Should().BeEmpty();
    }

    [Fact]
    public void GroupStartBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([
            0x08, 0x53, 0x70, 0x65, 0x63, 0x74, 0x72, 0x75, 0x6D
        ]);
        var block = new GroupStartBlock(new ByteStreamReader(stream));

        block.Name.Should().Be("Spectrum");
    }

    [Fact]
    public void GroupStartBlock_ShouldSerializeToBytes()
    {
        var block = new GroupStartBlock
        {
            Name = "Block description"
        };

        var serializer = new BlockSerializer();
        var result = serializer.Serialize(block);

        result.Should().Equal(0x11, 0x42, 0x6c, 0x6f, 0x63, 0x6b, 0x20, 0x64, 0x65,
            0x73, 0x63, 0x72, 0x69, 0x70, 0x74, 0x69, 0x6f, 0x6e);
    }
}