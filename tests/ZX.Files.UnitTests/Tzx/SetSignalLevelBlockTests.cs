using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.UnitTests.Tzx;

public class SetSignalLevelBlockTests
{
    [Fact]
    public void SetSignalLevelBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new SetSignalLevelBlock();

        block.Level.Should().Be(SignalLevel.Low);
    }

    [Fact]
    public void SetSignalLevelBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([0x01, 0x00, 0x00, 0x00, 0x01]);
        var block = new SetSignalLevelBlock(new ByteStreamReader(stream));

        block.Level.Should().Be(SignalLevel.High);
    }

    [Fact]
    public void SetSignalLevelBlock_ShouldSerializeToBytes()
    {
        var block = new SetSignalLevelBlock{ Level = SignalLevel.High };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x2B, 0x01, 0x00, 0x00, 0x00, 0x01);
    }
}