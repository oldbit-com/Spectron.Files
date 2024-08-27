using OldBit.ZXTape.IO;
using OldBit.ZXTape.Szx.Blocks;

namespace OldBit.ZXTape.Szx;

/// <summary>
/// Represents a .szx file.
/// </summary>
public sealed class ZxStateFile
{
    public ZxStateHeader Header { get; private set; } = new();

    /// <summary>
    /// Gets or sets the program that created this zx-state file.
    /// </summary>
    public CreatorBlock? Creator { get; set; }

    /// <summary>
    /// Gets the Z80 registers and other internal state values.
    /// </summary>
    public Z80RegsBlock Z80Registers { get; private set; } = new();

    /// <summary>
    /// Gets the Spectrum's ULA state specifying the current border colour, memory paging status etc.
    /// </summary>
    public SpecRegsBlock SpecRegs { get; private set; } = new();

    /// <summary>
    /// Gets or sets the status of the ZX Printer.
    /// </summary>
    public ZxPrinterBlock? ZxPrinter { get; set; }

    /// <summary>
    /// Gets or sets the state of the Spectrum keyboard and any keyboard joystick emulation.
    /// </summary>
    public KeyboardBlock? Keyboard { get; set; }

    /// <summary>
    /// Gets or sets joystick setup for both players.
    /// </summary>
    public JoystickBlock? Joystick { get; set; }

    /// <summary>
    /// Gets or sets the custom ROM that has been installed for the current Spectrum model.
    /// </summary>
    public CustomRomBlock? CustomRom { get; set; }

    /// <summary>
    /// Loads a SZX file from the given stream.
    /// </summary>
    /// <param name="stream">The stream containing the SZX data.</param>
    /// <returns>The loaded SzxFile object.</returns>
    public static ZxStateFile Load(Stream stream)
    {
        var reader = new ByteStreamReader(stream);
        var header = ZxStateHeader.Read(reader);

        if (header.Magic != BlockIds.Magic)
        {
            throw new InvalidDataException("Not a valid SZX file. Invalid magic number.");
        }

        var state = new ZxStateFile { Header = header };

        while (BlockHeader.Read(reader) is { } blockHeader)
        {
            switch (blockHeader.BlockId)
            {
                case BlockIds.Creator:
                    state.Creator = CreatorBlock.Read(reader, blockHeader.Size);
                    break;

                case BlockIds.Z80Regs:
                    state.Z80Registers = Z80RegsBlock.Read(reader, blockHeader.Size);
                    break;

                case BlockIds.SpecRegs:
                    state.SpecRegs = SpecRegsBlock.Read(reader, blockHeader.Size);
                    break;

                case BlockIds.ZxPrinter:
                    state.ZxPrinter = ZxPrinterBlock.Read(reader, blockHeader.Size);
                    break;

                case BlockIds.Keyboard:
                    state.Keyboard = KeyboardBlock.Read(reader, blockHeader.Size);
                    break;

                case BlockIds.Joystick:
                    state.Joystick = JoystickBlock.Read(reader, blockHeader.Size);
                    break;

                case BlockIds.CustomRom:
                    state.CustomRom = CustomRomBlock.Read(reader, blockHeader.Size);
                    break;

                default:
                    // Ignore this block, not needed for now.
                    reader.ReadBytes(blockHeader.Size);
                    break;
            }
        }

        return state;
    }

    /// <summary>
    /// Loads a SNA file from the given file.
    /// </summary>
    /// <param name="fileName">The file containing the SZX data.</param>
    /// <returns>The loaded SzxFile object.</returns>
    public static ZxStateFile Load(string fileName)
    {
        using var stream = File.OpenRead(fileName);

        return Load(stream);
    }
}