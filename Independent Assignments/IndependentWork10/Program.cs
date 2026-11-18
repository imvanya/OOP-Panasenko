using System.Globalization;
using System.Text;

namespace InvertedIndexDemo;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        if (args.Length > 0 && args[0].Equals("bench", StringComparison.OrdinalIgnoreCase))
        {
            RunBenchmarks();
            return;
        }

        var index = new InvertedIndex();
        LoadDocuments(index);

        Console.WriteLine($"Loaded {index.DocumentCount} documents");
        Console.WriteLine($"Index contains {index.UniqueWords} unique words");
        Console.WriteLine("Tips: 'any:word1 word2' - OR search, 'bench' - performance test");
        Console.WriteLine();

        while (true)
        {
            Console.Write("Enter search query (or 'exit'): ");
            var query = Console.ReadLine();
            if (query == null || query.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

            if (query.Trim().Equals("bench", StringComparison.OrdinalIgnoreCase))
            {
                RunBenchmarks();
                continue;
            }

            bool anyMode = query.TrimStart().StartsWith("any:", StringComparison.OrdinalIgnoreCase);
            if (anyMode) query = query.TrimStart().Substring(4);

            var words = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0) continue;

            Console.WriteLine(anyMode ? "\n--- Search Results (OR) ---" : "\n--- Search Results (AND) ---");
            var results = anyMode ? index.SearchAnyWord(words) : index.SearchMultipleWords(words);

            foreach (var doc in results)
            {
                Console.WriteLine(doc);
                Console.WriteLine($"  {doc.Content.Substring(0, Math.Min(100, doc.Content.Length))}...");
            }
            Console.WriteLine($"\nFound {results.Count} documents\n");
        }
    }

    private static void LoadDocuments(InvertedIndex index)
    {
        foreach (var doc in TestData.CreateDocuments())
        {
            index.AddDocument(doc);
        }
    }

    private static void RunBenchmarks()
    {
        const string word = "async";
        int[] sizes = { 10, 100, 1000, 10000 };

        Console.WriteLine($"\nПошук слова \"{word}\". Час у мікросекундах (мкс), середнє за кілька повторів.\n");
        Console.WriteLine("| Документів | Лінійний, мкс | Індекс, мкс | Побудова індексу, мкс | Знайдено (лін./інд.) | Прискорення | Окупність, запитів |");
        Console.WriteLine("|---:|---:|---:|---:|:---:|---:|---:|");

        foreach (int size in sizes)
        {
            var documents = TestData.Generate(size);
            int repetitions = Math.Max(10, 100_000 / size);
            var r = PerformanceTest.CompareSearchMethods(documents, word, repetitions);

            string match = r.ResultsMatch ? "" : " ⚠ НЕ збігається";
            Console.WriteLine(
                $"| {r.DocumentCount} | {r.LinearMicros:F2} | {r.IndexMicros:F2} | {r.BuildMicros:F2} | " +
                $"{r.LinearFound} / {r.IndexFound}{match} | x{r.Speedup:F1} | {r.BreakEvenQueries:F1} |");
        }
        Console.WriteLine();
    }
}