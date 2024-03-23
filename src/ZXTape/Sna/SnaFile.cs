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
}