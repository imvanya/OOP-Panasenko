
public class Bookstore
{
    // Приватні поля
    private string _name = "";
    private string _location = "";
    private int _booksAvailable;

    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Назва магазину не може бути порожньою.");
            }

            _name = value;
        }
    }

    public string Location
    {
        get { return _location; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Місце розташування не може бути порожнім.");
            }

            _location = value;
        }
    }

    // Властивість тільки для читання
    public int BooksAvailable
    {
        get { return _booksAvailable; }
    }

    public Bookstore(string name, string location, int initialBooksAvailable)
    {
        Name = name;
        Location = location;

        if (initialBooksAvailable < 0)
        {
            throw new ArgumentException(
                "Кількість книг не може бути від'ємною."
            );
        }

        _booksAvailable = initialBooksAvailable;
    }

    public Bookstore()
        : this("Невідомий магазин", "Невідоме місце", 0)
    {
    }

    // Метод продажу книги
    public void SellBook(string bookTitle)
    {
        if (string.IsNullOrWhiteSpace(bookTitle))
        {
            Console.WriteLine("Назва книги не може бути порожньою.");
            return;
        }

        if (_booksAvailable > 0)
        {
            _booksAvailable--;

            Console.WriteLine(
                $"Магазин \"{Name}\": книгу \"{bookTitle}\" продано."
            );

            Console.WriteLine(
                $"Залишилось книг: {_booksAvailable}"
            );
        }
        else
        {
            Console.WriteLine(
                $"Магазин \"{Name}\": книгу \"{bookTitle}\" продати неможливо."
            );

            Console.WriteLine("Книг немає в наявності.");
        }
    }

    ~Bookstore()
    {
        Console.WriteLine(
            $"Деструктор: об'єкт магазину \"{_name}\" знищено."
        );
    }
}