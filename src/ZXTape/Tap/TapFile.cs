using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.Tap;

/// <summary>
/// Represents a TAP file.
/// </summary>
public sealed class TapFile
{
    public List<TapeData> Blocks { get; } = [];

    /// <summary>
    /// Initializes a new instance of the TapFile class.
    /// </summary>
    public TapFile()
    {
    }

    /// <summary>
    /// Loads a TAP file from the given stream.
    /// </summary>
    /// <param name="stream">The stream containing the TAP data.</param>
    /// <returns>The loaded TapFile object.</returns>
    public static TapFile Load(Stream stream)
    {
        var tapFile = new TapFile();
        var reader = new ByteStreamReader(stream);

        // TODO: Implement loading of TAP file.
        var length = reader.ReadWord();
        if (!TapeData.TryParse(reader.ReadBytes(length), out var tapData))
        {
            throw new InvalidDataException("Invalid TAP data.");
        }

        return tapFile;
    }

    /// <summary>
    /// Loads a TAP file from the given file.
    /// </summary>
    /// <param name="fileName">The file containing the TAP data.</param>
    /// <returns>The loaded TapFile object.</returns>
    public static TapFile Load(string fileName)
    {
        using var stream = File.OpenRead(fileName);
        return Load(stream);
    }
}