namespace lab16v18.Models;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<City> Cities { get; set; } = new();
}