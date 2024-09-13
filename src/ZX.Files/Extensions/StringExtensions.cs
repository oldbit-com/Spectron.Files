namespace OldBit.ZX.Files.Extensions;

internal static class StringExtensions
{
    public static IEnumerable<byte> ToAsciiBytes(this string s)
    {
        return s.Select(Convert.ToByte);
    }
}
