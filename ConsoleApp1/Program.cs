namespace Crossword;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var crossword = new Crosswords();
        await crossword.GetCrosswords();
    }
}