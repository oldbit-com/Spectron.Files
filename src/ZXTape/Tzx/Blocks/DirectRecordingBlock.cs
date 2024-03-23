﻿using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Direct Recording' block.
/// </summary>
public class DirectRecordingBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [FileData(Order = 0)]
    public byte BlockId => BlockCode.DirectRecording;

    /// <summary>
    /// Gets or sets the number of T-states per sample.
    /// </summary>
    [FileData(Order = 1)]
    public Word StatesPerSample { get; set; }

    /// <summary>
    /// Gets or sets the pause value (in millisecond) that should be applied after this block.
    /// </summary>
    [FileData(Order = 2)]
    public Word PauseDuration { get; set; }

    /// <summary>
    /// Gets or sets the used bits in the last byte (other bits should be 0). E.g. if this is 6, then the bits used (x)
    /// in the last byte are: xxxxxx00, where MSb is the leftmost bit, LSb is the rightmost bit.
    /// </summary>
    [FileData(Order = 3)]
    public byte UsedBitsLastByte { get; set; }

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the data.
    /// </summary>
    [FileData(Order = 4, Size = 3)]
    private int Length => Data.Count;

    /// <summary>
    /// Gets or sets the data as in .TAP file.
    /// </summary>
    [FileData(Order = 5)]
    public List<byte> Data { get; set; } = [];

    /// <summary>
    /// Creates a new instance of the 'Direct Recording' block.
    /// </summary>
    public DirectRecordingBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Direct Recording' block using the byte reader.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the DirectRecordingBlock properties.</param>
    internal DirectRecordingBlock(ByteStreamReader reader)
    {
        StatesPerSample = reader.ReadWord();
        PauseDuration = reader.ReadWord();
        UsedBitsLastByte = reader.ReadByte();
        var length = reader.ReadByte() | reader.ReadByte() << 8 | reader.ReadByte() << 16;
        Data.AddRange(reader.ReadBytes(length));
    }
}