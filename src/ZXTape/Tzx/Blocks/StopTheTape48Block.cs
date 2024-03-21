﻿using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Stop The Tape' block. Applies only to 48K Spectrum.
/// </summary>
public class StopTheTape48Block : IBlock
{
    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the block without these 4 bytes. The actual value is always zero.
    /// </summary>
    [BlockProperty(Order = 0)]
    private int Length => 0;

    /// <summary>
    /// Creates a new instance of the 'Stop The Tape 48k' block.
    /// </summary>
    public StopTheTape48Block()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Stop The Tape 48k' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal StopTheTape48Block(IByteStreamReader reader)
    {
        reader.ReadDWord();
    }
}
