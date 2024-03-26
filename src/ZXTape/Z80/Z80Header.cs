using OldBit.ZXTape.Extensions;
using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;

namespace OldBit.ZXTape.Z80;

/// <summary>
/// Represents the .z80 file header. Depending on the version, it may contain additional data.
/// </summary>
public sealed class Z80Header
{
    private byte[] _rawData;

    /// <summary>
    /// Gets or sets the raw header data.
    /// </summary>
    [FileData(Order = 0)]
    public byte[] RawData
    {
        get => _rawData;
        set => _rawData = value;
    }

    /// <summary>
    /// Gets or sets the A register.
    /// </summary>
    public byte A
    {
        get => RawData[0];
        set => RawData[0] = value;
    }

    /// <summary>
    /// Gets or sets the F register.
    /// </summary>
    public byte F
    {
        get => RawData[1];
        set => RawData[1] = value;
    }

    /// <summary>
    /// Gets or sets the BC register.
    /// </summary>
    public Word BC
    {
        get => RawData.GetWord(2);
        set => RawData.SetWord(2, value);
    }

    /// <summary>
    /// Gets or sets the HL register.
    /// </summary>
    public Word HL
    {
        get => RawData.GetWord(4);
        set => RawData.SetWord(4, value);
    }

    /// <summary>
    /// Gets or sets the PC register.
    /// </summary>
    public Word PC
    {
        get => RawData.GetWord(_rawData.Length == 30 ? 6 : 32);
        set => RawData.SetWord(_rawData.Length == 30 ? 6 : 32, value);
    }

    /// <summary>
    /// Gets or sets the SP register.
    /// </summary>
    public Word SP
    {
        get => RawData.GetWord(8);
        set => RawData.SetWord(8, value);
    }

    /// <summary>
    /// Gets or sets the I register.
    /// </summary>
    public byte I
    {
        get => RawData[10];
        set => RawData[10] = value;
    }

    /// <summary>
    /// Gets or sets the R register.
    /// </summary>
    public byte R
    {
        get => RawData[11];
        set => RawData[11] = value;
    }

    /// <summary>
    /// Gets or sets the DE register.
    /// </summary>
    public Word DE
    {
        get => RawData.GetWord(13);
        set => RawData.SetWord(13, value);
    }

    /// <summary>
    /// Gets or sets the BC' register.
    /// </summary>
    public Word BCPrime
    {
        get => RawData.GetWord(15);
        set => RawData.SetWord(15, value);
    }

    /// <summary>
    /// Gets or sets the DE' register.
    /// </summary>
    public Word DEPrime
    {
        get => RawData.GetWord(17);
        set => RawData.SetWord(17, value);
    }

    /// <summary>
    /// Gets or sets the HL' register.
    /// </summary>
    public Word HLPrime
    {
        get => RawData.GetWord(19);
        set => RawData.SetWord(19, value);
    }

    /// <summary>
    /// Gets or sets the A' register.
    /// </summary>
    public byte APrime
    {
        get => RawData[21];
        set => RawData[21] = value;
    }

    /// <summary>
    /// Gets or sets the F' register.
    /// </summary>
    public byte FPrime
    {
        get => RawData[22];
        set => RawData[22] = value;
    }

    /// <summary>
    /// Gets or sets the IY register.
    /// </summary>
    public Word IY
    {
        get => RawData.GetWord(23);
        set => RawData.SetWord(23, value);
    }

    /// <summary>
    /// Gets or sets the IX register.
    /// </summary>
    public Word IX
    {
        get => RawData.GetWord(25);
        set => RawData.SetWord(25, value);
    }

    /// <summary>
    /// Gets or sets the Interrupt flip-flop: 0=DI, otherwise EI.
    /// </summary>
    public byte Interrupt
    {
        get => RawData[27];
        set => RawData[27] = value;
    }

    /// <summary>
    /// Gets or sets the IFF2 value.
    /// </summary>
    public byte IFF2
    {
        get => RawData[28];
        set => RawData[28] = value;
    }

    /// <summary>
    /// Gets the version of the Z80 header.
    /// </summary>
    public int Version => _rawData.Length switch
    {
        30 => 1,
        53 => 2,
        _ => 3
    };

    /// <summary>
    /// Creates a new instance of the Z80Header header.
    /// </summary>
    public Z80Header(int headerSize = 54)
    {
        if (headerSize != 30 && headerSize != 53 && headerSize != 54 && headerSize != 55)
        {
            throw new ArgumentOutOfRangeException(nameof(headerSize), headerSize, "Invalid header size. Must be 30 for V1, 53 for V2, 54 or 55 for V3.");
        }

        _rawData = new byte[headerSize];
    }

    /// <summary>
    /// Creates a new instance of the Z80Header header.
    /// </summary>
    /// <param name="reader">The ByteStreamReader used to initialize the Z80Header properties.</param>
    internal Z80Header(ByteStreamReader reader)
    {
        _rawData = reader.ReadBytes(30);
        if (_rawData[6] != 0 || _rawData[7] != 0)
        {
            // V1, no additional header
            return;
        }

        // V2 or greater, read additional header
        var extraHeaderLength = reader.ReadWord();
        var extraHeader = reader.ReadBytes(extraHeaderLength);

        // Resize and copy the additional header data
        Array.Resize(ref _rawData, 30 + extraHeaderLength);
        Array.Copy(extraHeader, 0, _rawData, 30, extraHeaderLength);
    }
}