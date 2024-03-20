using OldBit.ZXTape.Tap;

namespace OldBit.ZXTape.Extensions;

public static class TapeHeaderExtensions
{
    public static string GetDataTypeName(this TapeHeader header)
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