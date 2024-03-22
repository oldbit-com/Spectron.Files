namespace OldBit.ZXTape.IO;

/// <summary>
/// Represents a reader that helps reading bytes from a stream.
/// </summary>
internal sealed class ByteStreamReader : IByteStreamReader
{
    private readonly Stream _stream;

    /// <summary>
    /// Create a new instance of the byte reader.
    /// </summary>
    /// <param name="stream">The stream that provides data for the reader.</param>
    public ByteStreamReader(Stream stream)
    {
        _stream = stream;
    }

    /// <summary>
    /// Reads a byte from the stream and advances the position within the stream by one byte.
    /// </summary>
    /// <returns>The byte retrieved from the stream.</returns>
    /// <exception cref="EndOfStreamException">Thrown when not enough data is in the stream.</exception>
    public byte ReadByte()
    {
        if (!TryReadByte( out var data))
        {
            throw new EndOfStreamException();
        }

        return data;
    }

    /// <summary>
    /// Reads a word from the stream and advances the position within the stream by two bytes.
    /// </summary>
    /// <returns>The word retrieved from the stream.</returns>
    /// <exception cref="EndOfStreamException">Thrown when not enough data is in the stream.</exception>
    public Word ReadWord()
    {
        var byte1 = _stream.ReadByte();
        var byte2 = _stream.ReadByte();

        if (byte1 == -1 || byte2 == -1)
        {
            throw new EndOfStreamException();
        }

        return (Word)(byte1 | byte2 << 8);
    }

    /// <summary>
    /// Reads a dword from the stream and advances the position within the stream by four bytes.
    /// </summary>
    /// <returns>The dword retrieved from the stream.</returns>
    public DWord ReadDWord()
    {
        return (DWord)(ReadWord() | ReadWord() << 16);
    }

    /// <summary>
    /// Reads a sequence of bytes from the stream and advances the position within the stream by 'count' bytes.
    /// </summary>
    /// <param name="count">The number of bytes to read from the stream.</param>
    /// <returns>An array of bytes retrieved from the stream.</returns>
    /// <exception cref="EndOfStreamException">Thrown when not enough data is in the stream.</exception>
    public byte[] ReadBytes(int count)
    {
        var buffer = new byte[count];
        var data = _stream.Read(buffer);

        if (data != count)
        {
            throw new EndOfStreamException();
        }

        return buffer;
    }

    /// <summary>
    /// Reads a sequence of words from the stream and advances the position within the stream by 2 * 'count' bytes.
    /// </summary>
    /// <param name="count">The number of words to read from the stream.</param>
    /// <returns>An array of words retrieved from the stream.</returns>
    public Word[] ReadWords(int count)
    {
        var buffer = new byte[2 * count];
        var data = _stream.Read(buffer);

        if (data != 2 * count)
        {
            throw new EndOfStreamException();
        }

        var words = new Word[count];
        for (var i = 0; i < data; i +=2)
        {
            words[i / 2] = (Word)(buffer[i] | buffer[i + 1] << 8);
        }

        return words;
    }

    /// <summary>
    /// Attempts to read a byte from the stream.
    /// </summary>
    /// <param name="result">The byte retrieved from the stream or zero if data could not be read.</param>
    /// <returns>true if an item was read; otherwise, false.</returns>
    public bool TryReadByte(out byte result)
    {
        result = 0;
        var data = _stream.ReadByte();

        if (data == -1)
        {
            return false;
        }

        result = (byte)data;
        return true;
    }
}