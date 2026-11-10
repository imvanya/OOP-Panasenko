using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lab14v18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Monitor monitor = new Monitor();

            Console.WriteLine("HashSet: унікальні сервіси");
            string[] names = { "auth-api", "payments", "mail", "auth-api", "search", "payments" };
            foreach (string name in names)
            {
                bool added = monitor.AddService(name);
                Console.WriteLine("  Add(\"" + name + "\"): " + (added ? "додано" : "дублікат, пропущено"));
            }
            monitor.PrintServices();
            Console.WriteLine("Contains(\"mail\"): " + monitor.IsMonitored("mail"));
            Console.WriteLine("Contains(\"cdn\"): " + monitor.IsMonitored("cdn"));
            Console.WriteLine("Remove(\"search\"): " + monitor.RemoveService("search"));
            monitor.PrintServices();

            Console.WriteLine();
            Console.WriteLine("Queue: черга перевірок (FIFO)");
            monitor.ScheduleCheck("payments");
            monitor.ScheduleCheck("auth-api");
            monitor.ScheduleCheck("mail");
            monitor.ScheduleCheck("cdn");       
            Console.WriteLine("Peek (наступний на перевірку): " + monitor.PeekNextCheck());
            Console.WriteLine("У черзі: " + monitor.QueueCount);

            Console.WriteLine("Обробка черги по порядку додавання:");
            while (monitor.QueueCount > 0)
            {
                monitor.ProcessNextCheck();
            }
            monitor.ProcessNextCheck();       

            Console.WriteLine();
            Console.WriteLine("Stack: історія станів (LIFO)");
            monitor.PrintHistory();
            Console.WriteLine("Peek (останній стан): " + monitor.PeekLastStatus());
            monitor.UndoLastStatus();
            monitor.UndoLastStatus();
            Console.WriteLine("Лишилось у історії: " + monitor.HistoryCount);
            monitor.PrintHistory();

            Console.WriteLine();
            Console.WriteLine("Пошук елемента: List проти HashSet");
            int size = 20000;
            List<int> list = new List<int>();
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < size; i++)
            {
                list.Add(i);
                set.Add(i);
            }

            Stopwatch watch = Stopwatch.StartNew();
            int found = 0;
            for (int i = 0; i < size; i++)
            {
                if (list.Contains(size - 1 - i)) found++;
            }
            watch.Stop();
            Console.WriteLine("List.Contains: " + watch.ElapsedMilliseconds + " мс (знайдено " + found + ")");

            watch.Restart();
            found = 0;
            for (int i = 0; i < size; i++)
            {
                if (set.Contains(size - 1 - i)) found++;
            }
            watch.Stop();
            Console.WriteLine("HashSet.Contains: " + watch.ElapsedMilliseconds + " мс (знайдено " + found + ")");
        }
    }
}