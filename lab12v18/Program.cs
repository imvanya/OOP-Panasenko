using System;
using System.Collections.Generic;

namespace Lab12v18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<int> numbers = new List<int> { 3, 8, 15, 4, 23, 42, 7 };
            List<string> words = new List<string> { "apple", "kiwi", "banana", "fig", "cherry" };
            List<Student> students = new List<Student>
            {
                new Student("Oleh", 19, 82.5),
                new Student("Maria", 20, 91),
                new Student("Andrii", 18, 67.5),
                new Student("Sofia", 19, 95),
                new Student("Ihor", 21, 74)
            };

            Console.WriteLine("int");
            Print("Вхідні числа", numbers);
            Print("Map (x * x)", FunctionalUtils.Map(numbers, x => x * x));
            Print("Filter (парні)", FunctionalUtils.Filter(numbers, x => x % 2 == 0));
            Console.WriteLine("Sum: " + FunctionalUtils.Sum(numbers, x => x));
            Console.WriteLine("Count (> 10): " + FunctionalUtils.Count(numbers, x => x > 10));
            Console.WriteLine("FirstOrDefault (> 20): " + FunctionalUtils.FirstOrDefault(numbers, x => x > 20));
            Console.WriteLine("FirstOrDefault (> 100): " + FunctionalUtils.FirstOrDefault(numbers, x => x > 100));

            Console.WriteLine();
            Console.WriteLine("string");
            Print("Вхідні слова", words);
            Print("Map (довжина)", FunctionalUtils.Map(words, w => w.Length));
            Print("Filter (довжина > 4)", FunctionalUtils.Filter(words, w => w.Length > 4));
            Console.WriteLine("Sum довжин: " + FunctionalUtils.Sum(words, w => w.Length));
            Console.WriteLine("Count (містить 'a'): " + FunctionalUtils.Count(words, w => w.Contains("a")));
            Console.WriteLine("FirstOrDefault (довжина > 5): " + FunctionalUtils.FirstOrDefault(words, w => w.Length > 5));
            string notFound = FunctionalUtils.FirstOrDefault(words, w => w.StartsWith("z"));
            Console.WriteLine("FirstOrDefault (починається з 'z'): " + (notFound == null ? "(null)" : notFound));

            Console.WriteLine();
            Console.WriteLine("Student");
            Print("Усі студенти", students);
            Print("Map (імена)", FunctionalUtils.Map(students, s => s.Name));
            Print("Filter (оцінка >= 80)", FunctionalUtils.Filter(students, s => s.Grade >= 80));
            Console.WriteLine("Sum віку: " + FunctionalUtils.Sum(students, s => s.Age));
            Console.WriteLine("Sum оцінок: " + FunctionalUtils.Sum(students, s => s.Grade));
            Console.WriteLine("Count (вік >= 20): " + FunctionalUtils.Count(students, s => s.Age >= 20));
            Student best = FunctionalUtils.FirstOrDefault(students, s => s.Grade > 90);
            Console.WriteLine("FirstOrDefault (оцінка > 90): " + best);
            Student none = FunctionalUtils.FirstOrDefault(students, s => s.Age > 30);
            Console.WriteLine("FirstOrDefault (вік > 30): " + (none == null ? "(null)" : none.ToString()));
        }

        static void Print<T>(string title, List<T> list)
        {
            Console.WriteLine(title + ": " + string.Join(", ", list));
        }
    }
}