using System.IO.Compression;
using OldBit.Spectron.Files.IO;
using OldBit.Spectron.Files.Szx;
using OldBit.Spectron.Files.Szx.Blocks;

namespace OldBit.Spectron.Files.Tests.Szx.Blocks;

public class Beta128BlockTests
{
    private readonly byte[] _customRom;

    public Beta128BlockTests()
    {
        var random = new Random(1982);
        _customRom = Enumerable.Range(0, 16384).Select(_ => (byte)random.Next(8, 16)).ToArray();
    }

    [Fact]
    public void Beta128_ShouldConvertToBytes()
    {
        var beta128 = GetBeta128Block();
        using var writer = new MemoryStream();

        beta128.Write(writer);

        var data = writer.ToArray();

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).ShouldBe(0x38323142);
        BitConverter.ToUInt32(data[4..8].ToArray()).ShouldBe(10);

        // Properties
        BitConverter.ToUInt32(data[8..12].ToArray()).ShouldBe(5);
        data[12].ShouldBe(2);       // NumDrives
        data[13].ShouldBe(222);     // SysReg
        data[14].ShouldBe(11);      // TrackReg
        data[15].ShouldBe(1);       // SectorReg
        data[16].ShouldBe(12);      // DataReg
        data[17].ShouldBe(129);     // StatusReg
    }

    [Fact]
    public void Beta128_WithCustomRom_ShouldConvertToBytes()
    {
        var beta128 = GetBeta128Block(_customRom);
        using var writer = new MemoryStream();

        beta128.Write(writer);

        var data = writer.ToArray();

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).ShouldBe(0x38323142);
        BitConverter.ToUInt32(data[4..8].ToArray()).ShouldBe(7551);

        // Properties
        BitConverter.ToUInt32(data[8..12].ToArray()).ShouldBe(39);
        data[12].ShouldBe(2);       // NumDrives
        data[13].ShouldBe(222);     // SysReg
        data[14].ShouldBe(11);      // TrackReg
        data[15].ShouldBe(1);       // SectorReg
        data[16].ShouldBe(12);      // DataReg
        data[17].ShouldBe(129);     // StatusReg

        // Custom ROM
        ZLibHelper.Decompress(data[18..]).ShouldBe(_customRom);
    }

    [Fact]
    public void Beta128_ShouldConvertFromBytes()
    {
        var beta128Data = GetBeta128BlockData();
        using var memoryStream = new MemoryStream(beta128Data);
        var reader = new ByteStreamReader(memoryStream);

        var beta128 = Beta128Block.Read(reader, beta128Data.Length);

        beta128.Flags.ShouldBe(Beta128Block.FlagsConnected | Beta128Block.FlagsPaged);
        beta128.DataReg.ShouldBe(12);
        beta128.NumDrives.ShouldBe(2);
        beta128.SectorReg.ShouldBe(1);
        beta128.SysReg.ShouldBe(222);
        beta128.TrackReg.ShouldBe(11);
        beta128.RomData.ShouldBeNull();
    }

    [Fact]
    public void Beta128_WithCustomRom_ShouldConvertFromBytes()
    {
        var beta128Data = GetBeta128BlockData(_customRom);
        using var memoryStream = new MemoryStream(beta128Data);
        var reader = new ByteStreamReader(memoryStream);

        var beta128 = Beta128Block.Read(reader, beta128Data.Length);

        beta128.Flags.ShouldBe(Beta128Block.FlagsConnected | Beta128Block.FlagsPaged | Beta128Block.FlagsCompressed | Beta128Block.FlagsCustomRom);
        beta128.DataReg.ShouldBe(12);
        beta128.NumDrives.ShouldBe(2);
        beta128.SectorReg.ShouldBe(1);
        beta128.SysReg.ShouldBe(222);
        beta128.TrackReg.ShouldBe(11);
        beta128.RomData.ShouldBe(_customRom);
    }

    private static byte[] GetBeta128BlockData(byte[]? customRom = null)
    {
        var beta128 = GetBeta128Block(customRom);
        using var writer = new MemoryStream();

        beta128.Write(writer);

        return writer.ToArray()[8..].ToArray();
    }

    private static Beta128Block GetBeta128Block(byte[]? customRom = null)
    {
        var beta128 = new Beta128Block(
            Beta128Block.FlagsConnected | Beta128Block.FlagsPaged, customRom, CompressionLevel.Optimal)
        {
            DataReg = 12,
            NumDrives = 2,
            SectorReg = 1,
            StatusReg = 129,
            SysReg = 222,
            TrackReg = 11,
        };

        return beta128;
    }
}