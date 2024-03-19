// See https://aka.ms/new-console-template for more information

using System.CommandLine;

var rootCommand = new RootCommand
{
    new Option<string>(
        ["--file", "-f"],
        "The file to load"),

    new Option<string>(
        ["--output", "-o"],
        "The output file"),

    new Argument<string>(
        name: "file",
        description: "The file to display information about")
};

var listCommand = new Command("list")
{
    new Option<bool>(
        ["--verbose", "-v"],
        "Show verbose output"),

    new Argument<string>(
        name: "file",
        description: "The file to display information about")
};
listCommand.SetHandler(fileName =>
{
    Console.WriteLine($"Listing file {fileName}");
});

rootCommand.AddCommand(listCommand);


await rootCommand.InvokeAsync(args);
