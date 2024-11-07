using FluentAssertions;
using Moq;

namespace Crossword.Test
{
    public class CrosswordsTests
    {
        [Fact]
        public async Task Get_Crosswords_AttemptsToGet30Crosswords()
        {
            var mockCrossword = new Mock<ICrossword>();
            mockCrossword.Setup(x => x.DownloadAsync(It.IsAny<DateTime>())).ReturnsAsync(true);

            var pdfMerger = new Mock<IPdfMerger>();
            var dut = new Crosswords(mockCrossword.Object, pdfMerger.Object);
            await dut.GetCrosswords();

            mockCrossword.Verify(x => x.DownloadAsync(It.Is<DateTime>(date => date < DateTime.Now)), Times.Exactly(30));
        }

        [Fact]
        public async Task Get_Crosswords_AttemptsToMergePdfs()
        {
            var mockCrossword = new Mock<ICrossword>();
            mockCrossword.Setup(x => x.DownloadAsync(It.IsAny<DateTime>())).ReturnsAsync(true);

            var pdfMerger = new Mock<IPdfMerger>();
            var dut = new Crosswords(mockCrossword.Object, pdfMerger.Object);
            await dut.GetCrosswords();
            pdfMerger.Verify(x => x.Merge(It.IsAny<string>(), It.IsAny<string[]>()), Times.Once);
        }

        [Fact]
        public async Task Get_Crosswords_DeletesIntermediateFiles()
        {
            var mockCrossword = new Mock<ICrossword>();
            mockCrossword.Setup(x => x.DownloadAsync(It.IsAny<DateTime>())).ReturnsAsync(true);

            var pdfMerger = new Mock<IPdfMerger>();
            var dut = new Crosswords(mockCrossword.Object, pdfMerger.Object);

            var file = $"{DateTime.Now.AddDays(-1):yyyyMMdd}.pdf";
            await File.WriteAllTextAsync(file, " ");
            File.Exists(file).Should().BeTrue();
            await dut.GetCrosswords();
            File.Exists(file).Should().BeFalse();
        }

        [Fact]
        public async Task Get_Crosswords_DeletesIntermediateFiles2()
        {
            var mockCrossword = new Mock<ICrossword>();
            mockCrossword.Setup(x => x.DownloadAsync(It.IsAny<DateTime>())).ReturnsAsync(true);

            var pdfMerger = new Mock<IPdfMerger>();
            var dut = new Crosswords(mockCrossword.Object, pdfMerger.Object);

            var file = $"{DateTime.Now.AddDays(-2):yyyyMMdd}.pdf";
            await File.WriteAllTextAsync(file, " ");
            File.Exists(file).Should().BeTrue();
            await dut.GetCrosswords();
            File.Exists(file).Should().BeFalse();
        }
    }
}
