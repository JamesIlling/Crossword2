using FluentAssertions;

namespace Crossword.Test
{
    public class ConsoleWrapperTests
    {
        [Fact]
        public void Test()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var consoleWrapper = new ConsoleWrapper();

            consoleWrapper.WriteLine("test");

            var output = stringWriter.ToString();
            output.Should().Be($"test\r\n");
        }
    }
}
