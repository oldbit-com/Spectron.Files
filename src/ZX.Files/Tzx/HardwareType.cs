namespace OldBit.ZX.Files.Tzx;

public static class HardwareType
{
    public const byte Computer = 0x00;
    public const byte ExternalStorage = 0x01;
    public const byte MemoryAddOn = 0x02;
    public const byte SoundDevice = 0x03;
    public const byte Joystick = 0x04;
    public const byte Mouse = 0x05;
    public const byte OtherController = 0x06;
    public const byte SerialPort = 0x07;
    public const byte ParallelPort = 0x08;
    public const byte Printer = 0x09;
    public const byte Modem = 0x0A;
    public const byte Digitizer = 0x0B;
    public const byte NetworkAdapter = 0x0C;
    public const byte Keyboard = 0x0D;
    public const byte AdDaConverter = 0x0E;
    public const byte EpromProgrammer = 0x0F;
    public const byte Graphics = 0x10;

    public static string GetName(byte id) => id switch
    {
        Computer => "Computer",
        ExternalStorage => "External Storage",
        MemoryAddOn => "ROM/RAM type add-on",
        SoundDevice => "Sound Device",
        Joystick => "Joystick",
        Mouse => "Mouse",
        OtherController => "Other Controller",
        SerialPort => "Serial Port",
        ParallelPort => "Parallel Port",
        Printer => "Printer",
        Modem => "Modem",
        Digitizer => "Digitizer",
        NetworkAdapter => "Network Adapter",
        Keyboard => "Keyboard/Keypad",
        AdDaConverter => "AD/DA Converter",
        EpromProgrammer => "EPROM Programmer",
        Graphics => "Graphics",
        _ => "Unknown Hardware Type"
    };
}

public static class ComputerType
{
    public const byte ZxSpectrum16 = 0x00;
    public const byte ZxSpectrum48 = 0x01;
    public const byte ZxSpectrum48Issue1 = 0x02;
    public const byte ZxSpectrum128 = 0x03;
    public const byte ZxSpectrum128Plus2 = 0x04;
    public const byte ZxSpectrum128Plus2APlus3 = 0x05;
    public const byte Timex2048 = 0x06;
    public const byte Timex2068 = 0x07;
    public const byte Pentagon128 = 0x08;
    public const byte SamCoupe = 0x09;
    public const byte DidaktikM = 0x0A;
    public const byte DidaktikGama = 0x0B;
    public const byte Zx80 = 0x0C;
    public const byte Zx81 = 0x0D;
    public const byte ZxSpectrum128Spanish = 0x0E;
    public const byte ZxSpectrumArabic = 0x0F;
    public const byte MicrodigitalTk90X = 0x10;
    public const byte MicrodigitalTk95 = 0x11;
    public const byte Byte = 0x12;
    public const byte Elwro800 = 0x13;
    public const byte ZxScorpion256 = 0x14;
    public const byte AmstradCpc464 = 0x15;
    public const byte AmstradCpc664 = 0x16;
    public const byte AmstradCpc6128 = 0x17;
    public const byte AmstradCpc464Plus = 0x18;
    public const byte AmstradCpc6128Plus = 0x19;
    public const byte JupiterAce = 0x1A;
    public const byte Enterprise = 0x1B;
    public const byte Commodore64 = 0x1C;
    public const byte Commodore128 = 0x1D;
    public const byte InvesSpectrumPlus = 0x1E;
    public const byte Profi = 0x1F;
    public const byte GrandRomMax = 0x20;
    public const byte Kay1024 = 0x21;
    public const byte IceFelixHc91 = 0x22;
    public const byte IceFelixHc2000 = 0x23;
    public const byte AmaterskeRasioMistrum = 0x24;
    public const byte Quorum128 = 0x25;
    public const byte MicroArtAtm = 0x26;
    public const byte MicroArtAatmTurbo2 = 0x27;
    public const byte Chrome = 0x28;
    public const byte ZxBadaloc = 0x29;
    public const byte Timex1500 = 0x2A;
    public const byte Lambda = 0x2B;
    public const byte Tk65 = 0x2C;
    public const byte Zx97 = 0x2D;

