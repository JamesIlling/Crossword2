namespace Crossword;

internal class Crosswords
{
    private const int Days = 30;
    private const string Path = "crosswords.pdf";

    public async Task GetCrosswords()
    {
        var files = new List<string>();
        var guardian = new GuardianCrossword();

        // Get the last 30 days worth of crosswords
        for (var last = 0; last < Days; last++)
        {
            var date = DateTime.Now.AddDays(last - Days);
            if (await guardian.DownloadAsync(date))
            {
                files.Add($"{date:yyyyMMdd}.pdf");
            }
        }

        // Merger the Pdfs
        PdfMerger.Merge(Path, files.ToArray());

        foreach (var file in files)
        {
            File.Delete(file);
        }
    }
}