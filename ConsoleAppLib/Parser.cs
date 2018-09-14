namespace ConsoleAppLib
{

    public class CommandParser : IParser
    {
        private CommandDispatcher dispatcher;

        public IParserOutput TryParse(string input)
        {
            // Распознаёт команду из диспетчера команд

            var decomposed = Decompose(input);
            var command = dispatcher.GetCommandByName(decomposed.Name);

            CommandParserResponse response;
            switch (command)
            {
                case null:
                    response = CommandParserResponse.InvalidCommand; break;
                case Command c when !c.IsValid(decomposed.parameters):
                    response = CommandParserResponse.InvalidParameters; break;
                default:
                    response = CommandParserResponse.Ok; break;
            }
            return new CommandParserOutput(response, command, decomposed.parameters);
        }
        private (string Name, string[] parameters) Decompose(string input)
        {
            var arr = input.Split(' ');
            var name = arr[0];
            if (arr.Length == 1)
                return (name, null);
            else
            {
                var parameters = new string[arr.Length - 1];
                for (int i = 0; i < parameters.Length; i++)
                    parameters[i] = arr[i + 1];
                return (name, parameters);
            }
        }

        public CommandParser(CommandDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }
    }

}
