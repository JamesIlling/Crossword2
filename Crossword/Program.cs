using Microsoft.Extensions.DependencyInjection;

namespace Crossword;

public static class Program
{
    private static async Task Main()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var provider = services.BuildServiceProvider();

        var crossword = provider.GetRequiredService<Crosswords>();
        await crossword.GetCrosswords();
    }

    public static void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<HttpClient>();
        services.AddSingleton<IConsole, ConsoleWrapper>();
        services.AddSingleton<Crosswords>();
        services.AddSingleton<ICrossword, GuardianCrossword>();
        services.AddSingleton<IPdfMerger, PdfMerger>();
    }
}