    public static string GetName(byte id) => id switch
    {
        ZxSpectrum16 => "ZX Spectrum 16k",
        ZxSpectrum48 => "ZX Spectrum 48k, Plus",
        ZxSpectrum48Issue1 => "ZX Spectrum 48k ISSUE 1",
        ZxSpectrum128 => "ZX Spectrum 128k +(Sinclair)",
        ZxSpectrum128Plus2 => "ZX Spectrum 128k +2 (grey case)",
        ZxSpectrum128Plus2APlus3 => "ZX Spectrum 128k +2A, +3",
        Timex2048 => "Timex Sinclair TC-2048",
        Timex2068 => "Timex Sinclair TS-2068",
        Pentagon128 => "Pentagon 128",
        SamCoupe => "Sam Coupe",
        DidaktikM => "Didaktik M",
        DidaktikGama => "Didaktik Gama",
        Zx80 => "ZX-80",
        Zx81 => "ZX-81",
        ZxSpectrum128Spanish => "ZX Spectrum 128k, Spanish version",
        ZxSpectrumArabic => "ZX Spectrum, Arabic version",
        MicrodigitalTk90X => "Microdigital TK 90-X",
        MicrodigitalTk95 => "Microdigital TK 95",
        Byte => "Byte",
        Elwro800 => "Elwro 800-3",
        ZxScorpion256 => "ZS Scorpion 256",
        AmstradCpc464 => "Amstrad CPC 464",
        AmstradCpc664 => "Amstrad CPC 664",
        AmstradCpc6128 => "Amstrad CPC 6128",
        AmstradCpc464Plus => "Amstrad CPC 464+",
        AmstradCpc6128Plus => "Amstrad CPC 6128+",
        JupiterAce => "Jupiter ACE",
        Enterprise => "Enterprise",
        Commodore64 => "Commodore 64",
        Commodore128 => "Commodore 128",
        InvesSpectrumPlus => "Inves Spectrum+",
        Profi => "Profi",
        GrandRomMax => "GrandRomMax",
        Kay1024 => "Kay 1024",
        IceFelixHc91 => "Ice Felix HC 91",
        IceFelixHc2000 => "Ice Felix HC 2000",
        AmaterskeRasioMistrum => "Amaterske RADIO Mistrum",
        Quorum128 => "Quorum 128",
        MicroArtAtm => "MicroART ATM",
        MicroArtAatmTurbo2 => "MicroART ATM Turbo 2",
        Chrome => "Chrome",
        ZxBadaloc => "ZX Badaloc",
        Timex1500 => "TS-1500",
        Lambda => "Lambda",
        Tk65 => "TK-65",
        Zx97 => "ZX-97",
        _ => "Unknown Computer Type"
    };
}

public static class ExternalStorageType
{
    public const byte ZxMicrodrive = 0x00;
    public const byte OpusDiscovery = 0x01;
    public const byte MgtDisciple = 0x02;
    public const byte MgtPlusD = 0x03;
    public const byte RotronicsWafadrive = 0x04;
    public const byte BetaDisk = 0x05;
    public const byte ByteDrive = 0x06;
    public const byte Watsford = 0x07;
    public const byte Fiz = 0x08;
    public const byte Radofin = 0x09;
    public const byte DidaktikDiskDrives = 0x0A;
    public const byte Mb02 = 0x0B;
    public const byte ZxSpectrumPlus3DiskDrive = 0x0C;
    public const byte JloDiskInterface = 0x0D;
    public const byte TimexFdd3000 = 0x0E;
    public const byte ZebraDiskDrive = 0x0F;
    public const byte RamexMillenia = 0x10;
    public const byte Larken = 0x11;
    public const byte KempstonDiskInterface = 0x12;
    public const byte Sandy = 0x13;
    public const byte ZxSpectrumPlus3EHardDisk = 0x14;
    public const byte ZxAtaSp = 0x15;
    public const byte DivIde = 0x16;
    public const byte ZxCf = 0x17;

    public static string GetName(byte id) => id switch
    {
        ZxMicrodrive => "ZX Microdrive",
        OpusDiscovery => "Opus Discovery",
        MgtDisciple => "MGT Disciple",
        MgtPlusD => "MGT Plus-D",
        RotronicsWafadrive => "Rotronics Wafadrive",
        BetaDisk => "TR-DOS (BetaDisk)",
        ByteDrive => "Byte Drive",
        Watsford => "Watsford",
        Fiz => "FIZ",
        Radofin => "Radofin",
        DidaktikDiskDrives => "Didaktik disk drives",
        Mb02 => "BS-DOS (MB-02)",
        ZxSpectrumPlus3DiskDrive => "ZX Spectrum +3 disk drive",
        JloDiskInterface => "JLO (Oliger) disk interface",
        TimexFdd3000 => "Timex FDD3000",
        ZebraDiskDrive => "Zebra disk drive",
        RamexMillenia => "Ramex Millenia",
        Larken => "Larken",
        KempstonDiskInterface => "Kempston disk interface",
        Sandy => "Sandy",
        ZxSpectrumPlus3EHardDisk => "ZX Spectrum +3e hard disk",
        ZxAtaSp => "ZXATASP",
        DivIde => "DivIDE",
        ZxCf => "ZXCF",
        _ => "Unknown External Storage"
    };
}

