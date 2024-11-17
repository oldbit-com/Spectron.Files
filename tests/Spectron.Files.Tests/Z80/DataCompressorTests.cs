using OldBit.Spectron.Files.Z80;

namespace OldBit.Spectron.Files.Tests.Z80;

public class DataCompressorTests
{
    [Theory]
    [InlineData(new byte[] { 0xED, 0, 0, 0, 0, 0, 0 }, new byte[] { 0xED, 0, 0xED, 0xED, 5, 0, 0, 0xED, 0xED, 0 }, true)]
    [InlineData(new byte[] { 0xED }, new byte[] { 0xED, 0, 0xED, 0xED, 0 }, true)]
    [InlineData(new byte[] { 0xED }, new byte[] { 0xED }, false)]
    [InlineData(new byte[] { 0xED, 0xED }, new byte[] { 0xED, 0xED, 2, 0xED }, false)]
    public void Compress_ReturnsCompressedData(byte[] inputData, byte[] expectedData, bool appendEndMarker)
    {
        var compressedData = DataCompressor.Compress(inputData, appendEndMarker);

        compressedData.Should().Equal(expectedData);
    }

    [Theory]
    [InlineData(new byte[] { 0xED, 0, 0xED, 0xED, 5, 0, 0, 0xED, 0xED, 0 }, new byte[] { 0xED, 0, 0, 0, 0, 0, 0 }, true)]
    [InlineData(new byte[] { 0xED, 0, 0xED, 0xED, 0 }, new byte[] { 0xED }, true)]
    [InlineData(new byte[] { 0xED }, new byte[] { 0xED }, false)]
    [InlineData(new byte[] { 0xED, 0xED, 2, 0xED }, new byte[] { 0xED, 0xED }, false)]
    public void Decompress_ReturnsDecompressedData(byte[] compressedData, byte[] expectedData, bool hasEndMarker)
    {
        var decompressedData = DataCompressor.Decompress(compressedData, hasEndMarker);

        decompressedData.Should().Equal(expectedData);
    }

    [Fact]
    public void Compress_ShouldCompressEmptyArray()
    {
        var compressedData = DataCompressor.Compress(new byte[1024], true);

        compressedData.Should().BeEquivalentTo(
        [
            0xED, 0xED, 0xFF, 0x00,
            0xED, 0xED, 0xFF, 0x00,
            0xED, 0xED, 0xFF, 0x00,
            0xED, 0xED, 0xFF ,0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0xED, 0xED, 0x00
        ]);
    }
}