using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.UnitTests.Tzx;

public class PulseSequenceBlockTests
{
    [Fact]
    public void PulseSequenceBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new PulseSequenceBlock();

        block.PulseLengths.Count.Should().Be(0);
    }

    [Fact]
    public void PulseSequenceBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([0x03, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F]);
        var block = new PulseSequenceBlock(new ByteStreamReader(stream));

        block.PulseLengths.Count.Should().Be(3);
        block.PulseLengths[0].Should().Be(0x0B0A);
        block.PulseLengths[1].Should().Be(0x0D0C);
        block.PulseLengths[2].Should().Be(0x0F0E);
    }

    [Fact]
    public void PulseSequenceBlock_ShouldSerializeToBytes()
    {
        var block = new PulseSequenceBlock
        {
            PulseLengths = [100, 200, 300]
        };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x13, 0x03, 0x64, 0x00, 0xC8, 0x00, 0x2C, 0x01);
    }
}