using lab16v18.Data;
using lab16v18.Extensions;
using lab16v18.Models;

namespace lab16v18;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        List<Country> countries = TestData.Create();

        ShowAllCities(countries);
        ShowRegionPages(countries, pageSize: 2);
        ShowRegionPopulations(countries);
    }

    private static void ShowAllCities(List<Country> countries)
    {
        Console.WriteLine("1. SelectMany: усі міста");
        List<City> allCities = countries
            .SelectMany(country => country.Regions)   
            .SelectMany(region => region.Cities)      
            .ToList();

        foreach (City city in allCities)
            Console.WriteLine($"  {city.Id,2}. {city}");
        Console.WriteLine($"Усього міст: {allCities.Count}");
    }

    private static void ShowRegionPages(List<Country> countries, int pageSize)
    {
        Console.WriteLine("\n2. Пагінація: регіони по 2 на сторінку");
        List<Region> allRegions = countries.SelectMany(c => c.Regions).ToList();
        int totalPages = (int)Math.Ceiling(allRegions.Count / (double)pageSize);

        for (int page = 1; page <= totalPages; page++)
        {
            Console.WriteLine($"--- Сторінка {page} з {totalPages} ---");
            IEnumerable<Region> pageItems = allRegions
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            foreach (Region region in pageItems)
                Console.WriteLine($"  [{region.Id}] {region.Name} (міст: {region.Cities.Count})");
        }
    }

    private static void ShowRegionPopulations(List<Country> countries)
    {
        Console.WriteLine("\n3. Extension: TotalRegionPopulation()");
        List<Region> allRegions = countries.SelectMany(c => c.Regions).ToList();

        foreach (Region region in allRegions)
            Console.WriteLine($"  {region.Name,-26} {region.TotalRegionPopulation():N0}");

        Region biggest = allRegions.OrderByDescending(r => r.TotalRegionPopulation()).First();
        Console.WriteLine($"Найбільш населений регіон: {biggest.Name} ({biggest.TotalRegionPopulation():N0})");
    }
}