using System.IO.Compression;
using OldBit.Spectron.Files.Szx.Extensions;

namespace OldBit.Spectron.Files.Szx.Blocks;

/// <summary>
/// This block ('B','D','S','K') contains the inserted disk contents.
/// </summary>
public class BetaDiskBlock
{
    /// <summary>
    /// The actual disk image is embedded in this block.
    /// </summary>
    public const DWord FlagsEmbedded = 1;

    /// <summary>
    /// If FlagsEmbedded is also set, the disk image in this block is compressed using the Zlib compression library.
    /// </summary>
    public const DWord FlagsCompressed = 2;

    /// <summary>
    /// Specifies whether the disk image is write-protected.
    /// </summary>
    public const DWord FlagsWriteProtect = 4;

    /// <summary>
    /// The disk image is in the .trd format.
    /// </summary>
    public const byte DiskTypeTrd = 0;

    /// <summary>
    /// The disk image is in the .scl format.
    /// </summary>
    public const byte DiskTypeScl = 1;

    /// <summary>
    /// The disk image is in the .fdi format.
    /// </summary>
    public const byte DiskTypeFdi = 2;

    /// <summary>
    /// The disk image is in the Ultra disk image (.udi) format.
    /// </summary>
    public const byte DiskTypeUdi = 3;

    /// <summary>
    /// Gets or sets the various flags.
    /// </summary>
    public DWord Flags { get; private set; }

    /// <summary>
    /// Gets or sets the drive to insert this disk image into (0-3).
    /// </summary>
    public byte DriveNum { get; set; }

    /// <summary>
    /// Gets or sets the cylinder the drive heads are currently over (0-86).
    /// </summary>
    public byte Cylinder { get; set; }

    /// <summary>
    /// Gets or sets the type of disk image.
    /// </summary>
    public byte DiskType { get; private init; }

    /// <summary>
    /// Gets or sets the file name of the disk image which should be opened and inserted into this drive,
    /// if the disk image is not embedded.
    /// </summary>
    public string? FileName { get; private init; }

    /// <summary>
    /// If the disk image is embedded, this member is the (possibly compressed) disk image to be inserted into this drive.
    /// </summary>
    public byte[]? DiskImage { get; set; }

    public BetaDiskBlock(byte[] diskImage, byte diskType, CompressionLevel compressionLevel = CompressionLevel.SmallestSize)
    {
        if (diskType > DiskTypeUdi)
        {
            throw new ArgumentOutOfRangeException(nameof(diskType));
        }

        var compress = compressionLevel != CompressionLevel.NoCompression;

        if (compress)
        {

        }
    }

    internal void Write(Stream writer)
    {
        var header = new BlockHeader(BlockIds.BetaDisk, 10 + (DiskImage?.Length ?? 0));
        header.Write(writer);

        writer.WriteDWord(Flags);
        writer.WriteByte(DriveNum);
        writer.WriteByte(Cylinder);
        writer.WriteByte(DiskType);

        writer.WriteBytes(RomData ?? []);
    }
}