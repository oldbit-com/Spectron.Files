﻿using OldBit.ZXTape.Extensions;
using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Text Description' block.
/// </summary>
public class TextDescriptionBlock : IBlock
{
    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the text description.
    /// </summary>
    [BlockProperty(Order = 0)]
    private byte Length => (byte)Description.Length;

    /// <summary>
    /// Gets or sets the description in ASCII format.
    /// </summary>
    [BlockProperty(Order = 1)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Creates a new instance of the 'Text Description' block.
    /// </summary>
    public TextDescriptionBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Text Description' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal TextDescriptionBlock(IByteStreamReader reader)
    {
        var length = reader.ReadByte();
        Description = reader.ReadBytes(length).ToAsciiString();
    }
}
