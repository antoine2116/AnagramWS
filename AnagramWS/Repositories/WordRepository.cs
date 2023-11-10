using AnagramWS.Exceptions;
using AnagramWS.Helpers;

namespace AnagramWS.Repositories;

public class WordRepository : IWordRepository
{
    private Dictionary<string, List<string>> _wordsDictionary = new();

    /// <summary>
    /// Creates a new instance of the WordRepository class.
    /// Reads the words from the DictionaryFile and populates the dictionary.
    /// </summary>
    /// <exception cref="FileNotFoundException">Thrown when the Data/words.txt file is not found</exception>
    public WordRepository(IConfiguration configuration)
    {
        var dir = Directory.GetCurrentDirectory();
        var wordsFile = configuration["Settings:DictionaryFile"];
        
        if (!File.Exists(wordsFile))
            throw new FileNotFoundException($"The file {wordsFile} was not found.");

        var wordsList = File.ReadAllLines(wordsFile);
        
        PopulateDictionary(wordsList);
    }

    /// <summary>
    /// Creates a new instance of the WordRepository class and populates the dictionary.
    /// </summary>
    /// <param name="wordsList"></param>
    public WordRepository(string[] wordsList) => PopulateDictionary(wordsList);
    
    /// <summary>
    /// Populates the dictionary with the words from the wordsList.
    /// </summary>
    /// <param name="wordsList"></param>
    private void PopulateDictionary(string[] wordsList)
    {
        _wordsDictionary = new Dictionary<string, List<string>>();
        foreach (var word in wordsList)
        {
            Add(word);
        }
    }

    public Dictionary<string, List<string>> GetWordsDictionary() => _wordsDictionary;
    
    public void Add(string word)
    {
        var sortedWord = word.ToLowerAndSorted();

        if (!_wordsDictionary.ContainsKey(sortedWord))
        {
            _wordsDictionary.Add(sortedWord, new List<string> {word});
            return;
        }

        if (_wordsDictionary[sortedWord].Contains(word))
            throw new WordAlreadyExistsException(word);
        
        _wordsDictionary[sortedWord].Add(word);
    }
    
    public void Remove(string word)
    {
        var sortedWord = word.ToLowerAndSorted();

        if (!_wordsDictionary.ContainsKey(sortedWord) || !_wordsDictionary[sortedWord].Contains(word))
            throw new WordNotFoundException(word);
        
        _wordsDictionary[sortedWord].Remove(word);
        
        if (_wordsDictionary[sortedWord].Count == 0)
            _wordsDictionary.Remove(sortedWord);
    }
    
    public IEnumerable<string> FindAnagrams(string word)
    {
        var sortedWord = word.ToLowerAndSorted();
        
        if (!_wordsDictionary.ContainsKey(sortedWord))
            return new List<string>();
        
        return _wordsDictionary[sortedWord].Where(w => w != word).ToList();
    }
   
    public int CountWordsWithAnagrams()
    {
        return _wordsDictionary.Count(w => w.Value.Count > 1);
    }
}