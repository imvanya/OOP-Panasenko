using System;
using System.Text;

namespace OOPLab1
{
    public class Bookstore
    {
        private string name;
        private string location;
        private int booksAvailable;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Location
        {
            get => location;
            set => location = value;
        }

        public int BooksAvailable
        {
            get => booksAvailable;
            set
            {
                if (value >= 0)
                {
                    booksAvailable = value;
                }
                else
                {
                    Console.WriteLine("Помилка: кількість книг не може бути від'ємною!");
                }
            }
        }

        public Bookstore(string name, string location, int booksAvailable)
        {
            this.name = name;
            this.location = location;
            BooksAvailable = booksAvailable; 
        }


        public void SellBook()
        {
            if (booksAvailable > 0)
            {
                booksAvailable--;
                Console.WriteLine($"[ПРОДАЖ] У книгарні \"{name}\" ({location}) продано 1 книгу. Залишок: {booksAvailable}");
            }
            else
            {
                Console.WriteLine($"[ПОМИЛКА] У книгарні \"{name}\" ({location}) немає книг у наявності для продажу!");
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine("  Лабораторна робота №1. Варіант 18: Клас Bookstore");
            Console.WriteLine("==================================================\n");

            Bookstore store1 = new Bookstore("Є Книгарня", "вул. Соборна, 15", 2);
            Bookstore store2 = new Bookstore("Книжковий Лев", "пр. Свободи, 7", 1);
            Bookstore store3 = new Bookstore("Yakaboo Store", "вул. Хрещатик, 22", 0);

            Console.WriteLine($"--- Тестування книгарні \"{store1.Name}\" ---");
            Console.WriteLine($"Початковий залишок через властивість: {store1.BooksAvailable}");
            store1.SellBook(); 
            store1.SellBook(); 
            store1.SellBook(); 
            Console.WriteLine();

            Console.WriteLine($"--- Тестування книгарні \"{store2.Name}\" ---");
            Console.WriteLine($"Початковий залишок через властивість: {store2.BooksAvailable}");
            store2.SellBook(); 
            store2.SellBook(); 
            Console.WriteLine();

            Console.WriteLine($"--- Тестування книгарні \"{store3.Name}\" ---");
            Console.WriteLine($"Початковий залишок через властивість: {store3.BooksAvailable}");
            store3.SellBook(); 
            Console.WriteLine();

            Console.WriteLine("==================================================");
            Console.WriteLine("Програму успішно виконано.");
        }
    }
}