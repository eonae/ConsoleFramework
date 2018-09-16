using System;

namespace ConsoleAppLib
{
    /* У фрейма есть две возможные функции:
     * - либо принять команду, т. е. одно слово + набор параметров
     * - либо принять целиком текст и что-то вернуть, причём было бы интересно вставлять многострочный текст.
     * - либо 
     * Т.е. MainFrame
     */
    public enum InternalAction { Quit, RepeatInput, ExecuteCommand }


    public class Frame
    {
        public CommandDispatcher Dispatcher { get; private set; }
        public CommandParser CommandParser { get; private set; }
        public bool MainFrame { get; set; } = true;

        private bool quit = false;

        public void Run()
        {
            ConsolePrinter.DisplayGreetings();

            while (!quit)
            {
                ConsolePrinter.DisplayInputSymbols();
                var input = Console.ReadLine();
                var parsed = (CommandParserOutput)CommandParser.TryParse(input);

                if (parsed.Response == CommandParserResponse.Ok)
                    parsed.Command.Execute(parsed.Parameters);
                else
                    ConsolePrinter.DisplayParserResponse(parsed.Response);
            }
            ConsolePrinter.DisplayFarewell();

            if (MainFrame)
                Console.ReadKey();
        }

        public Frame()
        {
            Dispatcher = new CommandDispatcher();
            CommandParser = new CommandParser(Dispatcher);

            Add(
                name: "Quit",
                action: (args) => { return quit = true; },
                commandinfo: "Exits application.");

            Add(
                name: "Clear",
                action: (args) =>
                {
                    Console.Clear();
                    ConsolePrinter.DisplayGreetings();
                    return true;
                },
                commandinfo: "Clears the console");

            Add(
                name: "Info",
                action: (args) =>
                {
                    ConsolePrinter.DisplayCommandInfo(Dispatcher.GetCommandByName(args[0]));
                    return true;
                },
                validation: (args) =>
                {
                    if (args != null)
                        if (args.Length == 1)
                            if (Dispatcher.GetCommandByName(args[0]) != null)
                                return true; // Добавить возможность...
                    return false;
                },
                commandinfo: "@command_name - Displays command info.");

            Add(
                name: "Help",
                action: (args) =>
                {
                    Console.WriteLine();
                    ConsolePrinter.DisplayLine();
                    foreach (var command in Dispatcher.GetAllCommands())
                        ConsolePrinter.DisplayCommandInfo(command);
                    ConsolePrinter.DisplayLine();
                    Console.WriteLine();

                    return true;
                },
                commandinfo: "Displays full list of commands for current frame.");
        }

        protected void Add(string name, Func<string[], bool> action, Func<string[], bool> validation, string commandinfo = "No info for this command")
        {
            Dispatcher.Add(new Command(name, action, validation, commandinfo));
        }
        protected void Add(string name, Func<string[], bool> action, string commandinfo = "No info for this command")
        {
            Dispatcher.Add(new CommandNonParams(name, action, commandinfo));
        }
        protected void Add(Command command)
        {
            Dispatcher.Add(command); //Если делать каждую команду отдельным классом.
        }

    }


}
