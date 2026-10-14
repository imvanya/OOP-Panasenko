using System;
using System.Collections.Generic;

namespace Lab11v18
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CacheManager> caches = new List<CacheManager>();
            caches.Add(new MemoryCache());
            caches.Add(new RedisCache());
            caches.Add(new FileCache());

            foreach (CacheManager cache in caches)
            {
                cache.Cache("Profile 42", "Iван Петренко");
            }

            Console.WriteLine("Повторний виклик");
            caches[0].Cache("Settings", "dark");

            Console.WriteLine("Пiдсумок");
            foreach (CacheManager cache in caches)
            {
                Console.WriteLine(cache.GetType().Name + ": ключ " + cache.Key
                    + ", операцiй " + cache.OperationsCount);
            }

            MemoryCache memory = (MemoryCache)caches[0];
            Console.WriteLine("Записiв у MemoryCache: " + memory.StoredCount);
        }
    }
}