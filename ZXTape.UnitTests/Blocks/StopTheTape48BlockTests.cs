using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Blocks;

public class StopTheTape48BlockTests
{
    [Fact]
    public void StopTheTape48Block_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([0x00, 0x00, 0x00, 0x00]);
        var reader = new ByteStreamReader(stream);
        var block = new StopTheTape48Block(reader);

        Assert.Throws<EndOfStreamException>(() => reader.ReadByte());
    }

    [Fact]
    public void StopTheTape48Block_ShouldSerializeToBytes()
    {
        var block = new StopTheTape48Block();

        var serializer = new BlockSerializer();
        var result = serializer.Serialize(block);

        result.Should().Equal(0x00, 0x00, 0x00, 0x00);
    }
}