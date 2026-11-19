namespace IndependentWork11;

public static class DataGenerator
{
    private const int Seed = 42;
    private const int CustomerCount = 80;
    private const int SaleCount = 5000;

    private static readonly (string Category, int MinPrice, int MaxPrice, string[] Names)[] Catalog =
    {
        ("Electronics", 500, 30000, new[]
        {
            "Laptop", "Smartphone", "Tablet", "Headphones", "Monitor", "Keyboard",
            "Mouse", "Webcam", "Router", "Smartwatch", "Speaker", "Charger"
        }),
        ("Books", 100, 800, new[]
        {
            "Novel", "Cookbook", "Atlas", "Dictionary", "Biography", "Textbook",
            "Comics", "Poetry Collection", "Thriller", "Fantasy Saga", "History Guide", "Travel Journal"
        }),
        ("Home", 200, 6000, new[]
        {
            "Table Lamp", "Coffee Maker", "Blender", "Vacuum Cleaner", "Kettle", "Toaster",
            "Pillow", "Blanket", "Curtains", "Mirror", "Clock", "Carpet"
        }),
        ("Sports", 150, 5000, new[]
        {
            "Football", "Basketball", "Tennis Racket", "Yoga Mat", "Dumbbells", "Bicycle Helmet",
            "Running Shoes", "Jump Rope", "Backpack", "Water Bottle", "Skis", "Swim Goggles"
        }),
        ("Toys", 100, 3000, new[]
        {
            "Puzzle", "Lego Set", "Doll", "Toy Car", "Board Game", "Teddy Bear",
            "Robot Kit", "Play Dough", "Drone", "Train Set", "Building Blocks", "Kite"
        })
    };

    private static readonly string[] FirstNames =
    {
        "Oleksandr", "Maria", "Ivan", "Olena", "Andrii", "Natalia", "Petro", "Iryna", "Serhii", "Tetiana"
    };

    private static readonly string[] LastNames =
    {
        "Kovalenko", "Shevchenko", "Bondarenko", "Tkachenko", "Kravchenko", "Melnyk", "Boyko", "Moroz"
    };

    private static readonly string[] Cities =
    {
        "Rivne", "Lviv", "Kyiv", "Kharkiv", "Odesa", "Dnipro", "Lutsk", "Ternopil"
    };

    public static SalesDataset Generate()
    {
        var random = new Random(Seed);
        var products = GenerateProducts(random);
        var customers = GenerateCustomers(random);
        var sales = GenerateSales(random, products, customers);
        return new SalesDataset(products, customers, sales);
    }

    private static List<Product> GenerateProducts(Random random)
    {
        var products = new List<Product>();
        int id = 1;
        foreach (var (category, minPrice, maxPrice, names) in Catalog)
        {
            foreach (var name in names)
            {
                products.Add(new Product
                {
                    Id = id++,
                    Name = name,
                    Category = category,
                    UnitPrice = random.Next(minPrice, maxPrice + 1)
                });
            }
        }
        return products;
    }

    private static List<Customer> GenerateCustomers(Random random)
    {
        var customers = new List<Customer>();
        for (int i = 0; i < CustomerCount; i++)
        {
            customers.Add(new Customer
            {
                Id = i + 1,
                Name = $"{FirstNames[i % FirstNames.Length]} {LastNames[i / FirstNames.Length % LastNames.Length]}",
                City = Cities[random.Next(Cities.Length)]
            });
        }
        return customers;
    }

    private static List<Sale> GenerateSales(Random random, List<Product> products, List<Customer> customers)
    {
        var start = new DateTime(2024, 1, 1);
        var sales = new List<Sale>();
        for (int i = 0; i < SaleCount; i++)
        {
            sales.Add(new Sale
            {
                Id = i + 1,
                ProductId = products[random.Next(products.Count)].Id,
                CustomerId = customers[random.Next(customers.Count)].Id,
                Quantity = random.Next(1, 11),
                SaleDate = start.AddDays(random.Next(0, 730))
            });
        }
        return sales;
    }
}