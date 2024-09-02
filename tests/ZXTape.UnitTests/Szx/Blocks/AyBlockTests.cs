using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Blocks;
using OldBit.ZXTape.Szx.Serialization;

namespace OldBit.ZXTape.UnitTests.Szx.Blocks;

public class AyBlockTests
{
    [Fact]
    public void Ay_ShouldConvertToBytes()
    {
        var ay = GetAyBlock();
        var writer = new ByteWriter();

        ay.Write(writer);

        var data = writer.GetData();
        data.Length.Should().Be(8 + 18);

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x00005941);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(8);

        // Data
        data[8].Should().Be(AyBlock.Flags128Ay);
        data[9].Should().Be(0x0F);
        data[10..26].ToArray().Should().BeEquivalentTo(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
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
        ay.Registers.Should().BeEquivalentTo(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
    }

    private static byte[] GetAyBlockData()
    {
        var ay = GetAyBlock();
        var writer = new ByteWriter();

        ay.Write(writer);
        writer.GetData();

        return writer.GetData()[8..].ToArray();
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