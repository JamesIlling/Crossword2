namespace Crossword;

public interface ICrossword
{
    Task<bool> DownloadAsync(DateTime date);
}