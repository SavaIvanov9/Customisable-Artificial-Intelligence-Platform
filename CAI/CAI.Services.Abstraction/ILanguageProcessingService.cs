namespace CAI.Services.Abstraction
{
    public interface ILanguageProcessingService
    {
        string[] GreedyTokenize(string text);

        string[] Tokenize(string text, bool keepDigits);
    }
}
