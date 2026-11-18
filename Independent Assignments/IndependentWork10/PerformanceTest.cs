using System.Diagnostics;

namespace InvertedIndexDemo;

public record PerfResult(
    int DocumentCount,
    double LinearMicros,   
    double IndexMicros,    
    double BuildMicros,    
    int LinearFound,
    int IndexFound)
{
    public bool ResultsMatch => LinearFound == IndexFound;
    public double Speedup => IndexMicros > 0 ? LinearMicros / IndexMicros : double.PositiveInfinity;

    public double BreakEvenQueries =>
        LinearMicros > IndexMicros ? BuildMicros / (LinearMicros - IndexMicros) : double.PositiveInfinity;
}

public static class PerformanceTest
{
    public static PerfResult CompareSearchMethods(List<Document> documents, string searchWord, int repetitions)
    {
        string normalizedWord = searchWord.ToLowerInvariant();

        List<Document> linearResults = LinearSearch(documents, normalizedWord);
        long start = Stopwatch.GetTimestamp();
        for (int i = 0; i < repetitions; i++)
        {
            linearResults = LinearSearch(documents, normalizedWord);
        }
        double linearMicros = ElapsedMicros(start) / repetitions;

        int buildRepeats = Math.Max(3, repetitions / 10);
        var index = new InvertedIndex();
        foreach (var doc in documents) index.AddDocument(doc); 

        start = Stopwatch.GetTimestamp();
        for (int i = 0; i < buildRepeats; i++)
        {
            index = new InvertedIndex();
            foreach (var doc in documents) index.AddDocument(doc);
        }
        double buildMicros = ElapsedMicros(start) / buildRepeats;

        List<Document> indexResults = index.SearchSingleWord(searchWord);
        start = Stopwatch.GetTimestamp();
        for (int i = 0; i < repetitions; i++)
        {
            indexResults = index.SearchSingleWord(searchWord);
        }
        double indexMicros = ElapsedMicros(start) / repetitions;

        return new PerfResult(documents.Count, linearMicros, indexMicros, buildMicros,
            linearResults.Count, indexResults.Count);
    }

    private static List<Document> LinearSearch(List<Document> documents, string normalizedWord)
    {
        return documents
            .Where(d => TextProcessor.Tokenize(d.Content).Contains(normalizedWord))
            .ToList();
    }

    private static double ElapsedMicros(long startTimestamp)
        => Stopwatch.GetElapsedTime(startTimestamp).TotalMilliseconds * 1000.0;
}