namespace IndependentWork11;

public class Sale
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime SaleDate { get; set; }
    public int CustomerId { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal UnitPrice { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
}

public record SalesDataset(List<Product> Products, List<Customer> Customers, List<Sale> Sales);

public record ProductTotal(string Name, string Category, int Quantity);

public record CategoryTop(string Category, List<ProductTotal> Top);

public record CityRevenue(string City, decimal Revenue);

public record MonthlySales(int Year, int Month, int Units, decimal Revenue);

public record CustomerSummary(string Name, string City, decimal TotalSpent, string FavoriteCategory);