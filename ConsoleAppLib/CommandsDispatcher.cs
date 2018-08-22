using System.Collections.Generic;

namespace ConsoleAppLib
{
    public sealed class CommandDispatcher
    {
        // Хранение и дополнение списка доступных комманд. Предоставление ссылок на них по имени.

        private Dictionary<string,Command> commands = new Dictionary<string, Command>();

        public Command GetCommandByName(string name)
        {
            return commands[name]; // Если ключ не найден, вернёт null?
        }

        public void Add(Command command)
        {
            commands.Add(command.Name, command);
        }
    }
}
