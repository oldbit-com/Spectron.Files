using OldBit.ZXTape.Tap;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.IO;

/// <summary>
/// Writes a data block to a stream.
/// </summary>
internal class BlockWriter(Stream stream)
{
    private readonly BlockSerializer _serializer = new();

    /// <summary>
    /// Writes a data block to the stream.
    /// </summary>
    /// <param name="block">The block to write.</param>
    internal void Write(IBlock block)
    {
        var data = BlockSerializer.Serialize(block);
        stream.Write(data, 0, data.Length);
    }

    /// <summary>
    /// Writes a TAP data to the stream.
    /// </summary>
    /// <param name="tapData">The data to write.</param>
    internal void Write(TapData tapData)
    {
        var data = BlockSerializer.Serialize(tapData);
        stream.Write(data, 0, data.Length);
    }
}