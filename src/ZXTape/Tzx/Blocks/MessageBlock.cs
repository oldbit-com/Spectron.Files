using OldBit.ZXTape.Extensions;
using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Message' block.
/// </summary>
public class MessageBlock : IBlock
{
    /// <summary>
    /// Gets or sets the time (in seconds) for which the message should be displayed.
    /// </summary>
    [BlockProperty(Order = 0)]
    public byte Time { get; set; }

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the text message.
    /// </summary>
    [BlockProperty(Order = 1)]
    private byte Length => (byte)Message.Length;

    /// <summary>
    /// Gets or sets the message that should be displayed in ASCII format.
    /// </summary>
    [BlockProperty(Order = 2)]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Creates a new instance of the 'Message' block.
    /// </summary>
    public MessageBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Message' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal MessageBlock(IByteStreamReader reader)
    {
        Time = reader.ReadByte();
        var length = reader.ReadByte();
        Message = reader.ReadBytes(length).ToAsciiString();
    }
}
