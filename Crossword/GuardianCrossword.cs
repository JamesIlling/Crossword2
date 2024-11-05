namespace Crossword;

internal class GuardianCrossword
{
    private readonly HttpClient _httpClient;
    private readonly IConsole _console;
    public GuardianCrossword(HttpClient client, IConsole console)
    {
        _httpClient = client;
        _console = console;
    }

    /// <summary>
    ///     Attempt to download a cryptic crossword for a specific date.
    /// </summary>
    /// <param name="date">The date of the crossword to get</param>
    /// <returns>True on success or false if an issue occurs.</returns>
    public async Task<bool> DownloadAsync(DateTime date)
    {
        var url = new Uri($"https://crosswords-static.guim.co.uk/gdn.cryptic.{date:yyyyMMdd}.pdf");
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            _console.WriteLine($"Got crossword for {date:yyyy-MM-dd}");
            var filename = $"{date:yyyyMMdd}.pdf";
            await File.WriteAllBytesAsync(filename, await response.Content.ReadAsByteArrayAsync());
            return true;
        }

        return false;
    }
}
