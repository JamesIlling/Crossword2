using FluentAssertions;

namespace Crossword.Test
{
    public class PdfMergerTests
    {
        [Fact]
        public void Merge_EnsureLogsToConsole()

        {
            const string file = "test.pdf";
            var console = new TestConsole();
            var merger = new PdfMerger(console);

            merger.Merge(file, []);
            console.Messages.Should().Contain("Writing to single pdf");
        }

        [Fact]
        public void Merge_NoFilesDoesNotCreateFile()

        {
            const string file = "test.pdf";
            var console = new TestConsole();
            var merger = new PdfMerger(console);

            merger.Merge(file, []);
            File.Exists(file).Should().BeFalse();
        }

        [Fact]
        public void Merge_CreateFile()

        {
            const string file = "test.pdf";
            var console = new TestConsole();
            var merger = new PdfMerger(console);

            merger.Merge(file, ["Resources/gdn.cryptic.20241021.pdf"]);
            File.Exists(file).Should().BeTrue();
            File.Delete(file);
        }

        [Fact]
        public void Merge_SavesFile()

        {
            const string file = "test.pdf";
            var console = new TestConsole();
            var merger = new PdfMerger(console);

            merger.Merge(file, ["Resources/gdn.cryptic.20241021.pdf"]);
            File.Exists(file).Should().BeTrue();
            File.ReadAllBytes(file).Length.Should().BeGreaterThan(0);
            File.Delete(file);
        }
    }
}
