using System;

namespace ConsoleAppLib
{
    public enum InternalAction { Quit, RepeatInput, NoAction }

    public class ConsoleFrame
    {
        private CommandsDispatcher dispatcher;

        // Как бы сделать так, чтобы только команды могли влиять на эти поля!
        private InternalAction action = InternalAction.NoAction;
        private StandardMessage standard_message = StandardMessage.NoMessage;

        public void AddCommand(Command command)
        {
            dispatcher.Add(command);
        }
        public void AddCommands(Command[] commands)
        {
            foreach (var c in commands)
            {
                dispatcher.Add(c);
            }
        }

        public void ChangeAction(InternalAction changeTo) 
        {
            action = changeTo;
        } 
        public void ChangeMessage(StandardMessage changeTo)
        {
            standard_message = changeTo;
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
                StandardMessages.Display(standard_message);
                standard_message = StandardMessage.NoMessage;

                switch (action)
                {
                    case InternalAction.Quit:
                        Styler.DisplayFarewell();
                        Console.ReadLine();
                        break;
                    case InternalAction.RepeatInput:
                        continue;
                }
            }
        }

        public ConsoleFrame()
        {
            dispatcher = new CommandsDispatcher(this);
        }
    }
}
