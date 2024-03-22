using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.Tzx;

/// <summary>
/// Represents a TZX file.
/// </summary>
public sealed class TzxFile
{
    /// <summary>
    /// Gets the TZX file header.
    /// </summary>
    public HeaderBlock Header { get; private set; }

    /// <summary>
    /// Gets a list of TZX file blocks.
    /// </summary>
    public List<IBlock> Blocks { get; } = [];

    /// <summary>
    /// Initializes a new instance of the TzxFile class.
    /// </summary>
    public TzxFile()
    {
        Header = new HeaderBlock();
    }

    /// <summary>
    /// Loads a TZX file from a stream.
    /// </summary>
    /// <param name="stream">The stream to load the TZX file from.</param>
    /// <returns>A TzxFile object representing the loaded TZX file.</returns>
    public static TzxFile Load(Stream stream)
    {
        var reader = new TzxBlockReader(stream);

        var tzx = new TzxFile();
        while (true)
        {
            var block = reader.ReadNextBlock();
            if (block == null)
            {
                break;
            }

            if (block is HeaderBlock headerBlock)
            {
                if (!headerBlock.IsValid)
                {
                    throw new InvalidDataException("Invalid TZX header.");
                }
                tzx.Header = headerBlock;
            }
            else
            {
                tzx.Blocks.Add(block);
            }
        }

        return tzx;
    }

    /// <summary>
    /// Loads a TZX file from a file.
    /// </summary>
    /// <param name="fileName">The name of the file to load the TZX file from.</param>
    /// <returns>A TzxFile object representing the loaded TZX file.</returns>
    public static TzxFile Load(string fileName)
    {
        using var stream = File.OpenRead(fileName);

        return Load(stream);
    }


    /// <summary>
    /// Saves the TZX file to a stream.
    /// </summary>
    /// <param name="stream">The stream to save the TZX file to.</param>
    public void Save(Stream stream)
    {
        var writer = new TzxBlockWriter(stream);

        writer.WriteBlock(Header);
        foreach (var block in Blocks)
        {
            writer.WriteBlock(block);
        }
    }

    /// <summary>
    /// Saves the TZX file to a file.
    /// </summary>
    /// <param name="fileName">The name of the file to save the TZX file to.</param>
    public void Save(string fileName)
    {
        using var stream = File.Create(fileName);
        Save(stream);
    }
}