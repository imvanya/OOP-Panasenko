using Polly;
using Polly.Retry;

namespace IndependentWork13;

public class TransientDbException : Exception
{
    public TransientDbException(string message) : base(message)
    {
    }
}

public class PermanentDbException : Exception
{
    public PermanentDbException(string message) : base(message)
    {
    }
}

public class FakeDatabase
{
    private readonly int _failuresBeforeSuccess;
    private int _calls;

    public FakeDatabase(int failuresBeforeSuccess)
    {
        _failuresBeforeSuccess = failuresBeforeSuccess;
    }

    public string Query(string sql)
    {
        _calls++;
        Logger.Log($"БД: спроба {_calls}, запит \"{sql}\"");
        Thread.Sleep(200);

        if (_calls <= _failuresBeforeSuccess)
        {
            throw new TransientDbException("тайм-аут підключення до сервера БД");
        }
        return "3 рядки";
    }

    public string Insert(string sql)
    {
        _calls++;
        Logger.Log($"БД: спроба {_calls}, запит \"{sql}\"");
        Thread.Sleep(100);
        throw new PermanentDbException("порушено унікальне обмеження (duplicate key)");
    }
}

public static class DatabaseScenario
{
    public static void Run()
    {
        Console.WriteLine("=== Сценарій 1: доступ до БД (Retry з експоненційною затримкою) ===");

        var pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions
            {
                ShouldHandle = new PredicateBuilder().Handle<TransientDbException>(),
                MaxRetryAttempts = 3,
                Delay = TimeSpan.FromMilliseconds(300),
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = false,
                OnRetry = args =>
                {
                    Logger.Log(
                        $"Retry #{args.AttemptNumber + 1} через {args.RetryDelay.TotalMilliseconds:F0} мс. " +
                        $"Причина: {args.Outcome.Exception?.Message}");
                    return default;
                }
            })
            .Build();

        Console.WriteLine("--- Запуск 1: дві тимчасові помилки, потім успіх ---");
        var flakyDatabase = new FakeDatabase(failuresBeforeSuccess: 2);
        Execute(pipeline, () => flakyDatabase.Query("SELECT * FROM Orders"));

        Console.WriteLine("--- Запуск 2: постійна помилка (повторювати не потрібно) ---");
        var constraintDatabase = new FakeDatabase(failuresBeforeSuccess: 0);
        Execute(pipeline, () => constraintDatabase.Insert("INSERT INTO Users (Email) VALUES ('a@b.c')"));

        Console.WriteLine("--- Запуск 3: БД недоступна, усі спроби вичерпано ---");
        var downDatabase = new FakeDatabase(failuresBeforeSuccess: 100);
        Execute(pipeline, () => downDatabase.Query("SELECT * FROM Products"));

        Console.WriteLine();
    }

    private static void Execute(ResiliencePipeline pipeline, Func<string> operation)
    {
        try
        {
            string result = pipeline.Execute(operation);
            Logger.Log($"Результат: {result}");
        }
        catch (Exception ex)
        {
            Logger.Log($"Операція остаточно невдала ({ex.GetType().Name}): {ex.Message}");
        }
    }
}