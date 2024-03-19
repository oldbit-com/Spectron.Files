using System.Text;
using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Select' block.
/// </summary>
public class SelectBlock : IBlock
{
    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the length of the whole block.
    /// </summary>
    [BlockProperty(Order = 0)]
    private Word Length => (ushort)(1 + Selections.Count * 3 + Selections.Sum(x => x.Length));

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the number of selections.
    /// </summary>
    [BlockProperty(Order = 1)]
    private byte Count => (byte)Selections.Count;

    /// <summary>
    /// Gets or sets the array of selections.
    /// </summary>
    [BlockProperty(Order = 2)]
    public List<Selection> Selections { get; set; } = [];

    /// <summary>
    /// Creates a new instance of the 'Select' block.
    /// </summary>
    public SelectBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Select' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal SelectBlock(IByteStreamReader reader)
    {
        reader.ReadWord();
        var count = reader.ReadByte();
        for (var i = 0; i < count; i++)
        {
            Selections.Add(new Selection(reader));
        }
    }

    /// <summary>
    /// Represents the SELECT structure.
    /// </summary>
    public class Selection
    {
        /// <summary>
        /// Gets or sets the relative offset.
        /// </summary>
        [BlockProperty(Order = 0)]
        public short Offset { get; set; }

        /// <summary>
        /// Helper property needed by the serialization
        /// Gets the length of the description text.
        /// </summary>
        [BlockProperty(Order = 1)]
        internal byte Length => (byte)Description.Length;

        /// <summary>
        /// Gets or sets the description text.
        /// </summary>
        [BlockProperty(Order = 2)]
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Creates a new instance of the 'SELECT' structure.
        /// </summary>
        public Selection()
        {
        }

        /// <summary>
        /// Creates a new instance of the 'SELECT' structure using the byte reader..
        /// </summary>
        /// <param name="reader">A byte reader.</param>
        public Selection(IByteStreamReader reader)
        {
            Offset = (short)reader.ReadWord();
            var length = reader.ReadByte();
            Description = Encoding.ASCII.GetString(reader.ReadBytes(length));
        }
    }
}
