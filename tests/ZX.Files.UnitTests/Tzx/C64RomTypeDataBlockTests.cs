using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.UnitTests.Tzx;

public class C64RomTypeDataBlockTests
{
    [Fact]
    public void C64RomTypeDataBlock_ShouldDeserializeFromStream()
    {
        var data = new byte[]
        {
            0xEE, 0x00, 0x00, 0x00, 0x68, 0x02, 0x00, 0x6A, 0x98, 0x04, 0x80, 0x03, 0x68,
            0x02, 0x80, 0x03, 0x80, 0x03, 0x68, 0x02, 0x01, 0x98, 0x04, 0x80, 0x03, 0x98,
            0x04, 0x68, 0x02, 0x68, 0x02, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0xCA, 0x00,
            0x00, 0x89, 0x88, 0x87, 0x86, 0x85, 0x84, 0x83, 0x82, 0x81, 0x01, 0x01, 0x08,
            0xF0, 0x9F, 0x54, 0x49, 0x54, 0x41, 0x4E, 0x49, 0x43
        };

        var newData = new byte[0xEE + 4]; // block length + first 4 bytes
        Array.Copy(data, newData, data.Length);
        for (var i = data.Length; i < newData.Length; i++)
        {
            newData[i] = 0x20;
        }
        data = newData;

        using var stream = new MemoryStream(data);
        var block = new C64RomTypeDataBlock(new ByteStreamReader(stream));

        block.Length.Should().Be(0xEE);
        block.PilotTonePulseLength.Should().Be(616);
        block.NumberOfWavesInPilotTone.Should().Be(27136);
        block.FirstWaveSyncPulseLength.Should().Be(1176);
        block.SecondWaveSyncPulseLength.Should().Be(896);
        block.ZeroBitFirstWavePulseLength.Should().Be(616);
        block.ZeroBitSecondWavePulseLength.Should().Be(896);
        block.OneBitFirstWavePulseLength.Should().Be(896);
        block.OneBitSecondWavePulseLength.Should().Be(616);
        block.Checksum.Should().Be(1);
        block.FinishByteFirstWavePulseLength.Should().Be(1176);
        block.FinishByteSecondWavePulseLength.Should().Be(896);
        block.FinishDataFirstWavePulseLength.Should().Be(1176);
        block.FinishDataSecondWavePulseLength.Should().Be(616);
        block.TrailingTonePulseLength.Should().Be(616);
        block.NumberOfWavesInTrailingTone.Should().Be(0);
        block.UsedBitsLastByte.Should().Be(8);
        block.GeneralPurpose.Should().Be(0);
        block.PauseDuration.Should().Be(0);
        block.DataLength.Should().Be(202);
        block.Data.Should().StartWith([
            0x89, 0x88, 0x87, 0x86, 0x85, 0x84, 0x83, 0x82, 0x81, 0x01, 0x01, 0x08,
            0xF0, 0x9F, 0x54, 0x49, 0x54, 0x41, 0x4E, 0x49, 0x43, 0x20, 0x20, 0x20
        ]);
    }

    [Fact]
    public void CC64RomTypeDataBlock__ShouldSerializeToBytes()
    {
        var block = new C64RomTypeDataBlock
        {
            PilotTonePulseLength = 616,
            NumberOfWavesInPilotTone = 27136,
            FirstWaveSyncPulseLength = 1176,
            SecondWaveSyncPulseLength = 896,
            ZeroBitFirstWavePulseLength = 616,
            ZeroBitSecondWavePulseLength = 896,
            OneBitFirstWavePulseLength = 896,
            OneBitSecondWavePulseLength = 616,
            Checksum = 1,
            FinishByteFirstWavePulseLength = 1176,
            FinishByteSecondWavePulseLength = 896,
            FinishDataFirstWavePulseLength = 1176,
            FinishDataSecondWavePulseLength = 616,
            TrailingTonePulseLength = 616,
            NumberOfWavesInTrailingTone = 0,
            UsedBitsLastByte = 8,
            GeneralPurpose = 0,
            PauseDuration = 0,
            Data =
            [
                0x89, 0x88, 0x87, 0x86, 0x85, 0x84, 0x83, 0x82, 0x81, 0x01, 0x01, 0x08,
                0xF0, 0x9F, 0x54, 0x49, 0x54, 0x41, 0x4E, 0x49, 0x43, 0x20, 0x20, 0x20
            ]
        };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(
            0x16,
            0x3C, 0x00, 0x00, 0x00, 0x68, 0x02, 0x00, 0x6A, 0x98, 0x04, 0x80, 0x03, 0x68,
            0x02, 0x80, 0x03, 0x80, 0x03, 0x68, 0x02, 0x01, 0x98, 0x04, 0x80, 0x03, 0x98,
            0x04, 0x68, 0x02, 0x68, 0x02, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x18, 0x00,
            0x00, 0x89, 0x88, 0x87, 0x86, 0x85, 0x84, 0x83, 0x82, 0x81, 0x01, 0x01, 0x08,
            0xF0, 0x9F, 0x54, 0x49, 0x54, 0x41, 0x4E, 0x49, 0x43, 0x20, 0x20, 0x20);
    }
}