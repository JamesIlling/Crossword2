namespace Crossword.Test
{
    internal class TestConsole : IConsole
    {
        private readonly List<string> _messages = [];

        public IReadOnlyCollection<string> Messages => _messages.AsReadOnly();

        public void WriteLine(string message)
        {
            _messages.Add(message);
        }
    }
}
