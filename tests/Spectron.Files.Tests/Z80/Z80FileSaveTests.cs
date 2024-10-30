using OldBit.Spectron.Files.Z80;
using OldBit.Spectron.Files.Z80.Types;

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
                Flags1 =
                {
                    BorderColor = 7,
                    IsDataCompressed = true
                },
                DE = 0x5CB9,
                BCPrime = 0x4566,
                DEPrime = 0x4294,
                HLPrime = 0xABCF,
                APrime = 0x04,
                FPrime = 0x05,
                IY = 0x1110,
                IX = 0x1312,
                IFF1 = 1,
                IFF2 = 0,
                Flags2 =
                {
                    InterruptMode = 1,
                    Issue2Emulation = true,
                    DoubleInterruptFrequency = true,
                    VideoSynchronization = 2,
                    JoystickType = JoystickType.Sinclair2
                },
                HardwareMode = HardwareMode.Spectrum48 | HardwareMode.Interface1,
                Port7FFD = 0xA5,
                IsInterface1RomPage = true,
                PortFFFD = 0x23,
                AyRegisters = Enumerable.Range(1, 16).Select(x => (byte)x).ToArray(),
            }
        };
        snapshot.Header.Flags3!.EmulateRegisterR = true;
        snapshot.Header.Flags3.EmulateLdirInstruction = true;
        snapshot.Header.Flags3.UseAySound = true;
        snapshot.Header.Flags3.EmulateFullerAudioBox = true;
        snapshot.Header.Flags3.ModifyHardware = true;

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
        data[21].Should().Be(0x04);     // A'
        data[22].Should().Be(0x05);     // F'
        data[23].Should().Be(0x10);     // IY
        data[24].Should().Be(0x11);     // IY
        data[25].Should().Be(0x12);     // IX
        data[26].Should().Be(0x13);     // IX
        data[27].Should().Be(1);        // IFF1
        data[28].Should().Be(0);        // IFF2
        data[29].Should().Be(0xED);     // Flags2
        data[30].Should().Be(0x36);     // Extra header length
        data[31].Should().Be(0);        // Extra header length
        data[32].Should().Be(0x53);     // PC
        data[33].Should().Be(0x11);     // PC
        data[34].Should().Be(1);        // Hardware mode
        data[35].Should().Be(0xA5);     // Port 7FFD
        data[36].Should().Be(0xFF);     // Interface 1 ROM paged
        data[37].Should().Be(0xC7);     // Flags3
        data[38].Should().Be(0x23);     // Port FFFD
        data[39..55].Should().BeEquivalentTo(Enumerable.Range(1, 16)); // AY registers

        data[32].Should().Be(0x53);     // PC
        data[33].Should().Be(0x11);     // PC
    }
}