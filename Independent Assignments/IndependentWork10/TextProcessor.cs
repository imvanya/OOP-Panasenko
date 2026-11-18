using System.Text.RegularExpressions;

namespace InvertedIndexDemo;

public static class TextProcessor
{
    private static readonly Regex WordRegex = new Regex(@"\w+", RegexOptions.Compiled);

    private static readonly HashSet<string> StopWords = new HashSet<string>
    {
        "a", "an", "the", "is", "are", "was", "were",
        "in", "on", "at", "to", "for", "of", "and", "or"
    };

    public static IEnumerable<string> Tokenize(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return Enumerable.Empty<string>();
        }
        return WordRegex.Matches(text.ToLowerInvariant()).Select(m => m.Value);
    }

    public static IEnumerable<string> RemoveStopWords(IEnumerable<string> words)
    {
        return words.Where(w => !StopWords.Contains(w));
    }
}