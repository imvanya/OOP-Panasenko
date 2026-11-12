using System;
using System.IO;
using System.Net.Http;

namespace Lab17v18
{
    class Program
    {
        static bool IsTransient(Exception ex)
        {
            return ex is IOException || ex is HttpRequestException;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            TimeSpan delay = TimeSpan.FromMilliseconds(200);

            Console.WriteLine("1. SaveCache: 4 невдачі, потім успіх");
            FileProcessor files = new FileProcessor();
            RetryHelper.ExecuteWithRetry(() =>
            {
                files.SaveCache("cache.dat", "key1=value1;key2=value2");
                return true;   
            }, 5, delay, IsTransient);

            Console.WriteLine();
            Console.WriteLine("2. UploadCache: 2 невдачі, потім успіх");
            NetworkClient network = new NetworkClient();
            bool uploaded = RetryHelper.ExecuteWithRetry(
                () => network.UploadCache("https://example.com/cache", "key1=value1"),
                3, delay, IsTransient);
            Console.WriteLine("  Результат UploadCache: " + uploaded);

            Console.WriteLine();
            Console.WriteLine("3. Повторів замало (retryCount = 2, а невдач 4)");
            files.Reset();
            try
            {
                RetryHelper.ExecuteWithRetry(() =>
                {
                    files.SaveCache("cache.dat", "data");
                    return true;
                }, 2, delay, IsTransient);
            }
            catch (IOException ex)
            {
                Console.WriteLine("  Піймано у Main: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("4. shouldRetry: ArgumentException повторювати не треба");
            try
            {
                RetryHelper.ExecuteWithRetry(() =>
                {
                    files.SaveCache("", "data");  
                    return true;
                }, 5, delay, IsTransient);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  Піймано у Main: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("5. Без shouldRetry повторюється будь-який виняток");
            try
            {
                RetryHelper.ExecuteWithRetry(() =>
                {
                    files.SaveCache("", "data");
                    return true;
                }, 2, delay);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  Піймано у Main: " + ex.Message);
            }
        }
    }
}