﻿using OldBit.SpeccyLib.Blocks;
using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Set Signal Level' block.
/// </summary>
public class SetSignalLevelBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [BlockProperty(Order = 0)]
    public byte BlockId => BlockCode.SetSignalLevel;

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the block without these 4 bytes.
    /// </summary>
    [BlockProperty(Order = 1)]
    private int Length => 1;

    /// <summary>
    /// Gets or sets the signal level.
    /// </summary>
    [BlockProperty(Order = 2)]
    public SignalLevel Level { get; set; }

    /// <summary>
    /// Creates a new instance of the 'Set Signal Level' block.
    /// </summary>
    public SetSignalLevelBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Set Signal Level' block using the byte reader.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the SetSignalLevelBlock properties.</param>
    internal SetSignalLevelBlock(ByteStreamReader reader)
    {
        Level = (SignalLevel)reader.ReadByte();
    }
}