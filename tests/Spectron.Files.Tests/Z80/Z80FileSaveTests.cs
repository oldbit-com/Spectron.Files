using OldBit.Spectron.Files.Z80;

namespace OldBit.Spectron.Files.Tests.Z80;

public class Z80FileSaveTests
{
    [Fact]
    public void ShouldSave48KSnapshotVersion3()
    {
        var snapshot = new Z80File
        {
            Header =
            {
                A = 0x01,
                F = 0x02,
                BC = 0x1721,
                HL = 0x19B6,
                PC = 0x1153,
                SP = 0xFF48,
                I = 0x3F,
                R = 0x89,
                Flags1 = { BorderColor = 7, IsDataCompressed = true },
                DE = 0x5CB9,
                BCPrime = 0x4566,
                DEPrime = 0x4294,
                HLPrime = 0xABCF,
            }
        };

        var stream = new MemoryStream();
        snapshot.Save(stream);
        var data = stream.ToArray();

        data[0].Should().Be(0x01);      // A
        data[1].Should().Be(0x02);      // F
        data[2].Should().Be(0x21);      // BC
        data[3].Should().Be(0x17);      // BC
        data[4].Should().Be(0xB6);      // HL
        data[5].Should().Be(0x19);      // HL
        data[6].Should().Be(0);         // PC == 0 indicates version 3
        data[7].Should().Be(0);         // PC == 0 indicates version 3
        data[8].Should().Be(0x48);      // SP
        data[9].Should().Be(0xFF);      // SP
        data[10].Should().Be(0x3F);     // I
        data[11].Should().Be(0x89);     // R
        data[12].Should().Be(0x2F);     // Flags1
        data[13].Should().Be(0xB9);     // DE
        data[14].Should().Be(0x5C);     // DE
        data[15].Should().Be(0x66);     // BC'
        data[16].Should().Be(0x45);     // BC'
        data[17].Should().Be(0x94);     // DE'
        data[18].Should().Be(0x42);     // DE'
        data[19].Should().Be(0xCF);     // HL'
        data[20].Should().Be(0xAB);     // HL'

        data[32].Should().Be(0x53);     // PC
        data[33].Should().Be(0x11);     // PC
    }
}