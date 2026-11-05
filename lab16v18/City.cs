namespace lab16v18.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Population { get; set; }

    public override string ToString() => $"{Name} ({Population:N0})";
}