using WebApi.Entities;

namespace WebApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataSource _dataSource;

    public ProductRepository(DataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public Product Create(Product product)
    {
        if ( _dataSource.Products.Any(x => x.Id == product.Id) )
        {
            int maxId = _dataSource.Products.Max(x => x.Id);
            product.Id = maxId + 1;
        }

        _dataSource.Products.Add(product);
        return product;
    }

    public void UpdateById(int id, Product product)
    {
        var fromDbProduct = GetById(id);

        if ( fromDbProduct != null)
        {
            fromDbProduct.Title = product.Title;
            fromDbProduct.UnitPrice = product.UnitPrice;
        }
    }

    public void DeleteById(int id)
    {
        var deletedProduct = GetById(id);

        if (deletedProduct != null)
        {
            _dataSource.Products.Remove(deletedProduct);
        }
    }

    public Product GetById(int id)
    {
        return _dataSource.Products.FirstOrDefault(x => x.Id == id);
    }

    public List<Product> GetAll(string title, string order)
    {
        var products = _dataSource.Products.AsQueryable();

        if ( !string.IsNullOrWhiteSpace(title) )
        {
            products = products.Where(x => x.Title.ToLower().Contains(title.ToLower()));
        }

        if ( !string.IsNullOrWhiteSpace(order) )
        {
            if ( order.ToLower() == "desc" )
            {
                products = products.OrderByDescending(x => x.Id);
            }
            else
            {
                products =  products.OrderBy(x => x.Id);
            }
        }

        return products.ToList();
    }

    public bool IsExistsProductById(int id)
    {
        return _dataSource.Products.Exists(x => x.Id == id);
    }
}