using Polly;
using Polly.Retry;
using Polly.Timeout;

namespace IndependentWork13;

public static class ReportScenario
{
    private static int _attempts;

    private static async Task<string> GenerateReportAsync(CancellationToken cancellationToken)
    {
        int attempt = Interlocked.Increment(ref _attempts);
        Logger.Log($"Звіт: спроба {attempt} почалась");

        int durationMs = attempt <= 2 ? 3000 : 300;
        await Task.Delay(durationMs, cancellationToken);

        Logger.Log($"Звіт: спроба {attempt} завершилась за {durationMs} мс");
        return "sales-report.pdf";
    }

    public static async Task RunAsync()
    {
        Console.WriteLine("=== Сценарій 3: довга операція (Retry + Timeout) ===");

        var pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions
            {
                ShouldHandle = new PredicateBuilder().Handle<TimeoutRejectedException>(),
                MaxRetryAttempts = 3,
                Delay = TimeSpan.FromMilliseconds(500),
                BackoffType = DelayBackoffType.Constant,
                UseJitter = false,
                OnRetry = args =>
                {
                    Logger.Log(
                        $"Retry #{args.AttemptNumber + 1} через {args.RetryDelay.TotalMilliseconds:F0} мс " +
                        $"після {args.Outcome.Exception?.GetType().Name}");
                    return default;
                }
            })
            .AddTimeout(new TimeoutStrategyOptions
            {
                Timeout = TimeSpan.FromSeconds(1),
                OnTimeout = args =>
                {
                    Logger.Log($"Timeout: операцію скасовано після {args.Timeout.TotalSeconds:F1} с");
                    return default;
                }
            })
            .Build();

        try
        {
            string file = await pipeline.ExecuteAsync<string>(async token => await GenerateReportAsync(token));
            Logger.Log($"Результат: {file}");
        }
        catch (Exception ex)
        {
            Logger.Log($"Операція остаточно невдала ({ex.GetType().Name}): {ex.Message}");
        }

        Console.WriteLine();
    }
}