using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Z80;

/// <summary>
/// Represents a .z80 file.
/// </summary>
public sealed class Z80File
{
    /// <summary>
    /// Gets or sets the Z80 data.
    /// </summary>
    public Z80Data Data { get; private set; } = new();

    /// <summary>
    /// Loads a Z80 file from the given stream.
    /// </summary>
    /// <param name="stream">The stream containing the Z80 data.</param>
    /// <returns>The loaded Z80File object.</returns>
    public static Z80File Load(Stream stream) => new()
    {
        Data = new Z80Data(new ByteStreamReader(stream))
    };

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
        var writer = new DataWriter(stream);
        throw new NotImplementedException();
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