using System;

namespace ConsoleAppLib
{
    public class CommandNonParams : Command
    {
        public CommandNonParams(string name, Func<string[], bool> action, string commandinfo = "No info for this command") :
          base
            (name: name, action: action,
             validation: (parameters) => { return parameters == null; },
             commandinfo: commandinfo// Возвращает true, только если параметры не переданы.
      ) { }
    }
}
