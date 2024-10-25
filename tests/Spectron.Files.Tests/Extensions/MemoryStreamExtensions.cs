namespace OldBit.Spectron.Files.Tests.Extensions;

internal static class MemoryStreamExtensions
{
    internal static byte[] GetAllBytes(this MemoryStream stream)
    {
        return stream.ToArray();
    }
}