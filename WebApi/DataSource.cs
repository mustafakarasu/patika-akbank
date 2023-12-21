using WebApi.Entities;

namespace WebApi;

public static class DataSource
{
    public static List<Product> Products { get; set; } = new List<Product>(InitialProducts());
    private static List<Product> InitialProducts()
    {
        return new List<Product>()
        {
            new Product() {Id = 1, Title = "iPhone 15", UnitPrice = 53.999M},
            new Product() {Id = 2, Title = "iPhone 15 Pro", UnitPrice = 69.999M},
            new Product() {Id = 3, Title = "Galaxy S23", UnitPrice = 37.999M},
            new Product() {Id = 4, Title = "Galaxy S23+", UnitPrice = 47.999M},
            new Product() {Id = 5, Title = "iPhone 14", UnitPrice = 41.999M},
            new Product() {Id = 6, Title = "iPhone 14 Pro", UnitPrice = 44.999M},
            new Product() {Id = 7, Title = "Galaxy S22", UnitPrice = 27.499M},
            new Product() {Id = 8, Title = "Galaxy S22+", UnitPrice = 29.499M},
        };
    }

    public static Product Create(Product product)
    {
        if (Products.Any(x => x.Id == product.Id) )
        {
            int maxId = Products.Max(x => x.Id);
            product.Id = maxId + 1;
        }

        Products.Add(product);
        return product;
    }
}