using System;

namespace ConsoleAppLib
{
    public enum StandardMessage { InvalidCommand, InvalidParameters }
    public static class StandardMessages
    {
        public static string InvalidCommand { get; set; } = "Invalid command!";
        public static string InvalidParameters { get; set; } = "Invalid command parameters!";

        public static void Display(StandardMessage messageName)
        {
            var msg = typeof(StandardMessages).GetProperty(messageName.ToString()).GetValue(null);
            Console.WriteLine(msg);
        }
    }
}
