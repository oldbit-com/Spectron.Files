using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'CSW Recording' block.
/// </summary>
public class CswRecordingBlock : IBlock
{
    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the block length.
    /// </summary>
    [BlockProperty(Order = 0)]
    private int Length => 10 + Data.Count;

    /// <summary>
    /// Gets or sets the pause value (in millisecond) that should be applied after this block.
    /// </summary>
    [BlockProperty(Order = 1)]
    public Word PauseDuration { get; set; }

    /// <summary>
    /// Gets or sets the sampling rate.
    /// </summary>
    [BlockProperty(Order = 2, Size = 3)]
    public int SamplingRate { get; set; }

    /// <summary>
    /// Gets or sets the compression type.
    /// </summary>
    [BlockProperty(Order = 3)]
    public CompressionType CompressionType { get; set; } = CompressionType.Rle;

    /// <summary>
    /// Gets or sets the number of stored pulses (after decompression, for validation purposes).
    /// </summary>
    [BlockProperty(Order = 4)]
    public DWord StoredPulsesCount { get; set; }

    /// <summary>
    /// Gets or sets the CSW data encoded according to the CSW file format specification.
    /// </summary>
    [BlockProperty(Order = 4)]
    public List<byte> Data { get; set; } = [];

    /// <summary>
    /// Creates a new instance of the 'CSW Recording' block.
    /// </summary>
    public CswRecordingBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'CSW Recording' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal CswRecordingBlock(IByteStreamReader reader)
    {
        var length = reader.ReadDWord() - 10;
        PauseDuration = reader.ReadWord();
        SamplingRate = reader.ReadByte() | reader.ReadByte() << 8 | reader.ReadByte() << 16;
        CompressionType = (CompressionType)reader.ReadByte();
        StoredPulsesCount = reader.ReadDWord();
        Data.AddRange(reader.ReadBytes((int)length));
    }
}
