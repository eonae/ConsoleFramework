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
            ///internalCommands.Add(new Command());
        }


    }
}
