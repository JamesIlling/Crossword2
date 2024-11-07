using System.Net;
using FluentAssertions;
using RichardSzalay.MockHttp;

namespace Crossword.Test;

public class GuardianCrosswordTests
{
    private static HttpClient SetupSuccessfulClient()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://crosswords-static.guim.co.uk/*")
            .Respond("application/pdf", File.OpenRead("Resources/gdn.cryptic.20241021.pdf"));
        var http = new HttpClient(mockHttp);
        return http;
    }

    private static HttpClient SetupFailClient()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://crosswords-static.guim.co.uk/*").Respond(HttpStatusCode.BadRequest);
        var http = new HttpClient(mockHttp);
        return http;
    }

    [Fact]
    public async Task DownloadAsync_WritesFile_IfDownloadSucceeds()
    {
        var testConsole = new TestConsole();
        var http = SetupSuccessfulClient();

        var crosswords = new GuardianCrossword(http, testConsole);

        await crosswords.DownloadAsync(new DateTime(2024, 10, 21));

        File.Exists("20241021.pdf").Should().BeTrue();
        var downloaded = File.ReadAllBytes("20241021.pdf");
        var source = File.ReadAllBytes("Resources/gdn.cryptic.20241021.pdf");
        downloaded.SequenceEqual(source).Should().BeTrue();
        File.Delete("20241021.pdf");
    }


    [Fact]
    public async Task DownloadAsync_WritesToConsole_IfDownloadSucceeds()
    {
        var testConsole = new TestConsole();
        var http = SetupSuccessfulClient();

        var crosswords = new GuardianCrossword(http, testConsole);
        await crosswords.DownloadAsync(new DateTime(2024, 10, 21));
        testConsole.Messages.Should().Contain("Got crossword for 2024-10-21");

        File.Delete("20241021.pdf");
    }

    [Fact]
    public async Task DownloadAsync_ReturnsTrue_IfDownloadSucceeds()
    {
        var testConsole = new TestConsole();
        var http = SetupSuccessfulClient();

        var crosswords = new GuardianCrossword(http, testConsole);
        (await crosswords.DownloadAsync(new DateTime(2024, 10, 21))).Should().BeTrue();
    }

    [Fact]
    public async Task DownloadAsync_WritesFile_IfDownloadFails()
    {
        var testConsole = new TestConsole();
        var http = SetupFailClient();

        var crosswords = new GuardianCrossword(http, testConsole);

        await crosswords.DownloadAsync(new DateTime(2024, 10, 21));

        File.Exists("20241021.pdf").Should().BeFalse();
    }

    [Fact]
    public async Task DownloadAsync_WritesToConsole_IfDownloadFails()
    {
        var testConsole = new TestConsole();
        var http = SetupFailClient();

        var crosswords = new GuardianCrossword(http, testConsole);
        await crosswords.DownloadAsync(new DateTime(2024, 10, 21));
        testConsole.Messages.Should().BeEmpty();
    }


    [Fact]
    public async Task DownloadAsync_ReturnsFalse_IfDownloadFails()
    {
        var testConsole = new TestConsole();
        var http = SetupFailClient();

        var crosswords = new GuardianCrossword(http, testConsole);
        (await crosswords.DownloadAsync(new DateTime(2024, 10, 21))).Should().BeFalse();
    }


}