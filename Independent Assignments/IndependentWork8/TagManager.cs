namespace Lab8Self;

public class TagManager
{
    private readonly Dictionary<string, HashSet<string>> _articleTags =
        new Dictionary<string, HashSet<string>>();

    public int ArticleCount => _articleTags.Count;

    private static string Normalize(string tag) => tag.Trim().ToLowerInvariant();

    public void AddTag(string articleId, string tag)
    {
        ArgumentNullException.ThrowIfNull(articleId);
        if (string.IsNullOrWhiteSpace(tag)) return;

        if (!_articleTags.TryGetValue(articleId, out var tags))
        {
            tags = new HashSet<string>();
            _articleTags[articleId] = tags;
        }
        tags.Add(Normalize(tag)); 
    }

    public void RemoveTag(string articleId, string tag)
    {
        if (articleId == null || string.IsNullOrWhiteSpace(tag)) return;

        if (_articleTags.TryGetValue(articleId, out var tags))
        {
            tags.Remove(Normalize(tag));
            if (tags.Count == 0)
            {
                _articleTags.Remove(articleId); 
            }
        }
    }

    public HashSet<string> GetAllUniqueTags()
    {
        var all = new HashSet<string>();
        foreach (var tags in _articleTags.Values)
        {
            all.UnionWith(tags);
        }
        return all;
    }

    public HashSet<string> FindCommonTags(string articleId1, string articleId2)
    {
        if (!_articleTags.TryGetValue(articleId1, out var tags1) ||
            !_articleTags.TryGetValue(articleId2, out var tags2))
        {
            return new HashSet<string>();
        }

        var common = new HashSet<string>(tags1); 
        common.IntersectWith(tags2);
        return common;
    }

    public HashSet<string> FindUniqueTags(string articleId1, string articleId2)
    {
        if (!_articleTags.TryGetValue(articleId1, out var tags1))
        {
            return new HashSet<string>();
        }

        var result = new HashSet<string>(tags1);
        if (_articleTags.TryGetValue(articleId2, out var tags2))
        {
            result.ExceptWith(tags2);
        }
        return result;
    }
}