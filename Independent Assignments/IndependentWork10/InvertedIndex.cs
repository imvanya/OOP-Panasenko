namespace InvertedIndexDemo;

public class InvertedIndex
{
    private readonly Dictionary<string, HashSet<int>> _index;

    private readonly Dictionary<int, Document> _documents;

    private readonly bool _removeStopWords;

    /// <param name="removeStopWords">Чи не індексувати стоп-слова (за замовчуванням ні).</param>
    public InvertedIndex(bool removeStopWords = false)
    {
        _index = new Dictionary<string, HashSet<int>>();
        _documents = new Dictionary<int, Document>();
        _removeStopWords = removeStopWords;
    }

    public int DocumentCount => _documents.Count;
    public int UniqueWords => _index.Keys.Count;

    public void AddDocument(Document doc)
    {
        ArgumentNullException.ThrowIfNull(doc);
        if (!_documents.TryAdd(doc.Id, doc))
        {
            throw new ArgumentException($"Документ з Id={doc.Id} уже є в індексі.", nameof(doc));
        }

        foreach (var word in Terms(doc.Content))
        {
            if (!_index.TryGetValue(word, out var ids))
            {
                ids = new HashSet<int>();
                _index[word] = ids;
            }
            ids.Add(doc.Id);
        }
    }

    public List<Document> SearchSingleWord(string word)
    {
        var ids = Lookup(word);
        return ids == null ? new List<Document>() : ToDocuments(ids);
    }

    public List<Document> SearchMultipleWords(params string[] words)
    {
        HashSet<int> result = null;
        foreach (var word in words ?? Array.Empty<string>())
        {
            var ids = Lookup(word);
            if (ids == null) continue; // слово без значущих токенів (напр. стоп-слово) ігноруємо

            if (result == null)
            {
                result = ids;
            }
            else
            {
                result.IntersectWith(ids);
            }

            if (result.Count == 0) break; // далі перетин вже не зміниться
        }
        return result == null ? new List<Document>() : ToDocuments(result);
    }

    public List<Document> SearchAnyWord(params string[] words)
    {
        var result = new HashSet<int>();
        foreach (var word in words ?? Array.Empty<string>())
        {
            var ids = Lookup(word);
            if (ids != null)
            {
                result.UnionWith(ids);
            }
        }
        return ToDocuments(result);
    }

    private IEnumerable<string> Terms(string text)
    {
        var tokens = TextProcessor.Tokenize(text);
        return _removeStopWords ? TextProcessor.RemoveStopWords(tokens) : tokens;
    }

    private HashSet<int> Lookup(string word)
    {
        HashSet<int> result = null;
        foreach (var token in Terms(word))
        {
            if (!_index.TryGetValue(token, out var ids))
            {
                return new HashSet<int>(); // токена немає в індексі — збігів немає
            }

            if (result == null)
            {
                result = new HashSet<int>(ids);
            }
            else
            {
                result.IntersectWith(ids);
            }
        }
        return result;
    }

    private List<Document> ToDocuments(HashSet<int> ids)
    {
        return ids.OrderBy(id => id).Select(id => _documents[id]).ToList();
    }
}