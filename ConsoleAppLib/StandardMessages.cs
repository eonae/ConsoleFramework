using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleAppLib
{
    public enum StandardMessage { InvalidCommand, Test }
    public static class StandardMessages
    {
        public static string InvalidCommand { get; set; } = "Invalid command!";
        public static string Test { get; set; } = "Test!";

        public static void Display(StandardMessage messageName)
        {
            var msg = typeof(StandardMessages).GetProperty(messageName.ToString()).GetValue(null);
            Console.WriteLine(msg);
        }
    }
}
