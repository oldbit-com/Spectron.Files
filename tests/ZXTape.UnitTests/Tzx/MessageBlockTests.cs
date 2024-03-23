using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class MessageBlockTests
{
    [Fact]
    public void MessageBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new MessageBlock();

        block.Time.Should().Be(0);
        block.Message.Should().BeEmpty();
    }

    [Fact]
    public void MessageBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([
            0x0A, 0x08, 0x53, 0x69, 0x6E, 0x63, 0x6C, 0x61, 0x69, 0x72
        ]);
        var block = new MessageBlock(new ByteStreamReader(stream));

        block.Time.Should().Be(10);
        block.Message.Should().Be("Sinclair");
    }

    [Fact]
    public void MessageBlock_ShouldSerializeToBytes()
    {
        var block = new MessageBlock
        {
            Time = 20,
            Message = "message"
        };

        var result = BlockSerializer.Serialize(block);

        result.Should().Equal(0x31, 0x14, 0x07, 0x6d, 0x65, 0x73, 0x73, 0x61, 0x67, 0x65);
    }
}