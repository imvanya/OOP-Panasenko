using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace IndependentWork11;

public static class Program
{
    private const int WarmRepeats = 50;

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        var data = DataGenerator.Generate();
        Console.WriteLine(
            $"Дані: продуктів {data.Products.Count}, клієнтів {data.Customers.Count}, продажів {data.Sales.Count}");
        Console.WriteLine();

        RunTopProducts(data);
        RunRevenueByCity(data);
        RunSalesByMonth(data);
        RunTopCustomers(data);
    }

    private static void RunTopProducts(SalesDataset data)
    {
        Console.WriteLine("Пайплайн 1: найпопулярніші продукти за категоріями");
        var (result, firstMs, averageMs) = Measure(
            () => SalesPipelines.TopProductsByCategory(data.Sales, data.Products));

        foreach (var category in result)
        {
            Console.WriteLine($"  {category.Category}");
            for (int i = 0; i < category.Top.Count; i++)
            {
                var item = category.Top[i];
                Console.WriteLine($"    {i + 1}. {item.Name} - {item.Quantity} шт.");
            }
        }
        PrintTime(firstMs, averageMs);
    }

    private static void RunRevenueByCity(SalesDataset data)
    {
        Console.WriteLine("Пайплайн 2: загальний дохід за містами клієнтів");
        var (result, firstMs, averageMs) = Measure(
            () => SalesPipelines.RevenueByCity(data.Sales, data.Products, data.Customers));

        foreach (var item in result)
        {
            Console.WriteLine($"  {item.City,-10} {item.Revenue,14:F2} грн");
        }
        PrintTime(firstMs, averageMs);
    }

    private static void RunSalesByMonth(SalesDataset data)
    {
        Console.WriteLine("Пайплайн 3: продажі за місяцями та роками");
        var (result, firstMs, averageMs) = Measure(
            () => SalesPipelines.SalesByMonth(data.Sales, data.Products));

        foreach (var item in result)
        {
            Console.WriteLine($"  {item.Year}-{item.Month:D2}  {item.Units,6} шт.  {item.Revenue,14:F2} грн");
        }
        PrintTime(firstMs, averageMs);
    }

    private static void RunTopCustomers(SalesDataset data)
    {
        Console.WriteLine("Пайплайн 4: топ-5 клієнтів за витратами та улюблена категорія");
        var (result, firstMs, averageMs) = Measure(
            () => SalesPipelines.TopCustomersWithFavoriteCategory(data.Sales, data.Products, data.Customers));

        for (int i = 0; i < result.Count; i++)
        {
            var item = result[i];
            Console.WriteLine(
                $"  {i + 1}. {item.Name} ({item.City}) - {item.TotalSpent:F2} грн, улюблена категорія: {item.FavoriteCategory}");
        }
        PrintTime(firstMs, averageMs);
    }

    private static (T Result, double FirstMs, double AverageMs) Measure<T>(Func<T> pipeline)
    {
        var firstRun = Stopwatch.StartNew();
        T result = pipeline();
        firstRun.Stop();

        var warmRuns = Stopwatch.StartNew();
        for (int i = 0; i < WarmRepeats; i++)
        {
            pipeline();
        }
        warmRuns.Stop();

        return (result, firstRun.Elapsed.TotalMilliseconds, warmRuns.Elapsed.TotalMilliseconds / WarmRepeats);
    }

    private static void PrintTime(double firstMs, double averageMs)
    {
        Console.WriteLine(
            $"  Час: перший запуск {firstMs:F3} мс, середній за {WarmRepeats} повторних запусків {averageMs:F3} мс");
        Console.WriteLine();
    }
}