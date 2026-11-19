using System.Text;

namespace IndependentWork13;

public static class Program
{
    public static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        DatabaseScenario.Run();
        QueueScenario.Run();
        await ReportScenario.RunAsync();

        Console.WriteLine("--- Кінець ---");
    }
}