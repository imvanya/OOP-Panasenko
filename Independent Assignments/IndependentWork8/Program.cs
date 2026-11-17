using System.Globalization;
using System.Text;
using Lab8Self;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

static string FormatSet<T>(HashSet<T> set) =>
    set.Count == 0 ? "(нічого не знайдено)" : "{" + string.Join(", ", set.OrderBy(x => x)) + "}";

Console.WriteLine("Сценарій 1: FileMetadataCache");
var cache = new FileMetadataCache();
var modified = new DateTime(2026, 11, 10, 14, 30, 0);

cache.Add(new FileMetadata { FileName = "report.pdf", Size = 1048576, LastModified = modified, Hash = "a3f5c9" });
cache.Add(new FileMetadata { FileName = "photo.jpg", Size = 2500000, LastModified = modified, Hash = "7be120" });
cache.Add(new FileMetadata { FileName = "notes.txt", Size = 1024, LastModified = modified, Hash = "c91d44" });
Console.WriteLine($"Додано 3 файли. У кеші: {cache.Count}");

bool added = cache.Add(new FileMetadata { FileName = "report.pdf", Size = 1, LastModified = modified, Hash = "00" });
Console.WriteLine($"Повторне додавання report.pdf: {added}");

var found = cache.Get("REPORT.PDF"); 
Console.WriteLine($"Get(\"REPORT.PDF\"): {found.FileName}, {found.Size} байт, " +
                  $"{found.LastModified:yyyy-MM-dd HH:mm}, hash={found.Hash}");
Console.WriteLine($"Contains(\"photo.jpg\"): {cache.Contains("photo.jpg")}");
Console.WriteLine($"Remove(\"notes.txt\"): {cache.Remove("notes.txt")}");
Console.WriteLine($"Contains(\"notes.txt\"): {cache.Contains("notes.txt")}");
Console.WriteLine($"Get(\"missing.bin\"): {(cache.Get("missing.bin") == null ? "не знайдено" : "знайдено")}");
Console.WriteLine($"У кеші: {cache.Count}");
Console.WriteLine();

Console.WriteLine("Сценарій 2: TagManager");
var tags = new TagManager();
tags.AddTag("article1", "csharp");
tags.AddTag("article1", "dotnet");
tags.AddTag("article1", "collections");
tags.AddTag("article2", "dotnet");
tags.AddTag("article2", "LINQ");
tags.AddTag("article2", "Collections"); 
tags.AddTag("article3", "python");
tags.AddTag("article3", "CSharp");
tags.AddTag("article3", "csharp");      

Console.WriteLine($"Унікальні теги: {FormatSet(tags.GetAllUniqueTags())}");
Console.WriteLine($"Спільні теги article1 і article2: {FormatSet(tags.FindCommonTags("article1", "article2"))}");
Console.WriteLine($"Спільні теги article1 і article3: {FormatSet(tags.FindCommonTags("article1", "article3"))}");
Console.WriteLine($"Теги тільки в article1 (відносно article2): {FormatSet(tags.FindUniqueTags("article1", "article2"))}");

tags.RemoveTag("article1", "dotnet");
tags.RemoveTag("article3", "CSharp");
tags.RemoveTag("article3", "python"); 
Console.WriteLine($"Унікальні теги після видалень: {FormatSet(tags.GetAllUniqueTags())}");
Console.WriteLine($"Статей зі тегами: {tags.ArticleCount}");
Console.WriteLine();

Console.WriteLine("Сценарій 3: SearchIndex");
var index = new SearchIndex();
index.AddDocument(1, "The quick brown fox jumps over the lazy dog");
index.AddDocument(2, "Dictionary and HashSet are fast collections in dotnet");
index.AddDocument(3, "The fox likes fast dotnet collections");
index.AddDocument(4, "Lazy dog sleeps");
Console.WriteLine($"Унікальних слів в індексі: {index.WordCount}");

Console.WriteLine($"Search(\"fox\"): {FormatSet(index.Search("fox"))}");
Console.WriteLine($"Search(\"DOTNET\"): {FormatSet(index.Search("DOTNET"))}");
Console.WriteLine($"Search(\"cat\"): {FormatSet(index.Search("cat"))}");
Console.WriteLine($"SearchMultiple(\"fox\", \"dotnet\"): {FormatSet(index.SearchMultiple("fox", "dotnet"))}");
Console.WriteLine($"SearchMultiple(\"the\", \"lazy\", \"dog\"): {FormatSet(index.SearchMultiple("the", "lazy", "dog"))}");
Console.WriteLine($"SearchMultiple(\"fast\", \"collections\"): {FormatSet(index.SearchMultiple("fast", "collections"))}");
Console.WriteLine($"SearchMultiple(\"fox\", \"cat\"): {FormatSet(index.SearchMultiple("fox", "cat"))}");