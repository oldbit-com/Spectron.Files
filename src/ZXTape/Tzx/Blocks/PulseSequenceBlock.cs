﻿using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Pulse Sequence' block.
/// </summary>
public class PulseSequenceBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [FileData(Order = 0)]
    public byte BlockId => BlockCode.PulseSequence;

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the pulses' array.
    /// </summary>
    [FileData(Order = 1, Size = 1)]
    private int PulseCount => PulseLengths.Count;

    /// <summary>
    /// Gets or sets an array of pulses' lengths.
    /// </summary>
    [FileData(Order = 2)]
    public List<Word> PulseLengths { get; set; } = new();

    /// <summary>
    /// Creates a new instance of the 'Pulse Sequence' block.
    /// </summary>
    public PulseSequenceBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Pulse Sequence' block using the byte reader.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the PulseSequenceBlock properties.</param>
    internal PulseSequenceBlock(ByteStreamReader reader)
    {
        var length = reader.ReadByte();
        PulseLengths.AddRange(reader.ReadWords(length));
    }
}