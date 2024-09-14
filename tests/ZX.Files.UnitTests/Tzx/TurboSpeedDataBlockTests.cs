using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.UnitTests.Tzx;

public class TurboSpeedDataBlockTests
{
    [Fact]
    public void TurboSpeedDataBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new TurboSpeedDataBlock();

        block.PilotPulseLength.Should().Be(0);
        block.FirstSyncPulseLength.Should().Be(0);
        block.SecondSyncPulseLength.Should().Be(0);
        block.ZeroBitPulseLength.Should().Be(0);
        block.OneBitPulseLength.Should().Be(0);
        block.PilotToneLength.Should().Be(0);
        block.UsedBitsLastByte.Should().Be(0);
        block.PauseDuration.Should().Be(0);
        block.Data.Count.Should().Be(0);
    }

    [Fact]
    public void TurboSpeedDataBlock_ShouldDeserializeFromStream()
    {
        var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x03, 0x00, 0x00, 0xA0, 0xA1, 0xA2 };
        using var stream = new MemoryStream(bytes);
        var block = new TurboSpeedDataBlock(new ByteStreamReader(stream));

        block.PilotPulseLength.Should().Be(0x0201);
        block.FirstSyncPulseLength.Should().Be(0x0403);
        block.SecondSyncPulseLength.Should().Be(0x0605);
        block.ZeroBitPulseLength.Should().Be(0x0807);
        block.OneBitPulseLength.Should().Be(0x0A09);
        block.PilotToneLength.Should().Be(0x0C0B);
        block.UsedBitsLastByte.Should().Be(0x0D);
        block.Data.Count.Should().Be(3);
        block.Data[0].Should().Be(0xA0);
        block.Data[1].Should().Be(0xA1);
        block.Data[2].Should().Be(0xA2);
    }

    [Fact]
    public void TurboSpeedDataBlock_ShouldSerializeToBytes()
    {
        var block = new TurboSpeedDataBlock
        {
            PilotPulseLength = 1000,
            FirstSyncPulseLength = 2000,
            SecondSyncPulseLength = 3000,
            ZeroBitPulseLength = 4000,
            OneBitPulseLength = 5000,
            PilotToneLength = 6000,
            UsedBitsLastByte = 5,
            PauseDuration = 7000,
            Data = [0x01, 0x02, 0x03, 0x04]
        };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x11,0xE8, 0x03, 0xD0, 0x07, 0xB8, 0x0B, 0xA0, 0x0F, 0x88,
            0x13, 0x70, 0x17, 0x05, 0x58, 0x1B, 0x04, 0x00, 0x00, 0x01, 0x02, 0x03, 0x04);
    }
}