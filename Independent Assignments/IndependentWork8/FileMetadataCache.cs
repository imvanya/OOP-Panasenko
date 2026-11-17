namespace Lab8Self;

public class FileMetadata
{
    public string FileName { get; set; }
    public long Size { get; set; }
    public DateTime LastModified { get; set; }
    public string Hash { get; set; }
}

public class FileMetadataCache
{
    private readonly Dictionary<string, FileMetadata> _cache =
        new Dictionary<string, FileMetadata>(StringComparer.OrdinalIgnoreCase);

    public int Count => _cache.Count;

    public bool Add(FileMetadata metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata);
        if (string.IsNullOrWhiteSpace(metadata.FileName))
        {
            throw new ArgumentException("Ім'я файлу не може бути порожнім.", nameof(metadata));
        }
        return _cache.TryAdd(metadata.FileName, metadata);
    }

    public FileMetadata Get(string fileName)
    {
        if (fileName == null) return null;
        return _cache.TryGetValue(fileName, out var metadata) ? metadata : null;
    }

    public bool Remove(string fileName)
    {
        return fileName != null && _cache.Remove(fileName);
    }

    public bool Contains(string fileName)
    {
        return fileName != null && _cache.ContainsKey(fileName);
    }
}