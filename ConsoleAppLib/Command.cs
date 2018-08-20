using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLib
{
    /// <summary>
    /// Класс, отвечает за приём, обработку команды и вызов соответствующего метода
    /// </summary>
    
    public interface ICommandParameters { }

    public class BlankSingleton : ICommandParameters
    {
        private static BlankSingleton instance;

        private BlankSingleton() { }
        public static BlankSingleton GetInstance()
        {
            if (instance == null)
                instance = new BlankSingleton();
            return instance;
        }
    }

    public class CommandParameters : ICommandParameters
    {
        private string[] arr;
        
        public CommandParameters(params string[] parameters)
        {
            arr = parameters;
        }
    }

    public delegate void CommandDelegate(ICommandParameters parameters);

    public class Command
    {
        public readonly string Name;
        public readonly bool IsInternal;
        public readonly bool NeedParameters;
        private CommandDelegate commandDelegate;

        public Command(bool isInternal, string name, bool needParameters, CommandDelegate commandDelegate)
        {
            Name = name;
            this.commandDelegate += commandDelegate;
            IsInternal = isInternal;
            NeedParameters = needParameters;
        }

        public void Execute(ICommandParameters parameters)
        {
            if (!NeedParameters)
                if (parameters.GetType() != BlankSingleton.GetInstance().GetType())
                    throw new Exception("You should pass BlankSingleton.GetInstance to this method, because command doesn't need parameters!");
            commandDelegate.Invoke(parameters);
        }
    }
}
