using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'C64 ROM Type Data' block. This block is deprecated.
/// </summary>
public class C64RomTypeDataBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [BlockProperty(Order = 0)]
    public byte BlockId => BlockCode.C64RomTypeData;

    /// <summary>
    /// Gets the block length.
    /// </summary>
    [BlockProperty(Order = 1)]
    public int Length => 0x24 + DataLength;

    /// <summary>
    /// Gets or sets the pilot tone pulse length.
    /// </summary>
    [BlockProperty(Order = 2)]
    public Word PilotTonePulseLength { get; set; }

    /// <summary>
    /// Gets or sets the number of waves in the pilot tone.
    /// </summary>
    [BlockProperty(Order = 3)]
    public Word NumberOfWavesInPilotTone { get; set; }

    /// <summary>
    /// Gets or sets the first wave sync pulse length.
    /// </summary>
    [BlockProperty(Order = 4)]
    public Word FirstWaveSyncPulseLength { get; set; }

    /// <summary>
    /// Gets or sets the second wave sync pulse length.
    /// </summary>
    [BlockProperty(Order = 5)]
    public Word SecondWaveSyncPulseLength { get; set; }

    /// <summary>
    /// Gets or sets the pulse length for the zero bit in the first wave.
    /// </summary>
    [BlockProperty(Order = 6)]
    public Word ZeroBitFirstWavePulseLength { get; set; }

    /// <summary>
    /// Gets or sets the pulse length for the zero bit in the second wave.
    /// </summary>
    [BlockProperty(Order = 7)]
    public Word ZeroBitSecondWavePulseLength { get; set; }

    /// <summary>
    /// Gets or sets the pulse length for the one bit in the first wave.
    /// </summary>
    [BlockProperty(Order = 8)]
    public Word OneBitFirstWavePulseLength { get; set; }

    /// <summary>
    /// Gets or sets the pulse length for the one bit in the second wave.
    /// </summary>
    [BlockProperty(Order = 9)]
    public Word OneBitSecondWavePulseLength { get; set; }

    /// <summary>
    /// Gets or sets the XOR checksum bit for each data byte.
    /// <remarks>
    /// </remarks>
    ///     00 - Start XOR checksum with value 0
    ///     01 - Start XOR checksum with value 1
    ///     FF - No checksum bit
    /// </summary>
    [BlockProperty(Order = 10)]
    public byte Checksum { get; set; }

    /// <summary>
    /// Gets or sets the pulse length for the finish byte in the first wave.
    /// </summary>
    [BlockProperty(Order = 11)]
    public Word FinishByteFirstWavePulseLength { get; set; }

    /// <summary>
    /// Gets or sets the pulse length for the finish byte in the second wave.
    /// </summary>
    [BlockProperty(Order = 12)]
    public Word FinishByteSecondWavePulseLength { get; set; }

    /// <summary>
    /// Gets or sets the pulse length for the finish data in the first wave.
    /// </summary>
    [BlockProperty(Order = 13)]
    public Word FinishDataFirstWavePulseLength { get; set; }

    /// <summary>
    /// Gets or sets the pulse length for the finish data in the second wave.
    /// </summary>
    [BlockProperty(Order = 14)]
    public Word FinishDataSecondWavePulseLength { get; set; }

    /// <summary>
    /// Gets or sets the pulse length for the trailing tone.
    /// </summary>
    [BlockProperty(Order = 15)]
    public Word TrailingTonePulseLength { get; set; }

    /// <summary>
    /// Gets or sets the number of waves in the trailing tone.
    /// </summary>
    [BlockProperty(Order = 16)]
    public Word NumberOfWavesInTrailingTone { get; set; }

    /// <summary>
    /// Gets or sets the used bits in the last byte (other bits should be 0). E.g. if this is 6, then the bits used (x)
    /// in the last byte are: xxxxxx00, where MSb is the leftmost bit, LSb is the rightmost bit.
    /// </summary>
    [BlockProperty(Order = 17)]
    public byte UsedBitsLastByte { get; set; }

    /// <summary>
    /// Gets or sets the general purpose byte.
    /// <remarks>
    /// </remarks>
    ///     bit 0 - Data Endianess (0=LSb first, 1=MSb first)
    /// </summary>
    [BlockProperty(Order = 18)]
    public byte GeneralPurpose { get; set; }

    /// <summary>
    /// Gets or sets the pause value (in millisecond) that should be applied after this block.
    /// </summary>
    [BlockProperty(Order = 19)]
    public Word PauseDuration { get; set; }

    /// <summary>
    /// Gets the length of the data.
    /// </summary>
    [BlockProperty(Order = 20, Size = 3)]
    public int DataLength => Data.Count;

    /// <summary>
    /// Gets or sets the data as in .TAP file.
    /// </summary>
    [BlockProperty(Order = 21)]
    public List<byte> Data { get; set; } = [];

    /// <summary>
    /// Creates a new instance of the 'C64 ROM Type Data' block.
    /// </summary>
    public C64RomTypeDataBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'C64 ROM Type Data' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal C64RomTypeDataBlock(ByteStreamReader reader)
    {
        reader.ReadDWord();
        PilotTonePulseLength = reader.ReadWord();
        NumberOfWavesInPilotTone = reader.ReadWord();
        FirstWaveSyncPulseLength = reader.ReadWord();
        SecondWaveSyncPulseLength = reader.ReadWord();
        ZeroBitFirstWavePulseLength = reader.ReadWord();
        ZeroBitSecondWavePulseLength = reader.ReadWord();
        OneBitFirstWavePulseLength = reader.ReadWord();
        OneBitSecondWavePulseLength = reader.ReadWord();
        Checksum = reader.ReadByte();
        FinishByteFirstWavePulseLength = reader.ReadWord();
        FinishByteSecondWavePulseLength = reader.ReadWord();
        FinishDataFirstWavePulseLength = reader.ReadWord();
        FinishDataSecondWavePulseLength = reader.ReadWord();
        TrailingTonePulseLength = reader.ReadWord();
        NumberOfWavesInTrailingTone = reader.ReadWord();
        UsedBitsLastByte = reader.ReadByte();
        GeneralPurpose = reader.ReadByte();
        PauseDuration = reader.ReadWord();
        var dataLength = reader.ReadByte() | reader.ReadByte() << 8 | reader.ReadByte() << 16;
        Data = [..reader.ReadBytes(dataLength)];
    }
}