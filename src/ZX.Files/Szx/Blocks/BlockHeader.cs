using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Szx.Extensions;

namespace OldBit.ZX.Files.Szx.Blocks;

internal sealed class BlockHeader(DWord blockId, int size)
{
    internal DWord BlockId { get; } = blockId;
    internal int Size { get; } = size;

    internal void Write(Stream writer)
    {
        writer.WriteDWord(BlockId);
        writer.WriteDWord((DWord)Size);
    }

    internal static BlockHeader? Read(ByteStreamReader reader)
    {
        if (reader.TryReadDWord(out var blockId) && reader.TryReadDWord(out var size))
        {
            return new BlockHeader(blockId, (int)size);
        }

        return null;
    }
}