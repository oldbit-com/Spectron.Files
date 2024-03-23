using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class CallSequenceBlockTests
{
    [Fact]
    public void CallSequenceBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new CallSequenceBlock();

        block.Offsets.Count.Should().Be(0);
    }

    [Fact]
    public void CallSequenceBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([
            0x02, 0x00, 0x04, 0xFF, 0x08, 0x00
        ]);
        var block = new CallSequenceBlock(new ByteStreamReader(stream));

        block.Offsets[0].Should().Be(-252);
        block.Offsets[1].Should().Be(8);
    }

    [Fact]
    public void CallSequenceBlock_ShouldSerializeToBytes()
    {
        var block = new CallSequenceBlock();
        block.Offsets.Add(-10);
        block.Offsets.Add(10);

        var result = BlockSerializer.Serialize(block);

        result.Should().Equal(0x26, 0x02, 0x00, 0xF6, 0xFF, 0x0A, 0x00);
    }
}