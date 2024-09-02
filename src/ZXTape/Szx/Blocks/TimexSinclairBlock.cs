using OldBit.ZXTape.IO;

namespace OldBit.ZXTape.Szx.Blocks;

/// <summary>
/// This block ('S','C','L','D') contains the current screen mode and memory paging status of Timex Sinclair machines.
/// </summary>
public sealed class TimexSinclairBlock
{
    /// <summary>
    /// Gets or sets the current value of port 0xF4 which controls memory paging.
    /// </summary>
    public byte PortF4 { get; set; }

    /// <summary>
    /// Gets or sets the current value of port 0xFF which controls the Timex screen mode,
    /// interrupt state, high resolution colours, and the remainder of the memory paging system.
    /// </summary>
    public byte PortFF { get; set; }

    internal void Write(MemoryStream writer)
    {
        var header = new BlockHeader(BlockIds.TimexSinclair, 8);
        header.Write(writer);

        writer.WriteByte(PortF4);
        writer.WriteByte(PortFF);
    }

    internal static TimexSinclairBlock Read(ByteStreamReader reader, int size)
    {
        if (size != 2)
        {
            throw new InvalidDataException("Invalid TimexSinclair block size.");
        }

        return new TimexSinclairBlock
        {
            PortF4 = reader.ReadByte(),
            PortFF = reader.ReadByte()
        };
    }
}