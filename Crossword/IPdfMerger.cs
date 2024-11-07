namespace Crossword;

public interface IPdfMerger
{
    void Merge(string crosswordsPdf, string[] toArray);
}