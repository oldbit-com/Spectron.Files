using OldBit.ZXTape.IO;

namespace OldBit.ZXTape.Sna;

public sealed class SnaFile
{
    public SnaData Data { get; private set; } = new();

    public static SnaFile Load(Stream stream)
    {
        var snaFile = new SnaFile
        {
            Data = new SnaData(new ByteStreamReader(stream))
        };

        return snaFile;
    }

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