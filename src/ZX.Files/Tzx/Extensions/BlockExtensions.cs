using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.Tzx.Extensions;

public static class BlockExtensions
{
    /// <summary>
    /// Gets the display name of the specified block.
    /// </summary>
    /// <param name="block">The block object.</param>
    /// <returns>The display name of the block.</returns>
    public static string GetBlockName(this IBlock block)
    {
        return block.GetType().Name switch
        {
            nameof(HeaderBlock) => "TZX Header",
            nameof(StandardSpeedDataBlock) => "Standard Speed Data",
            nameof(TurboSpeedDataBlock) => "Turbo Speed Data",
            nameof(PureToneBlock) => "Pure Tone",
            nameof(PulseSequenceBlock) => "Pulse Sequence",
            nameof(PureDataBlock) => "Pure Data",
            nameof(DirectRecordingBlock) => "Direct Recording",
            nameof(CswRecordingBlock) => "CSW Recording",
            nameof(GeneralizedDataBlock) => "Generalized Data",
            nameof(PauseBlock) => "Pause",
            nameof(GroupStartBlock) => "Group Start",
            nameof(GroupEndBlock) => "Group End",
            nameof(JumpToBlock) => "Jump To Block",
            nameof(LoopStartBlock) => "Loop Start",
            nameof(LoopEndBlock) => "Loop End",
            nameof(CallSequenceBlock) => "Call Sequence",
            nameof(ReturnFromSequenceBlock) => "Return From Sequence",
            nameof(SelectBlock) => "Select Block",
            nameof(StopTheTape48Block) => "Stop the Tape if in 48k Mode",
            nameof(SetSignalLevelBlock) => "Set Signal Level",
            nameof(TextDescriptionBlock) => "Text Description",
            nameof(MessageBlock) => "Message",
            nameof(ArchiveInfoBlock) => "Archive Info",
            nameof(HardwareTypeBlock) => "Hardware Type",
            nameof(CustomInfoBlock) => "Custom Info",
            nameof(GlueBlock) => "Glue",
            _ => "Unknown Block Type"
        };
    }
}