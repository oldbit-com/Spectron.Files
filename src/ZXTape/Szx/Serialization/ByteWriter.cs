using System.Text;

namespace OldBit.ZXTape.Szx.Serialization;

internal class ByteWriter
{
    private readonly List<byte> _data = [];

    public ReadOnlySpan<byte> GetData() => new(_data.ToArray());

    public void WriteWord(int value) => Write((byte)value, (byte)(value >> 8));

    public void WriteDWord(DWord value) =>
        Write((byte)value, (byte)(value >> 8), (byte)(value >> 16), (byte)(value >> 24));

    public void WriteByte(byte value) => _data.Add(value);

    public void WriteChars(string value, int size)
    {
        if (value.Length > size)
        {
            value = value[..size];
        }

        var padded = value.PadRight(size, '\0');
        var bytes = Encoding.ASCII.GetBytes(padded);

        WriteBytes(bytes);
    }

    public void WriteId(string id, int size)
    {
        var bytes = Encoding.ASCII.GetBytes(id);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }

        WriteBytes(bytes);
        WriteDWord((DWord)size);
    }

    public void WriteBytes(byte[] value) => _data.AddRange(value);

    private void Write(params byte[] values) => _data.AddRange(values);
}