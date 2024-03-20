namespace OldBit.ZXTape.Sna;

public sealed class SnaFile
{
    public byte I { get; set; }
    public Word HLPrime { get; set; }
    public Word DEPrime { get; set; }
    public Word BCPrime { get; set; }
    public Word AFPrime { get; set; }
    public Word HL { get; set; }
    public Word DE { get; set; }
    public Word BC { get; set; }
    public Word IY { get; set; }
    public Word IX { get; set; }
    public byte IFF2 { get; set; }
    public byte R { get; set; }
    public Word AF { get; set; }
    public Word SP { get; set; }
    public byte InterruptMode { get; set; }
    public byte BorderColor { get; set; }

    public static SnaFile Load(string fileName)
    {
        throw new NotImplementedException();
    }

    public static bool IsSnaFile(string fileName)
    {
        var ext = Path.GetExtension(fileName);
        if (!ext.Equals(".sna", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var fileInfo = new FileInfo(fileName);
        if (fileInfo.Length != 49179)
        {
            return false;
        }

        throw new NotImplementedException();
    }
}