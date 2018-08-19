using System;
using ConsoleAppLib;

namespace Blank
{
    class ConsoleMain
    {
        static void Main(string[] args)
        {
            var app = new ConsoleFrame();
            Styler.Appearence.InnerMargin = (3, 0);
            app.Run();
        }
    }
}
