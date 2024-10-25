using System.Reflection;
using OldBit.ZX.Files.Z80;
using OldBit.ZX.Files.Z80.Types;

namespace OldBit.ZX.Files.Tests.Z80;

public class Z80FileLoadTests
{
    [Fact]
    public void ShouldLoad48KSnapshotVersion3()
    {
        using var file = LoadTestFile("Test48.z80");
        var snapshot = Z80File.Load(file);

        snapshot.Header.Version.Should().Be(3);
        snapshot.Header.ExtraHeaderLength.Should().Be(55);
        snapshot.Header.HardwareMode.Should().Be(HardwareMode.Spectrum48);

        snapshot.Header.A.Should().Be(0x01);
        snapshot.Header.F.Should().Be(0x5C);

        snapshot.Header.APrime.Should().Be(0x02);
        snapshot.Header.FPrime.Should().Be(0x44);

        snapshot.Header.BC.Should().Be(0x1721);
        snapshot.Header.DE.Should().Be(0x5CB9);
        snapshot.Header.HL.Should().Be(0x5CB6);
        snapshot.Header.IX.Should().Be(0x03D4);
        snapshot.Header.IY.Should().Be(0x5C3A);

        snapshot.Header.BCPrime.Should().Be(0x1720);
        snapshot.Header.DEPrime.Should().Be(0x0097);
        snapshot.Header.HLPrime.Should().Be(0x0017);

        snapshot.Header[6].Should().Be(0x00);       // Version 1 PC
        snapshot.Header[7].Should().Be(0x00);       // Version 1 PC
        snapshot.Header.SP.Should().Be(0xFF48);
        snapshot.Header.I.Should().Be(0x3F);
        snapshot.Header.R.Should().Be(0x21);
        snapshot.Header.IFF1.Should().Be(0x00);
        snapshot.Header.IFF2.Should().Be(0x00);

        // Flags1
        snapshot.Header[12].Should().Be(0x04);
        snapshot.Header.Flags1.Bit7R.Should().Be(0);
        snapshot.Header.Flags1.BorderColor.Should().Be(2);
        snapshot.Header.Flags1.IsSamRam.Should().BeFalse();
        snapshot.Header.Flags1.IsDataCompressed.Should().BeFalse();

        // Flags2
        snapshot.Header[29].Should().Be(0x41);
        snapshot.Header.Flags2.InterruptMode.Should().Be(1);
        snapshot.Header.Flags2.Issue2Emulation.Should().BeFalse();
        snapshot.Header.Flags2.DoubleInterruptFrequency.Should().BeFalse();
        snapshot.Header.Flags2.VideoSynchronization.Should().Be(0);
        snapshot.Header.Flags2.JoystickType.Should().Be(JoystickType.Kempston);

        // Flags3
        snapshot.Header[37].Should().Be(0);

        snapshot.Header.HighTStateCounter.Should().Be(0);
        snapshot.Header.LowTStateCounter.Should().Be(0);

        snapshot.MemoryBlocks.Should().HaveCount(3);
    }

    [Fact]
    public void ShouldLoad128KSnapshotVersion3()
    {
        using var file = LoadTestFile("Test128.z80");
        var snapshot = Z80File.Load(file);

        snapshot.Header.Version.Should().Be(3);
        snapshot.Header.ExtraHeaderLength.Should().Be(55);
        snapshot.Header.HardwareMode.Should().Be(HardwareMode.Spectrum128);

        snapshot.Header.A.Should().Be(0xDD);
        snapshot.Header.F.Should().Be(0x74);

        snapshot.Header.APrime.Should().Be(0x00);
        snapshot.Header.FPrime.Should().Be(0x44);

        snapshot.Header.BC.Should().Be(0x1300);
        snapshot.Header.DE.Should().Be(0x1213);
        snapshot.Header.HL.Should().Be(0x5C3B);
        snapshot.Header.IX.Should().Be(0xFD6C);
        snapshot.Header.IY.Should().Be(0x5C3A);

        snapshot.Header.BCPrime.Should().Be(0x1801);
        snapshot.Header.DEPrime.Should().Be(0x0068);
        snapshot.Header.HLPrime.Should().Be(0x0038);

        snapshot.Header[6].Should().Be(0x00);       // Version 1 PC
        snapshot.Header[7].Should().Be(0x00);       // Version 1 PC
        snapshot.Header.SP.Should().Be(0x5BF9);
        snapshot.Header.I.Should().Be(0x00);
        snapshot.Header.R.Should().Be(0x37);
        snapshot.Header.IFF1.Should().Be(0x00);
        snapshot.Header.IFF2.Should().Be(0x00);

        // Flags1
        snapshot.Header[12].Should().Be(0x0C);
        snapshot.Header.Flags1.Bit7R.Should().Be(0);
        snapshot.Header.Flags1.BorderColor.Should().Be(6);
        snapshot.Header.Flags1.IsSamRam.Should().BeFalse();
        snapshot.Header.Flags1.IsDataCompressed.Should().BeFalse();

        // Flags2
        snapshot.Header[29].Should().Be(0x01);
        snapshot.Header.Flags2.InterruptMode.Should().Be(1);
        snapshot.Header.Flags2.Issue2Emulation.Should().BeFalse();
        snapshot.Header.Flags2.DoubleInterruptFrequency.Should().BeFalse();
        snapshot.Header.Flags2.VideoSynchronization.Should().Be(0);
        snapshot.Header.Flags2.JoystickType.Should().Be(JoystickType.Cursor);

        // Flags3
        snapshot.Header[37].Should().Be(0x04);
        snapshot.Header.Flags3?.UseAYSound.Should().BeTrue();

        snapshot.Header.HighTStateCounter.Should().Be(0);
        snapshot.Header.LowTStateCounter.Should().Be(0);

        snapshot.MemoryBlocks.Should().HaveCount(8);
    }

    private static FileStream LoadTestFile(string fileName)
    {
        var location = typeof(Z80FileLoadTests).GetTypeInfo().Assembly.Location;
        var dir = Path.GetDirectoryName(location) ?? throw new InvalidOperationException();
        var path =  Path.Combine(dir, "TestFiles", fileName);

        return File.OpenRead(path);
    }
}