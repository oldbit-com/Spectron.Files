using System.IO.Compression;
using OldBit.Spectron.Files.IO;
using OldBit.Spectron.Files.Szx.Extensions;

namespace OldBit.Spectron.Files.Szx.Blocks;

/// <summary>
/// This block ('B','1','2','8') contains the state of the Beta 128 disk drive.
/// </summary>
public sealed class Beta128Block
{
    /// <summary>
    /// The interface is connected and enabled. This is always set for Pentagon and Scorpion machines.
    /// </summary>
    public const DWord FlagsConnected = 1;

    /// <summary>
    /// A custom TR-DOS ROM is installed. The ROM image begins at chRomData.
    /// The default ROM is the last version by Technology Research, version 5.03.
    /// </summary>
    public const DWord FlagsCustomRom = 2;

    /// <summary>
    /// The TR-DOS ROM is currently paged in.
    /// </summary>
    public const DWord FlagsPaged = 4;

    /// <summary>
    /// The Beta 128's Auto boot feature is enabled (48k ZX Spectrum only).
    /// </summary>
    public const DWord FlagsAutoBoot = 8;

    /// <summary>
    /// If set, the WD179x FDC's current seek direction is towards lower cylinder numbers.
    /// Otherwise, it is towards higher ones.
    /// </summary>
    public const DWord FlagsSeekLower = 16;

    /// <summary>
    /// If a custom TR-DOS ROM is embedded in this block, it has been compressed with the Zlib compression library.
    /// </summary>
    public const DWord FlagsCompressed = 32;

    /// <summary>
    /// Gets or sets the various flags.
    /// </summary>
    public DWord Flags { get; private set; }

    /// <summary>
    /// Gets or sets the number of disk drives connected (1-4).
    /// </summary>
    public byte NumDrives { get; set; }

    /// <summary>
    /// Gets or sets the last value written to the Beta 128's system register (port $ff).
    /// </summary>
    public byte SysReg { get; set; }

    /// <summary>
    /// Gets or sets the current value of the WD179x FDC's track register (port $3f).
    /// </summary>
    public byte TrackReg { get; set; }

    /// <summary>
    /// Gets or sets the current value of the WD179x FDC's sector register (port $5f).
    /// </summary>
    public byte SectorReg { get; set; }

    /// <summary>
    /// Gets or sets the current value of the WD179x FDC's data register (port $7f).
    /// </summary>
    public byte DataReg { get; set; }

    /// <summary>
    /// Gets or sets the current value of the WD179x FDC's status register (port $1f).
    /// </summary>
    public byte StatusReg { get; set; }

    /// <summary>
    /// Gets the compressed or uncompressed custom TR-DOS ROM (if one was installed).
    /// The uncompressed ROM size is always 16,384 bytes.
    /// </summary>
    public byte[]? RomData { get; private set; } = null;

    /// <summary>
    /// Creates a new instance of the Beta128Block class.
    /// </summary>
    /// <param name="flags">The miscellaneous flags,</param>
    /// <param name="romData">The custom ROM data.</param>
    /// <param name="compressionLevel">Specifies the default compression level if data compression is on.</param>
    public Beta128Block(DWord flags = 0, byte[]? romData = null, CompressionLevel compressionLevel = CompressionLevel.SmallestSize)
    {
        Flags = flags;

        if (romData == null)
        {
            Flags &= ~(FlagsCustomRom | FlagsCompressed);

            return;
        }

        if (romData.Length != 0x4000)
        {
            throw new InvalidDataException("Invalid Beta128 custom ROM size.");
        }

        var compress = compressionLevel != CompressionLevel.NoCompression;

        if (compress)
        {
            Flags |= FlagsCompressed;
            RomData = ZLibHelper.Compress(romData, compressionLevel);
        }
        else
        {
            RomData = romData;
        }

        Flags |= FlagsCustomRom;
    }

    internal void Write(Stream writer)
    {
        var header = new BlockHeader(BlockIds.Beta128, 10 + (RomData?.Length ?? 0));
        header.Write(writer);

        writer.WriteDWord(Flags);
        writer.WriteByte(NumDrives);
        writer.WriteByte(SysReg);
        writer.WriteByte(TrackReg);
        writer.WriteByte(SectorReg);
        writer.WriteByte(DataReg);
        writer.WriteByte(StatusReg);
        writer.WriteBytes(RomData ?? []);
    }

    internal static Beta128Block Read(ByteStreamReader reader, int size)
    {
        if (size < 10)
        {
            throw new InvalidDataException("Invalid Beta128 block size.");
        }

        var beta128 = new Beta128Block
        {
            Flags = reader.ReadDWord(),
            NumDrives = reader.ReadByte(),
            SysReg = reader.ReadByte(),
            TrackReg = reader.ReadByte(),
            SectorReg = reader.ReadByte(),
            DataReg = reader.ReadByte(),
            StatusReg = reader.ReadByte(),
        };

        var remaining = size - 10;

        if (remaining > 0)
        {
            var romData = reader.ReadBytes(remaining);

            if ((beta128.Flags & FlagsCompressed) != 0)
            {
                romData = ZLibHelper.Decompress(romData);
            }

            if (romData.Length != 0x4000)
            {
                throw new InvalidDataException("Invalid Beta128 custom ROM size.");
            }

            beta128.RomData = romData;
        }

        return beta128;
    }
}