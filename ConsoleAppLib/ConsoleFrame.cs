using System;

namespace ConsoleAppLib
{
    public enum CommandEnum { Quit, RepeatInput, DoSomething }

    public class ConsoleFrame
    {

        public void Run()
        {
            Styler.DisplayGreetings();
            CommandEnum command = CommandEnum.RepeatInput;
            while (command != CommandEnum.Quit)
            {
                command = PerformAction(TryParseCommand());
            }
            Styler.DisplayFarewell();
            Console.ReadLine();
        }
        // Мы забираем какое-то значение и отправляем его на валидацию. Валидация нам говорит:

        private static CommandEnum TryParseCommand()
        {
            Styler.DisplayInputSymbols();
            var input = Console.ReadLine();
            foreach (var name in Enum.GetNames(typeof(CommandEnum)))
                if (input.ToLower() == name.ToLower())
                    return (CommandEnum)Enum.Parse(typeof(CommandEnum), name);
            StandardMessages.Display(StandardMessage.InvalidCommand);
            return CommandEnum.RepeatInput;
        }

        private static CommandEnum PerformAction(CommandEnum command)
        {
            switch (command)
            {
                case CommandEnum.Quit:
                    return command;
                case CommandEnum.DoSomething:
                    Console.WriteLine("Here i will invoke some real method!");
                    // Invoke some method.
                    return command;
                default:
                    return CommandEnum.RepeatInput;
            }
        }
    }
}
