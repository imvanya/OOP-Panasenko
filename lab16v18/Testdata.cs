using lab16v18.Models;

namespace lab16v18.Data;

public static class TestData
{
    public static List<Country> Create()
    {
        return new List<Country>
        {
            new Country
            {
                Id = 1, Name = "Україна",
                Regions = new List<Region>
                {
                    new Region { Id = 1, Name = "Рівненська область", Cities = new List<City>
                    {
                        new City { Id = 1, Name = "Рівне",    Population = 245000 },
                        new City { Id = 2, Name = "Дубно",    Population = 36000 },
                        new City { Id = 3, Name = "Сарни",    Population = 26000 },
                    }},
                    new Region { Id = 2, Name = "Львівська область", Cities = new List<City>
                    {
                        new City { Id = 4, Name = "Львів",    Population = 717000 },
                        new City { Id = 5, Name = "Дрогобич", Population = 76000 },
                    }},
                    new Region { Id = 3, Name = "Київська область", Cities = new List<City>
                    {
                        new City { Id = 6, Name = "Біла Церква", Population = 205000 },
                        new City { Id = 7, Name = "Бровари",     Population = 100000 },
                    }},
                }
            },
            new Country
            {
                Id = 2, Name = "Польща",
                Regions = new List<Region>
                {
                    new Region { Id = 4, Name = "Малопольське воєводство", Cities = new List<City>
                    {
                        new City { Id = 8, Name = "Краків", Population = 800000 },
                        new City { Id = 9, Name = "Тарнів", Population = 105000 },
                    }},
                    new Region { Id = 5, Name = "Мазовецьке воєводство", Cities = new List<City>
                    {
                        new City { Id = 10, Name = "Варшава", Population = 1860000 },
                        new City { Id = 11, Name = "Радом",   Population = 200000 },
                    }},
                }
            },
            new Country
            {
                Id = 3, Name = "Німеччина",
                Regions = new List<Region>
                {
                    new Region { Id = 6, Name = "Баварія", Cities = new List<City>
                    {
                        new City { Id = 12, Name = "Мюнхен",   Population = 1510000 },
                        new City { Id = 13, Name = "Нюрнберг", Population = 520000 },
                        new City { Id = 14, Name = "Аугсбург", Population = 300000 },
                    }},
                    new Region { Id = 7, Name = "Саксонія", Cities = new List<City>
                    {
                        new City { Id = 15, Name = "Дрезден", Population = 560000 },
                        new City { Id = 16, Name = "Лейпциг", Population = 600000 },
                        new City { Id = 17, Name = "Хемніц",  Population = 245000 },
                        new City { Id = 18, Name = "Цвікау",  Population = 87000 },
                    }},
                }
            }
        };
    }
}