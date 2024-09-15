namespace OldBit.ZX.Files.Tap.Extensions;

public static class TapeHeaderExtensions
{
    public static string GetDataTypeName(this TapHeader header)
    {
        return header.DataType switch
        {
            HeaderDataType.Program => "Program",
            HeaderDataType.NumberArray => "Number Array",
            HeaderDataType.CharArray => "Character Array",
            HeaderDataType.Code => "Bytes",
            _ => "Unknown"
        };
    }
}