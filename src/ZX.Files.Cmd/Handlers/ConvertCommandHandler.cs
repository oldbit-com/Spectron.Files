using OldBit.ZX.Files.Cmd.Logging;
using OldBit.ZX.Files.Cmd.TapeFile;
using OldBit.ZX.Files.Tap;
using OldBit.ZX.Files.Tzx;

namespace OldBit.ZX.Files.Cmd.Handlers;

public class ConvertCommandHandler(IConsoleLogger consoleLogger)
{
    public void Convert(string sourceFileName, string outputFileName, FileFormat outputFormat, bool force)
    {
        var sourceFileFormat = TapeFileHelper.DetectFormat(sourceFileName);
        if (sourceFileFormat == outputFormat && force)
        {
            throw new Exception("Source and output formats are the same. Use force option to override.");
        }

        outputFileName = GetOutputFileName(sourceFileName, outputFileName, outputFormat);
        if (sourceFileFormat == FileFormat.Tzx)
        {
            var tzxFile = TzxFile.Load(sourceFileName);

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
            var tapFile = TapFile.Load(sourceFileName);
            if (outputFormat == FileFormat.Tzx)
            {
                TapToTzx(tapFile, outputFileName);
            }
            else if (outputFormat == FileFormat.Tap)
            {
                TapToTap(tapFile, outputFileName);
            }
            else
            {
                throw new Exception("Invalid output format. Only TAP and TZX files are supported.");
            }
        }
        else
        {
            throw new Exception("Invalid source file. Only TAP and TZX files are supported.");
        }
    }

    private void TzxToTzx(TzxFile tzxFile, string outputFileName)
    {
        consoleLogger.WriteLine("Source format: TZX");
        consoleLogger.WriteLine("Target format: TZX");

        tzxFile.Save(outputFileName);

        consoleLogger.WriteLine($"File saved: '{outputFileName}'");
    }

    private static void TzxToTap(TzxFile tzxFile, string outputFileName)
    {
        // var tapFile = new TapFile();
        // tapFile.AddBlocks(tzxFile.Blocks);
        // tapFile.Save(outputFileName);
    }

    private void TapToTap(TapFile tapFile, string outputFileName)
    {
        consoleLogger.WriteLine("Source format: TAP");
        consoleLogger.WriteLine("Target format: TAP");

        tapFile.Save(outputFileName);

        consoleLogger.WriteLine($"File saved: '{outputFileName}'");
    }

    private static void TapToTzx(TapFile tapFile, string outputFileName)
    {
        //tapFile.Save(outputFileName);
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