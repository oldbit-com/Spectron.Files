using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.Tests.Tzx;

public class TextDescriptionBlockTests
{
    [Fact]
    public void TextDescriptionBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new TextDescriptionBlock();

        block.Description.Should().BeEmpty();
    }

    [Fact]
    public void TextDescriptionBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([0x05, 0x5A, 0x58, 0x34, 0x38, 0x6B]);
        var block = new TextDescriptionBlock(new ByteStreamReader(stream));

        block.Description.Should().Be("ZX48k");
    }

    [Fact]
    public void TextDescriptionBlock_ShouldSerializeToBytes()
    {
        var block = new TextDescriptionBlock
        {
            Description = "description"
        };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x30, 0x0B, 0x64, 0x65, 0x73, 0x63, 0x72, 0x69,
            0x70, 0x74, 0x69, 0x6f, 0x6e);
    }
}