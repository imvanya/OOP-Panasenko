namespace IndependentWork14;

public class RetryPolicy
{
    private readonly int _maxRetries;
    private readonly TimeSpan _initialDelay;
    private readonly double _jitterFactor;

    public RetryPolicy(int maxRetries, TimeSpan initialDelay, double jitterFactor = 0.2)
    {
        if (maxRetries < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxRetries), "Кількість повторів не може бути від'ємною.");
        }
        if (initialDelay < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(initialDelay), "Затримка не може бути від'ємною.");
        }
        if (jitterFactor < 0 || jitterFactor > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(jitterFactor), "Коефіцієнт jitter має бути в діапазоні [0; 1].");
        }

        _maxRetries = maxRetries;
        _initialDelay = initialDelay;
        _jitterFactor = jitterFactor;
    }

    public T Execute<T>(Func<T> operation, Func<Exception, bool> shouldRetry = null)
    {
        ArgumentNullException.ThrowIfNull(operation);

        int retryAttempt = 0;
        while (true)
        {
            Log($"Спроба {retryAttempt + 1} з {_maxRetries + 1}: виконання операції");
            try
            {
                T result = operation();
                Log($"Спроба {retryAttempt + 1} успішна");
                return result;
            }
            catch (Exception ex)
            {
                Log($"Спроба {retryAttempt + 1} невдала: {ex.GetType().Name}: {ex.Message}");

                if (shouldRetry != null && !shouldRetry(ex))
                {
                    Log("Виняток не підлягає повтору (shouldRetry повернув false), виняток передається далі");
                    throw;
                }
                if (retryAttempt >= _maxRetries)
                {
                    Log($"Вичерпано всі {_maxRetries} повторів, виняток передається далі");
                    throw;
                }

                TimeSpan delay = CalculateDelay(retryAttempt);
                double baseMs = _initialDelay.TotalMilliseconds * Math.Pow(2, retryAttempt);
                Log($"Повтор #{retryAttempt + 1} через {delay.TotalMilliseconds:F0} мс " +
                    $"(базова затримка {baseMs:F0} мс, jitter ±{_jitterFactor * 100:F0}%)");

                Thread.Sleep(delay);
                retryAttempt++;
            }
        }
    }

    public TimeSpan CalculateDelay(int retryAttempt)
    {
        double jitter = 1 + Random.Shared.NextDouble() * _jitterFactor * 2 - _jitterFactor;
        double delayMs = _initialDelay.TotalMilliseconds * Math.Pow(2, retryAttempt) * jitter;
        return TimeSpan.FromMilliseconds(delayMs);
    }

    private static void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] {message}");
    }
}