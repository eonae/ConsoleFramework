using System;

namespace ConsoleAppLib
{
    public enum InternalAction { Quit, RepeatInput, ExecuteCommand }

    public class Frame
    {
        public CommandDispatcher Dispatcher { get; private set; }
        public Parser Parser { get; private set; }
        public bool MainFrame { get; set; } = true;

        private bool quit = false;
        public void Run()
        {
            ConsolePrinter.DisplayGreetings();
            while (!quit)
            {
                ConsolePrinter.DisplayInputSymbols();
                var input = Console.ReadLine();
                var parsed = Parser.TryParse(input);

                if (parsed.response == ParserResponse.Ok)
                    parsed.command.Execute(parsed.parameters);
                else
                    ConsolePrinter.DisplayParserResponse(parsed.response);
            }
            ConsolePrinter.DisplayFarewell();
            if (MainFrame)
                Console.ReadKey();
        }

        public Frame()
        {
            Dispatcher = new CommandDispatcher();
            Parser = new Parser(Dispatcher);

            Dispatcher.Add(new CommandNonParams(name: "Quit",
                                                action: (parameters) => { return quit = true; },
                                                commandinfo: "Command info: type'quit' to exit application."));
            Dispatcher.Add(new Command(name: "Info",
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
                                       commandinfo: "Command info: type 'info @command_name' to get info for any command."
                                       ));
            Dispatcher.Add(new CommandNonParams(name: "Help",
                                       action: (args) =>
                                       {
                                           foreach (var command in Dispatcher.GetAllCommands())
                                               ConsolePrinter.DisplayCommandInfo(command);
                                           return true;

                                       },
                                       commandinfo: "Command info: type 'help' to get the list of all avalible commands."
                                       ));
        }
    }
}
