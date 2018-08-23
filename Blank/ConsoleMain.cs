using ConsoleAppLib;

namespace Blank
{
    class ConsoleMain
    {
        static void Main(string[] args)
        {
            Frame frame = new MyFrame();

            frame.Run();
        }

    }
    public class MyFrame : Frame
    {
        public MyFrame()
        {
            Dispatcher.Add(new CommandNonParams(name: "Frame",
                                       action: (args) =>
                                       {
                                           var frame = new Frame();
                                           frame.Run();
                                           return true;
                                       }));
        }
    }
}
