using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Blocks;
using OldBit.ZXTape.Szx.Serialization;

namespace OldBit.ZXTape.UnitTests.Szx.Blocks;

public class ZxPrinterBlockTests
{
    [Fact]
    public void ZXPrinter_ShouldConvertToBytes()
    {
        var printer = GetZxPrinterBlock();
        var writer = new ByteWriter();

        printer.Write(writer);

        var data = writer.GetData();
        data.Length.Should().Be(8 + 2);

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x5250585A);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(2);

        // Data
        BitConverter.ToUInt16(data[8..10].ToArray()).Should().Be(1);
    }

    [Fact]
    public void ZXPrinter_ShouldConvertFromBytes()
    {
        var printerData = GetZxPrinterBlockData();
        using var memoryStream = new MemoryStream(printerData);
        var reader = new ByteStreamReader(memoryStream);

        var printer = ZxPrinterBlock.Read(reader, printerData.Length);

        printer.Flags.Should().Be(ZxPrinterBlock.FlagsEnabled);
    }

    private static byte[] GetZxPrinterBlockData()
    {
        var creator = GetZxPrinterBlock();
        var writer = new ByteWriter();

        creator.Write(writer);
        writer.GetData();

        return writer.GetData()[8..].ToArray();
    }

    private static ZxPrinterBlock GetZxPrinterBlock() => new()
    {
        Flags = ZxPrinterBlock.FlagsEnabled
    };
}