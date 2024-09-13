using System.CommandLine;
using OldBit.ZX.Files.Cmd.Logging;

namespace OldBit.ZX.Files.Cmd.Commands;

public sealed class CommandBuilder
{
    private readonly IConsoleLogger _consoleLogger = new ConsoleLogger();

    private readonly Option<bool> _verboseOption = new(
        "--verbose",
        "Show verbose output");

    public RootCommand CreateRootCommand()
    {
        var rootCommand = new RootCommand
        {
            _verboseOption
        };

        rootCommand.AddCommand(ListCommand.Create(_verboseOption, returnCode => ReturnCode = returnCode));

        var convertCommand = new ConvertCommand(_consoleLogger);
        rootCommand.AddCommand(convertCommand.Create(_verboseOption, returnCode => ReturnCode = returnCode));

        return rootCommand;
    }

    public int ReturnCode { get; private set; }
}