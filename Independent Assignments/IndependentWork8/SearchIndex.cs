using System.Text.RegularExpressions;

namespace Lab8Self;

public class SearchIndex
{
    private static readonly Regex WordRegex = new Regex(@"[\p{L}\p{Nd}]+", RegexOptions.Compiled);

    private readonly Dictionary<string, HashSet<int>> _invertedIndex =
        new Dictionary<string, HashSet<int>>(StringComparer.OrdinalIgnoreCase);

    public int WordCount => _invertedIndex.Count;

    public void AddDocument(int docId, string content)
    {
        if (string.IsNullOrWhiteSpace(content)) return;

        foreach (Match match in WordRegex.Matches(content))
        {
            string word = match.Value;
            if (!_invertedIndex.TryGetValue(word, out var docs))
            {
                docs = new HashSet<int>();
                _invertedIndex[word] = docs;
            }
            docs.Add(docId); 
        }
    }

    public HashSet<int> Search(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return new HashSet<int>();
        return _invertedIndex.TryGetValue(word.Trim(), out var docs)
            ? new HashSet<int>(docs)
            : new HashSet<int>();
    }

    public HashSet<int> SearchMultiple(params string[] words)
    {
        if (words == null || words.Length == 0) return new HashSet<int>();

        var sets = new List<HashSet<int>>();
        foreach (var word in words)
        {
            if (string.IsNullOrWhiteSpace(word) ||
                !_invertedIndex.TryGetValue(word.Trim(), out var docs))
            {
                return new HashSet<int>();
            }
            sets.Add(docs);
        }

        sets.Sort((a, b) => a.Count.CompareTo(b.Count));

        var result = new HashSet<int>(sets[0]);
        for (int i = 1; i < sets.Count && result.Count > 0; i++)
        {
            result.IntersectWith(sets[i]);
        }
        return result;
    }
}