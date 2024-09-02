using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Extensions;

namespace OldBit.ZXTape.Szx.Blocks;

/// <summary>
/// This block ('Z','X','P','R') contains the status of the ZX Printer.
/// </summary>
public sealed class ZxPrinterBlock
{
    /// <summary>
    /// ZX printer emulation is enabled.
    /// <remarks>ZXSTPRF_ENABLED</remarks>
    /// </summary>
    public const Word FlagsEnabled = 1;

    /// <summary>
    /// Gets or sets flags that specify the state of the ZX Printer.
    /// <remarks>
    /// If the ZXSTPRF_ENABLED bit set, this specifies ZX Printer emulation is enabled. Otherwise it should be disabled.
    /// </remarks>
    /// </summary>
    public Word Flags { get; set; }

    internal void Write(Stream writer)
    {
        var header = new BlockHeader(BlockIds.ZxPrinter, 2);
        header.Write(writer);

        writer.WriteWord(Flags);
    }

    internal static ZxPrinterBlock Read(ByteStreamReader reader, int size)
    {
        if (size != 2)
        {
            throw new InvalidDataException("Invalid ZxPrinter block size.");
        }

        return new ZxPrinterBlock
        {
            Flags = reader.ReadWord()
        };
    }
}