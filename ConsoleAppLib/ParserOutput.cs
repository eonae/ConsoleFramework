using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eonae.Terminal
{
    public enum CommandParserResponse { InvalidCommand, InvalidParameters, Ok }
    public enum ParserResponse { Ok, Fail, Abort }

    public interface IParserOutput { }

    public class ParserOutput: IParserOutput
    {
        public readonly ParserResponse Response;
        public readonly object Value;

        public ParserOutput(ParserResponse response, object value)
        {
            Response = response;
            Value = value;
        }
    }

    public class CommandParserOutput : IParserOutput
    {
        public readonly CommandParserResponse Response;
        public readonly Command Command;
        public readonly string[] Parameters;

        public CommandParserOutput(CommandParserResponse response, Command command, string[] parameters)
        {
            Response = response;
            Command = command;
            Parameters = parameters;
        }
    }
}
