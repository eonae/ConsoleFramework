using ConsoleAppLib;

namespace Blank
{
    public class MainFrame : Frame
    {
        public static void Main()
        {
            new MainFrame().Run();
        }
        public MainFrame()
        {

            Add(
                name: "AccessDB",
                action: (args) =>
                {
                    var frame = new AccessDbFrame();
                    frame.Run();
                    return true;
                },
                commandinfo: "Opens new frame. Just for testing.");
        }
    }
}
