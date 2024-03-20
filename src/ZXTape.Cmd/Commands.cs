using System.CommandLine;
using OldBit.ZXTape.Cmd.Handlers;

namespace OldBit.ZXTape.Cmd;

public class Commands
{
    public int ReturnCode { get; private set; }

    private Option<bool> _verboseOption = new(
        "--verbose",
        "Show verbose output");

    public Command CreateCommands()
    {
        var rootCommand = CreateRootCommand();

        rootCommand.AddCommand(CreateListCommand());
        rootCommand.AddCommand(CreateConvertCommand());

        return rootCommand;
    }

    private Command CreateRootCommand() => new RootCommand
    {
        _verboseOption
    };

    private Command CreateListCommand()
    {
        var fileNameArgument = new Argument<string>(
            name: "file",
            description: "The file to display information about");

        var listCommand = new Command("list", "Lists the contents of a file")
        {
            _verboseOption,
            fileNameArgument
        };

        listCommand.SetHandler(fileName =>
        {
            try
            {
                var handler = new ListCommandHandler();
                handler.List(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ReturnCode = 1;
            }
        }, fileNameArgument);

        return listCommand;
    }

    private Command CreateConvertCommand()
    {
        var fileNameArgument = new Argument<string>(
            name: "file",
            description: "The file to convert");

        var convertCommand = new Command("convert", "Converts a file to another format")
        {
            fileNameArgument
        };

        return convertCommand;
    }
}