using OldBit.ZXTape.Cmd.TapeFile;
using OldBit.ZXTape.Tzx;

namespace OldBit.ZXTape.Cmd.Handlers;

public static class ConvertCommandHandler
{
    public static void Convert(string sourceFileName, string outputFileName, FileFormat outputFormat, bool force)
    {
        var sourceFileFormat = TapeFileHelper.DetectFormat(sourceFileName);
        if (sourceFileFormat != FileFormat.Tzx && sourceFileFormat != FileFormat.Tap)
        {
            throw new Exception("Invalid source file. Only TAP and TZX files are supported.");
        }

        if (sourceFileFormat == FileFormat.Tzx)
        {
            var tzxFile = TzxFile.Load(sourceFileName);
            outputFileName = GetOutputFileName(sourceFileName, outputFileName, outputFormat);

            if (outputFormat == FileFormat.Tzx)
            {
                TzxToTzx(tzxFile, outputFileName);
            }
            else if (outputFormat == FileFormat.Tap)
            {
                TzxToTap(tzxFile, outputFileName);
            }
            else
            {
                throw new Exception("Invalid output format. Only TAP and TZX files are supported.");
            }
        }
        else if (sourceFileFormat == FileFormat.Tap)
        {
            // var tapFile = TapFile.Load(sourceFileName);
            // outputFileName = GetOutputFileName(sourceFileName, outputFileName, outputFormat);
            // tapFile.Save(outputFileName);
        }
    }

    private static void TzxToTzx(TzxFile tzxFile, string outputFileName)
    {
        tzxFile.Save(outputFileName);
    }

    private static void TzxToTap(TzxFile tzxFile, string outputFileName)
    {
        // var tapFile = new TapFile();
        // tapFile.AddBlocks(tzxFile.Blocks);
        // tapFile.Save(outputFileName);
    }

    private static string GetOutputFileName(string sourceFileName, string outputFileName, FileFormat outputFormat)
    {
        var extension = outputFormat switch
        {
            FileFormat.Tap => ".tap",
            FileFormat.Tzx => ".tzx",
            _ => string.Empty
        };

        if (string.IsNullOrEmpty(outputFileName))
        {
            return Path.ChangeExtension(sourceFileName, extension);
        }

        if (Directory.Exists(outputFileName))
        {
            outputFileName = Path.Combine(outputFileName, Path.GetFileName(sourceFileName));
        }

        return Path.ChangeExtension(outputFileName, extension);
    }
}