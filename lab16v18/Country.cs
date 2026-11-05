namespace lab16v18.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<Region> Regions { get; set; } = new();
}