﻿using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Return From Sequence' block.
/// </summary>
public class ReturnFromSequenceBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [FileData(Order = 0)]
    public byte BlockId => BlockCode.ReturnFromSequence;
}