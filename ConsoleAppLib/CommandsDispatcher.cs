using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLib
{
    public class CommandsDispatcher
    {
        // Хранение изменение списка доступных команд

        private List<Command> internalCommands = new List<Command>();
        private List<Command> userCommands = new List<Command>();

        public CommandsDispatcher()
        {
            internalCommands.Add(
                new Command(isInternal: true
                           , name: "Quit"
                           , needParameters: false
                           , commandDelegate: (parameters) =>
                           {
                               ConsoleFrame.ChangeAction(InternalAction.Quit);
                           }));
            internalCommands.Add(
                new Command(isInternal: true
                           , name: "RepeatInput"
                           , needParameters: false
                           , commandDelegate: (parameters) =>
                           {
                               ConsoleFrame.ChangeAction(InternalAction.RepeatInput);
                           }));
        }

        public (Command Command, ICommandParameters Parameters) TryParse(string input)
        {
            // Распознаёт команду
            var decomposed = Decompose(input);
            var command = GetCommandByName(decomposed.Name);
            if (command == null)
                return (GetCommandByName("RepeatInput"), BlankSingleton.GetInstance());
            else
                return (command, decomposed.parameters);
        }

        private (string Name, ICommandParameters parameters) Decompose(string input)
        {
            var arr = input.Split(' ');
            var name = arr[0];
            if (arr.Length == 1)
                return (name, BlankSingleton.GetInstance());
            else
            {
                var parameters = new string[arr.Length - 1];
                for (int i = 0; i < parameters.Length; i++)
                    parameters[i] = arr[i - 1];
                return (name, new CommandParameters(parameters));
            }
        }
        private Command GetCommandByName(string name)
        {
            foreach (var command in internalCommands.Union(userCommands).ToList())
                if (name.ToLower() == command.Name.ToLower())
                    return command;
            return null;   
        }
    }
}
