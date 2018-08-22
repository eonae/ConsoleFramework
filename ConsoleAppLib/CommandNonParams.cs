using System;

namespace ConsoleAppLib
{
    public class CommandNonParams : Command
    {
        public CommandNonParams(string name, Func<string[], bool> action) :
          base
            (name: name, action: action,
             validation: (parameters) => { return parameters == null; } // Возвращает true, только если параметры не переданы.
      ) { }
    }
}
