using Eonae.Terminal;

namespace Blank
{
    public class MainFrame : Frame
    {
        public static void Main()
        {
            new MainFrame().Run();
        }
        public MainFrame() : base(true)
        {
            Styler.GreetingsMessage = "Mainframe active";
            Styler.FarewellMessage = "Mainframe terminated";

            Add(
                name: "Access",
                action: (args) =>
                {
                    var frame = new AccessDbFrame();
                    frame.Run();
                    return true;
                },
                validation: (args) => { return ArgsCount(args) == 0; },
                commandinfo: "Opens new frame. Just for testing.");
        }
    }
}
