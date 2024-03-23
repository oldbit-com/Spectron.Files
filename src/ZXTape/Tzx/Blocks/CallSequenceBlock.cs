using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Call Sequence' block.
/// </summary>
public class CallSequenceBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [BlockProperty(Order = 0)]
    public byte BlockId => BlockCode.CallSequence;

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the number of calls to be made.
    /// </summary>
    [BlockProperty(Order = 1)]
    private Word Count => (Word)Offsets.Count;

    /// <summary>
    /// Gets or sets a list of call block numbers (relative-signed offsets).
    /// </summary>
    [BlockProperty(Order = 2)]
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
    /// <param name="reader">A byte reader.</param>
    internal CallSequenceBlock(ByteStreamReader reader)
    {
        var length = reader.ReadWord();
        Offsets = reader
            .ReadWords(length)
            .Select(offset => (short)offset).ToList();
    }
}