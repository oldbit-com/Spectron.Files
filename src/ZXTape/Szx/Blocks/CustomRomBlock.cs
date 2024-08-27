using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Serialization;

namespace OldBit.ZXTape.Szx.Blocks;

/// <summary>
/// This block ('R','O','M',0) contains custom ROM that has been installed for the current Spectrum model.
/// </summary>
public sealed class CustomRomBlock
{
    /// <summary>
    /// Indicates the ROM image is compressed.
    /// </summary>
    public const Word FlagsCompressed = 1;

    /// <summary>
    /// Gets or sets the various flags.
    /// </summary>
    public Word Flags { get; private init; }

    /// <summary>
    /// Gets or sets the size in bytes of the custom ROM. This will be one of:
    ///     16,384 for 16k/48k Spectrum
    ///     32,768 for Spectrum 128/+2
    ///     65,536 for Spectrum +2A/+3
    ///     32,768 for Pentagon 128
    ///     65,536 for ZS Scorpion
    ///     16,384 for Timex Sinclair TS/TC2048
    ///     24,576 for Timex Sinclair TS2068
    ///     32,768 for Spectrum SE
    /// </summary>
    public DWord UncompressedSize { get; private init; }

    public byte[] Data { get; private set; } = [];

    private CustomRomBlock()
    {
    }

    public CustomRomBlock(byte[] data, bool compress)
    {
        Flags = (Word)(compress ? FlagsCompressed : 0);
        UncompressedSize = (DWord)data.Length;
        Data = compress ? ZLibHelper.Compress(data) : data;
    }

    internal void Write(ByteWriter writer)
    {
        var header = new BlockHeader(BlockIds.CustomRom, 6 + Data.Length);
        header.Write(writer);

        writer.WriteWord(Flags);
        writer.WriteDWord(UncompressedSize);
        writer.WriteBytes(Data);
    }

    internal static CustomRomBlock Read(ByteStreamReader reader, int size)
    {
        var customRom = new CustomRomBlock
        {
            Flags = reader.ReadWord(),
            UncompressedSize = reader.ReadDWord()
        };

        var data = reader.ReadBytes(size - 6);
        customRom.Data = customRom.Flags == FlagsCompressed ? ZLibHelper.Decompress(data) : data;

        return customRom;
    }
}