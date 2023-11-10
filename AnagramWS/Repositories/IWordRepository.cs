namespace AnagramWS.Repositories;

public interface IWordRepository
{
    /// <summary>
    /// Returns the dictionary.
    /// </summary>
    Dictionary<string, List<string>> GetWordsDictionary();
    
    /// <summary>
    /// Add a word to the dictionary.
    /// </summary>
    /// <param name="word">Word to be added to the dictionary</param>
    /// <exception cref="Exception">Thrown when the word is already in the dictionary</exception>
    void Add(string word);
    
    /// <summary>
    /// Remove a word from the dictionary.
    /// </summary>
    /// <param name="word">Word to be removed from the dictionary</param>
    /// <exception cref="Exception">Thrown when the word is not found in the dictionary</exception>
    void Remove(string word);
    
    /// <summary>
    /// Returns a list of anagrams for the given word.
    /// </summary>
    /// <param name="word">Word to find anagrams for</param>
    IEnumerable<string> FindAnagrams(string word);
    
    /// <summary>
    /// Count the number of words in the dictionary that have anagrams.
    /// </summary>
    int CountWordsWithAnagrams();
}