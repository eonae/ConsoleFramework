using System;

namespace Eonae.Terminal
{
    public enum StandardMessage { NoMessage, InvalidCommand, InvalidParameters, InternalCommand }

    public static class StandardMessages
    {
        public static string InvalidCommand { get; set; } = "Invalid command!";
        public static string InvalidParameters { get; set; } = "Invalid command parameters!";
        public static string InternalCommand { get; set; } = "This command is internal. You cannot execute it from terminal";

        public static void Display(StandardMessage messageName)
        {
            if (messageName == StandardMessage.NoMessage)
                return;
            var msg = typeof(StandardMessages).GetProperty(messageName.ToString()).GetValue(null);
            Console.WriteLine(msg);
        }
    }
}
