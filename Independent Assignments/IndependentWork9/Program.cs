using System.Globalization;
using System.Text;
using Lab9Self;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

var numbers = new List<int> { 5, 12, 8, 21, 3, 16, 7 };

Console.WriteLine("List<int>");
Console.WriteLine("Вихідні числа: " + string.Join(", ", numbers));

var squares = CollectionUtils.Map(numbers, n => n * n);
Console.WriteLine("Квадрати: " + string.Join(", ", squares));

var maxNumber = CollectionUtils.MaxBy(numbers, n => n);
Console.WriteLine($"Максимальне число: {maxNumber}");
var maxLastDigit = CollectionUtils.MaxBy(numbers, n => n % 10);
Console.WriteLine($"Число з найбільшою останньою цифрою: {maxLastDigit}");

var evens = CollectionUtils.Filter(numbers, n => n % 2 == 0);
Console.WriteLine("Парні числа: " + string.Join(", ", evens));

Console.Write("Подвоєні значення: ");
CollectionUtils.ForEach(numbers, n => Console.Write(n * 2 + " "));
Console.WriteLine("\n");

var words = new List<string> { "apple", "banana", "kiwi", "cherry", "fig" };

Console.WriteLine("List<string>");
Console.WriteLine("Вихідні слова: " + string.Join(", ", words));

var upper = CollectionUtils.Map(words, w => w.ToUpper());
Console.WriteLine("Верхній регістр: " + string.Join(", ", upper));
var lengths = CollectionUtils.Map(words, w => w.Length);
Console.WriteLine("Довжини слів: " + string.Join(", ", lengths));

var longest = CollectionUtils.MaxBy(words, w => w.Length);
Console.WriteLine($"Найдовше слово: {longest}");

var longWords = CollectionUtils.Filter(words, w => w.Length > 4);
Console.WriteLine("Слова довші за 4 символи: " + string.Join(", ", longWords));

CollectionUtils.ForEach(words, w => Console.WriteLine($"  {w} ({w.Length})"));
Console.WriteLine();

var products = new List<Product>
{
    new Product { Name = "Laptop",   Price = 25000 },
    new Product { Name = "Mouse",    Price = 500 },
    new Product { Name = "Keyboard", Price = 1500 },
    new Product { Name = "Monitor",  Price = 8000 },
    new Product { Name = "Headset",  Price = 2200 }
};

Console.WriteLine("List<Product>");

var names = CollectionUtils.Map(products, p => p.Name);
Console.WriteLine("Names: " + string.Join(", ", names));
var discounted = CollectionUtils.Map(products, p => $"{p.Name}: {p.Price * 0.9m:F2}");
Console.WriteLine("Ціни зі знижкою 10%: " + string.Join("; ", discounted));

var mostExpensive = CollectionUtils.MaxBy(products, p => p.Price);
Console.WriteLine($"Most expensive: {mostExpensive.Name} - {mostExpensive.Price}");
var cheapest = CollectionUtils.MaxBy(products, p => -p.Price); // максимум від'ємної ціни = мінімальна ціна
Console.WriteLine($"Cheapest: {cheapest.Name} - {cheapest.Price}");

var expensive = CollectionUtils.Filter(products, p => p.Price > 1000);
Console.WriteLine("Expensive products:");
CollectionUtils.ForEach(expensive, p => Console.WriteLine($" - {p.Name}"));
var affordable = CollectionUtils.Filter(products, p => p.Price < 2000);
Console.WriteLine("Дешевші за 2000: " + string.Join(", ", CollectionUtils.Map(affordable, p => p.Name)));

Console.WriteLine("Усі товари:");
CollectionUtils.ForEach(products, p => Console.WriteLine($"{p.Name}: {p.Price} грн"));

Console.WriteLine();
Console.WriteLine("Комбінування (ланцюжок)");
var chain = CollectionUtils.Map(
    CollectionUtils.Filter(products, p => p.Price > 1000),
    p => p.Name.ToUpper());
Console.WriteLine("Назви товарів дорожчих за 1000: " + string.Join(", ", chain));