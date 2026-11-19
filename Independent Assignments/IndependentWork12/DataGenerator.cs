namespace IndependentWork12;

public static class DataGenerator
{
    private const int MinValue = 100_000;
    private const int MaxValue = 1_000_000;

    public static List<int> Generate(int count, int seed = 42)
    {
        var random = new Random(seed);
        var data = new List<int>(count);
        for (int i = 0; i < count; i++)
        {
            data.Add(random.Next(MinValue, MaxValue + 1));
        }
        return data;
    }
}