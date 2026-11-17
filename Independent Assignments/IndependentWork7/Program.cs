using System;

namespace IndependentWork7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Cache<string, string> cache = new Cache<string, string>(3);

            Console.WriteLine("Додаємо 3 елементи (ємність 3)");
            cache.Add("user1", "Oleh");
            cache.Add("user2", "Maria");
            cache.Add("user3", "Ihor");
            Console.WriteLine("Count: " + cache.Count);
            Console.WriteLine("Get(user2): " + cache.Get("user2"));

            Console.WriteLine();
            Console.WriteLine("Додаємо 4-й елемент: кеш повний");
            cache.Add("user4", "Sofia");
            Console.WriteLine("Count: " + cache.Count);
            Console.WriteLine("Contains(user1): " + cache.ContainsKey("user1") + "  (найстарішого видалено)");
            Console.WriteLine("Contains(user2): " + cache.ContainsKey("user2"));
            Console.WriteLine("Contains(user4): " + cache.ContainsKey("user4"));

            Console.WriteLine();
            Console.WriteLine("Оновлення існуючого ключа");
            cache.Add("user2", "Maria K.");
            Console.WriteLine("Get(user2): " + cache.Get("user2") + ", Count: " + cache.Count);

            Console.WriteLine();
            Console.WriteLine("Неіснуючий ключ");
            try
            {
                cache.Get("user1");
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                Console.WriteLine("Виняток: " + ex.Message);
            }
        }
    }
}