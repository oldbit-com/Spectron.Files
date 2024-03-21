﻿using OldBit.ZXTape.Extensions;
using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Group Start' block.
/// </summary>
public class GroupStartBlock : IBlock
{
    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the group name string.
    /// </summary>
    [BlockProperty(Order = 0)]
    private byte Length => (byte)Name.Length;

    /// <summary>
    /// Gets or sets the name in ASCII format.
    /// </summary>
    [BlockProperty(Order = 1)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Creates a new instance of the 'Group Start' block.
    /// </summary>
    public GroupStartBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Group Start' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal GroupStartBlock(IByteStreamReader reader)
    {
        var length = reader.ReadByte();
        Name = reader.ReadBytes(length).ToAsciiString();
    }
}
