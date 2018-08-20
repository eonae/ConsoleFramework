using ConsoleAppLib;

namespace Blank
{
    class ConsoleMain
    {
        static void Main(string[] args)
        {
            ConsoleFrame frame = new ConsoleFrame();
            // Как бы сделать так, чтобы изменять поля Action и standard_message могли ТОЛЬКО команды!

            frame.Run();
        }
    }
}
