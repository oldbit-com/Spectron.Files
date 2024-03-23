namespace OldBit.ZXTape.Cmd.Logging;

public class ConsoleLogger : IConsoleLogger
{
    public void WriteLine(string? value)
    {
        Console.WriteLine(value);
    }
}