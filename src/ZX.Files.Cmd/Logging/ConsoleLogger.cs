namespace OldBit.ZX.Files.Cmd.Logging;

public class ConsoleLogger : IConsoleLogger
{
    public void WriteLine(string? value)
    {
        Console.WriteLine(value);
    }
}