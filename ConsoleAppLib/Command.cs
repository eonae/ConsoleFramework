using System;

namespace ConsoleAppLib
{

    public delegate void ExecutionDelegate(CommandParameters parameters);
    public delegate bool ValidationDelegate(CommandParameters parameters);

    /// <summary>
    /// Класс, отвечает за приём, обработку команды и вызов соответствующего метода
    /// </summary>
    public class Command
    {
        public readonly string Name;
        public readonly bool IsInternal;
        private ExecutionDelegate commandDelegate;
        private ValidationDelegate validationDelegate;
        public bool NeedParameters { get { return validationDelegate != null; } }

        public bool ValidateParameters(CommandParameters parameters)
        {
            return true; // Пока так.
        }
        public void Execute(CommandParameters parameters)
        {
            if (validationDelegate.Invoke(parameters))
                commandDelegate.Invoke(parameters);
            else
                throw new Exception("Invalid parameters!"); // Проверка только на то, есть параметры вообще или нет.

        }

        public Command(bool isInternal, string name, ExecutionDelegate commandDelegate, ValidationDelegate validationDelegate)
        {
            Name = name;
            this.commandDelegate += commandDelegate;
            this.validationDelegate += validationDelegate;
            IsInternal = isInternal;
        }

        public Command(bool isInternal, string name, ExecutionDelegate commandDelegate):
            this(isInternal, name, commandDelegate, (parameters)=> { return true; }) { }

    }
}
