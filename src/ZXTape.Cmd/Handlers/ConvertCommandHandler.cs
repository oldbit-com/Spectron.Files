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
            tzxFile.Save(outputFileName);
        }
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

        return Path.ChangeExtension(outputFileName, extension); ;
    }
}