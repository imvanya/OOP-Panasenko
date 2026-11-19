namespace IndependentWork11;

public static class SalesPipelines
{
    public static List<CategoryTop> TopProductsByCategory(
        IEnumerable<Sale> sales, IEnumerable<Product> products, int topCount = 3)
    {
        return sales
            .GroupBy(s => s.ProductId)
            .Select(g => new { ProductId = g.Key, Quantity = g.Sum(s => s.Quantity) })
            .Join(products,
                total => total.ProductId,
                product => product.Id,
                (total, product) => new ProductTotal(product.Name, product.Category, total.Quantity))
            .GroupBy(item => item.Category)
            .OrderBy(group => group.Key)
            .Select(group => new CategoryTop(
                group.Key,
                group.OrderByDescending(item => item.Quantity)
                     .ThenBy(item => item.Name)
                     .Take(topCount)
                     .ToList()))
            .ToList();
    }

    public static List<CityRevenue> RevenueByCity(
        IEnumerable<Sale> sales, IEnumerable<Product> products, IEnumerable<Customer> customers)
    {
        return sales
            .Join(products,
                sale => sale.ProductId,
                product => product.Id,
                (sale, product) => new { sale.CustomerId, Revenue = sale.Quantity * product.UnitPrice })
            .Join(customers,
                item => item.CustomerId,
                customer => customer.Id,
                (item, customer) => new { customer.City, item.Revenue })
            .GroupBy(item => item.City)
            .Select(group => new CityRevenue(group.Key, group.Sum(item => item.Revenue)))
            .OrderByDescending(item => item.Revenue)
            .ToList();
    }

    public static List<MonthlySales> SalesByMonth(
        IEnumerable<Sale> sales, IEnumerable<Product> products)
    {
        return sales
            .Join(products,
                sale => sale.ProductId,
                product => product.Id,
                (sale, product) => new
                {
                    sale.SaleDate.Year,
                    sale.SaleDate.Month,
                    sale.Quantity,
                    Revenue = sale.Quantity * product.UnitPrice
                })
            .GroupBy(item => new { item.Year, item.Month })
            .Select(group => new MonthlySales(
                group.Key.Year,
                group.Key.Month,
                group.Sum(item => item.Quantity),
                group.Sum(item => item.Revenue)))
            .OrderBy(item => item.Year)
            .ThenBy(item => item.Month)
            .ToList();
    }

    public static List<CustomerSummary> TopCustomersWithFavoriteCategory(
        IEnumerable<Sale> sales,
        IEnumerable<Product> products,
        IEnumerable<Customer> customers,
        int topCount = 5)
    {
        return sales
            .Join(products,
                sale => sale.ProductId,
                product => product.Id,
                (sale, product) => new
                {
                    sale.CustomerId,
                    product.Category,
                    Revenue = sale.Quantity * product.UnitPrice
                })
            .GroupBy(item => item.CustomerId)
            .Select(group => new
            {
                CustomerId = group.Key,
                TotalSpent = group.Sum(item => item.Revenue),
                FavoriteCategory = group
                    .GroupBy(item => item.Category)
                    .OrderByDescending(category => category.Sum(item => item.Revenue))
                    .ThenBy(category => category.Key)
                    .First()
                    .Key
            })
            .OrderByDescending(item => item.TotalSpent)
            .ThenBy(item => item.CustomerId)
            .Take(topCount)
            .Join(customers,
                item => item.CustomerId,
                customer => customer.Id,
                (item, customer) => new CustomerSummary(
                    customer.Name, customer.City, item.TotalSpent, item.FavoriteCategory))
            .ToList();
    }
}