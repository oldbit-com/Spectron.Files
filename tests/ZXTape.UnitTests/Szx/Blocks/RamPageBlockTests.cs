using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx;
using OldBit.ZXTape.Szx.Blocks;

namespace OldBit.ZXTape.UnitTests.Szx.Blocks;

public class RamPageBlockTests
{
     private readonly byte[] _ramData;

    public RamPageBlockTests()
    {
        var random = new Random(1982);
        _ramData = Enumerable.Range(0, 16384).Select(_ => (byte)random.Next(8, 16)).ToArray();
    }

    [Theory]
    [InlineData(true, 7544)]
    [InlineData(false, 16387)]
    public void RamPage_ShouldConvertToBytes(bool isCompressed, DWord expectedSize)
    {
        var ramPage = GetRamPageBlock(isCompressed);
        using var writer = new MemoryStream();

        ramPage.Write(writer);

        var data = writer.ToArray();
        data.Length.Should().Be((int)(8 + expectedSize));

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x504D4152);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(expectedSize);

        // Data
        BitConverter.ToUInt16(data[8..10].ToArray()).Should().Be((Word)(isCompressed ? RamPageBlock.FlagsCompressed : 0));
        data[10].Should().Be(5);
        if (!isCompressed)
        {
            data[11..].ToArray().Should().BeEquivalentTo(_ramData);
        }
        else
        {
            var compressedData = ZLibHelper.Compress(_ramData);
            data[11..].ToArray().Should().BeEquivalentTo(compressedData);
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void  RamPage_ShouldConvertFromBytes(bool compress)
    {
        var ramPageData = GetRamPageBlockData(compress);
        using var memoryStream = new MemoryStream(ramPageData);
        var reader = new ByteStreamReader(memoryStream);

        var ramPage = RamPageBlock.Read(reader, ramPageData.Length);

        ramPage.Flags.Should().Be((Word)(compress ? RamPageBlock.FlagsCompressed : 0));
        ramPage.PageNumber.Should().Be(5);
        ramPage.Data.Should().BeEquivalentTo(_ramData);
    }

    private byte[] GetRamPageBlockData(bool compress)
    {
        var ramPage = GetRamPageBlock(compress);
        using var writer = new MemoryStream();

        ramPage.Write(writer);

        return writer.ToArray()[8..].ToArray();
    }

    private RamPageBlock GetRamPageBlock(bool isCompressed) => new(_ramData, 5, isCompressed);
}