public static class MemoryAddOnType
{
    public const byte SamRam = 0x00;
    public const byte MultifaceOne = 0x01;
    public const byte Multiface128 = 0x02;
    public const byte MultifacePlus3 = 0x03;
    public const byte MultiPrint = 0x04;
    public const byte Mb02 = 0x05;
    public const byte SoftRom = 0x06;
    public const byte Memory1K = 0x07;
    public const byte Memory16K = 0x08;
    public const byte Memory48K = 0x09;
    public const byte Memory8To16K = 0x0A;

    public static string GetName(byte id) => id switch
    {
        SamRam => "Sam Ram",
        MultifaceOne => "Multiface ONE",
        Multiface128 => "Multiface 128k",
        MultifacePlus3 => "Multiface +3",
        MultiPrint => "MultiPrint",
        Mb02 => "MB-02 ROM/RAM expansion",
        SoftRom => "SoftROM",
        Memory1K => "1k",
        Memory16K => "16k",
        Memory48K => "48k",
        Memory8To16K => "Memory in 8-16k used",
        _ => "Unknown Memory Add-On"
    };
}

public static class SoundDeviceType
{
    public const byte ClassicAy = 0x00;
    public const byte FullerBox = 0x01;
    public const byte CurrahMicroSpeech = 0x02;
    public const byte SpecDrum = 0x03;
    public const byte AyAcbStereo = 0x04;
    public const byte AyAbcStereo = 0x05;
    public const byte RamMusicMachine = 0x06;
    public const byte Covox = 0x07;
    public const byte GeneralSound = 0x08;
    public const byte IntecB8001 = 0x09;
    public const byte ZonX = 0x0A;
    public const byte QuickSilvaSoundBoard = 0x0B;
    public const byte JupiterAce = 0x0C;

    public static string GetName(byte id) => id switch
    {
        ClassicAy => "Classic AY hardware (compatible with 128k ZXs)",
        FullerBox => "Fuller Box AY sound hardware",
        CurrahMicroSpeech => "Currah microSpeech",
        SpecDrum => "SpecDrum",
        AyAcbStereo => "AY ACB stereo (A+C=left, B+C=right); Melodik",
        AyAbcStereo => "AY ABC stereo (A+B=left, B+C=right)",
        RamMusicMachine => "RAM Music Machine",
        Covox => "Covox",
        GeneralSound => "General Sound",
        IntecB8001 => "Intec Electronics Digital Interface B8001",
        ZonX => "Zon-X AY",
        QuickSilvaSoundBoard => "QuickSilva AY",
        JupiterAce => "Jupiter ACE",
        _ => "Unknown Sound Device"
    };
}

public static class JoystickType
{
    public const byte Kempston = 0x00;
    public const byte Cursor = 0x01;
    public const byte Sinclair2Left = 0x02;
    public const byte Sinclair1Right = 0x03;
    public const byte Fuller = 0x04;

    public static string GetName(byte id) => id switch
    {
        Kempston => "Kempston",
        Cursor => "Cursor, Protek, AGF",
        Sinclair2Left => "Sinclair 2 Left (12345)",
        Sinclair1Right => "Sinclair 1 Right (67890)",
        Fuller => "Fuller",
        _ => "Unknown Joystick Type"
    };
}

public static class MouseType
{
    public const byte Amx = 0x00;
    public const byte Kempston = 0x01;

    public static string GetName(byte id) => id switch
    {
        Amx => "AMX mouse",
        Kempston => "Kempston mouse",
        _ => "Unknown Mouse Type"
    };
}

public static class OtherControllerType
{
    public const byte Trickstick = 0x00;
    public const byte ZxLightGun = 0x01;
    public const byte ZebraGraphicsTablet = 0x02;
    public const byte DefenderLightGun = 0x03;

    public static string GetName(byte id) => id switch
    {
        Trickstick => "Trickstick",
        ZxLightGun => "ZX Light Gun",
        ZebraGraphicsTablet => "Zebra Graphics Tablet",
        DefenderLightGun => "Defender Light Gun",
        _ => "Unknown Other Controller"
    };
}

public static class SerialPortType
{
    public const byte ZxInterface1 = 0x00;
    public const byte ZxSpectrum128 = 0x01;

