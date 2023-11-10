namespace AnagramWS.Exceptions;

public class WordAlreadyExistsException : Exception
{
    public WordAlreadyExistsException(string word) : base($"The word \"{word}\" is already in the dictionary.")
    {
    }
}