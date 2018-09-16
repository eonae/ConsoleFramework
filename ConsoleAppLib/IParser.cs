namespace Eonae.Terminal
{
    public interface IParser
    {
        IParserOutput TryParse(string input);
    }
}
