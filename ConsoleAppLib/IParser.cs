namespace ConsoleAppLib
{
    public interface IParser
    {
        IParserOutput TryParse(string input);
    }

    public class IntParser : IParser
    {
        private string _escapeCommand;

        public IParserOutput TryParse(string input)
        {
            if (input.Equals(_escapeCommand))
                return new ParserOutput(ParserResponse.Abort, null);
            else
            {
                bool success = int.TryParse(input, out int value);
                if (success)
                    return new ParserOutput(ParserResponse.Ok, value);
                else
                    return new ParserOutput(ParserResponse.Fail, null);
            }
        }

        public IntParser(string escapeCommand = "esc")
        {
            _escapeCommand = escapeCommand;
        }
    }
}
