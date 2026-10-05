public class Program
{
    public static void Main(string[] args)
    {
        Recipe borscht = new Recipe("Борщ", 90, 6);
        borscht.PrintInfo();
        Console.WriteLine($"Швидкий рецепт? {(borscht.IsQuick() ? "Так" : "Ні")}");
        double beetroot = borscht.ScaleIngredient(300, 2);
        Console.WriteLine($"Буряка для 2 порцій: {beetroot:F0} г");
        Console.WriteLine();

        Playlist rock = new Playlist("Рок-класика");
        rock.AddTrack(215);
        rock.AddTrack(180);
        rock.AddTrack(305);
        bool added = rock.AddTrack(-10); 
        Console.WriteLine($"Некоректний трек додано? {(added ? "Так" : "Ні")}");
        rock.PrintInfo();
        Console.WriteLine();

        Employee developer = new Employee("Іваненко Іван Іванович", 30000, 5);
        developer.PrintInfo();
        developer.FullName = "Іваненко Іван Петрович"; 
        Console.WriteLine($"Нове ім'я: {developer.FullName}");
    }
}

public class Recipe
{
    private string _name;
    private int _cookingTimeMinutes;
    private int _servings;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int CookingTimeMinutes
    {
        get { return _cookingTimeMinutes; }
    }

    public int Servings
    {
        get { return _servings; }
    }

    public Recipe(string name, int cookingTimeMinutes, int servings)
    {
        _name = name;
        _cookingTimeMinutes = cookingTimeMinutes;
        _servings = servings;
    }

    public bool IsQuick()
    {
        return _cookingTimeMinutes <= 30;
    }

    public double ScaleIngredient(double gramsForCurrentServings, int newServings)
    {
        return gramsForCurrentServings / _servings * newServings;
    }

    public void PrintInfo()
    {
        string speed = IsQuick() ? "швидкий" : "довгий";
        Console.WriteLine($"Рецепт: {_name}, час: {_cookingTimeMinutes} хв ({speed}), порцій: {_servings}");
    }
}

public class Playlist
{
    private string _title;
    private List<int> _trackDurations; 

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public int TrackCount
    {
        get { return _trackDurations.Count; }
    }

    public Playlist(string title)
    {
        _title = title;
        _trackDurations = new List<int>();
    }

    public bool AddTrack(int durationSeconds)
    {
        if (durationSeconds <= 0)
        {
            return false;
        }

        _trackDurations.Add(durationSeconds);
        return true;
    }

    public double GetTotalMinutes()
    {
        return _trackDurations.Sum() / 60.0;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Плейлист: {_title}, треків: {TrackCount}, тривалість: {GetTotalMinutes():F1} хв");
    }
}

public class Employee
{
    private string _fullName;
    private double _baseSalary;
    private int _experienceYears;

    public string FullName
    {
        get { return _fullName; }
        set { _fullName = value; }
    }

    public int ExperienceYears
    {
        get { return _experienceYears; }
    }

    public Employee(string fullName, double baseSalary, int experienceYears)
    {
        _fullName = fullName;
        _baseSalary = baseSalary;
        _experienceYears = experienceYears;
    }

    public double CalculateSalary()
    {
        double bonusPercent = Math.Min(_experienceYears * 0.02, 0.30);
        return _baseSalary * (1 + bonusPercent);
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Працівник: {_fullName}, стаж: {_experienceYears} р., зарплата: {CalculateSalary():F2} грн");
    }
}