using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Szx.Blocks;

namespace OldBit.ZX.Files.Tests.Szx.Blocks;

public class TimexSinclairBlockTests
{
    [Fact]
    public void TimexSinclair_ShouldConvertToBytes()
    {
        var timex = GetTimexSinclairBlock();
        using var writer = new MemoryStream();

        timex.Write(writer);

        var data = writer.ToArray();
        data.Length.Should().Be(8 + 2);

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x444C4353);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(2);

        // Data
        data[8].Should().Be(0xF4);
        data[9].Should().Be(0xFF);
    }

    [Fact]
    public void TimexSinclair_ShouldConvertFromBytes()
    {
        var timexData = GetTimexSinclairBlockData();
        using var memoryStream = new MemoryStream(timexData);
        var reader = new ByteStreamReader(memoryStream);

        var timex = TimexSinclairBlock.Read(reader, timexData.Length);

        timex.PortF4.Should().Be(0xF4);
        timex.PortFF.Should().Be(0xFF);
    }

    private static byte[] GetTimexSinclairBlockData()
    {
        var timex = GetTimexSinclairBlock();
        using var writer = new MemoryStream();

        timex.Write(writer);

        return writer.ToArray()[8..].ToArray();
    }

    private static TimexSinclairBlock GetTimexSinclairBlock() => new()
    {
        PortF4 = 0xF4,
        PortFF = 0xFF
    };
}