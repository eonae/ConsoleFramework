namespace ConsoleAppLib
{
    public class InputParser
    {
        private CommandsDispatcher dispatcher;

        public (Command Command, CommandParameters Parameters) TryParse(string input)
        {
            // Распознаёт команду
            var decomposed = Decompose(input);
            var command = dispatcher.GetCommandByName(decomposed.Name);
            switch (command)
            {
                case null:
                    return (dispatcher.GetCommandByName("RepeatInput"), new CommandParameters("InvalidCommand")); //Invalid command
                case Command c when c.IsInternal:
                    return (dispatcher.GetCommandByName("RepeatInput"), new CommandParameters("InternalCommand")); //Internal command
                case Command c when !c.ValidateParameters(decomposed.parameters):
                    return (dispatcher.GetCommandByName("RepeatInput"), new CommandParameters("InvalidParameters")); //Invalid parameters
                default:
                    return (command, decomposed.parameters);
            }
        }
        private (string Name, CommandParameters parameters) Decompose(string input)
        {
            var arr = input.Split(' ');
            var name = arr[0];
            if (arr.Length == 1)
                return (name, new CommandParameters());
            else
            {
                var parameters = new string[arr.Length - 1];
                for (int i = 0; i < parameters.Length; i++)
                    parameters[i] = arr[i - 1];
                return (name, new CommandParameters(parameters));
            }
        }

        public InputParser(CommandsDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }
    }

}
