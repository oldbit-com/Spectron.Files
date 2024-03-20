using System.Diagnostics.CodeAnalysis;

namespace OldBit.ZXTape.Tap;

/// <summary>
/// Represents a block of data in the tape.
/// </summary>
public sealed class TapeData
{
    /// <summary>
    /// Gets or sets type of the block data.
    /// Typically there are two standard values: 0x00 for header and 0xFF for data.
    /// </summary>
    public byte Flag { get; set; }

    /// <summary>
    /// Gets or sets data.
    /// </summary>
    public List<byte> Data { get; set; } = [];

    /// <summary>
    /// Gets or sets the control sum of the data.
    /// </summary>
    public byte Checksum { get; set; }

    /// <summary>
    /// Tries to parse the given data into a TapeData object.
    /// </summary>
    /// <param name="data">The data to parse.</param>
    /// <param name="tapData">The resulting TapeData object if the parsing is successful.</param>
    /// <returns>True if the parsing is successful, otherwise false.</returns>
    public static bool TryParse(IEnumerable<byte> data, [NotNullWhen(true)] out TapeData? tapData)
    {
        tapData = null;

        var dataBytes = data.ToArray();
        if (dataBytes.Length < 2)
        {
            return false;
        }

        tapData = new TapeData
        {
            Flag = dataBytes[0],
            Data = dataBytes[1..^1].ToList(),
            Checksum = dataBytes[^1]
        };

        return true;
    }

    /// <summary>
    /// Calculates the checksum of the data.
    /// The checksum is calculated by performing a bitwise exclusive OR (XOR) operation
    /// on the flag and each byte in the data.
    /// </summary>
    /// <returns>The calculated checksum as a byte.</returns>
    public byte CalculateChecksum()
    {
        var checksum = Flag;
        Data.ForEach(b => { checksum ^= b; });
        return checksum;
    }
}