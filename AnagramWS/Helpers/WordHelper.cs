using System.Globalization;
using System.Text;

namespace AnagramWS.Helpers;

public static class WordHelper
{
    /// <summary>
    /// Returns a lowercased and sorted version of the word.
    /// </summary>
    /// <param name="word">Word to be lowercased and sorted.</param>
    public static string ToLowerAndSorted(this string word)
    {
        return string.Concat(word.RemoveDiacritics().ToLower().OrderBy(c => c));
    }
    
    /// <summary>
    /// Removes the diacritics from the string. (eg: niché becomes niche)
    /// (Source : James' White Linq implementation of Michael Kaplan's
    /// method : https://archives.miloush.net/michkap/archive/2007/05/14/2629747.html) 
    /// </summary>
    /// <param name="stIn"> Word to remove diacritics from.</param>
    public static string RemoveDiacritics(this string stIn)
    {
        var sb = new StringBuilder();

        sb.Append(
            stIn.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
                            != UnicodeCategory.NonSpacingMark)
                .ToArray());

        return (sb.ToString().Normalize(NormalizationForm.FormC));
    }
}