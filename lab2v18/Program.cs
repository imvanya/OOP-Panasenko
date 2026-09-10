
Console.WriteLine("=== Лабораторна робота №2 ===");
Console.WriteLine("Варіант 18: Клас Bookstore");
Console.WriteLine();

Console.WriteLine("Creating objects");

{
    Bookstore bookstore1 = new Bookstore(
        "Книжковий світ",
        "Рівне",
        5
    );

    Bookstore bookstore2 = new Bookstore();

    Bookstore bookstore3 = new Bookstore(
        "Буква",
        "Львів",
        2
    );

    Console.WriteLine("Objects created");
    Console.WriteLine();

    Console.WriteLine("=== Інформація про магазини ===");

    Console.WriteLine(
        $"Магазин: {bookstore1.Name}, " +
        $"Місце: {bookstore1.Location}, " +
        $"Книг: {bookstore1.BooksAvailable}"
    );

    Console.WriteLine(
        $"Магазин: {bookstore2.Name}, " +
        $"Місце: {bookstore2.Location}, " +
        $"Книг: {bookstore2.BooksAvailable}"
    );

    Console.WriteLine(
        $"Магазин: {bookstore3.Name}, " +
        $"Місце: {bookstore3.Location}, " +
        $"Книг: {bookstore3.BooksAvailable}"
    );

    Console.WriteLine();

    Console.WriteLine("=== Продаж книг ===");

    bookstore1.SellBook("Кобзар");

    Console.WriteLine();

    bookstore1.SellBook("Тигролови");

    Console.WriteLine();

    bookstore3.SellBook("Місто");

    Console.WriteLine();

    bookstore2.SellBook("Гаррі Поттер");
}

Console.WriteLine();
Console.WriteLine("End of Main, preparing for GC");

GC.Collect();
GC.WaitForPendingFinalizers();

Console.WriteLine("GC completed.");