using OldBit.ZX.Files.Tap;
using OldBit.ZX.Files.Tap.Extensions;
using OldBit.ZX.Files.Tzx;
using OldBit.ZX.Files.Tzx.Blocks;
using OldBit.ZX.Files.Tzx.Extensions;

namespace OldBit.ZX.Files.Cmd.Handlers;

public static class ListCommandHandler
{
    public static void List(string fileName)
    {
        var tzxFile = TzxFile.Load(fileName);

        for (var i = 0; i < tzxFile.Blocks.Count; i++)
        {
            var block = tzxFile.Blocks[i];
            Console.WriteLine(
                "{0,3} {1,-26} {2,12}   {3,-20}",
                i,
                block.GetBlockName(),
                GetBlockSize(block),
                GetBlockDescription(block));
        }

        Console.WriteLine($"    TZX Version: {tzxFile.Header.VerMajor}.{tzxFile.Header.VerMinor}");
    }

    private static string GetBlockDescription(IBlock block)
    {
        switch (block)
        {
            case TextDescriptionBlock textDescriptionBlock:
                return textDescriptionBlock.Description;

            case StandardSpeedDataBlock standardSpeedDataBlock:
            {
                if (TapData.TryParse(standardSpeedDataBlock.Data, out var tapeData))
                {
                    if (TapHeader.TryParse(tapeData.BlockData, out var tapeHeader))
                    {
                        return $"{tapeHeader.GetDataTypeName()}: {tapeHeader.FileName}";
                    }
                }

                break;
            }
        }

        return string.Empty;
    }

    private static string GetBlockSize(IBlock block) => block switch
    {
        StandardSpeedDataBlock standardSpeedDataBlock => $"{standardSpeedDataBlock.Data.Count-2} bytes",
        _ => string.Empty
    };
}