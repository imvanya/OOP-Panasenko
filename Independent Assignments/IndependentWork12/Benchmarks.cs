using System.Diagnostics;

namespace IndependentWork12;

public record Operation(string Name, Func<int, bool> Filter, Func<int, double> Transform);

public static class Benchmarks
{
    private const int Repeats = 3;
    private const int WarmUpSize = 100_000;

    private static readonly int[] Sizes = { 1_000_000, 5_000_000, 10_000_000 };

    private static readonly Operation[] Operations =
    {
        new Operation("Просте число", IsPrime, x => Math.Sqrt(x)),
        new Operation("Математична формула", x => x % 2 == 1, HeavyMath),
        new Operation("Проста операція", x => x % 3 == 0, x => x * 2.0)
    };

    public static void Run()
    {
        Console.WriteLine("=== Порівняння LINQ та PLINQ ===");
        Console.WriteLine($"Кожне вимірювання - середнє за {Repeats} запуски (після прогріву).");
        Console.WriteLine();

        WarmUp();

        Console.WriteLine($"{"Розмір",-8}{"Операція",-24}{"LINQ, мс",12}{"PLINQ, мс",12}{"Прискорення",14}  Результати");
        Console.WriteLine(new string('-', 82));

        foreach (int size in Sizes)
        {
            var data = DataGenerator.Generate(size);
            foreach (var operation in Operations)
            {
                var (linqMs, plinqMs, match) = Compare(data, operation);
                string speedup = $"x{linqMs / plinqMs:F2}";
                Console.WriteLine(
                    $"{size / 1_000_000 + "M",-8}{operation.Name,-24}{linqMs,12:F1}{plinqMs,12:F1}{speedup,14}  {(match ? "збігаються" : "РІЗНІ")}");
            }
            Console.WriteLine();
        }
    }

    private static void WarmUp()
    {
        var data = DataGenerator.Generate(WarmUpSize);
        foreach (var operation in Operations)
        {
            Compare(data, operation);
        }
    }

    private static (double LinqMs, double PlinqMs, bool Match) Compare(List<int> data, Operation operation)
    {
        double linqSum = 0;
        double plinqSum = 0;
        double linqTotalMs = 0;
        double plinqTotalMs = 0;

        for (int i = 0; i < Repeats; i++)
        {
            var linqWatch = Stopwatch.StartNew();
            linqSum = data.Where(operation.Filter).Select(operation.Transform).Sum();
            linqWatch.Stop();
            linqTotalMs += linqWatch.Elapsed.TotalMilliseconds;

            var plinqWatch = Stopwatch.StartNew();
            plinqSum = data.AsParallel().Where(operation.Filter).Select(operation.Transform).Sum();
            plinqWatch.Stop();
            plinqTotalMs += plinqWatch.Elapsed.TotalMilliseconds;
        }

        bool match = Math.Abs(linqSum - plinqSum) <= 1e-6 * Math.Max(1.0, Math.Abs(linqSum));
        return (linqTotalMs / Repeats, plinqTotalMs / Repeats, match);
    }

    private static bool IsPrime(int n)
    {
        if (n < 2) return false;
        if (n % 2 == 0) return n == 2;
        for (int i = 3; (long)i * i <= n; i += 2)
        {
            if (n % i == 0) return false;
        }
        return true;
    }

    private static double HeavyMath(int x)
    {
        return Math.Sqrt(x) * Math.Abs(Math.Sin(x))
             + Math.Log(x) * Math.Pow(Math.Cos(x), 2)
             + Math.Atan(x);
    }
}