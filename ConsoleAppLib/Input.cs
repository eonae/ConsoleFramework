using System;

namespace ConsoleAppLib
{
    public class ValueInput
    {
        private readonly string _text;
        private readonly IParser _parser;

        public (bool Abort, object Result) Read()
        {
            while (true)
            {
                Console.Write(_text);
                string input = Console.ReadLine();
                var parsed = (ParserOutput)_parser.TryParse(input);
                switch (parsed.Response)
                {
                    case ParserResponse.Ok:
                        return (false, parsed.Value);
                    case ParserResponse.Fail:
                        Console.WriteLine("Parsing failed!");
                        continue;
                    case ParserResponse.Abort:
                        return (true, null);
                }
            }
        }

        public ValueInput(string text, IParser parser)
        {
            _text = text;
            _parser = parser;
        }
    }
}
