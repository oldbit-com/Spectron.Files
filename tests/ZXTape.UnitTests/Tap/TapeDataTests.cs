using OldBit.ZXTape.Tap;

namespace OldBit.ZXTape.UnitTests.Tap;

public class TapeDataTests
{
    [Fact]
    public void TryParse_WhenValidData_ReturnsTrueAndValidTapeData()
    {
        byte[] validData = [0xFF, 0x01, 0x02, 0x03, 0x55];

        var result = TapeData.TryParse(validData, out TapeData? tapeData);

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

        var result = TapeData.TryParse(invalidData, out TapeData? tapeData);

        result.Should().BeFalse();
        tapeData.Should().BeNull();
    }

    [Fact]
    public void CalculateChecksum_ReturnsValidChecksum()
    {
        TapeData.TryParse([0x00, 0x03, 0x74, 0x65, 0x73, 0x74, 0x66, 0x69,
            0x6C, 0x65, 0x20, 0x20, 0x68, 0x02, 0x00, 0x00, 0xAC, 0x80, 0x55], out var tapeData);

        var checksum = tapeData!.CalculateChecksum();

        checksum.Should().Be(0x55);
    }
}