namespace CAI.Services.Abstraction
{
    public interface ILanguageProcessinService
    {
        string[] GreedyTokenize(string text);

        string[] Tokenize(string text, bool keepDigits);
    }
}
