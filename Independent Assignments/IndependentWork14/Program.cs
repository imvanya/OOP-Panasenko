using System.Text;

namespace IndependentWork14;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        RunSuccessAfterFailures();
        RunAlwaysFailing();
        RunNonRetryableException();

        Console.WriteLine("--- Кінець ---");
    }

    private static void RunSuccessAfterFailures()
    {
        Console.WriteLine("=== Сценарій 1: успіх після двох невдалих спроб ===");

        var policy = new RetryPolicy(maxRetries: 4, initialDelay: TimeSpan.FromMilliseconds(500), jitterFactor: 0.2);
        int calls = 0;

        string result = policy.Execute(
            () =>
            {
                calls++;
                if (calls <= 2)
                {
                    throw new HttpRequestException("сервіс тимчасово недоступний (503)");
                }
                return "дані замовлення #1042";
            },
            ex => ex is HttpRequestException);

        Console.WriteLine($"Результат: {result} (викликів операції: {calls})");
        Console.WriteLine();
    }

    private static void RunAlwaysFailing()
    {
        Console.WriteLine("=== Сценарій 2: операція завжди завершується невдачею ===");

        var policy = new RetryPolicy(maxRetries: 3, initialDelay: TimeSpan.FromMilliseconds(300));
        int calls = 0;

        try
        {
            policy.Execute<int>(() =>
            {
                calls++;
                throw new TimeoutException($"тайм-аут очікування відповіді (виклик {calls})");
            });
        }
        catch (TimeoutException ex)
        {
            Console.WriteLine($"Остаточна невдача після {calls} викликів: {ex.Message}");
        }
        Console.WriteLine();
    }

    private static void RunNonRetryableException()
    {
        Console.WriteLine("=== Сценарій 3: виняток, який не потрібно повторювати (shouldRetry) ===");

        var policy = new RetryPolicy(maxRetries: 3, initialDelay: TimeSpan.FromMilliseconds(300));
        int calls = 0;

        try
        {
            policy.Execute<int>(
                () =>
                {
                    calls++;
                    throw new ArgumentException("некоректний ідентифікатор замовлення");
                },
                ex => ex is TimeoutException || ex is HttpRequestException);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Виняток без повторів після {calls} виклику: {ex.Message}");
        }
        Console.WriteLine();
    }
}