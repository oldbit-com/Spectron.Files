using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Blocks;
using OldBit.ZXTape.Szx.Serialization;

namespace OldBit.ZXTape.UnitTests.Szx.Blocks;

public class KeyboardBlockTests
{
    [Fact]
    public void Keyboard_ShouldConvertToBytes()
    {
        var keyboard = GetKeyboardBlock();
        var writer = new ByteWriter();

        keyboard.Write(writer);

        var data = writer.GetData();
        data.Length.Should().Be(8 + 5);

        // Header
        BitConverter.ToUInt32(data[..4].ToArray()).Should().Be(0x4259454B);
        BitConverter.ToUInt32(data[4..8].ToArray()).Should().Be(5);

        // Data
        BitConverter.ToUInt32(data[8..12].ToArray()).Should().Be(KeyboardBlock.FlagsIssue2);
        data[12].Should().Be(KeyboardBlock.JoystickKempston);
    }

    [Fact]
    public void Keyboard_ShouldConvertFromBytes()
    {
        var keyboardData = GetKeyboardBlockData();
        using var memoryStream = new MemoryStream(keyboardData);
        var reader = new ByteStreamReader(memoryStream);

        var keyboard = KeyboardBlock.Read(reader, keyboardData.Length);

        keyboard.Flags.Should().Be(KeyboardBlock.FlagsIssue2);
        keyboard.Joystick.Should().Be(KeyboardBlock.JoystickKempston);
    }

    private static byte[] GetKeyboardBlockData()
    {
        var keyboard = GetKeyboardBlock();
        var writer = new ByteWriter();

        keyboard.Write(writer);
        writer.GetData();

        return writer.GetData()[8..].ToArray();
    }

    private static KeyboardBlock GetKeyboardBlock() => new()
    {
        Flags = KeyboardBlock.FlagsIssue2,
        Joystick = KeyboardBlock.JoystickKempston
    };
}