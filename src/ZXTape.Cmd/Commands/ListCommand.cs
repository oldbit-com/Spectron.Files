using System.CommandLine;
using OldBit.ZXTape.Cmd.Handlers;

namespace OldBit.ZXTape.Cmd.Commands;

public static class ListCommand
{
    public static Command Create(Option<bool> verboseOption, Action<int> setReturnCode)
    {
        var inputFileArgument = new Argument<string>(
            name: "file",
            description: "The file to display information about");

        var listCommand = new Command("list", "Lists the contents of a file")
        {
            verboseOption,
            inputFileArgument
        };

        listCommand.SetHandler(fileName =>
        {
            try
            {
                ListCommandHandler.List(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                setReturnCode(1);
            }
        }, inputFileArgument);

        return listCommand;
    }
}