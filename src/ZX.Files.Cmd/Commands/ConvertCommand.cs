using System.CommandLine;
using OldBit.ZX.Files.Cmd.Handlers;
using OldBit.ZX.Files.Cmd.Logging;
using OldBit.ZX.Files.Cmd.TapeFile;

namespace OldBit.ZX.Files.Cmd.Commands;

public class ConvertCommand(IConsoleLogger consoleLogger)
{
    public Command Create(Option<bool> verboseOption, Action<int> setReturnCode)
    {
        var sourceFileArgument = new Argument<string>(
            name: "file",
            description: "The file to convert");

        var outputFileOption = new Option<string>(
            name: "--output-file",
            description: "The output file name (default is the same as the input file with the new extension)");
        outputFileOption.AddAlias("-o");

        var outputFormatOption = new Option<string>(
            name: "--output-format",
            description: "The output file format (tap or tax)").FromAmong("tap", "tzx");
        outputFormatOption.AddAlias("-f");

        var forceOption = new Option<bool>(
            name: "--force",
            description: "Force the conversion event if it can cause data loss or may not be supported.");

        var convertCommand = new Command("convert", "Converts a file to another format (TAP or TZX)")
        {
            verboseOption,
            sourceFileArgument,
            outputFormatOption,
            outputFileOption,
            forceOption,
        };

        convertCommand.SetHandler((sourceFileName, outputFileName, outputFormat, force) =>
        {
            try
            {
                var convertCommandHandler = new ConvertCommandHandler(consoleLogger);
                convertCommandHandler.Convert(
                    sourceFileName,
                    outputFileName,
                    Enum.Parse<FileFormat>(outputFormat, true),
                    force);
            }
            catch (Exception ex)
            {
                consoleLogger.WriteLine(ex.Message);
                setReturnCode(1);
            }
        }, sourceFileArgument, outputFileOption, outputFormatOption, forceOption);

        return convertCommand;
    }
}