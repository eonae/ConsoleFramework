using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLib
{
    public enum ParserResponse
    {
        InvalidCommand,
        InvalidParameters,
        Ok
    }

    public class ParserOutput
    {
        public readonly ParserResponse response;
        public readonly Command command;
        public readonly string[] parameters;

        public ParserOutput(ParserResponse response, Command command, string[] parameters)
        {
            this.response = response;
            this.command = command;
            this.parameters = parameters;
        }
    }
}
