using OldBit.Spectron.Files.Tap;

namespace OldBit.Spectron.Files.Tests.Tap;

public class TapDataTests
{
    [Fact]
    public void TryParse_WhenValidData_ReturnsTrueAndValidTapeData()
    {
        byte[] validData = [0xFF, 0x01, 0x02, 0x03, 0x55];

        var result = TapData.TryParse(validData, out TapData? tapeData);

        result.Should().BeTrue();
        tapeData.Should().NotBeNull();
        tapeData!.Flag.Should().Be(0xFF);
        tapeData.Data.Should().BeEquivalentTo(new byte[] { 0x01, 0x02, 0x03 });
        tapeData.Checksum.Should().Be(0x55);
    }

    [Fact]
    public void TryParse_WhenInvalidData_ReturnsFalseAndNullTapeData()
    {
        byte[] invalidData = [0x03];

        var result = TapData.TryParse(invalidData, out TapData? tapeData);

        result.Should().BeFalse();
        tapeData.Should().BeNull();
    }

    [Fact]
    public void CalculateChecksum_ReturnsValidChecksum()
    {
        TapData.TryParse([0x00, 0x03, 0x74, 0x65, 0x73, 0x74, 0x66, 0x69,
            0x6C, 0x65, 0x20, 0x20, 0x68, 0x02, 0x00, 0x00, 0xAC, 0x80, 0x55], out var tapeData);

        var checksum = tapeData!.CalculateChecksum();

        checksum.Should().Be(0x55);
    }
}