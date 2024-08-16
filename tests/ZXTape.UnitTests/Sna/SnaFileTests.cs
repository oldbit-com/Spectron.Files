using OldBit.ZXTape.Serialization;
using OldBit.ZXTape.Sna;

namespace OldBit.ZXTape.UnitTests.Sna;

public class SnaFileTests
{
    [Fact]
    public void SnaFile_48K_ShouldDeserializeFromStream()
    {
        var data = GetSna48Data();

        using var stream = new MemoryStream(data);
        var snaFile = SnaFile.Load(stream);

        snaFile.Header.I.Should().Be(0x01);
        snaFile.Header.HLPrime.Should().Be(0x0302);
        snaFile.Header.DEPrime.Should().Be(0x0504);
        snaFile.Header.BCPrime.Should().Be(0x0706);
        snaFile.Header.AFPrime.Should().Be(0x0908);
        snaFile.Header.HL.Should().Be(0x0B0A);
        snaFile.Header.DE.Should().Be(0x0D0C);
        snaFile.Header.BC.Should().Be(0x0F0E);
        snaFile.Header.IY.Should().Be(0x1110);
        snaFile.Header.IX.Should().Be(0x1312);
        snaFile.Header.Interrupt.Should().Be(0x14);
        snaFile.Header.R.Should().Be(0x15);
        snaFile.Header.AF.Should().Be(0x1716);
        snaFile.Header.SP.Should().Be(0x1918);
        snaFile.Header.InterruptMode.Should().Be(0x1A);
        snaFile.Header.BorderColor.Should().Be(0x1B);
        snaFile.Ram48.Should().Equal(data[27..]);
        snaFile.Header128.Should().BeNull();
        snaFile.RamBanks.Should().BeNull();
    }

    [Fact]
    public void SnaFile_128K_ShouldDeserializeFromStream()
    {
        var data = GetSna128();
        using var stream = new MemoryStream(data);
        var snaFile = SnaFile.Load(stream);

        snaFile.Header.I.Should().Be(0x01);
        snaFile.Header.HLPrime.Should().Be(0x0302);
        snaFile.Header.DEPrime.Should().Be(0x0504);
        snaFile.Header.BCPrime.Should().Be(0x0706);
        snaFile.Header.AFPrime.Should().Be(0x0908);
        snaFile.Header.HL.Should().Be(0x0B0A);
        snaFile.Header.DE.Should().Be(0x0D0C);
        snaFile.Header.BC.Should().Be(0x0F0E);
        snaFile.Header.IY.Should().Be(0x1110);
        snaFile.Header.IX.Should().Be(0x1312);
        snaFile.Header.Interrupt.Should().Be(0x14);
        snaFile.Header.R.Should().Be(0x15);
        snaFile.Header.AF.Should().Be(0x1716);
        snaFile.Header.SP.Should().Be(0x1918);
        snaFile.Header.InterruptMode.Should().Be(0x1A);
        snaFile.Header.BorderColor.Should().Be(0x1B);
        snaFile.Ram48.Should().Equal(data[27..49179]);
        snaFile.Header128!.PC.Should().Be(0x1D1C);
        snaFile.Header128!.PageMode.Should().Be(0x05);
        snaFile.Header128!.TrDosRom.Should().Be(0x00);
        snaFile.RamBanks.Should().HaveCount(6);
        snaFile.RamBanks![0].Should().Equal(data.Skip(49183).Take(16384));
        snaFile.RamBanks![1].Should().Equal(data.Skip(65567).Take(16384));
        snaFile.RamBanks![2].Should().Equal(data.Skip(81951).Take(16384));
        snaFile.RamBanks![3].Should().Equal(data.Skip(98335).Take(16384));
        snaFile.RamBanks![4].Should().Equal(data.Skip(114719).Take(16384));
        snaFile.RamBanks![5].Should().Equal(data.Skip(131103).Take(16384));
    }

    [Fact]
    public void SnaFile_48K_ShouldSerializeToBytes()
    {
        var snaFile = new SnaFile()
        {
            Header = new SnaHeader
            {
                I = 0x01,
                HLPrime = 0x0302,
                DEPrime = 0x0504,
                BCPrime = 0x0706,
                AFPrime = 0x0908,
                HL = 0x0B0A,
                DE = 0x0D0C,
                BC = 0x0F0E,
                IY = 0x1110,
                IX = 0x1312,
                Interrupt = 0x14,
                R = 0x15,
                AF = 0x1716,
                SP = 0x1918,
                InterruptMode = 0x1A,
                BorderColor = 0x1B
            },
            Ram48 = Enumerable.Repeat((byte)0xFF, 49152).ToArray()
        };

        var result = FileDataSerializer.Serialize(snaFile);

        result.Should().Equal(GetSna48Data());
    }

    [Fact]
    public void SnaFile_128K_ShouldSerializeToBytes()
    {
        var snaFile = new SnaFile
        {
            Header = new SnaHeader
            {
                I = 0x01,
                HLPrime = 0x0302,
                DEPrime = 0x0504,
                BCPrime = 0x0706,
                AFPrime = 0x0908,
                HL = 0x0B0A,
                DE = 0x0D0C,
                BC = 0x0F0E,
                IY = 0x1110,
                IX = 0x1312,
                Interrupt = 0x14,
                R = 0x15,
                AF = 0x1716,
                SP = 0x1918,
                InterruptMode = 0x1A,
                BorderColor = 0x1B
            },
            Ram48 = Enumerable.Repeat((byte)0x40, 49152).ToArray(),
            Header128 = new SnaHeader128
            {
                PC = 0x1D1C,
                PageMode = 0x05,
                TrDosRom = 0x00
            },
            RamBanks = new List<byte[]>
            {
                Enumerable.Repeat((byte)0x50, 16384).ToArray(),
                Enumerable.Repeat((byte)0x60, 16384).ToArray(),
                Enumerable.Repeat((byte)0x70, 16384).ToArray(),
                Enumerable.Repeat((byte)0x70, 16384).ToArray(),
                Enumerable.Repeat((byte)0x80, 16384).ToArray(),
                Enumerable.Repeat((byte)0x90, 16384).ToArray()
            }
        };

        var result = FileDataSerializer.Serialize(snaFile);

        result.Should().Equal(GetSna128());
    }

    private static byte[] GetSna48Data()
    {
        var data = new byte[]
        {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A,
            0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10, 0x11, 0x12, 0x13, 0x14,
            0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B
        };
        Array.Resize(ref data, 49179);
        Array.Fill<byte>(data, 0xFF, 27, 0xC000);

        return data;
    }

    private static byte[] GetSna128()
    {
        var data = GetSna48Data();
        Array.Resize(ref data, 147487);
        Array.Fill<byte>(data, 0x40, 27, 0xC000);        // Blocks 2, 3, 5
        data[49179] = 0x1C; // PC
        data[49180] = 0x1D; // PC
        data[49181] = 0x05; // Page mode
        data[49182] = 0x00; // TR-DOS ROM paged
        Array.Fill<byte>(data, 0x50, 49183, 0x4000);     // Bank 0
        Array.Fill<byte>(data, 0x60, 65567, 0x4000);     // Bank 1
        Array.Fill<byte>(data, 0x70, 81951, 0x4000);     // Bank 4
        Array.Fill<byte>(data, 0x70, 98335, 0x4000);     // Bank 5 saved twice due to page setting
        Array.Fill<byte>(data, 0x80, 114719, 0x4000);    // Bank 6
        Array.Fill<byte>(data, 0x90, 131103, 0x4000);    // Bank 7

        return data;
    }
}