using System.Text;
using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Blocks;
using OldBit.ZXTape.Szx.Serialization;

namespace OldBit.ZXTape.UnitTests.Szx.Blocks;

public class CreatorBlockTests
{
    [Fact]
    public void Creator_ShouldConvertToBytes()
    {
        var creator = GetCreatorBlock();
        var writer = new ByteWriter();

        creator.Write(writer);

        var data = writer.GetData();
        data.Length.Should().Be(8 + 37);

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x52545243);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(37);

        // Data
        Encoding.ASCII.GetString(data[8..40]).Trim('\0').Should().Be("Test creator");
        BitConverter.ToUInt16(data[40..42].ToArray()).Should().Be(1);
        BitConverter.ToUInt16(data[42..44].ToArray()).Should().Be(2);
        data[44].Should().Be(0);
    }

    [Fact]
    public void Creator_ShouldConvertFromBytes()
    {
        var creatorData = GetCreatorBlockData();
        using var memoryStream = new MemoryStream(creatorData);
        var reader = new ByteStreamReader(memoryStream);

        var creator = CreatorBlock.Read(reader, creatorData.Length);

        creator.Name.Should().Be("Test creator");
        creator.MajorVersion.Should().Be(1);
        creator.MinorVersion.Should().Be(2);
    }

    private static byte[] GetCreatorBlockData()
    {
        var creator = GetCreatorBlock();
        var writer = new ByteWriter();

        creator.Write(writer);
        writer.GetData();

        return writer.GetData()[8..].ToArray();
    }

    private static CreatorBlock GetCreatorBlock() => new()
    {
        Name = "Test creator",
        MajorVersion = 1,
        MinorVersion = 2,
    };
}