using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Szx.Blocks;

namespace OldBit.ZX.Files.UnitTests.Szx.Blocks;

public class AyBlockTests
{
    [Fact]
    public void Ay_ShouldConvertToBytes()
    {
        var ay = GetAyBlock();
        using var writer = new MemoryStream();

        ay.Write(writer);

        var data = writer.ToArray();
        data.Length.Should().Be(8 + 18);

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x00005941);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(18);

        // Data
        data[8].Should().Be(AyBlock.Flags128Ay);
        data[9].Should().Be(0x0F);
        data[10..26].ToArray().Should().BeEquivalentTo(Enumerable.Range(1, 16).Select(i => (byte)i));
    }

    [Fact]
    public void Ay_ShouldConvertFromBytes()
    {
        var ayData = GetAyBlockData();
        using var memoryStream = new MemoryStream(ayData);
        var reader = new ByteStreamReader(memoryStream);

        var ay = AyBlock.Read(reader, ayData.Length);

        ay.Flags.Should().Be(AyBlock.Flags128Ay);
        ay.CurrentRegister.Should().Be(0x0F);
        ay.Registers.Should().BeEquivalentTo(Enumerable.Range(1, 16).Select(i => (byte)i));
    }

    private static byte[] GetAyBlockData()
    {
        var ay = GetAyBlock();
        using var writer = new MemoryStream();

        ay.Write(writer);

        return writer.ToArray()[8..].ToArray();
    }

    private static AyBlock GetAyBlock()
    {
        var ay = new AyBlock
        {
            Flags = AyBlock.Flags128Ay,
            CurrentRegister = 0x0F,
        };

        for (byte i = 1; i <= 16; i++)
        {
            ay.Registers[i - 1] = i;
        }

        return ay;
    }
}