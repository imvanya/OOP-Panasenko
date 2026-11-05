using lab16v18.Models;

namespace lab16v18.Extensions;

public static class RegionExtensions
{
    public static int TotalRegionPopulation(this Region region)
    {
        return region.Cities.Sum(c => c.Population);
    }
}