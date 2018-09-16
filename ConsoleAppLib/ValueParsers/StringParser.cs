using System;

namespace ConsoleAppLib
{
    public class StringParser : IParser
    {
        

        private string _escapeCommand;

        public IParserOutput TryParse(string input)
        {
            if (input.Equals(_escapeCommand))
                return new ParserOutput(ParserResponse.Abort, null);
            else
            {
                return new ParserOutput(ParserResponse.Ok, input);
            }
        }

        public StringParser(string escapeCommand = @"\q")
        {
            _escapeCommand = escapeCommand;
        }
    }
}
