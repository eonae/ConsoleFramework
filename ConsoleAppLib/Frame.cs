using System;

namespace ConsoleAppLib
{
    public enum InternalAction { Quit, RepeatInput, ExecuteCommand }

    public class Frame
    {
        public CommandDispatcher Dispatcher { get; private set; }
        public InputParser Parser { get; private set; }
        private bool quit = false;

        public void Run()
        {
            ConsolePrinter.DisplayGreetings();
            bool quit = false;
            while (!quit)
            {
                ConsolePrinter.DisplayInputSymbols();
                var input = Console.ReadLine();
                var parsed = Parser.TryParse(input);

                if (parsed.response == ParserResponse.Ok)
                    parsed.command.Execute(parsed.parameters);
                //else
                    // ConsolePrinter.DisplayStandardMessage(parsed.response);
            }
        }

        public Frame()
        {
            Dispatcher = new CommandDispatcher();
            Parser = new InputParser(Dispatcher);

            Dispatcher.Add(new CommandNonParams(name: "Quit",
                           action: (parameters) =>
                           {
                               return quit = true;
                           }));
        }
    }
}
