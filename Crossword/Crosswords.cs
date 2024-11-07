namespace Crossword;

public class Crosswords
{
    private const int Days = 30;
    private const string Path = "crosswords.pdf";
    private readonly ICrossword _crossword;
    private readonly IPdfMerger _pdfMerger;

    public Crosswords(ICrossword crossword, IPdfMerger pdfMerger)
    {
        _pdfMerger = pdfMerger;
        _crossword = crossword;
    }

    public async Task GetCrosswords()
    {
        var files = new List<string>();

        // Get the last 30 days worth of crosswords
        for (var last = 0; last < Days; last++)
        {
            var date = DateTime.Now.AddDays(last - Days);
            if (await _crossword.DownloadAsync(date))
            {
                var file = $"{date:yyyyMMdd}.pdf";
                if (File.Exists(file))
                {
                    files.Add(file);
                }
            }
        }

        // Merger the Pdfs

        _pdfMerger.Merge(Path, files.ToArray());

        foreach (var file in files.Where(File.Exists))
        {
            File.Delete(file);
        }
    }
}