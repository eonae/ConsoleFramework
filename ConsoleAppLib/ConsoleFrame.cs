using System;

namespace ConsoleAppLib
{
    public enum InternalAction { Quit, RepeatInput, NoAction }

    public class ConsoleFrame
    {
        private CommandsDispatcher dispatcher = new CommandsDispatcher();

        
        private static InternalAction action = InternalAction.NoAction;
        public static void ChangeAction(InternalAction changeTo)
        {
            action = changeTo;
        } 

        public void Run()
        {
            Styler.DisplayGreetings();
            while (action!=InternalAction.Quit)
            {
                Styler.DisplayInputSymbols();
                var input = Console.ReadLine();
                var parsed = dispatcher.TryParse(input);
                parsed.Command.Execute(parsed.Parameters);
                switch (action)
                {
                    case InternalAction.Quit:
                        Styler.DisplayFarewell();
                        Console.ReadLine();
                        break;
                    case InternalAction.RepeatInput:
                        StandardMessages.Display(StandardMessage.InvalidCommand);
                        continue;
                }
            }

        }
    }
}
