using System.Collections.Concurrent;
using System.Diagnostics;

namespace IndependentWork12;

public static class SideEffectsDemo
{
    private const int DemoSize = 2_000_000;
    private const int Runs = 5;

    public static void Run()
    {
        Console.WriteLine("=== Побічні ефекти в PLINQ ===");
        var data = DataGenerator.Generate(DemoSize);
        int expected = data.Count(x => x % 2 == 0);

        RunCounterScenario(data, expected);
        RunListScenario(data, expected);
    }

    private static void RunCounterScenario(List<int> data, int expected)
    {
        Console.WriteLine();
        Console.WriteLine($"Сценарій 1: спільний лічильник парних чисел. Очікується: {expected}");

        for (int run = 1; run <= Runs; run++)
        {
            int actual = UnsafeCounter(data);
            Console.WriteLine(
                $"  Без синхронізації, запуск {run}: {actual} (втрачено {expected - actual}) - {Verdict(actual, expected)}");
        }

        PrintFixed("lock", () => LockCounter(data), expected);
        PrintFixed("Interlocked.Increment", () => InterlockedCounter(data), expected);
        PrintFixed("Count() без спільного стану", () => PureCounter(data), expected);
    }

    private static void RunListScenario(List<int> data, int expected)
    {
        Console.WriteLine();
        Console.WriteLine($"Сценарій 2: спільний List<int> (збір парних чисел). Очікується: {expected}");

        for (int run = 1; run <= Runs; run++)
        {
            Console.WriteLine($"  List.Add без синхронізації, запуск {run}: {UnsafeList(data, expected)}");
        }

        PrintFixed("lock навколо List.Add", () => LockedList(data), expected);
        PrintFixed("ConcurrentBag<int>", () => BagList(data), expected);
        PrintFixed("ToList() без спільного стану", () => PureList(data), expected);
    }

    private static int UnsafeCounter(List<int> data)
    {
        int counter = 0;
        data.AsParallel().Where(x => x % 2 == 0).ForAll(_ => counter++);
        return counter;
    }

    private static int LockCounter(List<int> data)
    {
        int counter = 0;
        var sync = new object();
        data.AsParallel().Where(x => x % 2 == 0).ForAll(_ =>
        {
            lock (sync)
            {
                counter++;
            }
        });
        return counter;
    }

    private static int InterlockedCounter(List<int> data)
    {
        int counter = 0;
        data.AsParallel().Where(x => x % 2 == 0).ForAll(_ => Interlocked.Increment(ref counter));
        return counter;
    }

    private static int PureCounter(List<int> data)
    {
        return data.AsParallel().Count(x => x % 2 == 0);
    }

    private static string UnsafeList(List<int> data, int expected)
    {
        var list = new List<int>();
        try
        {
            data.AsParallel().Where(x => x % 2 == 0).ForAll(x => list.Add(x));
            return $"{list.Count} елементів (втрачено {expected - list.Count}) - {Verdict(list.Count, expected)}";
        }
        catch (AggregateException ex)
        {
            return $"виняток {ex.InnerExceptions[0].GetType().Name}";
        }
    }

    private static int LockedList(List<int> data)
    {
        var list = new List<int>();
        var sync = new object();
        data.AsParallel().Where(x => x % 2 == 0).ForAll(x =>
        {
            lock (sync)
            {
                list.Add(x);
            }
        });
        return list.Count;
    }

    private static int BagList(List<int> data)
    {
        var bag = new ConcurrentBag<int>();
        data.AsParallel().Where(x => x % 2 == 0).ForAll(bag.Add);
        return bag.Count;
    }

    private static int PureList(List<int> data)
    {
        return data.AsParallel().Where(x => x % 2 == 0).ToList().Count;
    }

    private static void PrintFixed(string name, Func<int> action, int expected)
    {
        var watch = Stopwatch.StartNew();
        int actual = action();
        watch.Stop();
        Console.WriteLine(
            $"  Виправлення: {name,-30} {actual} за {watch.Elapsed.TotalMilliseconds:F1} мс - {Verdict(actual, expected)}");
    }

    private static string Verdict(int actual, int expected)
    {
        return actual == expected ? "коректно" : "НЕКОРЕКТНО";
    }
}