namespace Crossword;

internal class Crosswords
{
    private const int Days = 30;
    private const string Path = "crosswords.pdf";
    private readonly HttpClient _client;
    private readonly IConsole _console;
    public Crosswords(HttpClient client, IConsole console)
    {
        _console = console;
        _client = client;
    }

    public async Task GetCrosswords()
    {
        var files = new List<string>();
        var guardian = new GuardianCrossword(_client, _console);

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
        var merger = new PdfMerger(_console);
        merger.Merge(Path, files.ToArray());

        foreach (var file in files)
        {
            File.Delete(file);
        }
    }
}