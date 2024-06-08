using OldBit.ZXTape.IO;

namespace OldBit.ZXTape.Sna;

/// <summary>
/// Represents a .sna file.
/// </summary>
public sealed class SnaFile
{
    /// <summary>
    /// Gets or sets the SNA data.
    /// </summary>
    public SnaData Data { get; init; } = new();

    /// <summary>
    /// Loads a SNA file from the given stream.
    /// </summary>
    /// <param name="stream">The stream containing the SNA data.</param>
    /// <returns>The loaded SnaFile object.</returns>
    public static SnaFile Load(Stream stream) => new()
    {
        Data = new SnaData(new ByteStreamReader(stream))
    };

    /// <summary>
    /// Loads a SNA file from the given file.
    /// </summary>
    /// <param name="fileName">The file containing the SNA data.</param>
    /// <returns>The loaded SnaFile object.</returns>
    public static SnaFile Load(string fileName)
    {
        using var stream = File.OpenRead(fileName);
        return Load(stream);
    }

    /// <summary>
    /// Saves the SNA file to a stream.
    /// </summary>
    /// <param name="stream">The stream to save the SNA data to.</param>
    public void Save(Stream stream)
    {
        var writer = new DataWriter(stream);
        writer.Write(Data);
    }

    /// <summary>
    /// Saves the SNA data to a file.
    /// </summary>
    /// <param name="fileName">The name of the file to save the SNA data to.</param>
    public void Save(string fileName)
    {
        using var stream = File.Create(fileName);
        Save(stream);
    }
}