using System.Collections.Generic;
using System.Linq;
using System;

namespace ConsoleAppLib
{
    public class CommandsDispatcher
    {
        // Хранение изменение списка доступных команд

        private List<Command> commands = new List<Command>();
        private ConsoleFrame frame;
        private InputParser parser;

        public (Command Command, CommandParameters Parameters) TryParse(string input)
        {
            return parser.TryParse(input);
        }
        public Command GetCommandByName(string name)
        {
            foreach (var command in commands)
                if (name.ToLower() == command.Name.ToLower())
                    return command;
            return null;
        }
        public void Add(Command command)
        {
            commands.Add(command);
        }


        public CommandsDispatcher(ConsoleFrame frame)
        {
            this.frame = frame;
            parser = new InputParser(this);

            commands.Add(
                new Command(isInternal: false
                           , name: "Quit"
                           , commandDelegate: (parameters) =>
                           {
                               frame.ChangeAction(InternalAction.Quit);
                           })); // Без валидации
            commands.Add(
                new Command(isInternal: true
                           , name: "RepeatInput"
                           , commandDelegate: (parameters) =>
                           {
                               frame.ChangeAction(InternalAction.RepeatInput);
                               frame.ChangeMessage((StandardMessage)parameters.Array[0].StringToEnum(StandardMessage.NoMessage));
                           }
                           , validationDelegate: (parameters) =>
                           {
                               if (!parameters.IsEmpty && parameters.Array.Length == 1)
                                   if (StandardMessage.InternalCommand.EnumToStringArr().Contains(parameters.Array[0]))
                                       return true;
                               return false;
                           }));
        }
    }
}
