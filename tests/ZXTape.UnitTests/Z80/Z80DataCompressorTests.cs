using OldBit.ZXTape.Z80;

namespace OldBit.ZXTape.UnitTests.Z80;

public class Z80DataCompressorTests
{
    [Theory]
    [InlineData(new byte[] { 0xED, 0, 0, 0, 0, 0, 0 }, new byte[] { 0xED, 0, 0xED, 0xED, 5, 0, 0, 0xED, 0xED, 0 })]
    [InlineData(new byte[] { 0xED }, new byte[] { 0xED, 0, 0xED, 0xED, 0 })]
    public void Compress_ReturnsCompressedData(byte[] inputData, byte[] expectedData)
    {
        var compressedData = Z80DataCompressor.Compress(inputData);

        compressedData.Should().Equal(expectedData);
    }

    [Theory]
    [InlineData(new byte[] { 0xED, 0, 0xED, 0xED, 5, 0, 0, 0xED, 0xED, 0 }, new byte[] { 0xED, 0, 0, 0, 0, 0, 0 })]
    [InlineData(new byte[] { 0xED, 0, 0xED, 0xED, 0 }, new byte[] { 0xED })]
    public void Decompress_ReturnsDecompressedData(byte[] compressedData, byte[] expectedData)
    {
        var decompressedData = Z80DataCompressor.Decompress(compressedData);

        decompressedData.Should().Equal(expectedData);
    }
}