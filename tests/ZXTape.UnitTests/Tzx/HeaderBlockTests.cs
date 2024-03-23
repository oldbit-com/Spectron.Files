using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class HeaderBlockTests
{
    [Fact]
    public void HeaderBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new HeaderBlock();

        block.Signature.Should().Be(HeaderBlock.TzxSignature);
        block.EotMarker.Should().Be(HeaderBlock.TzxEotMarker);
        block.VerMajor.Should().Be(HeaderBlock.TzxVerMajor);
        block.VerMinor.Should().Be(HeaderBlock.TzxVerMinor);
    }

    [Fact]
    public void HeaderBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([
            0x5A, 0x58, 0x54, 0x61, 0x70, 0x65, 0x21, 0x1A, 0x01, 0x0A
        ]);
        var block = new HeaderBlock(new ByteStreamReader(stream));

        block.Signature.Should().Be(HeaderBlock.TzxSignature);
        block.EotMarker.Should().Be(HeaderBlock.TzxEotMarker);
        block.VerMajor.Should().Be(HeaderBlock.TzxVerMajor);
        block.VerMinor.Should().Be(0x0A);
    }

    [Fact]
    public void HeaderBlock_ShouldSerializeToBytes()
    {
        var block = new HeaderBlock();

        var result = BlockSerializer.Serialize(block);

        result.Should().Equal(0x5A, 0x58, 0x54, 0x61, 0x70, 0x65, 0x21, 0x1A, 0x01, 0x14);
    }
}