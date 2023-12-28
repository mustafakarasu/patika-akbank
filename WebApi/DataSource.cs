using WebApi.Entities;

namespace WebApi;

/// <summary>
/// Database kullanımı örnek olması açısından veri kaynağı olarak static bir class.
/// </summary>
public class DataSource
{
    public List<Product> Products { get; set; } = new List<Product>(InitialProducts());

    /// <summary>
    /// Proje başladığında veri kaynağına başlangıç verileri yüklemek için. 
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Veri kaynağında client'tan alınan Id değeri varsa bu değeri kendimizin ayarlaması için.
    /// </summary>
    /// <param name="product">Oluşturulacak product entity değeridir.</param>
    /// <returns></returns>
    public Product Create(Product product)
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