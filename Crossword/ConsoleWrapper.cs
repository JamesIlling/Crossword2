namespace Crossword;

public class ConsoleWrapper : IConsole
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}