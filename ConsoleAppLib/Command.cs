using System;

namespace ConsoleAppLib
{
    public class Command
    {
        private Func<string[], bool> validation;
        private Func<string[], bool> action;
        public string Name { get; private set; }
        public string Commandinfo { get; private set; }
        public bool IsValid(string[] parameters)
        {
            return validation.Invoke(parameters);
        }
        public bool Execute(string[] parameters)
        {
            if (!IsValid(parameters))
                throw new Exception("Exception: invalid parameters!");
            return action.Invoke(parameters);
        }
        public Command(string name, Func<string[], bool> action, Func<string[], bool> validation, string commandinfo)
        {
            Name = name.ToLower();
            this.validation = validation;
            this.action = action;
            Commandinfo = commandinfo;
        }
    }
}
