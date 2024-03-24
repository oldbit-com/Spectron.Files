using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;
using OldBit.ZXTape.Sna;

namespace OldBit.ZXTape.UnitTests.Sna;

public class SnaDataTests
{
    [Fact]
    public void SnaData_48K_ShouldDeserializeFromStream()
    {
        var data = GetSna48Data();

        using var stream = new MemoryStream(data);
        var snaData = new SnaData(new ByteStreamReader(stream));

        snaData.Header.I.Should().Be(0x01);
        snaData.Header.HLPrime.Should().Be(0x0302);
        snaData.Header.DEPrime.Should().Be(0x0504);
        snaData.Header.BCPrime.Should().Be(0x0706);
        snaData.Header.AFPrime.Should().Be(0x0908);
        snaData.Header.HL.Should().Be(0x0B0A);
        snaData.Header.DE.Should().Be(0x0D0C);
        snaData.Header.BC.Should().Be(0x0F0E);
        snaData.Header.IY.Should().Be(0x1110);
        snaData.Header.IX.Should().Be(0x1312);
        snaData.Header.Interrupt.Should().Be(0x14);
        snaData.Header.R.Should().Be(0x15);
        snaData.Header.AF.Should().Be(0x1716);
        snaData.Header.SP.Should().Be(0x1918);
        snaData.Header.InterruptMode.Should().Be(0x1A);
        snaData.Header.Border.Should().Be(0x1B);
        snaData.Ram48.Should().Equal(data[27..]);
        snaData.Header128.Should().BeNull();
        snaData.RamBanks.Should().BeNull();
    }

    [Fact]
    public void SnaData_128K_ShouldDeserializeFromStream()
    {
        var data = GetSna128Data();
        using var stream = new MemoryStream(data);
        var snaData = new SnaData(new ByteStreamReader(stream));

        snaData.Header.I.Should().Be(0x01);
        snaData.Header.HLPrime.Should().Be(0x0302);
        snaData.Header.DEPrime.Should().Be(0x0504);
        snaData.Header.BCPrime.Should().Be(0x0706);
        snaData.Header.AFPrime.Should().Be(0x0908);
        snaData.Header.HL.Should().Be(0x0B0A);
        snaData.Header.DE.Should().Be(0x0D0C);
        snaData.Header.BC.Should().Be(0x0F0E);
        snaData.Header.IY.Should().Be(0x1110);
        snaData.Header.IX.Should().Be(0x1312);
        snaData.Header.Interrupt.Should().Be(0x14);
        snaData.Header.R.Should().Be(0x15);
        snaData.Header.AF.Should().Be(0x1716);
        snaData.Header.SP.Should().Be(0x1918);
        snaData.Header.InterruptMode.Should().Be(0x1A);
        snaData.Header.Border.Should().Be(0x1B);
        snaData.Ram48.Should().Equal(data[27..49179]);
        snaData.Header128!.PC.Should().Be(0x1D1C);
        snaData.Header128!.PageMode.Should().Be(0x05);
        snaData.Header128!.TrDosRom.Should().Be(0x00);
        snaData.RamBanks.Should().HaveCount(6);
        snaData.RamBanks![0].Should().Equal(data.Skip(49183).Take(16384));
        snaData.RamBanks![1].Should().Equal(data.Skip(65567).Take(16384));
        snaData.RamBanks![2].Should().Equal(data.Skip(81951).Take(16384));
        snaData.RamBanks![3].Should().Equal(data.Skip(98335).Take(16384));
        snaData.RamBanks![4].Should().Equal(data.Skip(114719).Take(16384));
        snaData.RamBanks![5].Should().Equal(data.Skip(131103).Take(16384));
    }

    [Fact]
    public void SnaData_48K_ShouldSerializeToBytes()
    {
        var snaData = new SnaData
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
                Border = 0x1B
            },
            Ram48 = Enumerable.Repeat((byte)0xFF, 49152).ToList()
        };

        var result = FileDataSerializer.Serialize(snaData);

        result.Should().Equal(GetSna48Data());
    }

    [Fact]
    public void SnaData_128K_ShouldSerializeToBytes()
    {
        var snaData = new SnaData
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
                Border = 0x1B
            },
            Ram48 = Enumerable.Repeat((byte)0x40, 49152).ToList(),
            Header128 = new SnaHeader128
            {
                PC = 0x1D1C,
                PageMode = 0x05,
                TrDosRom = 0x00
            },
            RamBanks = new List<List<byte>>
            {
                Enumerable.Repeat((byte)0x50, 16384).ToList(),
                Enumerable.Repeat((byte)0x60, 16384).ToList(),
                Enumerable.Repeat((byte)0x70, 16384).ToList(),
                Enumerable.Repeat((byte)0x70, 16384).ToList(),
                Enumerable.Repeat((byte)0x80, 16384).ToList(),
                Enumerable.Repeat((byte)0x90, 16384).ToList()
            }
        };

        var result = FileDataSerializer.Serialize(snaData);

        result.Should().Equal(GetSna128Data());
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

    private static byte[] GetSna128Data()
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