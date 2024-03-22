using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.IO;

public class TzxBlockWriter(Stream stream)
{
    private readonly BlockSerializer _serializer = new();

    public void WriteBlock(IBlock block)
    {
        var data = _serializer.Serialize(block);
        stream.Write(data, 0, data.Length);
    }
}