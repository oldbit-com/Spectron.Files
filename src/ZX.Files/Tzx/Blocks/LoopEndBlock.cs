﻿using OldBit.ZX.Files.Serialization;

namespace OldBit.ZX.Files.Tzx.Blocks;

/// <summary>
/// Represents the 'Loop End' block.
/// </summary>
public class LoopEndBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [FileData(Order = 0)]
    public byte BlockId => BlockCode.LoopEnd;
}