using AnagramWS.Helpers;

namespace AnagramWS.Test.Helpers;

public class WordHelperTest
{
    [Fact]
    public void RemoveDiacritics_ReturnsStringWithoutDiacritics()
    {
        var word = "niché";
        var wordWithoutDiacritics = word.RemoveDiacritics();
        Assert.Equal("niche", wordWithoutDiacritics);
    }
    
    [Fact]
    public void ToLowerAndSorted_ReturnsStringLowercasedAndSorted()
    {
        var word = "NiChé";
        var wordLowercasedAndSorted = word.ToLowerAndSorted();
        Assert.Equal("cehin", wordLowercasedAndSorted);
    }
}