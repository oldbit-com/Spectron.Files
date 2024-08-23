using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Blocks;

namespace OldBit.ZXTape.Szx;

/// <summary>
/// Represents a .szx file.
/// </summary>
public sealed class ZxStateFile
{
    public ZxStateHeader Header { get; private set; } = new();

    /// <summary>
    /// Gets or sets the program that created this zx-state file.
    /// </summary>
    public CreatorBlock? Creator { get; set; }

    /// <summary>
    /// Gets the Z80 registers and other internal state values.
    /// </summary>
    public Z80RegsBlock Z80Registers { get; private set; } = new();

    /// <summary>
    /// Gets the Spectrum's ULA state specifying the current border colour, memory paging status etc.
    /// </summary>
    public SpecRegsBlock SpecRegs { get; private set; } = new();

    public ZxPrinterBlock?  ZxPrinter { get; set; }

    /// <summary>
    /// Loads a SZX file from the given stream.
    /// </summary>
    /// <param name="stream">The stream containing the SZX data.</param>
    /// <returns>The loaded SzxFile object.</returns>
    public static ZxStateFile Load(Stream stream)
    {
        var reader = new ByteStreamReader(stream);
        var header = ZxStateHeader.Read(reader);

        if (header.Magic != BlockIds.Magic)
        {
            throw new InvalidDataException("Not a valid SZX file. Invalid magic number.");
        }

        var zxs = new ZxStateFile { Header = header };

        while (BlockHeader.Read(reader) is { } blockHeader)
        {
            switch (blockHeader.BlockId)
            {
                case BlockIds.Creator:
                    zxs.Creator = CreatorBlock.Read(reader, blockHeader.Size);
                    break;

                case BlockIds.Z80Regs:
                    zxs.Z80Registers = Z80RegsBlock.Read(reader, blockHeader.Size);
                    break;

                case BlockIds.SpecRegs:
                    zxs.SpecRegs = SpecRegsBlock.Read(reader, blockHeader.Size);
                    break;

                case BlockIds.ZxPrinter:
                    zxs.ZxPrinter = ZxPrinterBlock.Read(reader, blockHeader.Size);
                    break;

                default:
                    // Ignore this block, not needed for now.
                    reader.ReadBytes(blockHeader.Size);
                    break;
            }
        }

        return zxs;
    }

    /// <summary>
    /// Loads a SNA file from the given file.
    /// </summary>
    /// <param name="fileName">The file containing the SZX data.</param>
    /// <returns>The loaded SzxFile object.</returns>
    public static ZxStateFile Load(string fileName)
    {
        using var stream = File.OpenRead(fileName);

        return Load(stream);
    }
}