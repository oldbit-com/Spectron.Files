﻿using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;

namespace OldBit.ZX.Files.Tzx.Blocks;

/// <summary>
/// Represents the 'Call Sequence' block.
/// </summary>
public class CallSequenceBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [FileData(Order = 0)]
    public byte BlockId => BlockCode.CallSequence;

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the number of calls to be made.
    /// </summary>
    [FileData(Order = 1)]
    private Word Count => (Word)Offsets.Count;

    /// <summary>
    /// Gets or sets a list of call block numbers (relative-signed offsets).
    /// </summary>
    [FileData(Order = 2)]
    public List<short> Offsets { get; set; } = [];

    /// <summary>
    /// Creates a new instance of the 'Call Sequence' block.
    /// </summary>
    public CallSequenceBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Call Sequence' block using the byte reader.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the CallSequenceBlock properties.</param>
    internal CallSequenceBlock(ByteStreamReader reader)
    {
        var length = reader.ReadWord();
        Offsets = reader
            .ReadWords(length)
            .Select(offset => (short)offset).ToList();
    }
}