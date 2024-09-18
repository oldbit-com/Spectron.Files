using System.IO.Compression;
using System.Text;
using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Szx;
using OldBit.ZX.Files.Szx.Blocks;

namespace OldBit.ZX.Files.UnitTests.Szx.Blocks;

public class TapeBlockTests
{
private readonly byte[] _tapeData;

    public TapeBlockTests()
    {
        var random = new Random(1970);
        _tapeData = Enumerable.Range(0, 4560).Select(_ => (byte)random.Next(0, 16)).ToArray();
    }

    [Theory]
    [InlineData(true, 2641)]
    [InlineData(false, 4588)]
    public void Tape_ShouldConvertToBytes(bool isCompressed, DWord expectedSize)
    {
        var tape = GetTapeBlock(isCompressed);
        using var writer = new MemoryStream();

        tape.Write(writer);

        var data = writer.ToArray();
        data.Length.Should().Be((int)(8 + expectedSize));

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x45504154);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(expectedSize);

        // Data
        BitConverter.ToUInt16(data[8..10].ToArray()).Should().Be(2);
        BitConverter.ToUInt16(data[10..12].ToArray()).Should().Be((Word)(isCompressed ? TapeBlock.FlagsCompressed + TapeBlock.FlagsEmbedded : TapeBlock.FlagsEmbedded));
        BitConverter.ToUInt32(data[12..16].ToArray()).Should().Be(4560);
        BitConverter.ToUInt32(data[16..20].ToArray()).Should().Be((DWord)(isCompressed ? 2613 : 4560));
        Encoding.ASCII.GetString(data[20..36]).Trim('\0').Should().Be(".tap");
        if (!isCompressed)
        {
            data[36..].ToArray().Should().BeEquivalentTo(_tapeData);
        }
        else
        {
            var compressedData = ZLibHelper.Compress(_tapeData, CompressionLevel.SmallestSize);
            data[36..].ToArray().Should().BeEquivalentTo(compressedData);
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Tape_ShouldConvertFromBytes(bool compress)
    {
        var tapeData = GetTapeBlockData(compress);
        using var memoryStream = new MemoryStream(tapeData);
        var reader = new ByteStreamReader(memoryStream);

        var tape = TapeBlock.Read(reader, tapeData.Length);

        tape.CurrentBlockNo.Should().Be(2);
        tape.Flags.Should().Be((Word)(compress ? TapeBlock.FlagsCompressed + TapeBlock.FlagsEmbedded : TapeBlock.FlagsEmbedded));
        tape.UncompressedSize.Should().Be(4560);
        tape.CompressedSize.Should().Be((Word)(compress ? 2613 : tape.Data.Length));
        tape.FileExtension.Should().Be(".tap");
        tape.Data.Should().BeEquivalentTo(_tapeData);
    }

    private byte[] GetTapeBlockData(bool compress)
    {
        var tape = GetTapeBlock(compress);
        using var writer = new MemoryStream();

        tape.Write(writer);

        return writer.ToArray()[8..].ToArray();
    }

    private TapeBlock GetTapeBlock(bool isCompressed) =>
        new(_tapeData, isCompressed ? CompressionLevel.SmallestSize : CompressionLevel.NoCompression)
        {
            FileExtension = ".tap",
            CurrentBlockNo = 2,
        };
}