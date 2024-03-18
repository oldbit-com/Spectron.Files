namespace OldBit.ZX.Tape.Net.Reader;

public interface IByteStreamReader
{
    /// <summary>
    /// Reads a byte from the stream and advances the position within the stream by one byte.
    /// </summary>
    /// <returns>The byte retrieved from the stream.</returns>
    byte ReadByte();

    /// <summary>
    /// Reads a word from the stream and advances the position within the stream by two bytes.
    /// </summary>
    /// <returns>The word retrieved from the stream.</returns>
    Word ReadWord();

    /// <summary>
    /// Reads a dword from the stream and advances the position within the stream by four bytes.
    /// </summary>
    /// <returns>The dword retrieved from the stream.</returns>
    DWord ReadDWord();

    /// <summary>
    /// Reads a sequence of bytes from the stream and advances the position within the stream by 'count' bytes.
    /// </summary>
    /// <param name="count">The number of bytes to read from the stream.</param>
    /// <returns>An array of bytes retrieved from the stream.</returns>
    byte[] ReadBytes(int count);

    /// <summary>
    /// Reads a sequence of words from the stream and advances the position within the stream by 2 * 'count' bytes.
    /// </summary>
    /// <param name="count">The number of words to read from the stream.</param>
    /// <returns>An array of words retrieved from the stream.</returns>
    Word[] ReadWords(int count);

    /// <summary>
    /// Attempts to read a byte from the stream.
    /// </summary>
    /// <param name="result">The byte retrieved from the stream or zero if data could not be read.</param>
    /// <returns>true if an item was read; otherwise, false.</returns>
    bool TryReadByte(out byte result);
}
