using OldBit.Spectron.Files.Serialization;
using OldBit.Spectron.Files.Z80.Types;

namespace OldBit.Spectron.Files.Z80;

/// <summary>
/// Represents a .z80 file. Provides methods to load and save Z80 files.
/// </summary>
public sealed class Z80File
{
    /// <summary>
    /// Gets or sets the header.
    /// </summary>
    [FileData(Order = 0)]
    public Z80Header Header { get; }

    /// <summary>
    /// Gets or sets the 48K RAM data.
    /// </summary>
    [FileData(Order = 1)]
    public byte[] Memory { get; set; } = [];

    /// <summary>
    /// Gets or sets the memory blocks.
    /// </summary>
    [FileData(Order = 2)]
    public List<MemoryBlock> MemoryBlocks { get; } = [];

    internal Z80File(Z80Header header)
    {
        Header = header;
    }

    public Z80File(Z80Header header, byte[] memory)
    {
        Header = header;
        Memory = memory;
        Header.Flags1.IsDataCompressed = true;
        Header.HardwareMode = HardwareMode.Spectrum48;
    }

    public Z80File(Z80Header header, IEnumerable<MemoryBlock> memory)
    {
        Header = header;
        MemoryBlocks.AddRange(memory);
        Header.Flags1.IsDataCompressed = true;
        Header.HardwareMode = HardwareMode.Spectrum128;
    }

    /// <summary>
    /// Loads a Z80 file from the given stream.
    /// </summary>
    /// <param name="stream">The stream containing the Z80 data.</param>
    /// <returns>The loaded Z80File object.</returns>
    public static Z80File Load(Stream stream) => Parser.Parse(stream);

    /// <summary>
    /// Loads a Z80 file from the given file.
    /// </summary>
    /// <param name="fileName">The file containing the Z80 data.</param>
    /// <returns>The loaded Z80File object.</returns>
    public static Z80File Load(string fileName)
    {
        using var stream = File.OpenRead(fileName);

        return Load(stream);
    }

    /// <summary>
    /// Saves the Z80 data to a stream.
    /// </summary>
    /// <param name="stream">The stream to save the Z80 data to.</param>
    public void Save(Stream stream)
    {
        stream.Write(Header.Data, 0, Header.Data.Length);

        if (Memory.Length > 0)
        {
            var data = Header.Flags1.IsDataCompressed ? DataCompressor.Compress(Memory, true).ToArray() : Memory;
            stream.Write(data, 0, data.Length);
        }

        foreach (var block in MemoryBlocks)
        {
            var data = block.Serialize();
            stream.Write(data, 0, data.Length);
        }
    }

    /// <summary>
    /// Saves the Z80 data to a file.
    /// </summary>
    /// <param name="fileName">The name of the file to save the Z80 data to.</param>
    public void Save(string fileName)
    {
        using var stream = File.Create(fileName);

        Save(stream);
    }
}