using System.CommandLine;
using OldBit.ZXTape.Cmd.Commands;

var commandBuilder = new CommandBuilder();

var rootCommand = commandBuilder.CreateRootCommand();
await rootCommand.InvokeAsync(args);

return commandBuilder.ReturnCode;