namespace ConsoleAppLib
{
    public interface IParser
    {
        IParserOutput TryParse(string input);
    }
}
