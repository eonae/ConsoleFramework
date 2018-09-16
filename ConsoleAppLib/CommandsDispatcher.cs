using System.Collections.Generic;
using System.Linq;
using System;

namespace Eonae.Terminal
{
    public sealed class CommandDispatcher
    {
        // Хранение и дополнение списка доступных комманд. Предоставление ссылок на них по имени.

        private Dictionary<string,Command> commands = new Dictionary<string, Command>();

        public Command GetCommandByName(string name)
        {
            if (commands.ContainsKey(name.ToLower()))
                return commands[name.ToLower()]; // Если ключ не найден, вернёт null?
            else
                return null;
        }

        public void Add(Command command)
        {
            if (!commands.Keys.Contains(command.Name))
                commands.Add(command.Name, command);
            else throw new Exception("You can not add 2 commands with the same name");
        }
        public List<Command> GetAllCommands()
        {
            return commands.Select(c => c.Value).ToList();
        }
    }
}
