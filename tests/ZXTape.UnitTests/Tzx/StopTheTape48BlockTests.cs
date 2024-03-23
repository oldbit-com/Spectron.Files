using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.UnitTests.Tzx;

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

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x2A, 0x00, 0x00, 0x00, 0x00);
    }
}