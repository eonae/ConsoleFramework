namespace ConsoleAppLib
{
    public class InputParser
    {
        private CommandDispatcher dispatcher;

        public ParserOutput TryParse(string input)
        {
            // Распознаёт команду из диспетчера команд

            var decomposed = Decompose(input);
            /*
             * Предустановлено:
             *  quit - ввести можно, но это не команда
             *  help - вывести список команд
             *  info - вывести данные фрейма
             *  
             * Устанавливаемые в наследниках
             * access @database_name - можно ввести, команда (запускает новый фрейм)
             * 
             */
            var command = dispatcher.GetCommandByName(decomposed.Name);
            if (command == null)
            {
 
            }
            ParserResponse response;
            switch (command)
            {
                case null:
                    response = ParserResponse.InvalidCommand; break;
                case Command c when !c.IsValid(decomposed.parameters):
                    response = ParserResponse.InvalidParameters; break;
                default:
                    response = ParserResponse.Ok; break;
            }
            return new ParserOutput(response, command, decomposed.parameters);
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
                    parameters[i] = arr[i - 1];
                return (name, parameters);
            }
        }

        public InputParser(CommandDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }
    }

}
