using AnagramWS.Exceptions;
using AnagramWS.Repositories;
using Microsoft.Extensions.Configuration;

namespace AnagramWS.Test;

public class WordRepositoryTest
{
    [Fact]
    public void Constructor_WithWordsList_PopulatesDictionary()
    {
        var repository = new WordRepository(new string[]
        {
            "chien",
            "niche",
            "chine",
        });
        
        var dictionary = repository.GetWordsDictionary();
        
        Assert.Equal(1, dictionary.Count);
        Assert.Equal(3, dictionary["cehin"].Count);
    }
    
    [Fact]
    public void Constructor_WithConfiguration_PopulatesDictionary()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>()
            {
                {"Settings:DictionaryFile", "Data/mots.txt"}
            })
            .Build();
        
        var repository = new WordRepository(configuration);
        
        var dictionary = repository.GetWordsDictionary();
        
        Assert.NotNull(dictionary);
        Assert.Equal(1, dictionary.Count);
        Assert.Equal(3, dictionary["cehin"].Count);
    }
    
    [Fact]
    public void Constructor_WithConfigurationButWrongFile_ThrowsFileNotFoundException()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>()
            {
                {"Settings:DictionaryFile", "Data/mots2.txt"}
            })
            .Build();
        
        Assert.Throws<FileNotFoundException>(() => new WordRepository(configuration));
    }
    
    [Fact]
    public void Add_WordNotInDictionary_AddsWordToDictionary()
    {
        var repository = new WordRepository(new string[]{});
        
        repository.Add("chien");
        
        var dictionary = repository.GetWordsDictionary();
        
        Assert.NotNull(dictionary);
        Assert.Equal(1, dictionary.Count);
        Assert.Equal(1, dictionary["cehin"].Count);
        Assert.Contains("chien", repository.GetWordsDictionary()["cehin"]);
    }
    
    [Fact]
    public void Add_WordAlreadyInDictionary_ThrowsWordAlreadyExistsException()
    {
        var repository = new WordRepository(new string[]
        {
            "chien"
        });
        
        Assert.Throws<WordAlreadyExistsException>(() => repository.Add("chien"));
    }
    
    [Fact]
    public void Remove_WordInDictionnay_RemovesWordsFromDictionary()
    {
        var repository = new WordRepository(new string[]
        {
            "chien"
        });
        
        repository.Remove("chien");
        
        var dictionary = repository.GetWordsDictionary();
        
        Assert.NotNull(dictionary);
        Assert.Equal(0, dictionary.Count);
    }
    
    [Fact]
    public void Remove_WordNotInDictionnay_ThrowsWordNotFoundException()
    {
        var repository = new WordRepository(new string[]{});
        
        Assert.Throws<WordNotFoundException>(() => repository.Remove("chien"));
    }
    
    [Fact]
    public void FindAnagrams_WordInDictionary_ReturnsAnagrams()
    {
        var repository = new WordRepository(new string[]
        {
            "chien",
            "niche",
            "chine",
        });
        
        var anagrams = repository.FindAnagrams("chien");
        
        Assert.NotNull(anagrams);
        Assert.Equal(2, anagrams.Count());
        Assert.Contains("chine", anagrams);
        Assert.Contains("niche", anagrams);
    }
    
    [Fact]
    public void FindAnagrams_WordNotInDictionary_ReturnsEmptyList()
    {
        var repository = new WordRepository(new string[]{});
        
        var anagrams = repository.FindAnagrams("chien");
        
        Assert.NotNull(anagrams);
        Assert.Empty(anagrams);
    }
    
    [Fact]
    public void CountWordsWithAnagrams_DictionaryWithWords_ReturnsCount()
    {
        var repository = new WordRepository(new string[]
        {
            "chien",
            "niche",
            "chine",
            "gare",
            "rage"
        });
        
        var count = repository.CountWordsWithAnagrams();
        
        Assert.Equal(2, count);
    }
    
    [Fact]
    public void CountWordsWithAnagrams_DictionaryWithoutWords_ReturnsZero()
    {
        var repository = new WordRepository(new string[]{});
        
        var count = repository.CountWordsWithAnagrams();
        
        Assert.Equal(0, count);
    }
}