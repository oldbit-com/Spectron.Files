using OldBit.Spectron.Files.Serialization;
using OldBit.Spectron.Files.Tap;
using OldBit.Spectron.Files.Tzx;
using OldBit.Spectron.Files.Tzx.Blocks;

namespace OldBit.Spectron.Files.Extensions;

public static class TapFileExtensions
{
    /// <summary>
    /// Converts a TapFile to a TzxFile by serializing each block in the TapFile
    /// and adding it as a StandardSpeedDataBlock to the TzxFile.
    /// </summary>
    /// <param name="tapFile">The TapFile to convert.</param>
    /// <returns>A TzxFile containing the serialized blocks from the TapFile.</returns>
    public static TzxFile ToTzx(this TapFile tapFile)
    {
        var tzx = new TzxFile();

        foreach (var block in tapFile.Blocks)
        {
            tzx.Blocks.Add(new StandardSpeedDataBlock
            {
                Data = FileDataSerializer.Serialize(block).ToList(),
                PauseDuration = 1000,
            });
        }

        return tzx;
    }
}