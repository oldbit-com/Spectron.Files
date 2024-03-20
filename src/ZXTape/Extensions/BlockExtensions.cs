using OldBit.ZXTape.Tzx;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.Extensions;

public static class BlockExtensions
{
    /// <summary>
    /// Gets the ID of the specified block.
    /// </summary>
    /// <param name="block">The block object.</param>
    /// <returns>The ID of the block or -1 if there is no associated ID or block is unknown.</returns>
    public static int GetBlockCode(this IBlock block)
    {
        return block.GetType().Name switch
        {
            nameof(StandardSpeedDataBlock) => BlockCode.StandardSpeedData,
            nameof(TurboSpeedDataBlock) => BlockCode.TurboSpeedData,
            nameof(PureToneBlock) => BlockCode.PureTone,
            nameof(PulseSequenceBlock) => BlockCode.PulseSequence,
            nameof(PureDataBlock) => BlockCode.PureData,
            nameof(DirectRecordingBlock) => BlockCode.DirectRecording,
            nameof(CswRecordingBlock) => BlockCode.CswRecording,
            nameof(GeneralizedDataBlock) => BlockCode.GeneralizedData,
            nameof(PauseBlock) => BlockCode.Pause,
            nameof(GroupStartBlock) => BlockCode.GroupStart,
            nameof(GroupEndBlock) => BlockCode.GroupEnd,
            nameof(JumpToBlock) => BlockCode.JumpToBlock,
            nameof(LoopStartBlock) => BlockCode.LoopStart,
            nameof(LoopEndBlock) => BlockCode.LoopEnd,
            nameof(CallSequenceBlock) => BlockCode.CallSequence,
            nameof(ReturnFromSequenceBlock) => BlockCode.ReturnFromSequence,
            nameof(SelectBlock) => BlockCode.Select,
            nameof(StopTheTape48Block) => BlockCode.StopTheTape48,
            nameof(SetSignalLevelBlock) => BlockCode.SetSignalLevel,
            nameof(TextDescriptionBlock) => BlockCode.TextDescription,
            nameof(MessageBlock) => BlockCode.Message,
            nameof(ArchiveInfoBlock) => BlockCode.ArchiveInfo,
            nameof(HardwareTypeBlock) => BlockCode.HardwareType,
            nameof(CustomInfoBlock) => BlockCode.CustomInfo,
            nameof(GlueBlock) => BlockCode.Glue,
            _ => BlockCode.Unknown
        };
    }

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