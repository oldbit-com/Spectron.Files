using OldBit.ZX.Files.Z80;

namespace OldBit.ZX.Files.UnitTests.Z80;

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
}