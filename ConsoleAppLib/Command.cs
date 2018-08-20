using System;

namespace ConsoleAppLib
{

    public delegate void CommandDelegate(CommandParameters parameters);

    /// <summary>
    /// Класс, отвечает за приём, обработку команды и вызов соответствующего метода
    /// </summary>
    public class Command
    {
        public readonly string Name;
        public readonly bool IsInternal;
        public readonly bool NeedParameters;
        private CommandDelegate commandDelegate;

        public void Execute(CommandParameters parameters)
        {
            if (NeedParameters!=parameters.IsEmpty)
                commandDelegate.Invoke(parameters);
            else
                throw new Exception("Invalid parameters!"); // Проверка только на то, есть параметры вообще или нет.
            
        }

        public Command(bool isInternal, string name, bool needParameters, CommandDelegate commandDelegate)
        {
            Name = name;
            this.commandDelegate += commandDelegate;
            IsInternal = isInternal;
            NeedParameters = needParameters;
        }
    }
}
