namespace OldBit.ZXTape.UnitTests.Extensions;

internal static class MemoryStreamExtensions
{
    internal static byte[] GetAllBytes(this MemoryStream stream)
    {
        return stream.ToArray();
    }
}