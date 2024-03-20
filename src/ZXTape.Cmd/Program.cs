using System.CommandLine;
using OldBit.ZXTape.Cmd;

var commands = new Commands();
var rootCommand = commands.CreateCommands();

await rootCommand.InvokeAsync(args);

return commands.ReturnCode;