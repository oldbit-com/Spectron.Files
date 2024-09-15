using System.Diagnostics.CodeAnalysis;
using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tap.Extensions;

namespace OldBit.ZX.Files.Tap;

/// <summary>
/// Represents a block of data in the tape.
/// </summary>
public sealed class TapData
{
    /// <summary>
    /// Gets the length of the block data.
    /// </summary>
    [FileData(Order = 0)]
    private Word Length => (Word)(BlockData.Count + 2);

    /// <summary>
    /// Gets or sets type of the block data.
    /// Typically, there are two standard values: 0x00 for header and 0xFF for data.
    /// </summary>
    [FileData(Order = 1)]
    public byte Flag { get; set; }

    /// <summary>
    /// Gets or sets data.
    /// </summary>
    [FileData(Order = 2)]
    public List<byte> BlockData { get; set; } = [];

    /// <summary>
    /// Gets or sets the control sum of the data.
    /// </summary>
    [FileData(Order = 3)]
    public byte Checksum { get; set; }

    /// <summary>
    /// Tries to parse the given data into a TapeData object.
    /// </summary>
    /// <param name="data">The data to parse.</param>
    /// <param name="tapData">The resulting TapeData object if the parsing is successful.</param>
    /// <returns>True if the parsing is successful, otherwise false.</returns>
    public static bool TryParse(IEnumerable<byte> data, [NotNullWhen(true)] out TapData? tapData)
    {
        tapData = null;

        var dataBytes = data.ToArray();
        if (dataBytes.Length < 2)
        {
            return false;
        }

        tapData = new TapData
        {
            Flag = dataBytes[0],
            BlockData = dataBytes[1..^1].ToList(),
            Checksum = dataBytes[^1]
        };

        return true;
    }

    /// <summary>
    /// Gets a value indicating whether the block is a header.
    /// </summary>
    public bool IsHeader => Flag == 0x00;

    /// <summary>
    /// Calculates the checksum of the data.
    /// The checksum is calculated by performing a bitwise exclusive OR (XOR) operation
    /// on the flag and each byte in the data.
    /// </summary>
    /// <returns>The calculated checksum as a byte.</returns>
    public byte CalculateChecksum()
    {
        var checksum = Flag;

        BlockData.ForEach(b => { checksum ^= b; });

        return checksum;
    }

    /// <summary>
    /// Converts the TapData to its equivalent string representation.
    /// </summary>
    /// <returns>The string representation of this object.</returns>
    public override string ToString()
    {
        if (IsHeader && TapHeader.TryParse(BlockData, out var header))
        {
            var name = header.GetDataTypeName();
            return $"{name}: {header.FileName}";
        }

        return Length == 1 ? "1 byte" : $"{Length} bytes";;
    }
}