using OldBit.ZX.Files.Tap;

namespace OldBit.ZX.Files.Extensions;

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