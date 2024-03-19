using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.Tzx;

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

    public TzxFile()
    {
        Header = new HeaderBlock();
    }

    public static TzxFile Load(string path)
    {
        using var stream = File.OpenRead(path);
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
                tzx.Header = headerBlock;
            }
            else
            {
                tzx.Blocks.Add(block);
            }
        }

        return tzx;
    }
}
