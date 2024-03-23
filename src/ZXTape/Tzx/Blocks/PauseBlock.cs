﻿using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the Pause (silence) or 'Stop the Tape' command.
/// </summary>
public class PauseBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [FileData(Order = 0)]
    public byte BlockId => BlockCode.Pause;

    /// <summary>
    /// Gets or sets the pause duration in milliseconds.
    /// </summary>
    [FileData(Order = 1)]
    public Word Duration { get; set; }

    /// <summary>
    /// Creates a new instance of the 'Pause' block.
    /// </summary>
    public PauseBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Pause' block using the byte reader.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the PauseBlock properties.</param>
    internal PauseBlock(ByteStreamReader reader)
    {
        Duration = reader.ReadWord();
    }
}