using OldBit.ZXTape.Extensions;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.IO;

public class TzxBlockWriter(Stream stream)
{
    private readonly BlockSerializer _serializer = new();

    public void WriteBlock(IBlock block)
    {
        if (block is not HeaderBlock)
        {
            var blockCode = block.GetBlockCode();
            stream.WriteByte(blockCode);
        }

        var data = _serializer.Serialize(block);
        stream.Write(data, 0, data.Length);
    }
}