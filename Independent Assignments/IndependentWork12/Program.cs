using System.Globalization;
using System.Text;

namespace IndependentWork12;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        Console.WriteLine($"Логічних ядер процесора: {Environment.ProcessorCount}");
        Console.WriteLine();

        Benchmarks.Run();
        SideEffectsDemo.Run();
    }
}