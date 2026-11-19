using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;

namespace IndependentWork13;

public class BrokerOverloadedException : Exception
{
    public BrokerOverloadedException(string message) : base(message)
    {
    }
}

public class FakeMessageBroker
{
    private readonly int _overloadedCalls;
    private int _calls;

    public FakeMessageBroker(int overloadedCalls)
    {
        _overloadedCalls = overloadedCalls;
    }

    public string Publish(string message)
    {
        _calls++;
        Logger.Log($"Брокер: публікація \"{message}\" (виклик {_calls})");
        Thread.Sleep(100);

        if (_calls <= _overloadedCalls)
        {
            throw new BrokerOverloadedException("черга перевантажена");
        }
        return "SENT";
    }
}

public static class QueueScenario
{
    private const string Buffered = "BUFFERED";

    public static void Run()
    {
        Console.WriteLine("=== Сценарій 2: черга повідомлень (Circuit Breaker + Fallback) ===");

        var pipeline = new ResiliencePipelineBuilder<string>()
            .AddFallback(new FallbackStrategyOptions<string>
            {
                ShouldHandle = new PredicateBuilder<string>()
                    .Handle<BrokerOverloadedException>()
                    .Handle<BrokenCircuitException>(),
                FallbackAction = _ => new ValueTask<Outcome<string>>(Outcome.FromResult(Buffered)),
                OnFallback = args =>
                {
                    Logger.Log(
                        $"Fallback: повідомлення буде збережено локально ({args.Outcome.Exception?.GetType().Name})");
                    return default;
                }
            })
            .AddCircuitBreaker(new CircuitBreakerStrategyOptions<string>
            {
                ShouldHandle = new PredicateBuilder<string>().Handle<BrokerOverloadedException>(),
                FailureRatio = 0.5,
                SamplingDuration = TimeSpan.FromSeconds(10),
                MinimumThroughput = 3,
                BreakDuration = TimeSpan.FromMilliseconds(2100),
                OnOpened = args =>
                {
                    Logger.Log($"Circuit OPEN: виклики блокуються на {args.BreakDuration.TotalSeconds:F1} с");
                    return default;
                },
                OnHalfOpened = args =>
                {
                    Logger.Log("Circuit HALF-OPEN: пробний виклик");
                    return default;
                },
                OnClosed = args =>
                {
                    Logger.Log("Circuit CLOSED: брокер відновився");
                    return default;
                }
            })
            .Build();

        var broker = new FakeMessageBroker(overloadedCalls: 3);
        var outbox = new List<string>();

        for (int i = 1; i <= 10; i++)
        {
            string message = $"order-{i}";
            string status = pipeline.Execute(() => broker.Publish(message));

            if (status == Buffered)
            {
                outbox.Add(message);
            }
            Logger.Log($"Повідомлення {message}: {status}");
            Thread.Sleep(600);
        }

        Logger.Log($"У локальному буфері (outbox): {outbox.Count} повідомлень: {string.Join(", ", outbox)}");
        Console.WriteLine();
    }
}