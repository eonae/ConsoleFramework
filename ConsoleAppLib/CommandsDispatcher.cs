using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppLib
{
    public class CommandsDispatcher
    {
        // Хранение изменение списка доступных команд

        private List<Command> internalCommands = new List<Command>();
        private List<Command> userCommands = new List<Command>();
        private ConsoleFrame frame;
        private InputParser parser;

        public (Command Command, CommandParameters Parameters) TryParse(string input)
        {
            return parser.TryParse(input);
        }
        public Command GetCommandByName(string name)
        {
            foreach (var command in internalCommands.Union(userCommands).ToList())
                if (name.ToLower() == command.Name.ToLower())
                    return command;
            return null;
        }

        public CommandsDispatcher(ConsoleFrame frame)
        {
            this.frame = frame;
            parser = new InputParser(this);

            internalCommands.Add(
                new Command(isInternal: true
                           , name: "Quit"
                           , needParameters: false
                           , commandDelegate: (parameters) =>
                           {
                               frame.ChangeAction(InternalAction.Quit);
                           }));
            internalCommands.Add(
                new Command(isInternal: true
                           , name: "RepeatInput"
                           , needParameters: false
                           , commandDelegate: (parameters) =>
                           {
                               frame.ChangeAction(InternalAction.RepeatInput);
                           }));
        }
    }
}
