namespace AnagramWS.Exceptions;

public class WordNotFoundException: Exception
{
    public WordNotFoundException(string word) : base($"The word \"{word}\" was not found in the dictionary.")
    {
    }
}