    public static string GetName(byte id) => id switch
    {
        ZxInterface1 => "ZX Interface 1",
        ZxSpectrum128 => "ZX Spectrum 128k",
        _ => "Unknown Serial Port Type"
    };
}

public static class ParallelPortType
{
    public const byte KempstonS = 0x00;
    public const byte KempstonE = 0x01;
    public const byte ZxSpectrumPlus3 = 0x02;
    public const byte Tasman = 0x03;
    public const byte DkTronics = 0x04;
    public const byte Hilderbay = 0x05;
    public const byte InesPrinterface = 0x06;
    public const byte ZxLPrintInterface3 = 0x07;
    public const byte MultiPrint = 0x08;
    public const byte OpusDiscovery = 0x09;
    public const byte Standard8255Chip = 0x0A;

    public static string GetName(byte id) => id switch
    {
        KempstonS => "Kempston S",
        KempstonE => "Kempston E",
        ZxSpectrumPlus3 => "ZX Spectrum +3",
        Tasman => "Tasman",
        DkTronics => "DK'Tronics",
        Hilderbay => "Hilderbay",
        InesPrinterface => "INES Printerface",
        ZxLPrintInterface3 => "ZX LPrint Interface 3",
        MultiPrint => "MultiPrint",
        OpusDiscovery => "Opus Discovery",
        Standard8255Chip => "Standard 8255 chip with ports 31,63,95",
        _ => "Unknown Parallel Port Type"
    };
}

public static class PrinterType
{
    public const byte ZxPrinterCompatible = 0x00;
    public const byte GenericPrinter = 0x01;
    public const byte EpsonCompatible = 0x02;

    public static string GetName(byte id) => id switch
    {
        ZxPrinterCompatible => "ZX Printer, Alphacom 32 & compatibles",
        GenericPrinter => "Generic printer",
        EpsonCompatible => "EPSON compatible",
        _ => "Unknown Printer Type"
    };
}

public static class ModemType
{
    public const byte PrismVtx5000 = 0x00;
    public const byte Ts2050Westridge2050 = 0x01;

    public static string GetName(byte id) => id switch
    {
        PrismVtx5000 => "Prism VTX 5000",
        Ts2050Westridge2050 => "T/S 2050 or Westridge 2050",
        _ => "Unknown Modem Type"
    };
}

public static class DigitizerType
{
    public const byte RdDigitalTracer = 0x00;
    public const byte DkTronicsLightPen = 0x01;
    public const byte BritishMicroGraphPad = 0x02;
    public const byte RomanticRobotVideoface = 0x03;

    public static string GetName(byte id) => id switch
    {
        RdDigitalTracer => "RD Digital Tracer",
        DkTronicsLightPen => "DK'Tronics Light Pen",
        BritishMicroGraphPad => "British MicroGraph Pad",
        RomanticRobotVideoface => "Romantic Robot Videoface",
        _ => "Unknown Digitizer Type"
    };
}

public static class NetworkAdapterType
{
    public const byte ZxInterface1 = 0x00;

    public static string GetName(byte id) => id switch
    {
        ZxInterface1 => "ZX Interface 1",
        _ => "Unknown Network Adapter Type"
    };
}

public static class KeyboardKeypadType
{
    public const byte ZxSpectrum128Keypad = 0x00;

    public static string GetName(byte id) => id switch
    {
        ZxSpectrum128Keypad => "Keypad for ZX Spectrum 128k",
        _ => "Unknown Keyboard Keypad Type"
    };
}

public static class AdDaConverterType
{
    public const byte HarleySystemsAdc = 0x00;
    public const byte BlackboardElectronics = 0x01;

    public static string GetName(byte id) => id switch
    {
        HarleySystemsAdc => "Harley Systems ADC 8.2",
        BlackboardElectronics => "Blackboard Electronics",
        _ => "Unknown ADDA Converter Type"
    };
}

public static class EpromProgrammerType
{
    public const byte OrmeElectronics = 0x00;

    public static string GetName(byte id) => id switch
    {
        OrmeElectronics => "Orme Electronics",
        _ => "Unknown EPROM Programmer Type"
    };
}

public static class GraphicsType
{
    public const byte WrxHiRes = 0x00;
    public const byte G007 = 0x01;
    public const byte Memotech = 0x02;
    public const byte LambdaColour = 0x03;

    public static string GetName(byte id) => id switch
    {
        WrxHiRes => "WRX Hi-Res",
        G007 => "G007",
        Memotech => "Memotech",
        LambdaColour => "Lambda Colour",
        _ => "Unknown Graphics Type"
    };
}

