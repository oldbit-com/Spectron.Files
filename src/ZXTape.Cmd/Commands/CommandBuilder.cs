using System.CommandLine;

namespace OldBit.ZXTape.Cmd.Commands;

public sealed class CommandBuilder
{
    public int ReturnCode { get; private set; }

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
        rootCommand.AddCommand(ConvertCommand.Create(_verboseOption, returnCode => ReturnCode = returnCode));

        return rootCommand;
    }
}