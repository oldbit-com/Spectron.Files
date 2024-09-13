using System.CommandLine;
using OldBit.ZX.Files.Cmd.Commands;

var commandBuilder = new CommandBuilder();

var rootCommand = commandBuilder.CreateRootCommand();
await rootCommand.InvokeAsync(args);

return commandBuilder.ReturnCode;