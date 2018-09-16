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
        public Styler Styler { get; private set; } = new Styler();

        public bool _isMainFrame { get; set; } = true;

        private bool _quit = false;

        public void Run()
        {
            Console.Clear();
            Styler.DisplayGreetings();

            while (!_quit)
            {
                Styler.DisplayInputSymbols();
                var input = Console.ReadLine();
                var parsed = (CommandParserOutput)CommandParser.TryParse(input);

                if (parsed.Response == CommandParserResponse.Ok)
                    parsed.Command.Execute(parsed.Parameters);
                else
                    Styler.DisplayParserResponse(parsed.Response);
            }
            Console.Clear();
            Styler.DisplayFarewell();

            if (_isMainFrame)
                Console.ReadKey();
        }

        public Frame(bool is_mainframe)
        {
            Dispatcher = new CommandDispatcher();
            CommandParser = new CommandParser(Dispatcher);
            _isMainFrame = is_mainframe;

            Add(
                name: "Quit",
                action: (args) => { return _quit = true; },
                validation: (args) => { return ArgsCount(args) == 0; },
                commandinfo: "Exits application.");

            Add(
                name: "Clear",
                action: (args) =>
                {
                    Console.Clear();
                    Styler.DisplayGreetings();
                    return true;
                },
                validation: (args) => { return ArgsCount(args) == 0; },
                commandinfo: "Clears the console");

            Add(
                name: "Info",
                action: (args) =>
                {
                    Styler.DisplayCommandInfo(Dispatcher.GetCommandByName(args[0]));
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
                    Styler.DisplayLine();
                    foreach (var command in Dispatcher.GetAllCommands())
                        Styler.DisplayCommandInfo(command);
                    Styler.DisplayLine();
                    Console.WriteLine();

                    return true;
                },
                validation: (args) => { return ArgsCount(args) == 0; },
                commandinfo: "Displays full list of commands for current frame.");
        }

        protected void Add(string name, Func<string[], bool> action, Func<string[], bool> validation, string commandinfo = "No info for this command")
        {
            Dispatcher.Add(new Command(name, action, validation, commandinfo));
        }
        protected void Add(Command command)
        {
            Dispatcher.Add(command); //Если делать каждую команду отдельным классом.
        }

        protected static int ArgsCount(params string[] args)
        {
            if (args == null)
                return 0;
            else
            {
                if (!args.GetType().IsArray)
                    return 1;
                else
                    return args.Length;
            }
        }

    }


}
