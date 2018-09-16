namespace ConsoleAppLib
{
    public class AppearenceConfig
    {
        public (char Normal, char Strong) VerticalLineStyles { get; set; } = ('*', '*');
        public (char Normal, char Strong) HorizontalLineStyles { get; set; } = ('*', '*');
        public (int Left, int Top) Margins { get; set; } = (1, 1);
        public (int Horizontal, int Vertical) InnerMargin { get; set; } = (10, 1);
        public string InputSymbols { get; set; } = ">>";
    }
}
