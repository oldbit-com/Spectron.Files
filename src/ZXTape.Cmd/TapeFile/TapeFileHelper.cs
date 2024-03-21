namespace OldBit.ZXTape.Cmd.TapeFile;

public class TapeFileHelper
{
    public static FileFormat DetectFormat(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return extension switch
        {
            ".tzx" => FileFormat.Tzx,
            ".tap" => FileFormat.Tap,
            ".sna" => FileFormat.Sna,
            ".z80" => FileFormat.Z80,
            _ => FileFormat.Other
        };
    }
}