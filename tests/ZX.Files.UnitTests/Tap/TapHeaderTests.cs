using OldBit.ZX.Files.Tap;

namespace OldBit.ZX.Files.UnitTests.Tap;

public class TapHeaderTests
{
    [Fact]
    public void TryParse_WhenDataIsValid_ReturnsTrueAndValidHeaderData()
    {
        byte[] validData =
        [
            0x03, 0x74, 0x65, 0x73, 0x74, 0x66, 0x69, 0x6C, 0x65,
            0x20, 0x20, 0x68, 0x02, 0x00, 0x00, 0xAC, 0x80
        ];

        var result = TapHeader.TryParse(validData, out var tapeHeader);

        result.Should().BeTrue();
        tapeHeader.Should().NotBeNull();
        tapeHeader!.DataType.Should().Be(HeaderDataType.Code);
        tapeHeader!.FileName.Should().Be("testfile  ");
        tapeHeader!.DataLength.Should().Be(616);
        tapeHeader!.Parameter1.Should().Be(0);
        tapeHeader!.Parameter2.Should().Be(32940);
    }

    [Fact]
    public void TryParse_WhenDataIsInvalid_ReturnsFalseAndNullHeaderData()
    {
        byte[] invalidData = [0x03, 0x74, 0x65];

        var result = TapHeader.TryParse(invalidData, out var tapeHeader);

        result.Should().BeFalse();
        tapeHeader.Should().BeNull();
    }
}