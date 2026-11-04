using System;
using System.Collections.Generic;

namespace Lab13v18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var numbers = new List<int> { 12, 5, 30, 8, 17, 44 };
            var words = new List<string> { "pear", "orange", "plum", "grape", "mango" };
            var products = new List<Product>
            {
                new Product("Laptop", 25000, 3),
                new Product("Mouse", 500, 10),
                new Product("Monitor", 8000, 5),
                new Product("Cable", 150, 40)
            };

            Console.WriteLine("int");
            Show("Числа", numbers);
            Show("Map (x + 1)", FunctionalUtils.Map(numbers, x => x + 1));
            Show("Filter (> 10)", FunctionalUtils.Filter(numbers, x => x > 10));
            Console.WriteLine("Sum: " + FunctionalUtils.Sum(numbers, x => x));
            Console.WriteLine("Count (непарні): " + FunctionalUtils.Count(numbers, x => x % 2 != 0));
            Console.WriteLine("FirstOrDefault (> 20): " + FunctionalUtils.FirstOrDefault(numbers, x => x > 20));
            Console.WriteLine("FirstOrDefault (> 100): " + FunctionalUtils.FirstOrDefault(numbers, x => x > 100));

            Console.WriteLine();
            Console.WriteLine("string");
            Show("Слова", words);
            Show("Map (верхній регістр)", FunctionalUtils.Map(words, w => w.ToUpper()));
            Show("Filter (довжина 5)", FunctionalUtils.Filter(words, w => w.Length == 5));
            Console.WriteLine("Sum довжин: " + FunctionalUtils.Sum(words, w => w.Length));
            Console.WriteLine("Count (містить 'p'): " + FunctionalUtils.Count(words, w => w.Contains('p')));
            Console.WriteLine("FirstOrDefault (починається з 'm'): " + FunctionalUtils.FirstOrDefault(words, w => w.StartsWith("m")));
            string missing = FunctionalUtils.FirstOrDefault(words, w => w.StartsWith("z"));
            Console.WriteLine("FirstOrDefault (починається з 'z'): " + (missing ?? "(null)"));

            Console.WriteLine();
            Console.WriteLine("Product");
            Show("Товари", products);
            Show("Map (назви)", FunctionalUtils.Map(products, p => p.Name));
            Show("Filter (ціна < 1000)", FunctionalUtils.Filter(products, p => p.Price < 1000));
            Console.WriteLine("Sum кількості: " + FunctionalUtils.Sum(products, p => p.Quantity));
            Console.WriteLine("Sum вартості складу: " + FunctionalUtils.Sum(products, p => p.Price * p.Quantity));
            Console.WriteLine("Count (ціна > 1000): " + FunctionalUtils.Count(products, p => p.Price > 1000));
            Console.WriteLine("FirstOrDefault (кількість >= 40): " + FunctionalUtils.FirstOrDefault(products, p => p.Quantity >= 40));
            Product none = FunctionalUtils.FirstOrDefault(products, p => p.Price > 50000);
            Console.WriteLine("FirstOrDefault (ціна > 50000): " + (none == null ? "(null)" : none.Name));
        }

        static void Show<T>(string title, List<T> items)
        {
            Console.WriteLine(title + ": " + string.Join(", ", items));
        }
    }
}