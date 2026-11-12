using System;
using System.Threading;

namespace Lab17v18
{
    public static class RetryHelper
    {
        public static T ExecuteWithRetry<T>(
            Func<T> operation,
            int retryCount = 3,
            TimeSpan initialDelay = default,
            Func<Exception, bool> shouldRetry = null)
        {
            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }
            if (retryCount < 0)
            {
                throw new ArgumentOutOfRangeException("retryCount", "Кількість повторів не може бути від'ємною");
            }
            if (initialDelay == default(TimeSpan))
            {
                initialDelay = TimeSpan.FromMilliseconds(100);
            }

            int attempt = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("  Спроба " + (attempt + 1) + "...");
                    T result = operation();
                    Console.WriteLine("  Успіх з спроби " + (attempt + 1));
                    return result;
                }
                catch (Exception ex)
                {
                    bool canRetry = shouldRetry == null || shouldRetry(ex);

                    if (!canRetry)
                    {
                        Console.WriteLine("  Невдача: " + ex.GetType().Name + ": " + ex.Message);
                        Console.WriteLine("  Цей виняток не підлягає повтору, зупиняємось");
                        throw;
                    }

                    if (attempt >= retryCount)
                    {
                        Console.WriteLine("  Невдача: " + ex.GetType().Name + ": " + ex.Message);
                        Console.WriteLine("  Повтори вичерпано (" + retryCount + "), здаємось");
                        throw;
                    }

                    TimeSpan delay = TimeSpan.FromMilliseconds(
                        initialDelay.TotalMilliseconds * Math.Pow(2, attempt));

                    Console.WriteLine("  Невдача: " + ex.GetType().Name + ": " + ex.Message);
                    Console.WriteLine("  Чекаємо " + delay.TotalMilliseconds + " мс перед повтором");
                    Thread.Sleep(delay);
                    attempt++;
                }
            }
        }
    }
}